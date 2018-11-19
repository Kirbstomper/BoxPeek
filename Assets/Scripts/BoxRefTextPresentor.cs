using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxRefTextPresentor : MonoBehaviour {
    public BoxRef box_ref;
    public Text player_one_peek_time;
    public Text player_two_peek_time;
    public Text player_one_cooldown;
    public Text player_two_cooldown;
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        player_one_peek_time.text =  box_ref.GetPlayerOnePeekTime().ToString(); 
        player_two_peek_time.text = box_ref.GetPlayerTwoPeekTime().ToString();

        player_one_cooldown.text = box_ref.GetPlayerOneCooldown().ToString();
        player_two_cooldown.text = box_ref.GetPlayerTwoCooldown().ToString();

       
    }
}
