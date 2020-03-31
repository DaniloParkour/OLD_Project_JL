using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AmountItem : MonoBehaviour {

    private Image remove;
    private float timeAnim;
    
	// Use this for initialization
	void Start () {
        initRemoveObject();
        timeAnim = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if(timeAnim > 0) {
            timeAnim -= Time.deltaTime;
            animaRemove();
        }
	}

    public void initRemoveAnim() {
        timeAnim = 0.5f;
        remove.gameObject.SetActive(true);
    }

    private void animaRemove() {
        remove.transform.Translate(new Vector3(0, -5*Time.deltaTime, 0));
        remove.transform.localScale *= (1+Time.deltaTime);
        remove.color = new Color(1,remove.color.g - Time.deltaTime,remove.color.b - Time.deltaTime);
        if (timeAnim <= 0)
            resetRemoveAnim();
    }

    private void initRemoveObject() {
        GameObject go = Instantiate(new GameObject());
        go.AddComponent<RectTransform>();
        go.transform.SetParent(transform);
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.AddComponent<Image>();
        go.GetComponent<Image>().sprite = GetComponent<Image>().sprite;

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GetComponent<RectTransform>().sizeDelta.x);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetComponent<RectTransform>().sizeDelta.y);

        remove = go.GetComponent<Image>();
        remove.gameObject.SetActive(false);
    }

    private void resetRemoveAnim() {
        remove.transform.localPosition = new Vector3(0, 0, 0);
        remove.transform.localScale = new Vector3(1, 1, 1);
        remove.GetComponent<Image>().color = Color.white;
        remove.gameObject.SetActive(false);
    }
    
}
