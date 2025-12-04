using System;
using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class CardHandController : MonoBehaviour
{
    private InputManager _touch;
    public GameObject[] cards;
    public bool cardUsed;
    private int amount = 3;
    

    private void Start()
    {
        
        _touch = GetComponent<InputManager>();
         
        for (int i = 0; cards.Length > i; i++)
        {
            //float spacing = i * 1;
            //cards[i].transform.position = new Vector2(spacing,-2);
            CardHandPositioning();
        }
    }

    private void Update()
    {
        
    }
     void CardHandPositioning()
    {
        for (int i = 0; cards.Length > i; i++)
        {
            //double SetPosition = 1 / (1 + Mathf.Pow(math.E, - cards.Length * 2.5f * (i / amount - 0.5f)));
          //float xAxis = (float)SetPosition;
          //xAxis /= i;
          //cards[i].transform.position = new Vector2(xAxis, -2);

          int xAxis = i * 1;
          cards[i].transform.position = new Vector2(xAxis, -2);
          
          
        }
        
            
    }
}
