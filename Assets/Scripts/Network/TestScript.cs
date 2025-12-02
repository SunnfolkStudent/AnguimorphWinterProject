using UnityEngine;
using Mirror;
using TMPro;

public class TestScript : NetworkBehaviour
{
    [SerializeField]private TMP_Text text;
    
    [Command] 
    public void CmdPlayCard(string card)
    {
        RpcPlayCard(card);
        PlayCard(card);
    }

    [ClientRpc]
    public void RpcPlayCard(string card)
    {
       text.text = "Played Card:"+card; 
    }
    public void PlayCard(string card)
    {
        text.text = "Played Card:"+card; 
    }
}
