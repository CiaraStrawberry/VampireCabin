using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script provides spawn information for each section of the game, allowing for easy expansion of the content.
/// </summary>
public class LevelController : MonoBehaviour
{
    public GameObject[] allLocations;

    public Level[] allLevels;

    public Level currentLevel;

    [SerializeField] private GameObject vampirePrefab;

    [SerializeField] private GameObject[] SpawnPoints;

    [SerializeField] private Light[] levelLights;

    [SerializeField] private GameObject door;

    private void Start()
    {

        // the scene spawn data is saved like this instead of serialized since unity serialization has a habit of losing your data and i am not setting this up multiple times.
        // the data is serialized however so you can still see it ineditor, ingame.
        ThingToSpawn[] level1Spawner = new ThingToSpawn[3] {
            new ThingToSpawn(vampirePrefab,SpawnPoints[0].transform.position,new Vector3[1]{allLocations[0].transform.position },0,ThingToSpawn.difficulty.easy),
             new ThingToSpawn(vampirePrefab,SpawnPoints[1].transform.position,new Vector3[1]{allLocations[0].transform.position  },3,ThingToSpawn.difficulty.easy),
              new ThingToSpawn(vampirePrefab,SpawnPoints[2].transform.position,new Vector3[1]{allLocations[0].transform.position  },5,ThingToSpawn.difficulty.easy)
        };

        ThingToSpawn[] level2Spawner = new ThingToSpawn[4] {
            new ThingToSpawn(vampirePrefab,SpawnPoints[3].transform.position,new Vector3[1]{allLocations[1].transform.position  },0,ThingToSpawn.difficulty.med),
             new ThingToSpawn(vampirePrefab,SpawnPoints[4].transform.position,new Vector3[1]{allLocations[1].transform.position  },3,ThingToSpawn.difficulty.med),
              new ThingToSpawn(vampirePrefab,SpawnPoints[5].transform.position,new Vector3[1]{allLocations[1].transform.position  },5,ThingToSpawn.difficulty.med),
                new ThingToSpawn(vampirePrefab,SpawnPoints[6].transform.position,new Vector3[1]{allLocations[1].transform.position  },5,ThingToSpawn.difficulty.med)
        };

        allLevels = new Level[2] {
            new Level(0,3,allLocations[0],level1Spawner,levelLights[0]),
            new Level(1,4,allLocations[1],level2Spawner,null)
        };

        currentLevel = allLevels[0];
        Debug.Log(GlobalVariables.Instance);
        GlobalVariables.Instance.aiSpawner.InitializeLevelAIs(currentLevel.allCreeps);
    }

    public void incramentScore()
    {
        currentLevel.score++;  
        if (currentLevel.score == currentLevel.scoreToProgress)
        {
            if (allLevels.Length == currentLevel.name + 1)
            {
                Debug.Log("game won");
                GlobalVariables.Instance.sceneMan.win();
            }
            else
            {
                StartCoroutine(moveToNextLevel(allLevels[currentLevel.name + 1]));
            }
        }
    }

    // processes the various effects and scripts needed to move to the next part of the game.
    private IEnumerator moveToNextLevel(Level newLevel)
    {
      
        if (currentLevel.lighToTurnOn != null)
        {
            Hashtable ht3 = iTween.Hash("from", 5, "to", 15, "time", 4f, "onupdate", "setLightIntensity");
            iTween.ValueTo(gameObject, ht3);
        }
        yield return new WaitForSeconds(6f);
        GlobalVariables.Instance.weaponCon.addAmmo(4);
        Hashtable ht2 = iTween.Hash("from", 15, "to", 5, "time", 6f, "onupdate", "setLightIntensity");
        iTween.ValueTo(gameObject, ht2);

        currentLevel = newLevel;
        GlobalVariables.Instance.playerMovCon.movePlayerCharacterToPosition(currentLevel.location.transform.position);
        
        yield return new WaitForSeconds(4f);

        Hashtable ht = iTween.Hash("from", 0, "to", 1, "time", 3f, "onupdate", "setDoorPosition");
        iTween.ValueTo(gameObject, ht);

        GlobalVariables.Instance.aiSpawner.InitializeLevelAIs(currentLevel.allCreeps);


    }

    public void setLightIntensity(float val)
    {
        Debug.Log("light level: " + val);
        Level lastLevel = allLevels[0];
        lastLevel.lighToTurnOn.range = val;
    }

    public void setDoorPosition(float val)
    {
        door.transform.localPosition = Vector3.Lerp(new Vector3(-8, -1, -11), new Vector3(-8, -1, -6), val);
    }

    [System.Serializable]
    public class Level {
        public int name;
        public int score;
        public int scoreToProgress;
        public GameObject location;
        public ThingToSpawn[] allCreeps;

        public Light lighToTurnOn;

        public Level (int _name, int _scoreToProgress, GameObject _location,ThingToSpawn[] _allCreeps,Light _thingtoturn)
        {
            name = _name;
            score = 0;
            scoreToProgress = _scoreToProgress;
            location = _location;
            allCreeps = _allCreeps;
            lighToTurnOn = _thingtoturn;
        }
    }

}
