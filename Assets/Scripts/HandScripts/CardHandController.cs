using System;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class CardHandController : MonoBehaviour
{
    public GameObject[] cards;

    //private int amount = 3;

    public float spacing = 3f;
    private Vector3 touchPosWorld;
    TouchPhase phaseEnded = TouchPhase.Ended;

    private void Start()
    {
        CardHandPositioning();
       
    }

    private void Update()
    {
        
//reset position
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            for (int i = 0; cards.Length > i; i++)
            {
                cards[i].transform.position = new Vector2(0, 0);
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == phaseEnded)
            {
                touchPosWorld = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPosWorld2D = new  Vector2(touchPosWorld.x, touchPosWorld.y);
                
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
    
    void CardSelected(GameObject SelectedCard)
        {
            SelectedCard.transform.position += new Vector3(0, 2, 0);
        }
     void CardHandPositioning()
    {
        for (int i = 0; cards.Length > i; i++)
        {
            //Sondres Demo for advanced card interface. WIP*
            //double SetPosition = 1 / (1 + Mathf.Pow(math.E, - cards.Length * 2.5f * (i / amount - 0.5f)));
          //float xAxis = (float)SetPosition;
          //xAxis /= i;
          //cards[i].transform.position = new Vector2(xAxis, -2);

          
          float xAxis = i * spacing;
          cards[i].transform.position = new Vector2(xAxis - 2, -2);
          
        }
    }

    
}
