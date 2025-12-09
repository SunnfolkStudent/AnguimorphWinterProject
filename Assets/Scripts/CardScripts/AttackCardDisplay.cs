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
        nameText.text = card.Name;
        descriptionText.text = card.Description;
        
        artworkImage.sprite = card.artwork;
        
        attackText.text = card.attack.ToString();
        
        
    }

    public void PlayCard()
    {
        if (card != null)
        {
            takeDamage.TakeDamage(card.attack);
        }
    }
    
    public void OnButtonClick()
    {
        PlayCard();
    }
    
}
