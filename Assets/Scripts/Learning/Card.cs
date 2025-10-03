using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    public string name;

    public int manaCost;
    public int attackDmg;
    public int health;

    public Dictionary<string, int> abilities = new Dictionary<string, int>();

    public Card(string nameP, int manaCostP, int attackP, int healthP)
    {
        name = nameP;
        manaCost = manaCostP;
        attackDmg = attackP;
        health = healthP;
    }

    public void AddAbility(string nameOfAbility, int abilityDmg)
    {
        abilities.Add(nameOfAbility, abilityDmg);
    }

    public int UseAbility(string abiltyName, Card target)
    {
        int abiltydmg;
        int targetHealth;
        
        abilities.TryGetValue(abiltyName, out abiltydmg);

        targetHealth = target.health;
        targetHealth = targetHealth - abiltydmg;

        Debug.Log(name + " used " + abiltyName + " on " + target.name + " and dealt " + abiltydmg + " damage");
        return targetHealth;
    }
}
