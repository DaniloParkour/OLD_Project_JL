using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    public Transform target;
    public float smooth = 10;
    public Vector3 dxy; //0 a 1 menor e maior tamanho da tela.

    private Vector3 targetTo;
    private Vector3 sizeView;

    private Vector3 ant_pos_targ;
    
	// Use this for initialization
	void Start () {
        float z_dist = target.position.z - transform.position.z;
        float dx = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, z_dist)).x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, z_dist)).x;
        float dy = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, z_dist)).y - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, z_dist)).y;
        ant_pos_targ = transform.position;
        Debug.Log(dx + " - " + dy);
	}
	
	// Update is called once per frame
	void Update () {
        moveMe();
	}

    private void moveMe() {
        if (ant_pos_targ.x != target.position.x) {
            transform.Translate(target.position.x-ant_pos_targ.x, 0, 0);
            ant_pos_targ = target.position;
        }
    }
}
