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
using Random = UnityEngine.Random;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class CardGameManager : NetworkManager
{
	[SerializeField] private List<GameObject> players;
	public List<GameObject> Players { get { return players; } }
	public List<Card> cards
	{
		get
		{
			return Resources.LoadAll<Card>("CardScrubs").ToList();
		}
	}

	public List<Sprite> PlayerSprites
	{
		get { return Resources.LoadAll<Sprite>("EnemySprites").ToList(); }
	}

	public Transform hostSpawn
	{
		get { return GameObject.Find("HostSpawn").GetComponent<RectTransform>(); }
	}

	public Transform clientSpawn
	{
		get { return GameObject.Find("ClientSpawn").GetComponent<RectTransform>(); }
	}

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
	    GameObject player = Instantiate(playerPrefab);
	    NetworkServer.AddPlayerForConnection(conn, player);
	    player.GetComponent<TestScriptNetwork>().PlayerID = conn.connectionId;
	    player.GetComponent<TestScriptNetwork>().PlayerSPriteID = Random.Range(0, PlayerSprites.Count);
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
	    {
		    Debug.Log(player.GetComponentInChildren<TestScriptNetwork>().PlayerID+": played");
		    if (player.gameObject.GetComponentInChildren<TestScriptNetwork>().PlayerID != playerID)
		    {
			    Debug.Log("Damaging player:"+player.gameObject.GetComponentInChildren<TestScriptNetwork>().PlayerID);
			    player.GetComponentInChildren<TestScriptNetwork>().HealthPoints -= damage;
			    if (player.GetComponentInChildren<TestScriptNetwork>().HealthPoints <= 0)
			    {
				    foreach (GameObject otherPlayer in players)
				    {
					    if (otherPlayer != player)
					    {
						    otherPlayer.GetComponentInChildren<TestScriptNetwork>().Win();

					    }

					    player.GetComponentInChildren<TestScriptNetwork>().Lose();
				    }

				    //StopClient();
				    //StopHost();
			    }
		    }
	    }
    }
    public UnityEvent OnDisconnected;
}
