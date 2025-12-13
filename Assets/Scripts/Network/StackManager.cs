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
	private GameObject LastPlayedCard;
	[SyncVar]private int activePlayer;

	public int ActivePlayer
	{
		get { return activePlayer; }
	}

	[SerializeField]
	private List<Card> cards
	{
		get { return CardGameManager.singleton.cards; }
	}
	[SerializeField] private GameObject cardPrefab;
	[SerializeField] private TMP_Text activePlayerText;
	[SerializeField] private TMP_Text text;
	public static StackManager Instance;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(this);
	}

	
	[ClientRpc]
	public void RpcPlayCard(int cardID, int playerID)																	
	{
		if (playerID != activePlayer || (CardGameManager.singleton.Players.Count <= 1 && isServer))
		{
			activePlayer = playerID;
			Destroy(LastPlayedCard);
			foreach (Card card in cards)
			{
				if (card.ID == cardID)
				{
					CardGameManager.singleton.DamagePlayer(card.attack, playerID);
					LastPlayedCard = Instantiate(cardPrefab);
					LastPlayedCard.GetComponent<AttackCardDisplay>().card = card;
				}
			}
			StartTurn.Invoke();
		}
	}

	public UnityEvent StartTurn;

}
