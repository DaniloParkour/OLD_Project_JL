using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestToWebservice : MonoBehaviour {

    public bool testGetWebImage;
    public string imageURL = "http://sua-corrida-com-br.s3.amazonaws.com/wp-content/uploads/2013/10/Parkour-1.jpg";
    public bool testGetDataDictionary;
    public string url = "http://unity3d.com";
    public bool baixarPaginaWEB;
    public string urlDown = "https://www.jusbrasil.com.br/busca?q=teste";
    public string localToSave = "C:/Users/danil/Desktop/downpage.html";

    private bool asSprite = false;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {

        sr = GetComponent<SpriteRenderer>();
        
        if(testGetWebImage)
            StartCoroutine("Test");
        //if (testGetDataDictionary)
          //  StartCoroutine("getData");
        //if(baixarPaginaWEB)
          //  StartCoroutine("getWEBPage");
            
        

    }

    private bool pause = false;

    // Update is called once per frame
    void Update () {

	}

    IEnumerator getWEBPage() {
        WWW www = new WWW(urlDown);
        yield return www;

        System.IO.File.WriteAllText(localToSave, www.text);
        
        if (www.responseHeaders.Count > 0) {
            foreach (KeyValuePair<string, string> entry in www.responseHeaders) {
                Debug.Log(entry.Value + " <===> " + entry.Key);
            }
        }
    }

    IEnumerator getData() {
        WWW www = new WWW(url);
        yield return www;

        if (www.responseHeaders.Count > 0) {
            foreach (KeyValuePair<string, string> entry in www.responseHeaders) {
                Debug.Log(entry.Value + " <===> " + entry.Key);
            }
        }
    }

    IEnumerator Test() {
        
        if (sr == null) {
            WWW www = new WWW(imageURL);
            yield return www;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = www.texture;
            Debug.Log("FOI");
        } else {
            WWW www = new WWW(imageURL);
            yield return www;
            sr.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            //renderer.material.mainTexture = www.texture;
            Debug.Log("FOI");
        }
        
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        //sr.sprite = Sprite.Create(www.texture, sr.sprite.rect, sr.sprite.pivot);

        //Debug.Log("Voltou!");
        //Debug.Log(">"+getName.text);

        /*WWWForm form = new WWWForm();
        form.AddField("PARAMtest0", "parkour");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/WebserviceTest/TesteWebService?Tester", form);
        yield return www.Send();
        
        Debug.Log(www.ToString());

        if (www.isError)
            Debug.Log(www.error);
        else
            Debug.Log("Form upload complete!");*/
    }

}
