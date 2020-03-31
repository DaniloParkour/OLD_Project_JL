using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMachine : MonoBehaviour {

    private EnumsGame.LETTERS letter;

    public EnumsGame.LETTERS Letter { get { return letter; } set { letter = value; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initMe() {
        transform.FindChild("letter").GetComponent<Text>().text = GameSettings.enum2letter(letter);
    }
}
