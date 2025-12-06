using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Mirror;
using TMPro;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class CardGameManager : NetworkManager
{
	[SerializeField] private List<GameObject> players;
	[SerializeField] private Transform hostSpawn;
	[SerializeField] private Transform clientSpawn;
    // Overrides the base singleton so we don't
    // have to cast to this type everywhere.
    public static new CardGameManager singleton => (CardGameManager)NetworkManager.singleton;
    
    
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
	    // add player at correct spawn position
	    Transform start = numPlayers == 0 ? hostSpawn : clientSpawn;
	    GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
	    NetworkServer.AddPlayerForConnection(conn, player);
	    players.Add(player);
			   
    }

    public override void OnClientConnect()
    {
	    base.OnClientConnect();
	    OnConnected.Invoke();
    }
    public UnityEvent OnConnected;

    public override void OnClientDisconnect()
    {
	    base.OnClientDisconnect();
	    OnDisconnected.Invoke();
    }
    public UnityEvent OnDisconnected;
}
