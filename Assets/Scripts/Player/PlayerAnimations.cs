using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

    public SpriteRenderer[] playerParts;

    private float timeToBright = -10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bright();
	}

    private void bright() {
        if(timeToBright != -10) {
            timeToBright -= Time.deltaTime;
            if (timeToBright <= 0) {
                foreach(SpriteRenderer sr in playerParts) {
                    if (sr.color.a == 1)
                        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
                    else
                        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                }
                timeToBright = 0.2f;
            }
        }
    }

    public void brightMe(bool bright) {
        if (bright)
            timeToBright = 0.2f;
        else {
            timeToBright = -10;
            foreach (SpriteRenderer sr in playerParts)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        }
    }
}
