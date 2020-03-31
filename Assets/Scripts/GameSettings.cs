using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSettings {

    /*{Am, Bm, Cm, Dm, Em, Fm, Gm, Hm, Im, Jm, Km, Lm, Mm, Nm, Om, Pm,
    Qm, Rm, Sm, Tm, Um, Vm, Wm, Xm, Ym, Zm, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O,
    P, Q, R, S, T, U, V, W, X, Y, Z}*/

    private static EnumsGame.Syllables[] syllables = {
        EnumsGame.Syllables.Ba, EnumsGame.Syllables.Be, EnumsGame.Syllables.Bi, EnumsGame.Syllables.Bo, EnumsGame.Syllables.Bu,
        EnumsGame.Syllables.Ca,                                                 EnumsGame.Syllables.Co, EnumsGame.Syllables.Cu,
        EnumsGame.Syllables.Da, EnumsGame.Syllables.De, EnumsGame.Syllables.Di, EnumsGame.Syllables.Do, EnumsGame.Syllables.Du,
        EnumsGame.Syllables.Fa, EnumsGame.Syllables.Fe, EnumsGame.Syllables.Fi, EnumsGame.Syllables.Fo, EnumsGame.Syllables.Fu,
        EnumsGame.Syllables.Ga, EnumsGame.Syllables.Ge, EnumsGame.Syllables.Gi, EnumsGame.Syllables.Go, EnumsGame.Syllables.Gu,
        EnumsGame.Syllables.Ha, EnumsGame.Syllables.He, EnumsGame.Syllables.Hi, EnumsGame.Syllables.Ho, EnumsGame.Syllables.Hu,
        EnumsGame.Syllables.Ja, EnumsGame.Syllables.Je, EnumsGame.Syllables.Ji, EnumsGame.Syllables.Jo, EnumsGame.Syllables.Ju,
        EnumsGame.Syllables.La, EnumsGame.Syllables.Le, EnumsGame.Syllables.Li, EnumsGame.Syllables.Lo, EnumsGame.Syllables.Lu,
        EnumsGame.Syllables.Ma, EnumsGame.Syllables.Me, EnumsGame.Syllables.Mi, EnumsGame.Syllables.Mo, EnumsGame.Syllables.Mu,
        EnumsGame.Syllables.Na, EnumsGame.Syllables.Ne, EnumsGame.Syllables.Ni, EnumsGame.Syllables.No, EnumsGame.Syllables.Nu,
        EnumsGame.Syllables.Pa, EnumsGame.Syllables.Pe, EnumsGame.Syllables.Pi, EnumsGame.Syllables.Po, EnumsGame.Syllables.Pu,
        EnumsGame.Syllables.Ra, EnumsGame.Syllables.Re, EnumsGame.Syllables.Ri, EnumsGame.Syllables.Ro, EnumsGame.Syllables.Ru,
        EnumsGame.Syllables.Sa, EnumsGame.Syllables.Se, EnumsGame.Syllables.Si, EnumsGame.Syllables.So, EnumsGame.Syllables.Su,
        EnumsGame.Syllables.Ta, EnumsGame.Syllables.Te, EnumsGame.Syllables.Ti, EnumsGame.Syllables.To, EnumsGame.Syllables.Tu,
        EnumsGame.Syllables.Va, EnumsGame.Syllables.Ve, EnumsGame.Syllables.Vi, EnumsGame.Syllables.Vo, EnumsGame.Syllables.Vu,
        EnumsGame.Syllables.Xa, EnumsGame.Syllables.Xe, EnumsGame.Syllables.Xi, EnumsGame.Syllables.Xo, EnumsGame.Syllables.Xu,
        EnumsGame.Syllables.Za, EnumsGame.Syllables.Ze, EnumsGame.Syllables.Zi, EnumsGame.Syllables.Zo, EnumsGame.Syllables.Zu,

    };

    private static string[] syllables_str = {
        "Ba", "Be", "Bi", "Bo", "Bu", "Ca", "Co", "Cu", "Da", "De", "Di", "Do", "Du", "Fa", "Fe", "Fi", "Fo", "Fu",
        "Ga", "Ge", "Gi", "Go", "Gu", "Ha", "He", "Hi", "Ho", "Hu", "Ja", "Je", "Ji", "Jo", "Ju", "La", "Le", "Li", "Lo", "Lu",
        "Ma", "Me", "Mi", "Mo", "Mu","Na", "Ne", "Ni", "No", "Nu", "Pa", "Pe", "Pi", "Po", "Pu", "Ra", "Re", "Ri", "Ro", "Ru",
        "Sa", "Se", "Si", "So", "Su", "Ta", "Te", "Ti", "To", "Tu", "Va", "Ve", "Vi", "Vo", "Vu", "Xa", "Xe", "Xi", "Xo", "Xu",
        "Za", "Ze", "Zi", "Zo", "Zu"

    };

    public static void testSyllables() {
        for (int i = 0; i < syllables.Length; i++)
            Debug.Log(syllables[i]+"> "+syllables_str[i]);
    }
    
    //Function thats receive a enum LETER and returns the equivalente letter as string
    public static string enum2letter(EnumsGame.LETTERS letter) {

        //Vogais minúsculas / maiúsculas
        if (letter.Equals(EnumsGame.LETTERS.Am))
            return "a";
        else if (letter.Equals(EnumsGame.LETTERS.Em))
            return "e";
        else if (letter.Equals(EnumsGame.LETTERS.Im))
            return "i";
        else if (letter.Equals(EnumsGame.LETTERS.Om))
            return "o";
        else if (letter.Equals(EnumsGame.LETTERS.Um))
            return "u";
        else if (letter.Equals(EnumsGame.LETTERS.A))
            return "A";
        else if (letter.Equals(EnumsGame.LETTERS.E))
            return "E";
        else if (letter.Equals(EnumsGame.LETTERS.I))
            return "I";
        else if (letter.Equals(EnumsGame.LETTERS.O))
            return "O";
        else if (letter.Equals(EnumsGame.LETTERS.U))
            return "U";

        //Consoantes
        else if (letter.Equals(EnumsGame.LETTERS.B))
            return "B";
        else if (letter.Equals(EnumsGame.LETTERS.C))
            return "C";
        else if (letter.Equals(EnumsGame.LETTERS.D))
            return "D";
        else if (letter.Equals(EnumsGame.LETTERS.F))
            return "F";
        else if (letter.Equals(EnumsGame.LETTERS.G))
            return "G";
        else if (letter.Equals(EnumsGame.LETTERS.H))
            return "H";
        else if (letter.Equals(EnumsGame.LETTERS.J))
            return "J";
        else if (letter.Equals(EnumsGame.LETTERS.K))
            return "K";
        else if (letter.Equals(EnumsGame.LETTERS.L))
            return "L";
        else if (letter.Equals(EnumsGame.LETTERS.M))
            return "M";
        else if (letter.Equals(EnumsGame.LETTERS.N))
            return "N";
        else if (letter.Equals(EnumsGame.LETTERS.P))
            return "P";
        else if (letter.Equals(EnumsGame.LETTERS.Q))
            return "Q";
        else if (letter.Equals(EnumsGame.LETTERS.R))
            return "R";
        else if (letter.Equals(EnumsGame.LETTERS.S))
            return "S";
        else if (letter.Equals(EnumsGame.LETTERS.T))
            return "T";
        else if (letter.Equals(EnumsGame.LETTERS.V))
            return "V";
        else if (letter.Equals(EnumsGame.LETTERS.W))
            return "W";
        else if (letter.Equals(EnumsGame.LETTERS.X))
            return "X";
        else if (letter.Equals(EnumsGame.LETTERS.Y))
            return "Y";
        else if (letter.Equals(EnumsGame.LETTERS.Z))
            return "Z";

        return "";

    }

    public static EnumsGame.LETTERS letter2enum(string letter) {

        //Vogais minúsculas / maiúsculas
        if (letter.Equals("a"))
            return EnumsGame.LETTERS.Am;
        else if (letter.Equals("e"))
            return EnumsGame.LETTERS.Em;
        else if (letter.Equals("i"))
            return EnumsGame.LETTERS.Im;
        else if (letter.Equals("o"))
            return EnumsGame.LETTERS.Om;
        else if (letter.Equals("u"))
            return EnumsGame.LETTERS.Um;
        else if (letter.Equals("A"))
            return EnumsGame.LETTERS.A;
        else if (letter.Equals("E"))
            return EnumsGame.LETTERS.E;
        else if (letter.Equals("I"))
            return EnumsGame.LETTERS.I;
        else if (letter.Equals("O"))
            return EnumsGame.LETTERS.O;
        else if (letter.Equals("U"))
            return EnumsGame.LETTERS.U;

        //Consoantes maiúsculas
        else if (letter.Equals("B"))
            return EnumsGame.LETTERS.B;
        else if (letter.Equals("C"))
            return EnumsGame.LETTERS.C;
        else if (letter.Equals("D"))
            return EnumsGame.LETTERS.D;
        else if (letter.Equals("F"))
            return EnumsGame.LETTERS.F;
        else if (letter.Equals("G"))
            return EnumsGame.LETTERS.G;
        else if (letter.Equals("H"))
            return EnumsGame.LETTERS.H;
        else if (letter.Equals("J"))
            return EnumsGame.LETTERS.J;
        else if (letter.Equals("K"))
            return EnumsGame.LETTERS.K;
        else if (letter.Equals("L"))
            return EnumsGame.LETTERS.L;
        else if (letter.Equals("M"))
            return EnumsGame.LETTERS.M;
        else if (letter.Equals("N"))
            return EnumsGame.LETTERS.N;
        else if (letter.Equals("P"))
            return EnumsGame.LETTERS.P;
        else if (letter.Equals("Q"))
            return EnumsGame.LETTERS.Q;
        else if (letter.Equals("R"))
            return EnumsGame.LETTERS.R;
        else if (letter.Equals("S"))
            return EnumsGame.LETTERS.S;
        else if (letter.Equals("T"))
            return EnumsGame.LETTERS.T;
        else if (letter.Equals("V"))
            return EnumsGame.LETTERS.V;
        else if (letter.Equals("W"))
            return EnumsGame.LETTERS.W;
        else if (letter.Equals("X"))
            return EnumsGame.LETTERS.X;
        else if (letter.Equals("Y"))
            return EnumsGame.LETTERS.Y;
        else if (letter.Equals("Z"))
            return EnumsGame.LETTERS.Z;
        
        return EnumsGame.LETTERS.K;

    }

    /*Function to get all Lowercase Voewls*/
    public static List<EnumsGame.LETTERS> getLowercaseVowels() {
        List<EnumsGame.LETTERS> retorno = new List<EnumsGame.LETTERS>();
        retorno.Add(EnumsGame.LETTERS.Am);
        retorno.Add(EnumsGame.LETTERS.Em);
        retorno.Add(EnumsGame.LETTERS.Im);
        retorno.Add(EnumsGame.LETTERS.Om);
        retorno.Add(EnumsGame.LETTERS.Um);

        return retorno;
    }

    /*This function returns "quant" letters*/
    public static List<string> getLowerLetters(int quant) {
        List<string> retorno = new List<string>();
        List<string> allLetters = new List<string>();
        
        allLetters.Add("a"); allLetters.Add("e"); allLetters.Add("i"); allLetters.Add("m"); allLetters.Add("q");
        allLetters.Add("b"); allLetters.Add("f"); allLetters.Add("j"); allLetters.Add("n"); allLetters.Add("r");
        allLetters.Add("c"); allLetters.Add("g"); allLetters.Add("k"); allLetters.Add("o"); allLetters.Add("s");
        allLetters.Add("d"); allLetters.Add("h"); allLetters.Add("l"); allLetters.Add("p"); allLetters.Add("t");
        allLetters.Add("u"); allLetters.Add("w"); allLetters.Add("v"); allLetters.Add("x"); allLetters.Add("y");
        allLetters.Add("z");

        for(int i = 0; i < quant; i++) {
            int pos = Random.Range(0, allLetters.Count);
            retorno.Add(allLetters[pos]);
            allLetters.RemoveAt(pos);
        }

        return retorno;
    }

    /*This function returns "quant" letters*/
    public static List<string> getUpLetters(int quant) {
        List<string> retorno = new List<string>();
        List<string> allLetters = new List<string>();

        allLetters.Add("A"); allLetters.Add("E"); allLetters.Add("I"); allLetters.Add("M"); allLetters.Add("Q");
        allLetters.Add("B"); allLetters.Add("F"); allLetters.Add("J"); allLetters.Add("N"); allLetters.Add("R");
        allLetters.Add("C"); allLetters.Add("G"); allLetters.Add("K"); allLetters.Add("O"); allLetters.Add("S");
        allLetters.Add("D"); allLetters.Add("H"); allLetters.Add("L"); allLetters.Add("P"); allLetters.Add("T");

        allLetters.Add("U"); allLetters.Add("W"); allLetters.Add("V"); allLetters.Add("X"); allLetters.Add("Y");
        allLetters.Add("Z");

        for (int i = 0; i < quant; i++)
        {
            int pos = Random.Range(0, allLetters.Count);
            retorno.Add(allLetters[pos]);
            allLetters.RemoveAt(pos);
        }

        return retorno;
    }

    //Retorna um sprite para letra (seu nome em português).
    public static Sprite getImageByLeterPT(EnumsGame.LETTERS letter) {
        Sprite retorno = null;

        if (letter.Equals(EnumsGame.LETTERS.Am) || letter.Equals(EnumsGame.LETTERS.A)) {
            int val = Random.Range(0,5);

            var myAsset = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/boximages");

            if (myAsset == null)
                Debug.Log("Não carregou!");
            
            if (val == 0)
                retorno = myAsset.LoadAsset<Sprite>("abacate");
            else if (val == 1)
                retorno = myAsset.LoadAsset<Sprite>("abelha");
            else if (val == 2)
                retorno = myAsset.LoadAsset<Sprite>("anel");
            else if (val == 3)
                retorno = myAsset.LoadAsset<Sprite>("arvore");
            else if (val == 4)
                retorno = myAsset.LoadAsset<Sprite>("aviao");
            
            //retorno = myAsset.LoadAsset<Sprite>("aba");
        }

        return retorno;
    }
    
    public static string enum2SyllabeText(EnumsGame.Syllables syl) {
        string retorno = "";

        for (int i = 0; i < syllables.Length; i++)
            if (syllables[i].Equals(syl))
                retorno = syllables_str[i];

        return retorno;
    }

    public static EnumsGame.Syllables syllable2enum(string syl) {
        EnumsGame.Syllables retorno = 0;

        for (int i = 0; i < syllables.Length; i++)
            if (syllables_str[i].Equals(syl))
                retorno = syllables[i];

        return retorno;
    }

    public static List<EnumsGame.Syllables> getSomeSyllables(int quant) {
        List<EnumsGame.Syllables> retorno = new List<EnumsGame.Syllables>();

        List<int> values = new List<int>();
        for (int i = 0; i < syllables.Length; i++)
            values.Add(i);

        while (retorno.Count < quant && retorno.Count < syllables.Length) {
            int r = Random.Range(0, values.Count);
            retorno.Add(syllables[values[r]]);
            values.Remove(values[r]);
        }

        return retorno;
    }

    public static List<string> lettersToPpSyllable(int quant) {
        List<string> retono = new List<string>();
        List<string> retonar = new List<string>();
        
        retono.Add("M"); retono.Add("B"); retono.Add("F"); retono.Add("J");
        retono.Add("N"); retono.Add("R"); retono.Add("C"); retono.Add("G");
        retono.Add("S"); retono.Add("D"); retono.Add("H"); retono.Add("L");
        retono.Add("P"); retono.Add("T"); retono.Add("V"); retono.Add("X"); retono.Add("Z");

        if (quant > 17)
            return retono;

        for (int i = 0; i < quant; i++) {
            int rand = Random.Range(0, retono.Count);
            retonar.Add(retono[rand]);
            retono.Remove(retono[rand]);
        }

        return retonar;
    }

    public static string getSyllableByLetter(string letter) {
        string retorno = "";
        int rand = 0;

        if (!letter.Equals("C")) {
            rand = Random.Range(1, 6);
            if (rand == 1)
                retorno = letter + "a";
            else if (rand == 2)
                retorno = letter + "e";
            else if (rand == 3)
                retorno = letter + "i";
            else if (rand == 4)
                retorno = letter + "o";
            else if (rand == 5)
                retorno = letter + "u";
        } else {
            rand = Random.Range(1, 4);
            if (rand == 1)
                retorno = letter + "a";
            else if (rand == 2)
                retorno = letter + "o";
            else if (rand == 3)
                retorno = letter + "u";
        }

        return retorno;
    }

}
