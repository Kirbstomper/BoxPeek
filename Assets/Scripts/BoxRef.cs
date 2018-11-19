using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRef : MonoBehaviour {

    //The actual boxes. will be passed
    public BoxPeekPlr player_1;
    public BoxPeekPlr player_2;

    //Players time peeking, really basic
    private float player_1_peek_time;
    private float player_2_peek_time;
    // Player peek booleans, will control logic and start the counter
    private bool player_1_is_peek;
    private bool player_2_is_peek;
    // This enum determines the game status.
    public enum game_states {NO_WIN = 0,
                            PLAYER_1 = 1,
                            PLAYER_2 = 2,
                            GAME_OVER = 3}
    private int game_status = 0;
    // Use this for initialization
    void Start () {

        player_1_peek_time = player_2_peek_time = 0.0f;
        player_1_is_peek = player_2_is_peek = false;
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
                player_1 = player_2 = null;// Causes a null pointer error prob
            }

            //Otherwise increment whoever is peeking and check if someone has won!
            else
            {
                if (player_1_is_peek) player_1_peek_time += Time.deltaTime;
                if (player_2_is_peek) player_2_peek_time += Time.deltaTime;

                if (player_1_peek_time >= 4)
                {
                    game_status = (int)game_states.PLAYER_1;
                    print("Game ended due complete peek! Player " + game_status + "Wins!");
                    player_1 = player_2 = null;// Causes a null pointer error prob
                    game_status = (int)game_states.GAME_OVER;

                }
                if (player_2_peek_time >= 4)
                {
                    game_status = (int)game_states.PLAYER_2;
                    print("Game ended due complete peek! Player " + game_status + "Wins!");
                    player_1 = player_2 = null;// Causes a null pointer error prob
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

     public void GetInput(BoxPeekPlr plr)
    {
        if(plr == player_1)
        {
            if (!player_1_is_peek) Peek(plr);
            else { StopPeek(plr); }
        }
        if (plr == player_2)
        {
            if (!player_2_is_peek) Peek(plr);
            else { StopPeek(plr); }
        }
 
    }

    /**
     *This method will begin the peek for a player.
     * **/
    public void Peek(BoxPeekPlr plr)
    {
        if(plr == player_1 )
        {
            if (player_2_is_peek)
            {
                game_status = (int)game_states.PLAYER_1;
                return;
            }
            player_1_is_peek = true;
            return;
        }
        else
        {
            if (player_1_is_peek)
            {
                game_status = (int)game_states.PLAYER_2;
                return;
            }
            player_2_is_peek = true;
            return;
        }
    }

    /**
     * Stops a player from peeking, if they are allowed per box peek rules.
     * Can prob have a more intelligent way of checking which player initiates the action....
     * */
    public void StopPeek(BoxPeekPlr plr)
    {
        if(plr == player_1 && player_1_peek_time > 2)
        {
            player_1_is_peek = false;
            player_1_peek_time = 0.0f;
            return;
        }
        if (plr == player_2 && player_2_peek_time > 2)
        {
            player_2_is_peek = false;
            player_2_peek_time = 0.0f;
        }

    }


}
