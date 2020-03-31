using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PpWorldManager : MonoBehaviour {

    public GameObject canvas;
    public Sprite[] trophiesIcons;

    private GameObject[] levels;
    
	// Use this for initialization
	void Start () {
        new PlayerData();
        initLevelButtons();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initLevelButtons() {
        int t = canvas.transform.FindChild("levels").childCount;
        levels = new GameObject[t];
        for (int i = 0; i < t; i++) {
            levels[i] = canvas.transform.FindChild("levels").GetChild(i).gameObject;
        }

        foreach(PlayerData.PpW01LevelData ld in PlayerData.instance.dataPpW01) {
            Image im = levels[ld.level - 1].transform.FindChild("trophy").GetComponent<Image>();
            im.sprite = trophiesIcons[ld.trophy-1];
            im.color = Color.white;
        }

    }

    public void loadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
}
