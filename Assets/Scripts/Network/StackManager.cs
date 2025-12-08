using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using TMPro;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class StackManager : NetworkBehaviour
{
	[SyncVar]private int LastPlayedCard;
	[SyncVar] private int ActivePlayer;
	[SerializeField] private TMP_Text activePlayerText;
	[SerializeField] private TMP_Text text;
	public static StackManager Instance;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(this);
	}

	private void FixedUpdate()
	{
		if (ActivePlayer != null)
		{
			activePlayerText.text = ActivePlayer.ToString();
		}
	}
	
	[ClientRpc]
	public void RpcPlayCard(int card, int playerID)																	
	{
		if (playerID != ActivePlayer)
		{
			LastPlayedCard = card;
			ActivePlayer = playerID;
			text.text = "Player "+ActivePlayer+" Played:"+LastPlayedCard;
			CardGameManager.singleton.DamagePlayer(card, playerID);
			OnPlayCard.Invoke();
		}
	}
	
	public UnityEvent OnPlayCard;
}
