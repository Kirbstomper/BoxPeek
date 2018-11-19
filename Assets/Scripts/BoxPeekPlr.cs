using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPeekPlr : MonoBehaviour {

    //Holds a reference to the button used to peek or counter peek. Basically just sends a command to server/ref that handles all other logic
    public BoxRef box_ref;
    // The key set for  box object. Kind of dirty but I wanted to get it working
    public KeyCode input_key;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(input_key)) sendInput();
	}


    //Calls the box_ref's method GetInput to determine if it can peek or not. REF KNOWS ALL
    bool sendInput()
    {
       
        return box_ref.GetInput(this);

    }
}
