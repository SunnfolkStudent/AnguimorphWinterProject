using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class CopyCardHandController : MonoBehaviour
{
    //public Card SOcard;
    public List<GameObject> cards;
    public DeckList Deck;
    
    [SerializeField] private int handMaximum = 3;
    [SerializeField] private GameObject CardPrefab;
    
    //private int amount = 3;

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
    TouchPhase phaseEnded = TouchPhase.Ended;

    private void Start()
    {
        DrawCards();
       
    }

    private void Update()
    {
        // sondre edits
        UpdateOffset();
        CardHandPositioning();
        //reset position
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            for (int i = 0; cards.Count > i; i++)
            {
                cards[i].transform.position = new Vector2(0, 0);
            }
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            //DrawCards();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == phaseEnded)
            {
                if (Camera.main != null)
                {
                   touchPosWorld = Camera.main.ScreenToWorldPoint(touch.position); Vector2 touchPosWorld2D = new  Vector2(touchPosWorld.x, touchPosWorld.y);
                                   
                   RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
                   if (hitInformation.collider != null)
                   {
                       GameObject SelectedCard = hitInformation.transform.gameObject;
                       
                       Debug.Log("Touched "+ SelectedCard.transform.name);
                       CardSelected(SelectedCard = SelectedCard.transform.gameObject);
                   } 
                }
                
            }
            
        }
    }

    void DrawCards()
    {
       /* int randCard = UnityEngine.Random.Range(0,Deck.deckList.Count);
        Debug.Log(randCard);
        if (Deck.deckList[randCard] != null)
        {
            Debug.Log("passed not null");
            for (int i = 0; i < handMaximum; i++)
            {
                
                cards.Add(Deck.deckList[randCard]);
                CardHandPositioning();
                var spawnedCard = Instantiate(CardPrefab);
                
                spawnedCard.GetComponent<AttackCardDisplay>().card = SOcard;
            }
            
        }*/
        
        
    }    
    void CardSelected(GameObject SelectedCard)
        {
            SelectedCard.transform.position += new Vector3(0, 2, 0);
        }

    double Logistic(double x, float strength)
    {
        return 1 / (1 + Math.Pow(Math.E, -strength * (x - 0.5)));
    }
    double Circular(double x, float strength)
    {
        return Math.Sqrt(1 - strength * strength * (2 * x - 1) * (2 * x - 1));
    }

    // sondre edits
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

            xPosition *= spacing;
            //Debug.Log(xPosition);
          
            //float xAxis = i * spacing;
            //cards[i].transform.position = new Vector2(xAxis - 2, -2);
            cards[i].transform.position = new Vector2((float)xPosition, (float)yPosition);
            cards[i].transform.eulerAngles = new Vector3(0f,0f,(float)direction);
          
        }
    }

    // sondre edits
    void UpdateOffset()
    {   
        float dx = Input.GetAxis("Mouse X");
        if (Input.GetMouseButton(0))
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
