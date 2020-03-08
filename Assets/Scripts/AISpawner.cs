using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the AI to spawn enemies based on the data passed into this class.
/// </summary>
public class AISpawner : MonoBehaviour
{

    public void InitializeLevelAIs (ThingToSpawn[] spawner)
    {
        spawnAll(spawner);
    }

    private void spawnAll(ThingToSpawn[] thingsToSpawn)
    {
        for(int i = 0; i < thingsToSpawn.Length;i++)
        {
            StartCoroutine(spawnInSeconds(thingsToSpawn[i]));
        }
    }

    IEnumerator spawnInSeconds (ThingToSpawn thingToSpawn)
    {
        yield return new WaitForSeconds(thingToSpawn.timer);
        spawnThing(thingToSpawn);
    }

    private void spawnThing (ThingToSpawn subject)
    {
        GameObject newObj = GameObject.Instantiate(subject.prefab,subject.position,new Quaternion(0,0,0,0));
        _Vampire spawnClass = newObj.GetComponent<_Vampire>();
        spawnClass.initialize(subject.waypoints,subject.dif);
    }

   
}


[System.Serializable]
 public class ThingToSpawn
    {
    public enum difficulty {
        easy,
        med,
        hard
    }


        public GameObject prefab;
        public Vector3 position;
        public Vector3[] waypoints;
        public float timer;
    public difficulty dif;

        public ThingToSpawn(GameObject _prefab,Vector3 _position, Vector3[] _waypoints,float _timer,difficulty difficulty)
        {
            prefab = _prefab;
            position = _position;
            waypoints = _waypoints;
            timer = _timer;
        dif = difficulty;
        }

    }