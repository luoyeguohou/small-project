
package main

import (
	"fmt"
	"net"
	"log"
)
 
func main() {
    getNonLoopIP()
    listener, err := net.Listen("tcp", ":8080")
	if err != nil {
		log.Fatal(err)
	}
	defer listener.Close()

	fmt.Println("TCP server is listening on port 8080...")

	// 监听客户端连接
	for {
		conn, err := listener.Accept()
		if err != nil {
			log.Fatal(err)
		}
		fmt.Println("Client connected:", conn.RemoteAddr())

		// 处理客户端连接
		go handleConnection(conn)
	}
}

func handleConnection(conn net.Conn) {
	defer conn.Close()

	// 读取客户端数据
	buffer := make([]byte, 1024)
	for {
		n, err := conn.Read(buffer)
		if err != nil {
			fmt.Println("Error reading data:", err)
			break
		}

		// 打印客户端发送的数据
		fmt.Println("Received from client:", string(buffer[:n]))

		// 向客户端发送响应
		conn.Write([]byte("Message received"))
	}
}

func getNonLoopIP(){
   // 获取本地所有的网络接口信息
	interfaces, err := net.Interfaces()
	if err != nil {
		log.Fatal(err)
	}

	// 遍历所有接口
	for _, iface := range interfaces {
		// 排除回环接口和其他不需要的接口
		if iface.Flags&net.FlagUp == 0 || iface.Name == "lo" {
			// 如果接口不可用或是回环接口，跳过
			continue
		}

		// 获取接口的所有地址
		addrs, err := iface.Addrs()
		if err != nil {
			log.Fatal(err)
		}

		// 遍历每个地址
		for _, addr := range addrs {
			// 只处理 IPv4 地址
			ipNet, ok := addr.(*net.IPNet)
			if ok && ipNet.IP.To4() != nil && ipNet.IP.String() != "127.0.0.1" {
				// 打印出 IPv4 地址
				fmt.Println("IPv4 Address:", ipNet.IP.String())
			}
		}
	}
}

type PlyDataMap struct {
    Data string
    Uid  int
}

// c->s
// 更新我的状态   p:状态 uid 
// s->c
// 更新某人的状态  p:状态 uid step