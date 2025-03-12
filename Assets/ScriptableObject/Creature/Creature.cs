using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature")]
public class Creature : ScriptableObject
{
    [Header("Identification")]
    public int id;
    public string nameCreature;
    public string description;
    public Sprite image;

    [Header("Stats")]
    public float health;
    public float minHealth;
    public float maxHealth;
    public float defense;
    public float minDefense;
    public float maxDefense;
    public float attack;
    public float minAttack;
    public float maxAttack;
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public float attackTimer;
    public float attackMaxTimer;


}
