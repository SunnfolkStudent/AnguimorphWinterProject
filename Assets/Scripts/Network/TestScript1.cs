using System;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TestScriptNetwork : NetworkBehaviour
{
    [SyncVar] public int PlayerID;
    [SerializeField] StackManager stackManager;

    [SerializeField]
    private int number
    {
        get { return Random.Range(0,101); }
    }
    [SerializeField] private HealthBar healthBar;

    [SyncVar]public int HealthPoints = 100;

    [SerializeField] private TMP_Text healthText;

    private void Start()
    {
        stackManager = StackManager.Instance;
    }

    public void ChangeValue()
    {
        healthText.text = HealthPoints.ToString();
        if (HealthPoints <= 0)
        {
            Debug.Log("No health points left.");
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
