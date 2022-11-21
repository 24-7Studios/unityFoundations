using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.FizzySteam;


public class newNetworkHUD : MonoBehaviour
{
    NetworkManager manager;

    public bool showGUI = true;

    public int offsetX;
    public int offsetY;

    [SerializeField]
    List<Transport> transports;

    [SerializeField]
    private int index = 0;

    private Transport currentTransport;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    public Transport getCurrentTranport()
    {
        return currentTransport;
    }

    void OnGUI()
    {
#pragma warning disable 618
        if (!showGUI) return;
#pragma warning restore 618

        GUILayout.BeginArea(new Rect(10 + offsetX, 40 + offsetY, 215, 9999));
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
        }

        // client ready
        if (NetworkClient.isConnected && !NetworkClient.ready)
        {
            if (GUILayout.Button("Client Ready"))
            {
                NetworkClient.Ready();
                if (NetworkClient.localPlayer == null)
                {
                    NetworkClient.AddPlayer();
                }
            }
        }

        StopButtons();

        GUILayout.EndArea();
    }

    void StartButtons()
    {
        if (!NetworkClient.active)
        {
            // Server + Client
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                if (GUILayout.Button("Host Game"))
                {
                    manager.StartHost();
                }
            }

            // Client
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Join Game"))
            {
                manager.StartClient();
            }
            GUILayout.EndHorizontal();

            // Server Only
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // cant be a server in webgl build
                GUILayout.Box("(  WebGL cannot be server  )");
            }
            else
            {
                if (GUILayout.Button("Host Server Only")) manager.StartServer();
            }

            // transport
            /*
            GUILayout.BeginHorizontal();
            GUILayout.Button("Transport Index:");
            int.TryParse(GUILayout.TextField(index.ToString()), out index);
            GUILayout.EndHorizontal();
            */

            GUILayout.BeginVertical();
            GUILayout.Label("--Transports--");
            foreach(Transport t in transports)
            {
                if(GUILayout.Button(t.ToString()))
                {
                    currentTransport = t;
                }
                if (t != currentTransport)
                {
                    t.enabled = false;
                }
                else
                {
                    t.enabled = true;
                }
            }
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Current :" + currentTransport);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Desitnation:");
            manager.networkAddress = GUILayout.TextField(manager.networkAddress);
            GUILayout.EndHorizontal();

        }
        else
        {
            // Connecting
            GUILayout.Label("Connecting to " + manager.networkAddress + "..");
            if (GUILayout.Button("Cancel Connection Attempt"))
            {
                manager.StopClient();
            }
        }
    }

    void StatusLabels()
    {
        // host mode
        // display separately because this always confused people:
        //   Server: ...
        //   Client: ...
        if (NetworkServer.active && NetworkClient.active)
        {
            GUILayout.Label($"<b>Host</b>: running via {Transport.activeTransport}");
        }
        // server only
        else if (NetworkServer.active)
        {
            GUILayout.Label($"<b>Server</b>: running via {Transport.activeTransport}");
        }
        // client only
        else if (NetworkClient.isConnected)
        {
            GUILayout.Label($"<b>Client</b>: connected to {manager.networkAddress} via {Transport.activeTransport}");
        }
    }

    void StopButtons()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            if (GUILayout.Button("Stop Host"))
            {
                manager.StopHost();
            }
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            if (GUILayout.Button("Stop Client"))
            {
                manager.StopClient();
            }
        }
        // stop server if server-only
        else if (NetworkServer.active)
        {
            if (GUILayout.Button("Stop Server"))
            {
                manager.StopServer();
            }
        }
    }
}
