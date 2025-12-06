using System;
using UnityEngine;
using Mirror;
using TMPro;
using Random = UnityEngine.Random;

public class TestScriptNetwork : NetworkBehaviour
{
    [SerializeField] StackManager stackManager;

    [SerializeField]
    private int number
    {
        get { return Random.Range(0,101); }
    }

    private void Start()
    {
        stackManager = StackManager.Instance;
    }

    [Command]
    public void cmdPlayCard()
    {
        stackManager.RpcPlayCard(number);
    }
}
