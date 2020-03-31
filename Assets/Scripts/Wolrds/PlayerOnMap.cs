using UnityEngine;
using System.Collections;

public class PlayerOnMap : MonoBehaviour {

    public LevelIcon onLevel;

    private Vector3 posAnt;
    private float distanceOnWay;
    
	// Use this for initialization
	void Start () {
        posAnt = transform.position;
        distanceOnWay = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (onLevel != null && !transform.position.Equals(onLevel.transform.position)) {
            if (distanceOnWay > 1)
                distanceOnWay = 1;
            transform.position = Vector3.Lerp(posAnt, onLevel.transform.position, distanceOnWay);
            if (distanceOnWay == 1) {
                transform.position = onLevel.transform.position;
                posAnt = transform.position;
                distanceOnWay = 0;
            }
            distanceOnWay += Time.deltaTime;
        }

        verifyInputs();

	}

    private void verifyInputs() {
        if (transform.position.Equals(onLevel.transform.position)) {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal > 0) {
                foreach(LevelIcon l in onLevel.getNexts()) {
                    if (l.transform.position.x > (transform.position.x + 1) && l.isUnlocked())
                        onLevel = l;
                }
            } else if (horizontal < 0) {
                foreach (LevelIcon l in onLevel.getNexts()) {
                    if (l.transform.position.x < (transform.position.x - 1) && l.isUnlocked())
                        onLevel = l;
                }
            } else if (vertical > 0) {
                foreach (LevelIcon l in onLevel.getNexts()) {
                    if (l.transform.position.y > (transform.position.y + 1) && l.isUnlocked())
                        onLevel = l;
                }
            } else if (vertical < 0) {
                foreach (LevelIcon l in onLevel.getNexts()) {
                    if (l.transform.position.y < (transform.position.y - 1) && l.isUnlocked())
                        onLevel = l;
                }
            }

			if (Input.GetButtonDown ("Fire1"))
				onLevel.initLevel ();
        }

    }
}
