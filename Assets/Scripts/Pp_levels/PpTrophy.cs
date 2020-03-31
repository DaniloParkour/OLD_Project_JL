using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PpTrophy : MonoBehaviour {

    public string backToScene;
    public Transform goToPos;

    private bool collected = false;
    private float timeAnim = 0;
    private float lerpScale = 0;
    private float timeOnCollect = 0;

    private Vector3 toSize;
    
	// Use this for initialization
	void Start () {
        toSize = transform.localScale * 3;
	}
	
	// Update is called once per frame
	void Update () {
        timeAnim += Time.deltaTime;
        if (timeAnim < 2) {
            lerpScale += Time.deltaTime / 2;
            transform.localScale = Vector3.Lerp(transform.localScale, toSize, lerpScale);
        } else if (Input.anyKeyDown && !collected) {
            collected = true;
            lerpScale = 0;
            PpChooseLetter.instance.endLevel();
        }

        if (collected) {
            lerpScale += Time.deltaTime / 2;
            timeOnCollect += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, goToPos.position, Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, toSize/3, lerpScale);
            if (timeOnCollect > 0.65f)
                UnityEngine.SceneManagement.SceneManager.LoadScene(backToScene);
        }

	}



}
