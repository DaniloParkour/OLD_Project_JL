using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesManager : MonoBehaviour {

    public List<Sprite> sprites;
    public bool justVowels = true;

    private List<Sprite> used_sprites;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public Sprite getSpriteByLetter(EnumsGame.LETTERS letter);
    //public Sprite getVowelSprite();

    public Sprite getImage() {
        Sprite retorno = null;

        if (sprites.Count == 0)
            return null;

        if (sprites.Count == 1) {
            retorno = sprites[0];
            sprites.RemoveAt(0);
        } else {
            int rand = Random.Range(0, sprites.Count);
            retorno = sprites[rand];
            sprites.RemoveAt(rand);
        }

        if(used_sprites == null)
            used_sprites = new List<Sprite>();

        used_sprites.Add(retorno);
        if (sprites.Count == 0) {
            while(used_sprites.Count > 0) {
                sprites.Add(used_sprites[0]);
                used_sprites.RemoveAt(0);
            }
        }

        return retorno;
    }
    
}
