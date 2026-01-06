using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.Rendering;
using JetBrains.Annotations;



/*
创建新图表：
{"action_type":"create_chart","id":"chart_1762175442771_4","title":"e","type":"pie","location":"main","detail":"general","maxValue":""}
1.action_type:
2. id
3. title
4. type
5. location
6. detail
7. maxValue

删除图表：
"action_type":"delete_chart","id":"chart_1762175442771_4"
1. action_type
2. id

新建数据：
{"action_type":"create_data","id":"data_1762175532309_4","parentId":"chart_1762130910392_2","title":"e","value":"3"}
1. action_type
2. id
3. parentId
4. title
5. value


删除数据：

{"action_type":"delete_data","id":"data_1762175532309_4","parentId":"chart_1762130910392_2"}
1. action_type
2. id
3. parentId


*/


[Serializable]
public class MessageBase
{
    public string action_type;
}
//数据集
[Serializable]
public class CreateChartDataMessage : MessageBase
{
    public string id;
    public string title;
    public string type;
    public string location;
    public string detail;
    public float maxValue;


}


[Serializable]
public class DeleteChartDataMessage:MessageBase
{
    public string id;
    
}
[Serializable]
public class CreateDataMessage:MessageBase
{
    public string id;
    public string parentId;
    public string parentChartType;
    public string title;
    public float value;
}
[Serializable]
public class DeleteDataMessage:MessageBase
{
    public string id;
    public string parentId;
}


public class DataHandler : Singleton<DataHandler>
{
    [DllImport("__Internal")]
    private static extern void ConnectWebSocket(string url);

    [DllImport("__Internal")]
    private static extern void SendWebSocketMessage(string message);

    [DllImport("__Internal")]
    private static extern void CloseWebSocket();


    public string serverUrl = "ws://localhost:8080"; 

    public event Action OnConnected;
    public event Action<string> OnMessageReceived;
    public event Action<string> OnError;
    public event Action<string> OnClosed;


    override protected void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Connect();
#endif
    }

    public void Connect()
    {
        Debug.Log("C#: Attempting to connect to WebSocket: " + serverUrl);
        ConnectWebSocket(serverUrl);
    }
    //public void SendMessageToServer(string message)
    //{
    //    // 示例：发送一个JSON对象
    //    ServerMessage msgToSend = new ServerMessage
    //    {
    //        type = "chat",
    //        message = message,
    //        // ... 其他字段
    //    };
    //    string jsonString = JsonUtility.ToJson(msgToSend); // 序列化为JSON字符串

    //    Debug.Log("C#: Sending JSON: " + jsonString);
    //    SendWebSocketMessage(jsonString); // 发送JSON字符串
    //}

    // JSLib通过Unity.SendMessage调用的方法
    public void OnWebSocketOpen()
    {
        Debug.Log("C#: WebSocket Connected!");
        OnConnected?.Invoke();
        // 连接成功后可以发送一条消息
        //SendMessageToServer("Hello from Unity!");
    }

    // JSLib通过Unity.SendMessage调用的方法，接收到的是原始字符串
    public void OnWebSocketMessage(string rawMessage)
    {
        Debug.Log("C#: Received raw message: " + rawMessage);
        OnMessageReceived?.Invoke(rawMessage);

        // 在C#中解析JSON
        try
        {
            MessageBase mb = JsonUtility.FromJson<MessageBase>(rawMessage);
            CreateChartDataMessage ccdm = JsonUtility.FromJson<CreateChartDataMessage>(rawMessage);
            DeleteChartDataMessage dcdm = JsonUtility.FromJson<DeleteChartDataMessage>(rawMessage);
            CreateDataMessage cdm = JsonUtility.FromJson<CreateDataMessage>(rawMessage);
            DeleteDataMessage ddm = JsonUtility.FromJson<DeleteDataMessage>(rawMessage);

            if (mb.action_type != null)
            {
                Debug.Log($"C#: Server Notification: {mb.action_type}");
                switch (mb.action_type)
                {
                    case "create_chart":
                        //Debug.Log($"C#: Create chart message received. Data: {mb.action_type} and detail:\n{ccdm}");
                        Creater.Instance.CreateChart(ccdm);
                        break;
                    case "delete_chart":
                        //Debug.Log($"C#: Delete chart message received. Title: {mb.action_type} and detail:\n{dcdm}");
                        Creater.Instance.DeleteChart(dcdm);
                        break;
                    case "create_data":
                        //Debug.Log($"C#: Create data message received. Location: {mb.action_type} and detail:\n {cdm}");
                        Creater.Instance.CreateData(cdm);
                        break;
                    case "delete_data":
                        //Debug.Log($"C#: Delete data message received. Data: {mb.action_type} and detail: \n{ddm}");
                        Creater.Instance.DeleteData(ddm);
                        break;
                    default:
                        Debug.LogWarning($"C#: Unhandled message type. ");
                        break;
                }
            }
            else
            {
                Debug.LogWarning($"C#: Received JSON with no 'message' or 'type' field: {rawMessage}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("C#: Failed to parse JSON message: " + e.Message + "\nRaw Data: " + rawMessage);
        }
    }

    public void OnWebSocketError(string error)
    {
        Debug.LogError("C#: WebSocket Error: " + error);
        OnError?.Invoke(error);
    }

    public void OnWebSocketClose(string reason)
    {
        Debug.Log("C#: WebSocket Closed: " + reason);
        OnClosed?.Invoke(reason);
    }

     override protected void OnApplicationQuit()
    {
        // 应用程序退出时关闭WebSocket连接
#if UNITY_WEBGL && !UNITY_EDITOR
        CloseWebSocket();
#endif
    }

}
