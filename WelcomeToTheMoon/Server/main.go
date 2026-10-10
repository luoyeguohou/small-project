package main

import (
	"fmt"
	"log"
	"net"
	"sync"
	"encoding/json"
	"bufio"
)

// struct
const (
    RequestUid  = "RequestUid"
    UpdateStatus = "UpdateStatus"
	UpdatePublicStatus = "UpdatePublicStatus"
	Heart = "Heart"
)

type PlyData struct {
    Data        string  `json:"data"`
    Conn        net.Conn `json:"-"`
    Uid         int `json:"uid"`
    Name    	string  `json:"name"`
}

type Msg struct{
	Cmd string `json:"cmd"`
	Data string `json:"data"`
}

type RequestUidMsg struct {
	Uid int `json:"uid"` 
	Name string `json:"name"`
	Players []PlyData `json:"players"`
}

type RespRequestUidMsg struct {
	Uid int `json:"uid"` 
	Name string `json:"name"`
	Players []PlyData `json:"players"`
	PublicData string `json:"publicData"`
}

type LoseConnPlyData struct{
 	Uid         int `json:"uid"`
    Name    	string  `json:"name"`
    Data        string  `json:"data"`
}

type UpdateStatusMsg struct{
	Uid int `json:"uid"`
	Data string `json:"data"`
	Name string `json:"name"`
}

// global data
var clientConnections = make([]net.Conn, 0)
var mu sync.Mutex 
var uid = 1
var plyDatas = make(map[int]*PlyData)
var publicStatus = ""
var loseConnPlyDatas = make(map[string]*LoseConnPlyData)


func handleClient(conn net.Conn) {
	defer conn.Close()
	mu.Lock()
	clientConnections = append(clientConnections, conn) 
	mu.Unlock()

	fmt.Printf("New client connected: %s\n", conn.RemoteAddr())

	// 监听客户端发送的消息
	reader := bufio.NewReader(conn)
	for {
		bufferStr, err := reader.ReadString('\n')
		if err != nil {
			log.Println("Error reading from client:", err)
            break
		}
		fmt.Printf("Received message from %s: %s\n", conn.RemoteAddr(), bufferStr)
		
		var msg Msg
		if unmarshal(bufferStr,&msg) != nil {
			break
		}
		switch msg.Cmd{
		case RequestUid:
			srvRequestUid(msg,conn)
		case UpdateStatus:
			UpdateStatusFunc(msg,conn)
		case UpdatePublicStatus:
			UpdatePublicStatusFunc(msg,conn)
		case Heart:
			sendToClient(conn,Heart,"")
		}
	}

	// 断联处理
    mu.Lock()
	for i, c := range clientConnections {
		if c == conn {
			clientConnections = append(clientConnections[:i], clientConnections[i+1:]...)
			break
		}
	}
	for key,val := range plyDatas {
		if val.Conn == conn {
			 loseConnPlyData := &LoseConnPlyData{
				Name:val.Name,
				Uid:val.Uid,
				Data:val.Data,
			}
			loseConnPlyDatas[val.Name] = loseConnPlyData
			delete(plyDatas,key)
		}		
	}
	fmt.Printf("有客户端断联，目前在线人数： %s", len(plyDatas))
	mu.Unlock()
    fmt.Printf("Client %s disconnected.\n", conn.RemoteAddr())
}

func main() {
    getNonLoopIP()
	// 启动 TCP 监听器
	listener, err := net.Listen("tcp", ":8080")
	if err != nil {
		log.Fatal("Error starting server:", err)
	}
	defer listener.Close()
	fmt.Println("Server started on port 8080")

	// 接受客户端连接并处理
	for {
		conn, err := listener.Accept()
		if err != nil {
			log.Println("Error accepting connection:", err)
			continue
		}
		go handleClient(conn)
	}
}

func broadcastMessage(sender net.Conn,cmd string, data string) {
	mu.Lock()
	defer mu.Unlock()

	for _, conn := range clientConnections {
		if conn != sender {
			sendToClient(conn,cmd,data)
		}
	}
}

func srvRequestUid(msg Msg,conn net.Conn){
	mu.Lock()

	var p *PlyData
	if data, ok := loseConnPlyDatas[msg.Data]; ok{
		p = &PlyData {
			Data: data.Data,
			Conn: conn,
			Uid:data.Uid,
			Name:data.Name,
		}
		delete(loseConnPlyDatas,msg.Data)
	}else{
		p = &PlyData {
			Data: "",
			Conn: conn,
			Uid:uid,
			Name:msg.Data,
		}
		uid = uid +1
	}

	plyDatas[p.Uid] = p
	mu.Unlock()

	// 创建一个切片存储map的值
	values := make([]PlyData, 0, len(plyDatas))
	// 遍历map，将值添加到切片中
	for _, v := range plyDatas {
		values = append(values, *v)
	}

	fmt.Printf("有客户端加入，目前在线人数： %s", len(plyDatas))
	sendToClient(conn,RequestUid,marshal(&RespRequestUidMsg{Uid:p.Uid,Name:p.Name,Players:values,PublicData:publicStatus}))
	var data = UpdateStatusMsg{
		Uid:p.Uid,
		Data:p.Data,
		Name:p.Name,
	}
	broadcastMessage(conn,UpdateStatus ,marshal(data))
}

func sendToClient(conn net.Conn,cmd string,data string){
	messageData := &Msg{
		Cmd:cmd,
		Data: data,
	}
	_, err := conn.Write([]byte(marshal(messageData)+ "\n"))
	if err != nil {
		log.Println("Error sending message to client:", err)
	}
}

func UpdateStatusFunc(msg Msg,conn net.Conn){
	var uidData UpdateStatusMsg
	unmarshal(msg.Data,&uidData)
	// update
	plyDatas[uidData.Uid].Data = uidData.Data
	// broadCast
	var data = UpdateStatusMsg{
		Uid:uidData.Uid,
		Data:uidData.Data,
		Name:uidData.Name,
	}
	broadcastMessage(conn,UpdateStatus ,marshal(data))
}

func UpdatePublicStatusFunc(msg Msg,conn net.Conn){
	publicStatus = msg.Data
	broadcastMessage(conn,UpdatePublicStatus ,publicStatus)
}

func marshal(data interface{}) string{
	dataBytes ,err := json.Marshal(data)
	if err != nil {
		log.Println("marshal failed:", err)
	}
	return string(dataBytes)
}

func unmarshal(str string,result interface{}) error{
	err := json.Unmarshal([]byte(str),result)
	if err != nil {
		fmt.Println("Error unmarshalling:", err)
	}
	return err
}

// 作用是显示自己的ipv4地址，给其他人联机用
func getNonLoopIP(){
     interfaces, err := net.Interfaces()
     if err != nil {
         log.Fatal(err)
     }
     for _, iface := range interfaces {
         if iface.Flags&net.FlagUp == 0 || iface.Name == "lo" {
             continue
         }
         addrs, err := iface.Addrs()
         if err != nil {
             log.Fatal(err)
         }
         for _, addr := range addrs {
             ipNet, ok := addr.(*net.IPNet)
             if ok && ipNet.IP.To4() != nil && ipNet.IP.String() != "127.0.0.1" {
                 fmt.Println("IPv4 Address:", ipNet.IP.String())
             }
         }
     }
 }