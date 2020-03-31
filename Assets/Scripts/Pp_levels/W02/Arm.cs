using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arm : MonoBehaviour {

    private Transform[] positions;
    private int currentPosition = -1;
    private float timeCatching = 0;
    private Animator anim;
    private Transform catchedBox = null;
    private bool callChangeSyllables = true;
    private string syllable;

    public Transform[] Positions { get { return positions; }
        set { positions = value;
            if(currentPosition == -1)
                currentPosition = (positions.Length)/2;} }

    public string Syllable { get { return syllable; }
        set { syllable = value;
            transform.FindChild("getBox").FindChild("Text").GetComponent<Text>().text = syllable;
        } }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeCatching <= 0 && positions != null && transform.position.x != positions[currentPosition].position.x)
            moveMe();

        if (timeCatching <= 0 && Input.GetButtonUp("Left") && currentPosition > 0)
            currentPosition--;
        else if (timeCatching <= 0 && Input.GetButtonUp("Right") && currentPosition < positions.Length-1)
            currentPosition++;

        if (Input.GetButtonUp("Jump") && timeCatching <= 0 && transform.position.x == positions[currentPosition].position.x) {
            catchBox();
            timeCatching = 1.3f;
            callChangeSyllables = true;
        }

        if(timeCatching <= 0.45f && callChangeSyllables) {
            PpGetBoxManager.instance.changeSyllables();
            callChangeSyllables = false;
        }

        if (timeCatching > 0)
            timeCatching -= Time.deltaTime;
    }

    private void moveMe() {
        float dx = positions[currentPosition].position.x - transform.position.x;
        if (dx > 0.25f)
                transform.Translate(20 * Time.deltaTime, 0, 0);
        else if (dx < -0.25f)
            transform.Translate(-20 * Time.deltaTime, 0, 0);
        else
            transform.Translate(dx, 0, 0);
    }

    private void catchBox() {

        /*
        Debug.Log("PEGAR TEMPORARIO: Trocar depois por animação!");
        if (timeCatching > 0.25f)
            transform.Translate(0,-20 * Time.deltaTime,0);
        else
            transform.Translate(0, 20 * Time.deltaTime, 0);
        timeCatching -= Time.deltaTime;
        if (timeCatching < 0)
            timeCatching = 0;
        */

        anim.SetTrigger("getBox");

    }
    
    public void getBoxNow() {
        catchedBox = positions[currentPosition];
        catchedBox.parent = transform.FindChild("getBox");
    }

    public void letBoxOnMat() {
        PpGetBoxManager.instance.AddSyllableBoxOnMat(catchedBox);
        catchedBox = null;
        //lockPosition = currentPosition;
    }

    public bool compareAnswer() {
        /*Debug.Log(syllable + " = " + GameSettings.
            enum2SyllabeText(catchedBox.GetComponent<Box>().Syllable)[0] + " > " +
            syllable.Equals(GameSettings.
            enum2SyllabeText(catchedBox.GetComponent<Box>().Syllable).StartsWith(syllable)));
            */
        //string teste = GameSettings.enum2SyllabeText(catchedBox.GetComponent<Box>().Syllable);
        //return teste.StartsWith(syllable);

        return GameSettings.enum2SyllabeText(catchedBox.GetComponent<Box>().Syllable).StartsWith(syllable);

        /*
        return syllable.Equals(GameSettings.
            enum2SyllabeText(catchedBox.GetComponent<Box>().Syllable)[0]);
            */
    }

}
