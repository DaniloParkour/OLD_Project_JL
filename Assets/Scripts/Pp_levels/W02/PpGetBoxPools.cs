using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PpGetBoxPools : MonoBehaviour {

    private List<Transform> boxes;
    private List<Transform> used_boxes;

    // Use this for initialization
    void Start () {
        
        boxes = new List<Transform>();
        used_boxes = new List<Transform>();
        for (int i = 0; i < transform.FindChild("boxes").childCount; i++) {
            boxes.Add(transform.FindChild("boxes").GetChild(i));
            boxes[i].gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        verifyUsedPools();
	}

    private void verifyUsedPools() {
        if(used_boxes.Count > 0)
            for(int i = used_boxes.Count-1; i > 0; i--) {
                if (used_boxes[i].position.x >= 20) {
                    PpGetBoxManager.instance.RemoveSyllableBoxFromMat(used_boxes[i]);
                    used_boxes[i].GetComponent<Animator>().SetBool("explode", false);
                    addBox(used_boxes[i]);
                }
            }
    }

    public Transform getOneBox() {
        Transform b = boxes[Random.Range(0, boxes.Count)];
        b.gameObject.SetActive(true);
        boxes.Remove(b);
        used_boxes.Add(b);
        return b;
    }

    public void addBox(Transform b) {
        boxes.Add(b);
        if(used_boxes.Contains(b))
            used_boxes.Remove(b);
        b.gameObject.SetActive(false);
    }

}
