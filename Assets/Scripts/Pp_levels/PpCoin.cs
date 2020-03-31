using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PpCoin : MonoBehaviour {

    private Vector3 force;
    private Transform goToPos;
    private float distTarget = 0;
    private float timeToTarget = 0;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        timeToTarget += Time.deltaTime;

        if(timeToTarget > 0.4f) {
            distTarget += Time.deltaTime*2;
            transform.position = Vector3.Lerp(transform.position, goToPos.position, distTarget);
        }
        transform.Translate(force*Time.deltaTime);
        if(distTarget >= 1) {
            PpChooseLetter.instance.addCoin();
            PpChooseLetter.instance.playCoinAudio();
            gameObject.SetActive(false);
        }
	}

    public void throwMe(Vector3 force, Transform goToPos) {
        this.force = force;
        this.goToPos = goToPos;
        timeToTarget = 0;
        distTarget = 0;
    }

}
