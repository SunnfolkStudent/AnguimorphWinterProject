using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardHandController : MonoBehaviour
{
    public List<Card> SOcard;
    public List<GameObject> cards;
    public DeckList Deck;
    
    [SerializeField] private int handMaximum = 3;
    [SerializeField] private GameObject CardPrefab;
    
    // sondre edits
    public float spacing = 3f;
    public float spread = 2.5f;
    public float handHeight = 1f;
    public float rotationalFactor = 10f;
    public float offset = 0f;
    public float sensitivity = 30f;
    public float friction = 0.95f;
    public float bounce = 10f;
    private float offsetVelocity = 0f;
    
    private Vector3 touchPosWorld;


    private InputManager _input;
    private void Start()
    {
        _input = GetComponent<InputManager>();

        SOcard = Resources.LoadAll<Card>("CardScrubs").ToList();
        
        DrawCards();
        DrawCards();
        DrawCards();
    } 

    private void Update()
    {
        // sondre edits
        UpdateOffset();
        CardHandPositioning();

        foreach (GameObject card in cards)
        {
            card.GetComponentInChildren<Canvas>().sortingOrder = cards.IndexOf(card);
        }
       
        
        
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            DrawCards();
        }
        
       /* if (_input.tap)
        {
            if (Camera.main != null)
            {
               touchPosWorld = Camera.main.ScreenToWorldPoint(_input.touchPosition); 
               
               Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                               
               RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
               if (hitInformation.collider != null)
               {
                   GameObject SelectedCard = hitInformation.transform.gameObject;
                   
                   
                   
               } 
            }
        }*/
             
             
        

       
       
       
    }

    

    public void DrawCards()
    {
        
        int randCard = UnityEngine.Random.Range(0,Deck.deckList.Count);
        
        if (Deck.deckList[randCard] != null)
        {
            var spawnedCard = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity);
            
            cards.Add(spawnedCard);
            
            foreach (Card card in SOcard)
            {
                var randCardId = UnityEngine.Random.Range(0, SOcard.Count);
               
                if (card.ID == randCardId)
                {
                    spawnedCard.GetComponent<AttackCardDisplay>().card = card;
                }
                
            }
            
        }
        
    }    
    
    
    double Logistic(double x, float strength)
        {
            return 1 / (1 + Math.Pow(Math.E, -strength * (x - 0.5)));
        }
        double Circular(double x, float strength)
        {
            return Math.Sqrt(1 - strength * strength * (2 * x - 1) * (2 * x - 1));
        }    
    void CardHandPositioning()
    {
        for (int i = 0; cards.Count > i; i++)
        {
            
            
            double placeInHand = i / (double)cards.Count;

            double xPosition = Logistic(
                placeInHand + offset,
                cards.Count * spread
            );
            double yPosition = Circular(
                xPosition, 1
            ) * handHeight;
            double direction = (xPosition * 2 * Math.PI - Math.PI) * -rotationalFactor;

            xPosition -= 0.5;
            yPosition -= handHeight;
            xPosition *= spacing;
            
            xPosition += gameObject.transform.position.x;
            yPosition += gameObject.transform.position.y;
            
            
            cards[i].transform.position = new Vector2((float)xPosition, (float)yPosition);
            cards[i].transform.eulerAngles = new Vector3(0f,0f,(float)direction);
            

        }
    }
     
    // sondre edits
    void UpdateOffset()
    {   
       // float dx = Input.GetAxis("Mouse X");
        
        /*if (Input.GetMouseButton(0))
        {
            offsetVelocity += dx * sensitivity / cards.Count;
        }
        offsetVelocity *= friction;
        offset += offsetVelocity * Time.deltaTime;
        float maxOffset = 0.5f;
        if (offset < -maxOffset)
        {
            offset += (-maxOffset - offset) / bounce;
            offsetVelocity *= 0.85f;
        }
        if (offset > maxOffset)
        {
            offset += (maxOffset - offset) / bounce;
            offsetVelocity *= 0.85f;
        }*/

        float dx = _input.DeltaSwipe.x;
        
        {
            offsetVelocity += dx * sensitivity / cards.Count;
        }
        offsetVelocity *= friction;
        offset += offsetVelocity * Time.deltaTime;
        float maxOffset = 0.5f;
        if (offset < -maxOffset)
        {
            offset += (-maxOffset - offset) / bounce;
            offsetVelocity *= 0.85f;
        }
        if (offset > maxOffset)
        {
            offset += (maxOffset - offset) / bounce;
            offsetVelocity *= 0.85f;
        }
    }
}
