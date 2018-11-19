using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRef : MonoBehaviour {

    const float COMPLETE_PEEK_TIME = 4.0f;
    const float COOLDOWN_TIME = 6.0f;
    const float RETREAT_TIME = 2.0f;
    //The actual boxes. will be passed
    public BoxPeekPlr player_one;
    public BoxPeekPlr player_two;

    //Players time peeking, really basic
    private float player_one_peek_time;
    private float player_two_peek_time;
    // Player peek booleans, will control logic and start the counter
    private bool player_one_is_peek;
    private bool player_two_is_peek;
    //Player peek cooldown times after retreating from a peek.
    private float player_one_cooldown;
    private float player_two_cooldown;
    // This enum determines the game status.
    public enum game_states {NO_WIN = 0,
                            player_one = 1,
                            player_two = 2,
                            GAME_OVER = 3}
    private int game_status = 0;
    // Use this for initialization
    void Start () {

        player_one_peek_time = player_two_peek_time = 0.0f;
        player_one_is_peek = player_two_is_peek = false;
        Time.timeScale = 1;
    
	}
	
	// Update is called once per frame
	void Update () {

        //If game state has changed due to a counter peek occuring or someone winning, also basically end the game

        if (game_status != (int)game_states.GAME_OVER)
        {
            if (game_status != (int)game_states.NO_WIN)
            {
                print("Game ended due to counter peek! Player " + game_status + "Wins!");
                player_one = player_two = null;// Causes a null pointer error prob
            }

            //Otherwise increment whoever is peeking and check if someone has won!
            else
            {
                //Cooldown time tickdown
                if (player_one_cooldown > 0) player_one_cooldown -= Time.deltaTime;
                if (player_two_cooldown > 0) player_two_cooldown -= Time.deltaTime;

                //Peek Time for if a player is peeking
                if (player_one_is_peek) player_one_peek_time += Time.deltaTime;
                if (player_two_is_peek) player_two_peek_time += Time.deltaTime;

                if (player_one_peek_time >= COMPLETE_PEEK_TIME)
                {
                    game_status = (int)game_states.player_one;
                    print("Game ended due complete peek! Player " + game_status + "Wins!");
                    player_one = player_two = null;// Causes a null pointer error prob
                    game_status = (int)game_states.GAME_OVER;

                }
                if (player_two_peek_time >= COMPLETE_PEEK_TIME)
                {
                    game_status = (int)game_states.player_two;
                    print("Game ended due complete peek! Player " + game_status + "Wins!");
                    player_one = player_two = null;// Causes a null pointer error prob
                    game_status = (int)game_states.GAME_OVER;
                }
            }
        }
        else
        {
            //Logic for starting a new game
        }
   


	}


    /**
     * This method is activated when input is received from a player
     **/

     public bool GetInput(BoxPeekPlr plr)
    {
        if(plr == player_one)
        {
            if(player_one_cooldown <= 0.0f)
            {
                if (!player_one_is_peek) return Peek(plr);
                else { return StopPeek(plr); }
            }
            
        }
        if (plr == player_two)
        {
            if (player_two_cooldown <= 0.0f)
            {
                if (!player_two_is_peek) return Peek(plr);
                else { return StopPeek(plr); }
            }
          
        }
        return false;
 
    }

    /**
     *This method will begin the peek for a player.
     * **/
    public bool Peek(BoxPeekPlr plr)
    {
        if(plr == player_one )
        {
            if (player_two_is_peek)
            {
                game_status = (int)game_states.player_one;
                return true;
            }
            player_one_is_peek = true;
            return true;
        }
        if(plr == player_two)
        {
            if (player_one_is_peek)
            {
                game_status = (int)game_states.player_two;
                return true;
            }
            player_two_is_peek = true;
            return true;
        }
        return false;
    }

    /**
     * Stops a player from peeking, if they are allowed per box peek rules.
     * Can prob have a more intelligent way of checking which player initiates the action....
     * */
    public bool StopPeek(BoxPeekPlr plr)
    {
        if(plr == player_one && player_one_peek_time >= RETREAT_TIME)
        {
            player_one_is_peek = false;
            player_one_peek_time = 0.0f;
            player_one_cooldown = COOLDOWN_TIME;
            return true;
        }
        if (plr == player_two && player_two_peek_time >= RETREAT_TIME)
        {
            player_two_is_peek = false;
            player_two_peek_time = 0.0f;
            player_two_cooldown = COOLDOWN_TIME;
            return true;
        }
        return false;

    }

    public float GetPlayerOnePeekTime()
    {

        return player_one_peek_time;

    }

    public float GetPlayerTwoPeekTime()
    {
        return player_two_peek_time;
    }

    public float GetPlayerOneCooldown()
    {
        return player_one_cooldown;
    }
    public float GetPlayerTwoCooldown()
    {
        return player_two_cooldown;
    }

    public int GetGameStatus()
    {
        return game_status;
    }
}
