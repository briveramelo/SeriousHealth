using UnityEngine;
using System.Collections.Generic;


public enum Phoneme {
    La=0,
    Di=1,
    Da=2
}

public class PhonemeBank : MonoBehaviour {

    //public reference for other classes to access
    public static PhonemeBank Instance;

    /// <summary>
    /// Collection of phonemes and their unlock status
    /// </summary>
    static Dictionary<Phoneme, bool> phonemeIsUnlocked = new Dictionary<Phoneme, bool>() {
        {Phoneme.La, true },
        {Phoneme.Di, true },
        {Phoneme.Da, true }
    };


    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Tells you if the phoneme in question is already unlocked
    /// </summary>
    public static bool IsPhonemeUnlocked(Phoneme phonemeInQuestion) {
        return phonemeIsUnlocked[phonemeInQuestion];
    }

    /// <summary>
    /// Checks if you should unlock a new phoneme, and if so, unlocks that phoneme
    /// </summary>
    public static void CheckToUnlockNewPhoneme() {
        if (true) {
            UnlockNewPhoneme(0);
        }
    }


    /// <summary>
    /// Unlocks the requested phoneme
    /// </summary>
    static void UnlockNewPhoneme(Phoneme phonemeToUnlock) {
        phonemeIsUnlocked[phonemeToUnlock] = true;
    }
}
