using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShiftBackgrounds : MonoBehaviour {

    public SceneManager sceneManager;
    public float speed;
    
    private List<SpriteRenderer> scenarios;
    private List<SpriteRenderer> bottons;

    private float dx_scenario; //Tamanho em X dos cenários.
    private float dx_botton; //Tamanho em X dos bottons.
    
    // Use this for initialization
    void Start () {

       
        scenarios = new List<SpriteRenderer>();
        bottons = new List<SpriteRenderer>();
        Transform go_sc = transform.FindChild("scenario");
        Transform go_bo = transform.FindChild("botton");
        dx_scenario = 0;
        dx_botton = 0;

        float max_scenario = 0;
        float min_scenario = 0;
        float max_botton = 0;
        float min_botton = 0;

        for (int i = 0; i < go_sc.childCount; i++) {
            scenarios.Add(go_sc.GetChild(i).GetComponent<SpriteRenderer>());
            
            if (i == 0) {
                min_scenario = go_sc.GetChild(i).position.x - go_sc.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x/2;
                max_scenario = go_sc.GetChild(i).position.x + go_sc.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x/2;
            } else {
                if ((go_sc.GetChild(i).position.x-go_sc.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x/2) < min_scenario)
                    min_scenario = go_sc.GetChild(i).position.x - go_sc.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x / 2;
                if ((go_sc.GetChild(i).position.x + go_sc.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x / 2) > max_scenario)
                    max_scenario = go_sc.GetChild(i).position.x + go_sc.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x / 2;
            }
        }
        dx_scenario = max_scenario - min_scenario;
        for (int i = 0; i < go_bo.childCount; i++) {
            bottons.Add(go_bo.GetChild(i).GetComponent<SpriteRenderer>());
            if (i == 0) {
                min_botton = go_bo.GetChild(i).position.x - go_bo.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x/2;
                max_botton = go_bo.GetChild(i).position.x + go_bo.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x/2;
            } else {
                if ((go_bo.GetChild(i).position.x-go_bo.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x/2) < min_botton)
                    min_botton = go_bo.GetChild(i).position.x - go_bo.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x / 2;
                if ((go_bo.GetChild(i).position.x + go_bo.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x / 2) > max_botton)
                    max_botton = go_bo.GetChild(i).position.x + go_bo.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x / 2;
            }
        }
        dx_botton = max_botton - min_botton;
    }
	
	// Update is called once per frame
	void Update () {
        verifyParts();
    }

    private void verifyParts() {
        float x_camera = 0;

        if(PlayerController.instance.FrontToRight)
            x_camera = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x;
        else
            x_camera = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;

        foreach (SpriteRenderer sr in scenarios) {
            if(PlayerController.instance.FrontToRight) {
                if (sr.transform.position.x + sr.bounds.size.x / 2 < x_camera)
                    sr.transform.Translate(dx_scenario, 0, 0);
            } else {
                if (sr.transform.position.x - sr.bounds.size.x / 2 > x_camera)
                    sr.transform.Translate(-dx_scenario, 0, 0);
            }
        }
        
        foreach (SpriteRenderer sr in bottons) {
            if(PlayerController.instance.FrontToRight) {
                if (sr.transform.position.x + sr.bounds.size.x / 2 < x_camera)
                    sr.transform.Translate(dx_botton, 0, 0);
            } else {
                if (sr.transform.position.x - sr.bounds.size.x / 2 > x_camera)
                    sr.transform.Translate(-dx_botton, 0, 0);
            }
        }
        
    }

    public void moveMe(Vector3 d) {
        transform.Translate(d*speed);
    }
    
}
