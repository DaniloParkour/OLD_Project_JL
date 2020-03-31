using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePlane : MonoBehaviour {

    public EnumsGame.EFFECTS_SCRIPT type;
    public float duration = 1;

    private bool anima = false;
    private SpriteRenderer sr;

    public bool Anima { get { return anima; } set { anima = value; } }

	void Awake () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (type.Equals(EnumsGame.EFFECTS_SCRIPT.FADE_OUT) && anima) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime/duration);
        } else if (type.Equals(EnumsGame.EFFECTS_SCRIPT.FADE_IN) && anima) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + Time.deltaTime / duration);
        }
    }

    public void initAnima() {
        if (type.Equals(EnumsGame.EFFECTS_SCRIPT.FADE_OUT))
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        else if (type.Equals(EnumsGame.EFFECTS_SCRIPT.FADE_IN))
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        anima = true;
    }
    
}
