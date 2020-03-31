using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelTexts : MonoBehaviour {

    public Text[] texts;

    private float timeToAnima;
    private float typeOpen;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        animaWindow();
	}

    private void animaWindow() {
        if (timeToAnima < 0)
            return;
        if (typeOpen == 1) {
            if(timeToAnima > 1)
                transform.localScale = new Vector3(0.1f, transform.localScale.y + Time.deltaTime, 0);
            else
                transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, 1, 0);
        }

        timeToAnima -= Time.deltaTime;
        if (timeToAnima <= 0)
            transform.localScale = new Vector3(1,1,1);
    }

    public void openWindow(int animType) {
        typeOpen = animType;
        if(animType == 1) {
            transform.localScale = new Vector3(0.1f,0.1f,1);
            timeToAnima = 2;
        }
    }
    
    public void addTexts(string[] values) {
        for (int i = 0; i < values.Length; i++)
            texts[i].text = values[i];
    }
}
