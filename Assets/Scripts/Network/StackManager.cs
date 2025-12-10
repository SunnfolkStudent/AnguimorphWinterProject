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
	[SyncVar]private int ActivePlayer;

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

	private void FixedUpdate()
	{
		if (ActivePlayer != null)
		{
			activePlayerText.text = ActivePlayer.ToString();
		}
	}
	
	[ClientRpc]
	public void RpcPlayCard(int cardID, int playerID)																	
	{
		if (playerID != ActivePlayer)
		{
			ActivePlayer = playerID;
			Destroy(LastPlayedCard);
			foreach (Card card in cards)
			{
				if (card.ID == cardID)
				{
					CardGameManager.singleton.DamagePlayer(card.attack, playerID);
					LastPlayedCard = Instantiate(cardPrefab, GetComponentInChildren<Canvas>().gameObject.transform);
					LastPlayedCard.GetComponent<AttackCardDisplay>().card = card;
					text.text = "Player "+ActivePlayer+" Played:"+card.name;
					OnPlayCard.Invoke();
				}
			}
		}
	}
	
	public UnityEvent OnPlayCard;
}
