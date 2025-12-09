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
	[SyncVar]private int ActivePlayer;

	[SerializeField]
	private List<Card> cards
	{
		get { return CardGameManager.singleton.cards; }
	}
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
			LastPlayedCard = cardID;
			var  activeCard = CardGameManager.singleton.cards.Find(x => x.ID == cardID).name;
			ActivePlayer = playerID;
			text.text = "Player "+ActivePlayer+" Played:"+activeCard.ToString();
			foreach (Card card in cards)
			{
				if (card.ID == cardID)
				{
					CardGameManager.singleton.DamagePlayer(card.attack, playerID);
					Debug.Log(card.Name + " damaged");
					OnPlayCard.Invoke();
				}
			}
		}
	}
	
	public UnityEvent OnPlayCard;
}
