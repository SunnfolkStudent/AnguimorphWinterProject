using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackCardDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Image artworkImage;

    public TextMeshProUGUI attackText;

    public TestPlayerHealthBar takeDamage;
    
    void Start()
    {
        //nameText.text = card.Name;
        descriptionText.text = card.Description;
        
        artworkImage.sprite = card.artwork;
        
        attackText.text = card.attack.ToString();
        
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        
        
    }
    
    public void OnButtonClick()
    {
        Debug.Log(card.Name+" Clicked");
        foreach (TestScriptNetwork player in FindObjectsOfType<TestScriptNetwork>())
        {
            if (player.isLocalPlayer && player.myturn)
            {
                Debug.Log("Playing:" + card.Name);
                player.cmdPlayCard(card.ID);
                GameObject.Find("CardHand").GetComponent<CardHandController>().cards.Remove(gameObject);
                Destroy(gameObject);
            }
        }
        }
    }
