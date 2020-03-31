using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public float timeToRemove;
    public EnumsGame.BULLET_TYPE type;
    public LayerMask whatHit;
    public int hitForce;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        timeToRemove -= Time.deltaTime;
        if (timeToRemove <= 0 && timeToRemove > -10)
            removeMe();
        if (timeToRemove <= -10)
            Destroy(gameObject);

        if (type.Equals(EnumsGame.BULLET_TYPE.NORMAL) && timeToRemove <= 0.5f && timeToRemove > 0) {
            transform.localScale *= (1+Time.deltaTime);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 2*Time.deltaTime);
        }
        
	}

    private void removeMe() {
        if (type.Equals(EnumsGame.BULLET_TYPE.NORMAL) || type.Equals(EnumsGame.BULLET_TYPE.COLIDE_DESAPEAR))
            Destroy(gameObject);
    }

    public void initMe(float timeOnScene, EnumsGame.BULLET_TYPE typeBullet) {
        timeToRemove = timeOnScene;
        type = typeBullet;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (whatHit == (whatHit | (1 << col.gameObject.layer)))
            col.transform.FindChild("player").gameObject.GetComponent<PlayerController>().hitMe(hitForce);
        if (type.Equals(EnumsGame.BULLET_TYPE.COLIDE_DESAPEAR))
            Destroy(gameObject);
    }
    
}
