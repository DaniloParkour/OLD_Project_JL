using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour {

    public int quantBoxes = 3;

    private Box[] boxes;
    private Arm arm;
    private Vector3[] anteriorPositions;
    private float timeChaging = -10;
    private List<string> letter_askSyl;

	// Use this for initialization
	void Start () {
        letter_askSyl = new List<string>();
        letter_askSyl = GameSettings.lettersToPpSyllable(20);
        arm = transform.parent.FindChild("arm").GetComponent<Arm>();
        boxes = new Box[transform.childCount];
        anteriorPositions = new Vector3[boxes.Length];

        for (int i = 0; i < transform.childCount; i++) {
            boxes[i] = transform.GetChild(i).GetComponent<Box>();
            anteriorPositions[i] = boxes[i].transform.position;
            boxes[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < quantBoxes; i++) {
            boxes[i].gameObject.SetActive(true);
            boxes[i] = transform.GetChild(i).GetComponent<Box>();
        }

            initBoxes();
	}
	
	// Update is called once per frame
	void Update () {
        if (timeChaging >= 0)
            timeChaging -= Time.deltaTime;
        else if (timeChaging != -10) {
            timeChaging = -10;
            initBoxes();
        }
	}

    public void initBoxes() {
        List<EnumsGame.Syllables> syl = GameSettings.getSomeSyllables(quantBoxes);
        Transform[] positions = new Transform[quantBoxes];
        for(int i = 0; i < quantBoxes; i++) {
            boxes[i].gameObject.SetActive(true);
            PpGetBoxManager.instance.RemoveSyllableBoxFromMat(boxes[i].transform);
            boxes[i].setSyllable(syl[0]);
            boxes[i].transform.position = anteriorPositions[i];
            positions[i] = boxes[i].transform;
            syl.RemoveAt(0);
        }

        int rand = Random.Range(0, letter_askSyl.Count);
        arm.Syllable = letter_askSyl[rand];

        bool putRightSyllable = true;

        foreach (Box b in boxes)
            if (GameSettings.enum2SyllabeText(b.Syllable)[0].Equals(letter_askSyl[rand][0]))
                putRightSyllable = false;
        
        if (putRightSyllable) {
            string s = GameSettings.getSyllableByLetter(letter_askSyl[rand]);
            boxes[Random.Range(0, boxes.Length)].setSyllable(GameSettings.syllable2enum(s));
        }

        arm.Positions = positions;
    }

    public void changeSyllables() {
        for (int i = 0; i < quantBoxes; i++)
            if (boxes[i].transform.position.y != anteriorPositions[i].y) {
                //PpGetBoxManager.instance.Mat.le
                boxes[i].gameObject.SetActive(false);
            }
        timeChaging = 0;
    }
    
}
