using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxController : MonoBehaviour {
    
    public EnumsGame.TYPE_BOX type;
    public EnumsGame.LETTERS letter_a;
    public EnumsGame.LETTERS letter_b;
    public List<ItemController> items;

    private float timeToTestRight = 0;
    private PlayerAudios audios;
    private bool used = false;

    //Getters & Setters
    public bool Used { get { return used; } }

    void Awake() {
        if (type.Equals(EnumsGame.TYPE_BOX.IMAGE_BOX_RAND_VOWEL))
            letter_a = GameSettings.getLowercaseVowels()[Random.Range(0,5)];
        audios = GetComponent<PlayerAudios>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        attTimes();
	}

    public void holdMe() {
        transform.parent.GetComponent<WallController>().freeblocks();
    }

    public void wrongBox() {
        if (timeToTestRight > 0)
            return;
        timeToTestRight = 0.2f;
        if (type.Equals(EnumsGame.TYPE_BOX.IMAGE_BOX_RAND_VOWEL) || type.Equals(EnumsGame.TYPE_BOX.IMAGE_BOX)) {
            GetComponent<Animator>().SetTrigger("unrightBox");
            SceneManager.instance.removeCrystal(1);
            audios.playAudio(0, 1);
            
        } else {
            Rigidbody2D rgdb = GetComponent<Rigidbody2D>();
            if(Random.Range(0.0f,1f) > 0.5f)
                rgdb.velocity = new Vector2(Random.Range(1, 5), 8);
            else
                rgdb.velocity = new Vector2(Random.Range(-5, -1), 8);
            used = true;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    public void rightBox() {
        GetComponent<Animator>().SetTrigger("rightBox");
        audios.playAudio(0, 0);
        StartCoroutine("removeMe");
    }

    private void attTimes() {
        if (timeToTestRight > 0)
            timeToTestRight -= Time.deltaTime;
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        BoxController b_col = col.GetComponent<BoxController>();
        if ((type.Equals(EnumsGame.TYPE_BOX.IMAGE_BOX_RAND_VOWEL) || type.Equals(EnumsGame.TYPE_BOX.IMAGE_BOX)) && b_col != null) {
            if (!(b_col == PlayerController.instance.BoxHold))
                return;
            PlayerController.instance.letHold();
            if (letter_a.Equals(b_col.letter_a)) {
                b_col.gameObject.SetActive(false);
                rightBox();
                //PlayerData.instance.turnData.rights.Add(letter_a);
                PlayerData.instance.addRightLetter(letter_a, -1);
                PlayerData.instance.turnData.right++;
            } else if(timeToTestRight <= 0 && !b_col.used) {
                b_col.wrongBox();
                wrongBox();
                PlayerData.instance.addWrongLetter(letter_a, -1);
                PlayerData.instance.turnData.unright++;
            }
        }
    }

    //Coroutines
    IEnumerator removeMe() {
        yield return new WaitForSeconds(1f);
        if(items != null && items.Count != 0) {
            foreach(ItemController ic in items)
                Instantiate(ic, transform.position, Quaternion.identity);
        }
        Transform pc = transform.FindChild("protect");
        if (pc != null) {
            pc.gameObject.SetActive(true);
            pc.transform.parent = null;
            
            //GAMB => Por causa do tamanho alterado do player
            pc.transform.localScale = new Vector3(0.6f, 0.6f, 1);

        }
    }
    
}
