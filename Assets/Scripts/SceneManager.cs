using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

    public static SceneManager instance;

    public Sprite[] a_images;
    public Sprite[] e_images;
    public Sprite[] i_images;
    public Sprite[] o_images;
    public Sprite[] u_images;
    
    public Text t_coins;
    public Text t_crystals;

    public List<ShiftBackgrounds> parallax_objs;

    public PanelTexts endPanel;

    public string world;

    private Vector3 d_player;
    private bool winLevel = false;
    private float valueToShift = 0;
    
    void Awake() {
        instance = this;
        new PlayerData();
        PlayerData.instance.resetTurnData();
        //testeRand();
        instance = this;
    }

    private void testeRand() {
        int a = 0; int b = 0; int c = 0;

        int a1 = 0; int b1 = 0; int c1 = 0; int d1 = 0; int e1 = 0;

        for (int i = 0; i < 10000; i++) {
            int v1 = Random.Range(0, 3);
            int v2 = Random.Range(0, 5);
            if (v1 == 0)
                a++;
            else if (v1 == 1)
                b++;
            else if (v1 == 2)
                c++;

            if (v2 == 0)
                a1++;
            else if (v2 == 1)
                b1++;
            else if (v2 == 2)
                c1++;
            else if (v2 == 3)
                d1++;
            else if (v2 == 4)
                e1++;
        }

        //Debug.Log("_________________________________________________________________");
        //Debug.Log("1> "+a+", "+b+", "+c);
        //Debug.Log("2> " + a1 + ", " + b1 + ", " + c1 + ", " + d1 + ", " + e1);
    }

	// Use this for initialization
	void Start () {
        d_player = PlayerController.instance.transform.position;
        valueToShift = Camera.main.ViewportToWorldPoint(new Vector3(0.75f,0,0)).x -
            Camera.main.ViewportToWorldPoint(new Vector3(0.25f, 0, 0)).x;
    }
	
	// Update is called once per frame
	void Update () {
        adjustCameraPlayer();

        if(Input.anyKeyDown && endPanel.gameObject.activeSelf)
            UnityEngine.SceneManagement.SceneManager.LoadScene(world);
    }

    private void adjustCameraPlayer() {
        if (PlayerController.instance.loseGame || PlayerController.instance.WinLevel)
            return;
        Camera.main.transform.Translate(PlayerController.instance.transform.position.x - d_player.x, 0, 0);
        if (parallax_objs != null)
            foreach (ShiftBackgrounds sb in parallax_objs)
                sb.moveMe(new Vector3(PlayerController.instance.transform.position.x - d_player.x, 0, 0));

        d_player = PlayerController.instance.transform.position;
    }

    public void flipCamera() {
        Debug.Log("Deletar Depois!");
    }
    
    public void moveCameraVertical() {

        if (PlayerController.instance.loseGame)
            return;

        Camera.main.transform.Translate(0, (PlayerController.instance.transform.position.y - d_player.y),0);

        if (parallax_objs != null)
            foreach (ShiftBackgrounds sb in parallax_objs)
                sb.moveMe(new Vector3(0, PlayerController.instance.transform.position.y - d_player.y, 0));

    }

    public void addCoin(int quant) {
        PlayerData.instance.turnData.coins += quant;
        t_coins.text = "" + PlayerData.instance.turnData.coins;
    }

    public void addCrystals(int quant) {
        PlayerData.instance.turnData.crystals += quant;
        t_crystals.text = "" + PlayerData.instance.turnData.crystals;
    }

    public void removeCrystal(int quant) {

        if (PlayerData.instance.turnData.crystals == 0)
            return;

        PlayerData.instance.turnData.crystals -= quant;
        if (PlayerData.instance.turnData.crystals < 0)
            PlayerData.instance.turnData.crystals = 0;
        t_crystals.text = "" + PlayerData.instance.turnData.crystals;
        t_crystals.transform.parent.parent.gameObject.GetComponent<AmountItem>().initRemoveAnim();
    }

    public void endLevel() {
        if (endPanel.gameObject.activeSelf)
            return;
        endPanel.gameObject.SetActive(true);
        string[] texts = new string[4];
        texts[0] = ""+PlayerData.instance.turnData.coins;
        texts[1] = ""+PlayerData.instance.turnData.crystals;
        texts[2] = ""+PlayerData.instance.turnData.right;
        texts[3] = ""+PlayerData.instance.turnData.unright;
        endPanel.addTexts(texts);
        endPanel.openWindow(1);
        endPanel.transform.FindChild("result_mat").GetComponent<ResultMat>().updateValues();
        GetComponent<AudioSource>().volume = 0.3f;
    }

    public void loseGame() {
        PlayerController.instance.loseGame = true;
        StartCoroutine("leftLevel");
        GetComponent<AudioSource>().volume = 0.3f;
    }

    //Coroutines
    IEnumerator leftLevel() {
        yield return new WaitForSeconds(2f);
        StartCoroutine("loadWorld");
        for (int i = 0; i < 100; i++) {
            yield return new WaitForSeconds(0.005f);
            Camera.main.orthographicSize -= 0.05f;
        }
        //UnityEngine.SceneManagement.SceneManager.LoadScene("World01");
    }

    IEnumerator loadWorld() {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("World01");
    }
}
