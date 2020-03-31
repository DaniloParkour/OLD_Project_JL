using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultMat : MonoBehaviour {

    public EnumsGame.LETTERS[] letters;
    public Text[] texts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateValues() {

        int[] rights = new int[letters.Length];
        int[] wrongs = new int[letters.Length];

        /*
         * for (int i = 0; i < PlayerData.instance.turnData.rights.Count; i++)
            for(int j = 0; j < letters.Length; j++)
                if (PlayerData.instance.turnData.rights[i].Equals(letters[j]))
                    rights[j]++;

        for (int i = 0; i < PlayerData.instance.turnData.wrongs.Count; i++)
            for (int j = 0; j < letters.Length; j++)
                if (PlayerData.instance.turnData.wrongs[i].Equals(letters[j]))
                    wrongs[j]++;
        */

        //Debug.Log("ERROS> "+ wrongs[0] + " - " + wrongs[1] + " - " + wrongs[2] + " - " + wrongs[3] + " - " + wrongs[4]);
        //Debug.Log("ACERTOS> " + rights[0] + " - " + rights[1] + " - " + rights[2] + " - " + rights[3] + " - " + rights[4]);

        for (int i = 0; i < texts.Length; i++) {
            if((wrongs[i] + rights[i]) != 0)
            texts[i].text = GameSettings.enum2letter(letters[i]) + " = " + rights[i] + " / " + (wrongs[i] + rights[i]) + " > " +
                ((float)rights[i]/(wrongs[i] + rights[i]) * 100) + "%.";
        }
    }

}
