using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Mirror;
using TMPro;
using UnityEngine.UIElements;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class CardGameManager : NetworkManager
{
	[SerializeField] private List<GameObject> players;
	public List<Card> cards
	{
		get
		{
			return Resources.LoadAll<Card>("CardScrubs").ToList();
		}
	}
	[SerializeField] private Transform hostSpawn;
	[SerializeField] private Transform clientSpawn;
    // Overrides the base singleton so we don't
    // have to cast to this type everywhere.
    public static new CardGameManager singleton => (CardGameManager)NetworkManager.singleton;

    public override void Awake()
    {
	    base.Awake();
	    foreach (Card card in cards)
	    {
		    Debug.Log(card.name);
	    }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
	    // add player at correct spawn position
	    Transform start = numPlayers == 0 ? hostSpawn : clientSpawn;
	    GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
	    NetworkServer.AddPlayerForConnection(conn, player);
	    player.GetComponentInChildren<TestScriptNetwork>().PlayerID = conn.connectionId;
	    player.GetComponentInChildren<TestScriptNetwork>().Player.GetComponent<RectTransform>().position = start.position;
	    player.GetComponentInChildren<TestScriptNetwork>().HealthPoints = 100;
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

    public void DamagePlayer(int damage, int playerID)
    {
	    foreach (GameObject player in players)
		    if (player.gameObject.GetComponentInChildren<TestScriptNetwork>().PlayerID != playerID)
		    {
			    player.GetComponentInChildren<TestScriptNetwork>().HealthPoints -= damage;
		    }
    }
    public UnityEvent OnDisconnected;
}
