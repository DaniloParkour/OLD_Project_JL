using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour {

    private EnumsGame.Syllables syllable;
    private Text text;

    public EnumsGame.Syllables Syllable { get { return syllable; } }
    
	// Use this for initialization
	void Awake () {
        text = transform.FindChild("Text").gameObject.GetComponent<Text>();
        text.text = "%$";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setSyllable(EnumsGame.Syllables s) {
        syllable = s;
        if(text == null)
            text = transform.FindChild("Text").gameObject.GetComponent<Text>();
        text.text = GameSettings.enum2SyllabeText(s);
    }
}
