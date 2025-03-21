
using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Creature creatureData;
    public float health, minHealth, maxHealth;
    public float attackTimer, attackMaxTimer;
    public float attack, minAttack, maxAttack;
    public float defense, minDefense, maxDefense;
    public float speed, minSpeed, maxSpeed;
    public Vector3 spawnpoint;
}
