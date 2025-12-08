using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string Description;

    public Sprite artwork;

    public int attack;
    public float damageReduction;

    public CardTypes cardType;

    public enum CardTypes
    {
        Damage,
        DrawCard,
        ReduceDamageTo1,
        ReduceDamageByHalf,

    }
}
