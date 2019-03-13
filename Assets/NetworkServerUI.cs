using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkServerUI : MonoBehaviour {

    public Text serverInfo;

	// Use this for initialization
	void Start () {
		NetworkServer.Listen(25000);
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);
	}

    private void ServerReceiveMessage(NetworkMessage netMsg)
    {
        Debug.Log("Receive");
    }

    // Update is called once per frame
    void Update () {
		serverInfo.text = 
            "Status: " + NetworkServer.active + 
            "\nConnected: " + NetworkServer.connections.Count + 
            "\nPort: " +NetworkServer.listenPort
            ;
	}
}
