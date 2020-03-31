using UnityEngine;
using System.Collections;

public class FixedEnemy : MonoBehaviour {

    public bool toLeft = true;
    public bool throwUp = false;
    public Vector2 rand_ab;
    public Vector2 rand_dist;

    private Animator anim;
    private Transform inst_pos;
    
    //Variaveis do projeto:::::::::::::::::::::::::::::
    public GameObject bullet;
    private float timeToThrow = 1;
    //:::::::::::::::::::::::::::::::::::::::::::::::::

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        inst_pos = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (timeToThrow <= 0)
            StartCoroutine("throwBall");
        else
            timeToThrow -= Time.deltaTime;
        
	}

    //Inicio funcões do projeto :::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private IEnumerator throwBall() {

        if(!throwUp)
            anim.SetTrigger("shot");

        timeToThrow = Random.Range(rand_ab.x, rand_ab.y);

        yield return new WaitForSeconds(0.8f);

        GameObject go = Instantiate(bullet, inst_pos.position, Quaternion.identity) as GameObject;
        go.transform.rotation = transform.rotation;
        if (!throwUp) {
        if (toLeft)
            go.GetComponent<Rigidbody2D>().velocity = (new Vector2(Random.Range(-rand_dist.y,-rand_dist.x), Random.Range(rand_dist.x, rand_dist.y)));
        else
            go.GetComponent<Rigidbody2D>().velocity = (new Vector2(Random.Range(rand_dist.x, rand_dist.y), Random.Range(rand_dist.x, rand_dist.y)));
        } else {
            go.GetComponent<Rigidbody2D>().velocity = (new Vector2(Random.Range(-rand_dist.x, rand_dist.x), rand_dist.y));
        }
    }
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    
}
