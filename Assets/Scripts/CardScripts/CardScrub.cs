using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScrub : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public string description;

    public Sprite cardArtwork;

    public int Attack;

    public enum CardType
    {
        Attack,
        Effect,
        Instant,
    }
}
