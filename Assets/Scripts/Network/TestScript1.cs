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
    [SerializeField] private Image PlayerImage; 
    public GameObject Player;
    [SerializeField] StackManager stackManager;

    private int number
    {
        get
        {
            return Random.Range(0,CardGameManager.singleton.cards.Count);
        }
    }
    [SerializeField] private HealthBar healthBar;

    [SyncVar]public int HealthPoints;

    [SerializeField] private TMP_Text healthText;

    private void Start()
    {
        stackManager = StackManager.Instance;
        PlayerSprite = CardGameManager.singleton.PlayerSprites[PlayerSPriteID];
        if (isLocalPlayer) PlayerImage.enabled = false;
    }

    public void ChangeValue()
    {
        healthText.text = HealthPoints.ToString();
        if (HealthPoints <= 0)
        {
            Debug.Log("No health points left.");
        }
    }

    public void Win()
    {
        if (isLocalPlayer)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    public void Lose()
    {
        if (isLocalPlayer)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    [Command]
    public void cmdPlayCard()
    {
        stackManager.RpcPlayCard(number, PlayerID);
    }

    private void FixedUpdate()
    {
        GetComponentInChildren<Slider>().value = HealthPoints;
    }
}
