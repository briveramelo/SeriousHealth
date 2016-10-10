using UnityEngine;
using System.Collections.Generic;


public enum Phoneme {
    F = 0,
    M = 1,
    N = 2,
    S = 3,
    V = 4,
    SH = 5,
    A = 6,
    E = 7,
    I = 8,
    O = 9,
    U = 10,
    A_long = 11,
    E_long = 12,
    I_long = 13,
    O_long = 14,
    U_long = 15,
    B = 16,
    D = 17,
    G = 18,
    J = 19,
    K = 20,
    P = 21,
    T = 22,
    Y = 23,
    Z = 24,
    CH = 25,
    OW = 26,
    OY = 27,
    OO = 28,
    OO_moon = 29,
    H = 30,
    L = 31,
    R = 32,
    AR = 33,
    AR_chair = 34,
    IR_mirror = 35,
    OR = 36,
    UR = 37,
    W = 38,
    TH_thing = 39,
    TH_this = 40,
    NG = 41,
    ZH_garage = 42,
    WH_withbreath = 43
}

public struct TableDimension {
    public TableDimension(int i_Row, int i_Column) {
        row = i_Row;
        column = i_Column;
    }

    public int row, column;
}

public class PhonemeObj {

    public PhonemeObj(Phoneme i_Phoneme, string i_MyWatsonText, bool i_IsUnlocked) {
        myWatsonText = i_MyWatsonText;
        isUnlocked = i_IsUnlocked;
        myPhoneme = i_Phoneme;
    }
    public PhonemeObj() {}
    public Phoneme myPhoneme =0;
    public string myWatsonText ="";
    public bool isUnlocked = false;
    public bool isInUse =false;
}

public class PhonemeBank : MonoBehaviour {

    //public reference for other classes to access
    public static PhonemeBank Instance;

