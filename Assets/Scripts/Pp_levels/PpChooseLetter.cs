using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class PpChooseLetter : MonoBehaviour {

    public bool justVowels = true;
    public bool useMin = false;
    public bool useSong = true;
    public float totalTime = 40;
    public PpSwitchPlayer player;
    public static PpChooseLetter instance;
    public GameObject ui;
    public GameObject pools;
    public Transform coinsChest;
    public int[] trophiesValue;
    public AudioClip[] audios;
    public int level = 1;
    public string url = "10.0.0.1";

    private AudioSource audio;
    private SwitchManager switchManager;

    private float timeToResp = 1;

    private Text[] t_result;
    private Text t_coins;
    private int[] resultRightWrong = { 0, 0 };
    private int quantCoins = 0;
    private float timeToEndGame = -10;
    private float timeOnLevel = 0;
    private float timeToChoose = 0;
    private float timeToWait = 1;
    private bool addNesAsk = false;

    private Image i_time;

    private PpCoin[] coins;

    private Vector3[] forces;

    public SwitchManager SwitchManager { get { return switchManager; } }
    public int[] ResultRightWrong { get { return resultRightWrong; } }
    public float TimeToResp { get { return timeToResp; } }

    void Awake() {
        switchManager = transform.FindChild("switches").GetComponent<SwitchManager>();
        switchManager.LevelManager = this.GetComponent<PpChooseLetter>();
        instance = this;
        new PlayerData();
        PlayerData.instance.resetTurnData();
        audio = GetComponent<AudioSource>();
    }
    
    // Use this for initialization
    void Start () {

        StartCoroutine("sendPOST");

        Camera.main.transform.FindChild("fadeplane").gameObject.SetActive(true);
        Camera.main.transform.FindChild("fadeplane").GetComponent<FadePlane>().initAnima();
        timeToWait = Camera.main.transform.FindChild("fadeplane").GetComponent<FadePlane>().duration;
        
        t_result = new Text[2];
        t_result[0] = ui.transform.FindChild("right").transform.FindChild("t_quant").GetComponent<Text>();
        t_result[1] = ui.transform.FindChild("wrong").transform.FindChild("t_quant").GetComponent<Text>();
        i_time = ui.transform.FindChild("time_clock").GetComponent<Image>();

        t_coins = transform.FindChild("switches").FindChild("coinsChest").FindChild("quantCoins").GetComponent<Text>();

        coins = new PpCoin[pools.transform.FindChild("coins").childCount];
        for (int i = 0; i < pools.transform.FindChild("coins").childCount; i++) {
            coins[i] = pools.transform.FindChild("coins").GetChild(i).GetComponent<PpCoin>();
        }
        
        forces = new Vector3[coins.Length];
        if (coins.Length == 5) {
            forces[0] = new Vector3(-12, 0, 0);
            forces[1] = new Vector3(-6, -4, 0);
            forces[2] = new Vector3(0, -6, 0);
            forces[3] = new Vector3(6, -4, 0);
            forces[4] = new Vector3(12, 0, 0);
        }

        timeToChoose = 0;
        
    }
	
	// Update is called once per frame
	void Update () {

        timeToResp -= Time.deltaTime;

        if (timeToWait > 0) {
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0) {
                ui.SetActive(true);
                Camera.main.transform.FindChild("fadeplane").GetComponent<FadePlane>().Anima = false;
                Camera.main.transform.FindChild("fadeplane").gameObject.SetActive(false);
            }
            return;
        }
        
        if (timeToResp <= 0)
            timeToChoose += Time.deltaTime;

        if(addNesAsk && timeToResp < 0) {
            switchManager.newAsk();
            addNesAsk = false;
            player.TestOnTriggerStay = true;
            timeToChoose = 0;
        }

        if (timeToEndGame > 0)
            timeToEndGame -= Time.deltaTime;

        if (totalTime < timeOnLevel && timeToEndGame == -10) {

            timeToEndGame = -11;

            int trophie = 1;

            if ((resultRightWrong[0] - resultRightWrong[1]) < trophiesValue[0])
                pools.transform.FindChild("trophies").FindChild("01").gameObject.SetActive(true);
            else if ((resultRightWrong[0] - resultRightWrong[1]) < trophiesValue[1]) {
                pools.transform.FindChild("trophies").FindChild("02").gameObject.SetActive(true);
                trophie = 2;
            }
            else {
                pools.transform.FindChild("trophies").FindChild("03").gameObject.SetActive(true);
                trophie = 3;
            }

            player.StopAsk = true;

            StartCoroutine("sendPOST");
            
            PlayerData.instance.addDataPpW01(new PlayerData.PpW01LevelData(level, trophie));

        }

        //if (timeToEndGame != -10 && timeToEndGame < 0)
          //  UnityEngine.SceneManagement.SceneManager.LoadScene("World01");

        timeOnLevel += Time.deltaTime;
        if (timeOnLevel <= totalTime) {
            i_time.fillAmount = 1 - (timeOnLevel/totalTime);
        }
    }

    public void putAnswer(EnumsGame.LETTERS l_answer) {

        if (timeToResp > 0)
            return;

        if(switchManager.AskPanel.letter.Equals(l_answer)) {
            resultRightWrong[0]++;
            switchManager.animaResp(true);
            PlayerData.instance.addRightLetter(switchManager.AskPanel.letter, timeToChoose);

            for (int i = 0; i < coins.Length; i++) {
                coins[i].gameObject.SetActive(true);
                coins[i].transform.localPosition = Vector3.zero;
                coins[i].throwMe(forces[i],coinsChest);
            }

        } else {
            resultRightWrong[1]++;
            switchManager.animaResp(false);
            PlayerData.instance.addWrongLetter(switchManager.AskPanel.letter, timeToChoose);
        }

        if (GetComponent<ImagesManager>().sprites.Count == 0) {
            
        } else {
            //Nova pergunta
            addNesAsk = true;
            timeToResp = 0.6f;
        }

        t_result[0].text = resultRightWrong[0] + "";
        t_result[1].text = resultRightWrong[1] + "";

    }

    public void addCoin() {
        quantCoins++;
        t_coins.text = "" + quantCoins;
    }

    public void endLevel() {
        Debug.Log("Level terminado!");
        for(int i = 1; i <= pools.transform.FindChild("trophies").childCount; i++) {
            if (pools.transform.FindChild("trophies").FindChild("0" + i).gameObject.activeSelf)
                Debug.Log("ADD troféu " + pools.transform.FindChild("trophies").FindChild("0" + i).name);
        }
    }

    public void playCoinAudio() {
        audio.clip = audios[0];
        audio.Play();
    }

    IEnumerator sendPOST() {
        
        WWWForm form = new WWWForm();

        /*
        form.AddField("data", System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
        form.AddField("username", PlayerData.instance.username);
        form.AddField("minigame", "letras");
        form.AddField("fase", level);

        string letters = "";

        for (int i = 0; i < PlayerData.instance.turnData.letters.Count; i++) {
            string l = GameSettings.enum2letter(PlayerData.instance.turnData.letters[i].Letter);
            int r = PlayerData.instance.turnData.letters[i].Right;
            int w = PlayerData.instance.turnData.letters[i].Wrong;
            float t = PlayerData.instance.turnData.letters[i].medianTime();

            letters += "\"letra\":\"" + l + "\",\"acerto\":" + r + ",\"erro\":" + w + ",\"mediaTempo\":" + t;
            if (i + 1 < PlayerData.instance.turnData.letters.Count)
                letters += ",";
        }

        form.AddField("letras", letters);

        */

        //_____________________________________________________________________________________________
        //form = new WWWForm();
        //url = "http://localhost:8080/WebserviceTest/TesteWebService?tester";
        //form.AddField("str", "VAI PORRA!");
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

        //url = "http://10.0.22.201:3000/dados";

        //url = "http://10.0.22.201:3000/dados/idade?10";

        //url = "http://10.0.22.201:3000/dados/idade=10&nome=Lucas&Level=120";

        //url = "http://10.0.22.201:3000/dados/idade?10&nome?Lucas&Level?120";

        //byte[] b_data = Encoding.ASCII.GetBytes("Vai Caraio!");
        //byte[] b_data = Encoding.UTF8.GetBytes("Vai Caraio!");
        //WWW www = new WWW(url, b_data);
        //WWW www = new WWW(url);
        //yield return www;

        url = "http://edupweb.herokuapp.com/rest";
        
        form.AddField("nome", "Danilo");

        WWW wform = new WWW(url, form);

        //url = "https://edupweb.herokuapp.com/form?name=Cris";
        
        //GET
        //WWW wform = new WWW(url);

        yield return wform;
        if (!string.IsNullOrEmpty(wform.error))
            Debug.Log("DEU MERDA!!!");
        else {
            System.IO.File.WriteAllText("C:/Users/danil/Desktop/downpage.html", wform.text);
            Debug.Log(wform.text);
        }

        //ESSE POST NAO DEU CERTO NO WEBSERVICE ATUAL
        /*
        string data = "{";

        data += "\"data\":\"" + System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year + "\",";
        data += "\"username\":\"" + PlayerData.instance.username + "\",";
        data += "\"minigame\":\"letras\",";
        data += "\"fase\":" + level + ",";

        data += "\"letras\":{";

        for (int i = 0; i < PlayerData.instance.turnData.letters.Count; i++) {
            string l = GameSettings.enum2letter(PlayerData.instance.turnData.letters[i].Letter);
            int r = PlayerData.instance.turnData.letters[i].Right;
            int w = PlayerData.instance.turnData.letters[i].Wrong;
            float t = PlayerData.instance.turnData.letters[i].medianTime();

            data += "\"letra\":\"" + l + "\",\"acerto\":" + r + ",\"erro\":" + w + ",\"mediaTempo\":" + t;
            if (i + 1 < PlayerData.instance.turnData.letters.Count)
                data += ",";
        }

        data += "}}";

        Debug.Log(data);

        byte[] b_data = Encoding.ASCII.GetBytes(data);
        
        WWW www = new WWW(url, b_data);
        yield return www;
        Debug.Log("FOI!");
        */

    }

}
