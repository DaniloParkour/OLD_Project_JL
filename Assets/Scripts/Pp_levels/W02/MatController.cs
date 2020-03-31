using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatController : MonoBehaviour {

    private SpriteRenderer[] matPieces;
    private float size_x;
    private float velocity = 4;
    private List<Transform> objectsOnMat;

	// Use this for initialization
	void Start () {
        objectsOnMat = new List<Transform>();
        float minPos = 99999;
        float maxPos = -99999;
        matPieces = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            matPieces[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (matPieces[i].transform.position.x - matPieces[i].sprite.bounds.size.x / 2 < minPos)
                minPos = matPieces[i].transform.position.x - matPieces[i].sprite.bounds.size.x / 2;
            if (matPieces[i].transform.position.x + matPieces[i].sprite.bounds.size.x / 2 > maxPos)
                maxPos = matPieces[i].transform.position.x + matPieces[i].sprite.bounds.size.x / 2;
        }
        size_x = maxPos - minPos;

        GameSettings.getSomeSyllables(5);
        GameSettings.getSomeSyllables(15);
        //GameSettings.testSyllables();

	}
    
	// Update is called once per frame
	void Update () {
        moveMat();
        //transform.Translate(velocity * Time.deltaTime, 0, 0);
    }

    private void moveMat() {
        foreach(SpriteRenderer sr in matPieces) {
            sr.transform.Translate(velocity * Time.deltaTime ,0, 0);
            if (sr.transform.position.x - sr.sprite.bounds.size.x / 2 >= (transform.position.x + size_x / 2))
                sr.transform.Translate(-size_x, 0, 0);
        }
        foreach(Transform o in objectsOnMat)
            o.transform.Translate(velocity * Time.deltaTime, 0, 0);
    }

    public void addObjectOnMat(Transform obj) {
        objectsOnMat.Add(obj);
    }

    public void removeObjectFromMat(Transform obj) {
        if(objectsOnMat != null && objectsOnMat.Count > 0 && objectsOnMat.Contains(obj))
            objectsOnMat.Remove(obj);
    }

}
