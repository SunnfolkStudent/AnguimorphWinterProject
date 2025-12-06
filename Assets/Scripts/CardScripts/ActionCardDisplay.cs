using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionCardDisplay : NetworkBehaviour
{
    public Card card;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Image artworkImage;
    
    void Start()
    {
        nameText.text = card.Name;
        descriptionText.text = card.Description;
        
        artworkImage.sprite = card.artwork;
    }
}
