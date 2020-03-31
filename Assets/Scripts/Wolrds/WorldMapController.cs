using UnityEngine;
using System.Collections;

public class WorldMapController : MonoBehaviour {


    public LevelIcon[] levelIcons;
    public PlayerOnMap player;
    public Vector2 levelInitEnd;

    void Awake() {
        new PlayerData();
    }

	// Use this for initialization
	void Start () {
        int cont = 0;
        int currentPos = 0;
        while(cont <= PlayerData.instance.levelPlayer) {
            if(cont >= levelInitEnd.x && cont <= levelInitEnd.y) {
                levelIcons[currentPos].unlockLevel();
                currentPos++;
            }
            cont++;
        }
        player.onLevel = levelIcons[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
