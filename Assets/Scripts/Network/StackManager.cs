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
	[SerializeField] private TMP_Text text;
	public static StackManager Instance;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(this);
	}
	
	[ClientRpc]
	public void RpcPlayCard(int card)																	
	{
		LastPlayedCard = card;
		text.text = LastPlayedCard.ToString();
		OnPlayCard.Invoke();
		
	}
	
	public UnityEvent OnPlayCard;
}
