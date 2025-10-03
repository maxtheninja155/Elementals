using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public Card card1;
    public Card card2;

    public Player player1;
    public Player player2;


    // Start is called before the first frame update
    void Start()
    {
        card1 = new Card("Snake", 2, 4, 1);
        card2 = new Card("Giant", 5, 7, 25);

        card1.AddAbility("fireball", 6);
        card1.AddAbility("ice spike", 3);
        card1.AddAbility("lava Spash", 9);

        Debug.Log(card2.health);
        card2.health = card1.UseAbility("lava Spash", card2);
        Debug.Log(card2.health);

        player1 = new Player(45, 6, "Mike");
        player2 = new Player(32, 4, "Lucas");

        player1.AddCardToDeck(card2);
        player1.AddCardToDeck(card1);
        //player1.RemoveCardFromDeck(card2);

        player1.PrintPlayerDeck();

        Debug.Log(player1.playerName + " has " + player1.DisplayPlayerHealth() + " health left.");
        Debug.Log(player1.playerName + " has " + player1.DisplayPlayerMana() + " mana left.");

    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
