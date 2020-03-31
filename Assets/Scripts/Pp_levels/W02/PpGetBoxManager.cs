using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PpGetBoxManager : MonoBehaviour {

    [SerializeField]
    private MatController mat;
    [SerializeField]
    private Arm arm;
    [SerializeField]
    private BoxManager boxesManager;
    [SerializeField]
    private PpGetBoxPools pools;

    public MatController Mat { get { return mat; } }
    public Arm Arm { get { return arm; } }
    public BoxManager BoxesManager { get { return boxesManager; } }

    public static PpGetBoxManager instance;

    void Awake() {
        instance = this;
        for(int i = 0; i < pools.transform.FindChild("boxes").childCount; i++) {
            pools.transform.FindChild("boxes").GetChild(i).gameObject.SetActive(true);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddSyllableBoxOnMat(Transform t) {
        bool rightAnswer = arm.compareAnswer();
        Transform is_right = pools.getOneBox();

        mat.addObjectOnMat(t);
        
        if (rightAnswer) {
            is_right.transform.position = t.position;
            mat.addObjectOnMat(is_right);
        } else {
            is_right.transform.position = t.position;
            mat.addObjectOnMat(is_right);
            is_right.GetComponent<Animator>().SetBool("explode", true);
        }
        
        t.parent = boxesManager.transform;
    }

    public void RemoveSyllableBoxFromMat(Transform t) {
        mat.removeObjectFromMat(t);
    }

    public void changeSyllables() {
        boxesManager.changeSyllables();
    }
    
}
