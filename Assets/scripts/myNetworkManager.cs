using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class myNetworkManager : NetworkManager
{

    private newNetworkHUD hud;


    public override void OnValidate()
    {
        // always >= 0
        maxConnections = Mathf.Max(maxConnections, 0);

        if (playerPrefab != null && playerPrefab.GetComponent<NetworkIdentity>() == null)
        {
            Debug.LogError("NetworkManager - Player Prefab must have a NetworkIdentity.");
            playerPrefab = null;
        }

        // This avoids the mysterious "Replacing existing prefab with assetId ... Old prefab 'Player', New prefab 'Player'" warning.
        if (playerPrefab != null && spawnPrefabs.Contains(playerPrefab))
        {
            Debug.LogWarning("NetworkManager - Player Prefab should not be added to Registered Spawnable Prefabs list...removed it.");
            spawnPrefabs.Remove(playerPrefab);
        }
    }

    public override void Awake()
    {
        hud = GetComponent<newNetworkHUD>();
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        PlayerManager.getInstance().DisableJoining();
    }

    private void Update()
    {
        transport = hud.getCurrentTranport();
        Transport.activeTransport = transport;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        PlayerManager.getInstance().EnableJoining();


        //NetworkServer.RegisterHandler<playerStruct>(OnCreateCharacter);
    }


    public GameObject addPlayer()
    {
        GameObject pl = Instantiate(playerPrefab);
        pl.transform.position = GetStartPosition().position;
        NetworkServer.AddPlayerForConnection(NetworkServer.localConnection, pl);
        return pl;
    }
}
