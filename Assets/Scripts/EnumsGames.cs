using UnityEngine;
using System.Collections;

public class EnumsGame {
    //...

    public enum LETTERS {
        Am, Bm, Cm, Dm, Em, Fm, Gm, Hm, Im, Jm, Km, Lm, Mm, Nm, Om, Pm, Qm, Rm, Sm, Tm, Um, Vm, Wm, Xm, Ym, Zm, A, B, C, D, E, F, G, H, I, J, K, L,
        M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }

    public enum Syllables {
        //Por enquanto tem só as sílabas simples
        Ba, Be, Bi, Bo, Bu, Ca, Co, Cu, Da, De, Di, Do, Du, Fa, Fe, Fi, Fo, Fu, Ga, Ge, Gi, Go, Gu, Ha, He, Hi, Ho, Hu, Ja, Je, Ji, Jo, Ju,
        La, Le, Li, Lo, Lu, Ma, Me, Mi, Mo, Mu, Na, Ne, Ni, No, Nu, Pa, Pe, Pi, Po, Pu, Ra, Re, Ri, Ro, Ru, Sa, Se, Si, So, Su, Ta, Te, Ti, To, Tu,
        Va, Ve, Vi, Vo, Vu, Xa, Xe, Xi, Xo, Xu, Za, Ze, Zi, Zo, Zu
    }

    public enum TYPE_BOX { ONE_LETTER, TWO_LETTER, IMAGE_BOX, IMAGE_BOX_RAND_VOWEL }
    public enum TYPE_WALL { LOWER_VOWELS, MIX_VOWELS, ALL_LETTERS, DOUBLE_VOWELS, DOUBLE_LETTERS };

    public enum AUDIOS {P_JUMP, P_HITED, WIN_LEVEL, LOSE_LEVEL, P_HITS, COLLECT_COIN, COLLECT_CRYSTAL};

    public enum PROTECT {NORMAL, FIRE, POISON, DARK}

    public enum EFFECTS_SCRIPT { FADE_IN, FADE_OUT}

    //Enum UI
    public enum ANIMA_REMOVE_UI_ITEM {DROP, SCALE_UP, TO_RED}
    public enum MOVE_PLATFORM {ROTATE_X, ROTATE_Y, ROTATE_Z}
    public enum BULLET_TYPE {EXPLODE, EXPLODE_THROW, COLIDE_DESAPEAR, NORMAL}
    
}
