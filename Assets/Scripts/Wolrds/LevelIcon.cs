using UnityEngine;
using System.Collections;

public class LevelIcon : MonoBehaviour {

    [SerializeField]
    private LevelIcon[] nextLevel;
    [SerializeField]
    private bool unlocked;

    public string levelName;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public LevelIcon[] getNexts() {
        return nextLevel;
    }

    public void unlockLevel() {
        unlocked = true;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public bool isUnlocked() {
        return unlocked;
    }

    public void initLevel() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (levelName);
    }
    
}
