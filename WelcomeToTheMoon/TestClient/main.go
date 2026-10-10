package main

import (
	"fmt"
	"net"
	"log"
	"time"
)

func main() {
	// 连接到服务器
	conn, err := net.Dial("tcp", "localhost:8080")
	if err != nil {
		log.Fatal(err)
	}
	defer conn.Close()

	fmt.Println("Connected to server at localhost:8080")

	// 持续发送消息给服务器
	for {
		message := "Hello Server!"
		_, err := conn.Write([]byte(message))
		if err != nil {
			log.Fatal(err)
		}

		// 接收服务器的响应
		buffer := make([]byte, 1024)
		n, err := conn.Read(buffer)
		if err != nil {
			log.Fatal(err)
		}
		fmt.Println("Received from server:", string(buffer[:n]))

		// 等待一段时间再发送下一条消息
		time.Sleep(2 * time.Second)
	}
}