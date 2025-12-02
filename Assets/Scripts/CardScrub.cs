using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScrub : ScriptableObject
{
    public string name;
    public string description;

    public Sprite cardArtwork;

    public int Attack;

}
