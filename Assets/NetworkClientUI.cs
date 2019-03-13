using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkClientUI : MonoBehaviour
{
    NetworkClient client;
    void Start()
    {
        client = new NetworkClient();

    }

    public void Connect()
    {
        client.Connect("169.254.201.10", 25000);
    }

    public void SendJoystickInfo()
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = "wewewewewew";
            client.Send(888, msg);
        }
    }
}