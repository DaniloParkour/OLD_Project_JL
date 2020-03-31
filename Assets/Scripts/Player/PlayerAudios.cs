using UnityEngine;
using System.Collections;

public class PlayerAudios : MonoBehaviour {

    public AudioSource[] sources;
    public AudioClip[] clips;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
    }

    public void playAudio(int a_source, int a_clip) {
        //Debug.Log("("+a_source+", "+a_clip+")");
        sources[a_source].clip = clips[a_clip];
        sources[a_source].Play();
    }

    //44100 for delay is the delay of 1 second.
    public void withEcho(int source_a, int source_b, int clip, ulong delay) {
        sources[source_a].clip = clips[clip];
        sources[source_b].clip = clips[clip];
        sources[source_a].Play();
        sources[source_b].Play(delay);
    }

}
