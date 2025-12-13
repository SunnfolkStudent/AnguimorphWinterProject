using System;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TestScriptNetwork : NetworkBehaviour
{
    [SyncVar] public int PlayerID;
    [SyncVar] public int PlayerSPriteID;
    public Sprite PlayerSprite
    {
        set {PlayerImage.sprite = value;}
    }

    public bool myturn
    {
        get
        {
            var activePlayer = stackManager.ActivePlayer;
            if (activePlayer == PlayerID) return false;
            else return true;
        }
    }
    [SerializeField] private Image PlayerImage; 
    public GameObject Player;
    [SerializeField] StackManager stackManager;

   
    [SerializeField] private HealthBar healthBar;

    [SyncVar]public int HealthPoints;


    private void Start()
    {
        stackManager = StackManager.Instance;
        PlayerSprite = CardGameManager.singleton.PlayerSprites[PlayerSPriteID];
        if (isLocalPlayer) PlayerImage.enabled = false;
        
            if (isLocalPlayer)
            {
                transform.SetParent(CardGameManager.singleton.hostSpawn);
            }
            else
            {
                transform.SetParent(CardGameManager.singleton.clientSpawn);
            }
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        stackManager.StartTurn.AddListener(AutoDraw);
    }

    public void ChangeValue()
    {
        if (HealthPoints <= 0)
        {
            Debug.Log("No health points left.");
        }
    }

    public void Win()
    {
        if (isLocalPlayer)
        {
        }
    }

    public void Lose()
    {
        if (isLocalPlayer)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if (!isLocalPlayer)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    [Command]
    public void cmdPlayCard(int number)
    {
        stackManager.RpcPlayCard(number, PlayerID);
    }

    private void FixedUpdate()
    {
        GetComponentInChildren<Slider>().value = HealthPoints;
        if (HealthPoints <= 0)Lose();
    }

    private void AutoDraw()
    {
        if (isLocalPlayer && stackManager.ActivePlayer != PlayerID)
        {
            GameObject.Find("CardHand").GetComponent<CardHandController>().DrawCards();
        }
    }
}
