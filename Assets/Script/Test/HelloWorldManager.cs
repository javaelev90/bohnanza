using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class HelloWorldManager : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Edit/Reset Playerprefs")]
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif

    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        Screen.SetResolution(1024, 768, false);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
            if (NetworkManager.Singleton.IsHost && GUILayout.Button("Start")) gameManager.StartGame();

            //SubmitNewPosition();
        }
        GUILayout.EndArea();
    }

    void StartButtons()
    {
        if (GUILayout.Button("Host")) { NetworkManager.Singleton.StartHost(); }
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();

    }

    void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    void SubmitNewPosition()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        {
            if(NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
            {
                foreach(ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                {
                    NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<TestPlayer>().Move();
                }
            }
            else
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<TestPlayer>();
                player.Move();
            }
        }
    }
}