    /// <summary>
    /// Collection of phonemes, their watson text, and their unlock status
    /// </summary>
    Dictionary<Phoneme, PhonemeObj> allPhonemes = new Dictionary<Phoneme, PhonemeObj>() {
        {Phoneme.F, new PhonemeObj(Phoneme.F, "f", true) },
        {Phoneme.M, new PhonemeObj(Phoneme.M,"m", true) },
        {Phoneme.N, new PhonemeObj(Phoneme.N, "n", true) },
        {Phoneme.S, new PhonemeObj(Phoneme.S, "s", true) },
        {Phoneme.V, new PhonemeObj(Phoneme.V,"v", true) },
        {Phoneme.SH, new PhonemeObj(Phoneme.SH, "sh", true) },
        {Phoneme.A, new PhonemeObj(Phoneme.A, "a", true) },
        {Phoneme.E, new PhonemeObj(Phoneme.E,"e", true) },
        {Phoneme.I, new PhonemeObj(Phoneme.I, "i", true) },
        {Phoneme.O, new PhonemeObj(Phoneme.O, "o", true) },
        {Phoneme.U, new PhonemeObj(Phoneme.U,"u", true) },
        {Phoneme.A_long, new PhonemeObj(Phoneme.A_long, "aye", true) },
        {Phoneme.E_long, new PhonemeObj(Phoneme.E_long, "e", true) },
        {Phoneme.I_long, new PhonemeObj(Phoneme.I_long,"eye", true) },
        {Phoneme.O_long, new PhonemeObj(Phoneme.O_long, "o", true) },
        {Phoneme.U_long, new PhonemeObj(Phoneme.U_long, "you", true) },
        {Phoneme.B, new PhonemeObj(Phoneme.B,"b", true) },
        {Phoneme.D, new PhonemeObj(Phoneme.D, "d", true) },
        {Phoneme.G, new PhonemeObj(Phoneme.G, "g", true) },
        {Phoneme.J, new PhonemeObj(Phoneme.J,"j", true) },
        {Phoneme.K, new PhonemeObj(Phoneme.K, "k", true) },
        {Phoneme.P, new PhonemeObj(Phoneme.P, "p", true) },
        {Phoneme.T, new PhonemeObj(Phoneme.T,"t", true) },
        {Phoneme.Y, new PhonemeObj(Phoneme.Y, "y", true) },
        {Phoneme.Z, new PhonemeObj(Phoneme.Z, "z", true) },
        {Phoneme.CH, new PhonemeObj(Phoneme.CH,"ch", true) },
        {Phoneme.OW, new PhonemeObj(Phoneme.OW, "ow", true) },
        {Phoneme.OY, new PhonemeObj(Phoneme.OY, "oy", true) },
        {Phoneme.OO, new PhonemeObj(Phoneme.OO,"oo", true) },
        {Phoneme.OO_moon, new PhonemeObj(Phoneme.OO_moon, "oo", true) },
        {Phoneme.H, new PhonemeObj(Phoneme.H, "h", true) },
        {Phoneme.L, new PhonemeObj(Phoneme.L,"l", true) },
        {Phoneme.R, new PhonemeObj(Phoneme.R, "r", true) },
        {Phoneme.AR, new PhonemeObj(Phoneme.AR, "ar", true) },
        {Phoneme.AR_chair, new PhonemeObj(Phoneme.AR_chair,"air", true) },
        {Phoneme.IR_mirror, new PhonemeObj(Phoneme.IR_mirror, "ear", true) },
        {Phoneme.OR, new PhonemeObj(Phoneme.OR, "or", true) },
        {Phoneme.UR, new PhonemeObj(Phoneme.UR,"er", true) },
        {Phoneme.W, new PhonemeObj(Phoneme.W, "w", true) },
        {Phoneme.TH_thing, new PhonemeObj(Phoneme.TH_thing, "th", true) },
        {Phoneme.TH_this, new PhonemeObj(Phoneme.TH_this,"th", true) },
        {Phoneme.NG, new PhonemeObj(Phoneme.NG, "ng", true) },
        {Phoneme.ZH_garage, new PhonemeObj(Phoneme.ZH_garage, "zhe", true) },
        {Phoneme.WH_withbreath, new PhonemeObj(Phoneme.WH_withbreath,"wh", true) }
    };    

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        List<string> phonemes = new List<string>();
        foreach (KeyValuePair<Phoneme, PhonemeObj> phoDictItem in allPhonemes) {
            phonemes.Add(allPhonemes[phoDictItem.Key].myWatsonText);
        }
        SpeechEvaluator.Instance.UpdateKeywords(phonemes);
    }

    public PhonemeObj GetAvailablePhoneme() {
        foreach (KeyValuePair<Phoneme, PhonemeObj> phoDicItem in allPhonemes) {
            if (phoDicItem.Value.isUnlocked && !phoDicItem.Value.isInUse) {
                SetPhonemeInUse(phoDicItem.Value.myPhoneme);
                return phoDicItem.Value;
            }
        }
        return null;
    }

    /// <summary>
    /// Tells you if the phoneme in question is already in use
    /// </summary>
    public bool IsPhonemeInUse(Phoneme phonemeInQuestion) {
        return allPhonemes[phonemeInQuestion].isInUse;
    }

    /// <summary>
    /// Tells you if the phoneme in question is already in use
    /// </summary>
    public void SetPhonemeInUse(Phoneme phonemeInQuestion) {
        allPhonemes[phonemeInQuestion].isInUse = true;
    }

    /// <summary>
    /// Tells you if the phoneme in question is already unlocked
    /// </summary>
    public bool IsPhonemeUnlocked(Phoneme phonemeInQuestion) {
        return allPhonemes[phonemeInQuestion].isUnlocked;
    }

    /// <summary>
    /// Checks if you should unlock a new phoneme, and if so, unlocks that phoneme
    /// </summary>
    public void CheckToUnlockNewPhoneme(Phoneme phonemeToUnlock) {
        //To Do establish logic for checking
        // if you should unlock this or not
        if (true) {
            UnlockNewPhoneme(phonemeToUnlock);
        }
    }

    /// <summary>
    /// Unlocks the requested phoneme
    /// </summary>
    private void UnlockNewPhoneme(Phoneme phonemeToUnlock) {
        allPhonemes[phonemeToUnlock].isUnlocked= true;
    }
}
