using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPanel : MonoBehaviour {

    public EnumsGame.LETTERS letter;

    private float timeOnAnim = -10;
    private bool rightResp = true;
    private SpriteRenderer sr_slot;
    private SpriteRenderer sr_showImage;
    private Transform showImage;

    private float vel01 = 1;
    private float vel02 = 2;
    
	void Awake () {
        sr_slot = GetComponent<SpriteRenderer>();
        showImage = transform.FindChild("showImage");
        sr_showImage = showImage.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeOnAnim < 0 && timeOnAnim != -10) {
            transform.localScale = Vector3.one * 4;
            showImage.localScale = Vector3.one;
            showImage.localRotation = Quaternion.identity;
            sr_slot.color = Color.white;
            timeOnAnim = -10;
        }

        if(timeOnAnim >= 0) {
            timeOnAnim -= Time.deltaTime;
            if (rightResp)
                animaRightResp();
            else
                animaWrongResp();
        }

	}
    
    private void animaRightResp() {
        if (timeOnAnim > 0.3f) {
            transform.localScale *= (1 + Time.deltaTime/2);
            sr_slot.color = Color.Lerp(sr_slot.color, Color.green, Time.deltaTime*6);
            showImage.localScale *= (1 + 2 * Time.deltaTime);
        } else {
            showImage.localScale *= (1 - 5 * Time.deltaTime);
            showImage.transform.Rotate(0, 0, 900 * Time.deltaTime);
            sr_showImage.color = new Color(sr_showImage.color.r, sr_showImage.color.g, sr_showImage.color.b, sr_showImage.color.a - (2.5f * Time.deltaTime));
        }
    }

    private void animaWrongResp() {
        if (timeOnAnim > 0.4f) {
            sr_slot.color = Color.Lerp(sr_slot.color, Color.red, Time.deltaTime*6);
            showImage.localScale *= (1 - 10 * Time.deltaTime);
            sr_showImage.color = Color.Lerp(sr_showImage.color, Color.red, Time.deltaTime * 6);
        } else {
            sr_showImage.color = new Color(sr_showImage.color.r, sr_showImage.color.g, sr_showImage.color.b, sr_showImage.color.a - (2.5f * Time.deltaTime));
        }
    }

    public void initAnswAnim(bool isRight) {
        rightResp = isRight;
        timeOnAnim = 0.5f;
    }

    //public void initAsVowel();
    public void initPanel() {
        Sprite sprt = PpChooseLetter.instance.GetComponent<ImagesManager>().getImage();
        if (sprt == null)
            return;
        sr_showImage.sprite = sprt;
        sr_showImage.color = Color.white;
        if (sprt.name.StartsWith("A"))
            letter = EnumsGame.LETTERS.A;
        else if (sprt.name.StartsWith("E"))
            letter = EnumsGame.LETTERS.E;
        else if (sprt.name.StartsWith("I"))
            letter = EnumsGame.LETTERS.I;
        else if (sprt.name.StartsWith("O"))
            letter = EnumsGame.LETTERS.O;
        else if (sprt.name.StartsWith("U"))
            letter = EnumsGame.LETTERS.U;
    }
}
