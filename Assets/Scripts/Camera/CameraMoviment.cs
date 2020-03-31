using UnityEngine;
using System.Collections;

public class CameraMoviment : MonoBehaviour {

    private Vector3[] followPositions;
    private Vector3 goToPos;
    private bool playerToRight = true;

	// Use this for initialization
	void Start () {
        followPositions = new Vector3[2];
        followPositions[0] = new Vector3(0.3f, 0.3f, 10);
        followPositions[1] = new Vector3(0.7f, 0.3f, 10);
        playerToRight = PlayerController.instance.FrontToRight;
    }

    void Update() {
        if (playerToRight != PlayerController.instance.FrontToRight) {
            playerFlip();
            playerToRight = PlayerController.instance.FrontToRight;
        }
    }
	
    private void playerFlip() {
        if (PlayerController.instance.FrontToRight) {
            //posPlayer = 0;
        } else {
            //posPlayer = 1;
        }
    }
}
