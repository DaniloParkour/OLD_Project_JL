using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePlatform : MonoBehaviour {

    public EnumsGame.MOVE_PLATFORM moveType;
    public LayerMask drag_layer;

    public float rotateVel;
    public float vel;
    public Transform[] path;

    private int currentPosMove = 0;
    private Transform[] childs;
    private List<Transform> to_drag;
    private Vector3 antPos;

    private bool withPlayer = false;

	// Use this for initialization
	void Start () {
        childs = new Transform[transform.childCount];
        for (int i = 0; i < childs.Length; i++)
            childs[i] = transform.GetChild(i);
        to_drag = new List<Transform>();
        antPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        rotateMe();
        moveMe();
        dragObjects();

        antPos = transform.position;

        if (withPlayer && Mathf.Abs(PlayerController.instance.Rgdb.velocity.y) > 0.2f) {
            withPlayer = false;
            to_drag.Remove(PlayerController.instance.transform);
        }

	}

    private void moveMe() {
        if (path == null || path.Length < 2)
            return;
        float dx = (path[(currentPosMove + 1) % path.Length].position.x - path[currentPosMove].position.x) * vel * Time.deltaTime;
        float dy = (path[(currentPosMove + 1) % path.Length].position.y - path[currentPosMove].position.y) * vel * Time.deltaTime;
        transform.Translate(dx,dy,0);

        if (Vector3.Distance(transform.position, path[(currentPosMove + 1) % path.Length].position) <= 0.2f) {
            transform.position = path[(currentPosMove + 1) % path.Length].position;
            currentPosMove = (currentPosMove + 1) % path.Length;
        }
    }

    private void dragObjects() {
        for (int i = 0; i < to_drag.Count; i++) {
            if(!to_drag[i].gameObject.Equals(PlayerController.instance.gameObject)) { 
                to_drag[i].Translate(transform.position.x - antPos.x, 0, 0);
            } else {
                if (PlayerController.instance.FrontToRight)
                    to_drag[i].Translate(transform.position.x - antPos.x, 0, 0);
                else
                    to_drag[i].Translate(-(transform.position.x - antPos.x), 0, 0);
            }
        }
    }

    private void rotateMe() {
        if (moveType.Equals(EnumsGame.MOVE_PLATFORM.ROTATE_X))
            transform.Rotate(rotateVel * Time.deltaTime, 0, 0);
        else if (moveType.Equals(EnumsGame.MOVE_PLATFORM.ROTATE_Y))
            transform.Rotate(0, rotateVel * Time.deltaTime, 0);
        else if (moveType.Equals(EnumsGame.MOVE_PLATFORM.ROTATE_Z))
            //transform.Rotate(0, 0, rotateVel * Time.deltaTime);
            GetComponent<Rigidbody2D>().angularVelocity = rotateVel ;

        if (childs != null && childs.Length > 0)
            for (int i = 0; i < childs.Length; i++)
                childs[i].rotation = Quaternion.identity;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (drag_layer == (drag_layer | (1 << col.gameObject.layer))) {
            if (!to_drag.Contains(col.transform)) {
                to_drag.Add(col.transform);
                if (PlayerController.instance.gameObject.Equals(col.gameObject))
                    withPlayer = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        
        //IF gambiarra pois a animação do player faz a colisão sair quando o player anda.
        if (col.gameObject.Equals(PlayerController.instance.gameObject))
            return;

        if (drag_layer == (drag_layer | (1 << col.gameObject.layer))) {
            if (to_drag.Contains(col.transform))
                to_drag.Remove(col.transform);
        }
    }
}
