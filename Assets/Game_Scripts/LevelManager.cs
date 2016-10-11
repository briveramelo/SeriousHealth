using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level {
    public Level(List<Round> i_Rounds) {
        rounds = i_Rounds;
    }
    public List<Round> rounds = new List<Round>();
}

public class Round {
    public Round(List<PhonemeScreenObj> i_ScreenPhonemeObjects, List<Wave> i_Waves) {
        screenPhonemeObjects = i_ScreenPhonemeObjects;
        waves = i_Waves;
    }
    public List<PhonemeScreenObj> screenPhonemeObjects = new List<PhonemeScreenObj>();
    public List<Wave> waves = new List<Wave>();
}


public enum Direction {
    Up=0,
    Down=1,
    Left=2,
    Right=3
}
public class Wave {
    public Wave(List<PhonemeScreenObj> i_HighlightedPhonemeObjects, Direction i_Direction) {
        highlightedPhonemeObjects = i_HighlightedPhonemeObjects;
        myDirection = i_Direction;
    }
    public List<PhonemeScreenObj> highlightedPhonemeObjects = new List<PhonemeScreenObj>();
    public Direction myDirection = 0;
}

public class PhonemeScreenObj {

    public PhonemeScreenObj(PhonemeObj i_PhonemeObject, TableDimension i_ScreenPosition) {
        myPhonemeObject = i_PhonemeObject;
        screenPosition = i_ScreenPosition;
    }

    public PhonemeObj myPhonemeObject;
    public TableDimension screenPosition;

}

public class LevelManager : MonoBehaviour {

    List<Level> levels;
    public static LevelManager Instance;

    // Use this for initialization
    void Awake () {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        GenerateLevels();
	}

    void GenerateLevels() {
        levels = new List<Level>();

        //CHUNK TO REPEAT
        List<Round> rounds = new List<Round>() {
            GenerateRound(new TableDimension(3,1), 4),
            GenerateRound(new TableDimension(3,3), 4),
            GenerateRound(new TableDimension(5,2), 10),
            GenerateRound(new TableDimension(3,3), 4),
            GenerateRound(new TableDimension(3,3), 40),
        };
        levels.Add(new Level(rounds));
        if (true) { }
        //KEEP ON GOING TO ADD NEW LEVELS
    }

    /// <summary>
    /// Generates Rounds!
    /// </summary>
    Round GenerateRound(TableDimension tableSize, int numWaves) {
        List<PhonemeScreenObj> screenPhonemeObjects = new List<PhonemeScreenObj>();
        List<Wave> waves = new List<Wave>();

        //Create All Phoneme Screen Objects For Round
        for (int row = 0; row < tableSize.row; row++) {
            for (int col = 0; col < tableSize.column; col++) {
                TableDimension td = new TableDimension(row, col);
                PhonemeObj phoObj = PhonemeBank.Instance.GetAvailablePhoneme();
                PhonemeScreenObj pho = new PhonemeScreenObj(phoObj, td);
                screenPhonemeObjects.Add(pho);                
            }
        }

        //Create all Waves using the Phoneme Screen Objects
        Direction lastMoveDirection = (Direction)Random.Range(0, 4);
        for (int i = 0; i < numWaves; i++) {            
            Direction moveDirection = GetNewDirection(lastMoveDirection);
            List<PhonemeScreenObj> phoObjs = new List<PhonemeScreenObj>();

            if (moveDirection == Direction.Up || moveDirection == Direction.Down) {
                for (int row = 0; row < tableSize.row; row++) {
                    int column = Random.Range(0, tableSize.column);
                    PhonemeScreenObj phoObj = screenPhonemeObjects.Find(pho => (pho.screenPosition.row == row && pho.screenPosition.column == column));
                    phoObjs.Add(phoObj);
                }
            }
            else {
                for (int column = 0; column < tableSize.column; column++) {
                    int row = Random.Range(0, tableSize.row);
                    PhonemeScreenObj phoObj = screenPhonemeObjects.Find(pho => (pho.screenPosition.row == row && pho.screenPosition.column == column));
                    phoObjs.Add(phoObj);
                }
            }

            Wave wave = new Wave(phoObjs, moveDirection);
            waves.Add(wave);
        }

        return new Round(screenPhonemeObjects, waves);
    }

    /// <summary>
    /// Ensures the next direction is never the same as the last
    /// </summary>
    Direction GetNewDirection(Direction oldDirection) {
        Direction newDirection = oldDirection;
        while (newDirection == oldDirection) {
            newDirection = (Direction)Random.Range(0, 4);
        }
        return newDirection;
    }
	
	void Update () {
	
	}
}
