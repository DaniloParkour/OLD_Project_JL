using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterInfoW01 {

    private EnumsGame.LETTERS letter;
    private int right;
    private int wrong;
    private List<float> timeList;

    public EnumsGame.LETTERS Letter { get { return letter; } }
    public int Right { get { return right; } }
    public int Wrong { get { return wrong; } }
    public List<float> TimeList { get { return timeList; } }

    public LetterInfoW01(EnumsGame.LETTERS letter, int right, int wrong, float time) {
        this.letter = letter;
        this.right = right;
        this.wrong = wrong;
        timeList = new List<float>();
        timeList.Add(time);
    }

    public void addRight(float time) {
        right++;
        timeList.Add(time);
    }

    public void addWrong(float time) {
        wrong++;
        timeList.Add(time);
    }

    public float medianTime() {
        float retorno = 0;
        foreach (float f in timeList)
            retorno += f;
        return retorno/timeList.Count;
    }
    
}
