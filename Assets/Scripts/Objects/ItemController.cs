using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

    private Vector3 initialMov;
    
	// Use this for initialization
	void Start () {
        if (gameObject.tag.Equals("Crystal"))
            initialMov = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(1.0f,3.0f), 0);
	}
	
	// Update is called once per frame
	void Update () {
        initialMoviment();
	}

    private void initialMoviment() {
        if(initialMov.magnitude > 0) {
            transform.Translate(initialMov * Time.deltaTime);
            initialMov -= initialMov * Time.deltaTime;
            if (initialMov.magnitude * 20*Time.deltaTime < 0.1f)
                initialMov = Vector3.zero;
        }
    }

    public void collectMe() {
        GetComponent<Animator>().SetTrigger("collect");
    }

}
