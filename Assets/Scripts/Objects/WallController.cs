using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WallController : MonoBehaviour {
    
    public SceneManager sceneManager;
    
    public BoxController box;
    public EnumsGame.TYPE_WALL type;

    private List<BoxController> crates;
    private bool freezed = true;
    

    // Use this for initialization
    void Start () {
        crates = new List<BoxController>();
        List<string> new_letters;
        bool addLetterBox = true;

        //if(!type.Equals(TYPE_WALL.DOUBLE_VOWELS) || !type.Equals(TYPE_WALL.DOUBLE_LETTERS))
        new_letters = GameSettings.getLowerLetters(26);
        
        for (int i = 0; i < transform.childCount; i++) {
            BoxController b = transform.GetChild(i).GetComponent<BoxController>();
            if (b == null)
                continue;
            if (!b.type.Equals(EnumsGame.TYPE_BOX.IMAGE_BOX) && !b.type.Equals(EnumsGame.TYPE_BOX.IMAGE_BOX_RAND_VOWEL)) {
                if(type.Equals(EnumsGame.TYPE_WALL.LOWER_VOWELS)) {
                    int val = Random.Range(0, new_letters.Count);
                    b.transform.FindChild("Text").GetComponent<Text>().text = new_letters[val];
                    b.letter_a = GameSettings.letter2enum(new_letters[val]);
                    crates.Add(b);
                    new_letters.RemoveAt(val);
                    if (box != null && b.letter_a.Equals(box.letter_a))
                        addLetterBox = false;
                }
            }
        }
        
        if(box != null) {

            /*TEMPORARIO PARA PREENCHER BOX*/
            if (box.letter_a.Equals(EnumsGame.LETTERS.A) || box.letter_a.Equals(EnumsGame.LETTERS.Am))
                box.transform.FindChild("box_paper").transform.FindChild("image").GetComponent<SpriteRenderer>().sprite =
                    sceneManager.a_images[Random.Range(0,sceneManager.a_images.Length)];
            else if (box.letter_a.Equals(EnumsGame.LETTERS.E) || box.letter_a.Equals(EnumsGame.LETTERS.Em))
                box.transform.FindChild("box_paper").transform.FindChild("image").GetComponent<SpriteRenderer>().sprite =
                    sceneManager.e_images[Random.Range(0, sceneManager.e_images.Length)];
            else if (box.letter_a.Equals(EnumsGame.LETTERS.I) || box.letter_a.Equals(EnumsGame.LETTERS.Im))
                box.transform.FindChild("box_paper").transform.FindChild("image").GetComponent<SpriteRenderer>().sprite =
                    sceneManager.i_images[Random.Range(0, sceneManager.i_images.Length)];
            else if (box.letter_a.Equals(EnumsGame.LETTERS.O) || box.letter_a.Equals(EnumsGame.LETTERS.Om))
                box.transform.FindChild("box_paper").transform.FindChild("image").GetComponent<SpriteRenderer>().sprite =
                    sceneManager.o_images[Random.Range(0, sceneManager.o_images.Length)];
            else if (box.letter_a.Equals(EnumsGame.LETTERS.U) || box.letter_a.Equals(EnumsGame.LETTERS.Um))
                box.transform.FindChild("box_paper").transform.FindChild("image").GetComponent<SpriteRenderer>().sprite =
                    sceneManager.u_images[Random.Range(0, sceneManager.u_images.Length)];

            if (addLetterBox) {
                int randVal = Random.Range(0, crates.Count);
                crates[randVal].letter_a = box.letter_a;
                crates[randVal].transform.Find("Text").GetComponent<Text>().text = GameSettings.enum2letter(box.letter_a);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void freeblocks() {
        if (!freezed)
            return;
        foreach (BoxController c in crates) {
            c.GetComponent<Rigidbody2D>().isKinematic = false;
            c.gameObject.layer = 15;
        }
        freezed = false;
    }

    
    
}
