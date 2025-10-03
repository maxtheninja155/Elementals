using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int playerHealth;
    public int playerMana;
    
    public string playerName;

    public List<Card> deck = new List<Card>();

    public Player (int HP, int MP, string NP)
    {
        playerHealth = HP;
        playerMana = MP;
        playerName = NP;
    }


    public void AddCardToDeck(Card cardBeingAdded)
    {
        deck.Add(cardBeingAdded);
        Debug.Log(cardBeingAdded.name + " has been added to " + playerName + "'s deck");
    }

    public void RemoveCardFromDeck(Card cardBeingRemoved)
    {
        deck.Remove(cardBeingRemoved);
        Debug.Log("Just removed " + cardBeingRemoved.name + " from the deck.");  
    }

    public int DisplayPlayerHealth()
    {
        
        return playerHealth;
    }

    public int DisplayPlayerMana()
    {

        return playerMana;
    }

    public void PrintPlayerDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Debug.Log(deck[i].name);
        } 
    }

}
