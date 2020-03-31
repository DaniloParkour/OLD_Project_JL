using UnityEngine;
using System.Collections;

public class ProtectController : MonoBehaviour {

    private bool initied = false;
    private bool remove = false;
    private float timeToRemove = -10;
    private SpriteRenderer sr;
    
    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!initied)
            return;
        
        if (timeToRemove > -10) {
            timeToRemove -= Time.deltaTime;
            if (timeToRemove < 0)
                Destroy(gameObject);
        }

        if (remove)
            transform.localScale *= (1 + 1.5f * Time.deltaTime);

        if (transform.localPosition.magnitude > 0.2f) {
            transform.localPosition *= 0.9f;
            transform.Rotate(0, 0, 80);
        } else if (transform.localPosition != Vector3.zero)
            transform.localPosition = Vector3.zero;
	}

    public void removeMe() {
        GetComponent<Animator>().SetTrigger("use");
        timeToRemove = 1;
        remove = true;
    }

    public void initMe(Transform to_pos) {
        transform.parent = to_pos;
        initied = true;
        GetComponent<Animator>().SetTrigger("active");
    }
    
}
