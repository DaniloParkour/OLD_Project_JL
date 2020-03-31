 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData {

    public static PlayerData instance;

    public string username;
    public int levelPlayer;
    public int playerOnLevel;
    public int totalCoins;
    public int totalCrystals;

    public PlayerOnGame turnData;

    public List<PpW01LevelData> dataPpW01;

    public struct PlayerOnGame {
        public int coins;
        public int crystals;
        public int right;
        public int unright;
        public List<LetterInfoW01> letters;
    }

    public struct PpW01LevelData {
        public int level;
        public int trophy;

        public PpW01LevelData(int l, int t) {
            level = l;
            trophy = t;
        }
    }

    public PlayerData () {
        if (instance == null) {
            instance = this;
            dataPpW01 = new List<PpW01LevelData>();
        }
        
        //playerData.levelPlayer = PlayerPrefs.GetInt("currentLevel");
        
        //Só por enquanto [DELETAR DEPOIS]________________________
        levelPlayer = 16;
        username = "Xapuleta";
        //________________________________________________________
        
	}
	
    public void resetTurnData() {
        turnData = new PlayerOnGame();
        turnData.coins = 0;
        turnData.crystals = 0;
        turnData.right = 0;
        turnData.unright = 0;

        turnData.letters = new List<LetterInfoW01>();
    }
    
    public void addRightLetter(EnumsGame.LETTERS letter, float timeToChoose) {
        //Debug.Log("Right " + GameSettings.enum2letter(letter) + " in " + timeToChoose + " seconds.");
        foreach(LetterInfoW01 l in turnData.letters) {
            if (l.Letter.Equals(letter)) {
                l.addRight(timeToChoose);
                return;
            }
        }
        turnData.letters.Add(new LetterInfoW01(letter, 1, 0, timeToChoose));
    }

    public void addWrongLetter(EnumsGame.LETTERS letter, float timeToChoose) {
        //Debug.Log("Wrong " + GameSettings.enum2letter(letter) + " in " + timeToChoose + " seconds.");
        foreach (LetterInfoW01 l in turnData.letters) {
            if (l.Letter.Equals(letter)) {
                l.addWrong(timeToChoose);
                return;
            }
        }
        turnData.letters.Add(new LetterInfoW01(letter, 0, 1, timeToChoose));
    }

    public void addDataPpW01(PpW01LevelData data) {
        foreach (PpW01LevelData d in dataPpW01) {
            if (d.level.Equals(data.level)) {
                if(d.trophy >= data.trophy)
                    return;
                else {
                    dataPpW01.Remove(d);
                    break;
                }
            }
        }
        dataPpW01.Add(data);
    }


}
