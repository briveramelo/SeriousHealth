using UnityEngine;
using System.Collections;

public enum Evaluation {
    Bad=0,
    OK=1,
    Great=2
}

public class SpeechEvaluator : MonoBehaviour {

    //public reference for other classes to access
    public static SpeechEvaluator Instance;
   
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

}
