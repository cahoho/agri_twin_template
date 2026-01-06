const WebSocket = require('ws');
const http = require('http');
const fs = require('fs');

const server = http.createServer((req, res) => {
    if (req.url === '/' || req.url === '/index.html') {
        fs.readFile('index.html', (err, data) => {
            if (err) {
                res.writeHead(404, { 'Content-Type': 'text/plain' });
                res.end('404 Not Found');
            } else {
                res.writeHead(200, { 'Content-Type': 'text/html' });
                res.end(data);
            }
        });
    } else {
        res.writeHead(404, { 'Content-Type': 'text/plain' });
        res.end('404 Not Found');
    }
});

const wss = new WebSocket.Server({ server });

wss.on('connection', (ws) => {
    console.log('A client connected');
    
    ws.send(JSON.stringify({ message: 'Websocket站点连接成功！' }));

    ws.on('message', (message) => {
        console.log('Received: %s', message);
        console.log('Type of received message:', typeof message); // 检查类型
        console.log('Is Buffer:', Buffer.isBuffer(message));     // 检查是否是Buffer

        let messageToSend = message; // 默认发送原始消息

        // 如果收到的消息是 Buffer (即二进制数据)，尝试将其转换为 UTF-8 字符串
        if (Buffer.isBuffer(message)) {
            try {
                messageToSend = message.toString('utf8');
                console.log('Converted binary message to string:', messageToSend);
            } catch (e) {
                console.error('Failed to convert binary message to string:', e);
                // 如果转换失败，可以发送一个错误消息或者原始 Buffer
                messageToSend = JSON.stringify({ error: "Server received unreadable binary data", originalType: "Buffer" });
            }
        }

        // 广播消息给所有连接的客户端
        wss.clients.forEach(client => {
            if (client.readyState === WebSocket.OPEN) {
                // 确保发送的是字符串，这样客户端会收到文本帧
                client.send(messageToSend);
            }
        });
    });

    ws.on('close', () => {
        console.log('A client disconnected');
    });

    ws.on('error', (error) => {
        console.error('WebSocket error:', error);
    });
});

const PORT = 8080;
server.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`); 
});

