/*
[jslib]Raw WebSocket message received:
{"type":"update_all_charts",
"id":"all",
"data":{"chart_1762088539274_1"
            :{"properties":{
                    "id":"chart_1762088539274_1",
                    "title":"feaf",
                    "type":"line",
                    "location":"pest",
                    "detail":"general",
                    "maxValue":"15"},
                   "data":[
                        {"id":"data_1762130571027_1",
                        "title":"esfe",
                        "value":"125"}]
 }}}

*/

mergeInto(LibraryManager.library, {
    ConnectWebSocket: function (urlPtr) {
        const url = UTF8ToString(urlPtr);

        if (window.ws && window.ws.readyState !== WebSocket.CLOSED) {
            console.warn("[jslib]Existing WebSocket connection detected. Closing it before opening a new one.");
            window.ws.close();
        }

        window.ws = new WebSocket(url);

        window.ws.onopen = function (event) {
            console.log("[jslib]WebSocket connected successfully!");

            if (typeof unityInstance !== "undefined") {
                unityInstance.SendMessage('DataHandler', 'OnWebSocketOpen');
            }
        };

        window.ws.onerror = function (event) {
            console.error("[jslib]WebSocket error:", event);

            let errorMessage = "Unknown WebSocket Error";
            if (event && event.message) {
                errorMessage = event.message;
            } else if (event && event.type === "error") {
                errorMessage = "Network error or server rejection.";
            }

            if (typeof unityInstance !== "undefined") {
                unityInstance.SendMessage('DataHandler', 'OnWebSocketError', errorMessage);
            }
        };

        window.ws.onclose = function (event) {
            console.log("[jslib]WebSocket closed:", event.code, event.reason);

            let closeReason = event.reason || "Connection closed.";
            if (typeof unityInstance !== "undefined") {
                unityInstance.SendMessage('DataHandler', 'OnWebSocketClose', closeReason);
            }
        };

        window.ws.onmessage = function (event) {
            console.log("[jslib]Raw WebSocket message received:", event.data);

            if (typeof unityInstance !== "undefined") {
                unityInstance.SendMessage('DataHandler', 'OnWebSocketMessage', event.data);
            } else {
                console.warn("[jslib]unityInstance is undefined, cannot send message to Unity.");
            }

        };
    },

    SendWebSocketMessage: function (msgPtr) {
        const msg = UTF8ToString(msgPtr);
        if (window.ws && window.ws.readyState === WebSocket.OPEN) { // WebSocket.OPEN is 1
            window.ws.send(msg);
            console.log("[jslib]Sent message:", msg);
        } else {
            console.warn("[jslib]WebSocket not open. Cannot send message:", window.ws ? `State: ${window.ws.readyState}` : "Not initialized");

            if (typeof unityInstance !== "undefined") {
                unityInstance.SendMessage('DataHandler', 'OnWebSocketError', "Failed to send message: WebSocket not open.");
            }
        }
    },

    CloseWebSocket: function () {
        if (window.ws && window.ws.readyState !== WebSocket.CLOSED) {
            window.ws.close();
            console.log("[jslib]WebSocket close requested.");
        } else {
            console.log("[jslib]WebSocket is already closed or not initialized.");
        }
    }
});
