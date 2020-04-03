using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for other associated functions, provides an easy way to call anything on the vampire prefab by only needing a referance to this class.
/// also handles miscellanious functionality for the vampires.
/// </summary>
public class _Vampire : MonoBehaviour
{
    [SerializeField] private Pathfinding pathComponent;
    // Start is called before the first frame update

    public bool dead;

    public ThingToSpawn.difficulty difficulty;

    public void initialize(Vector3[] waypoints,ThingToSpawn.difficulty dif)
    {
        List<WayPoint> waypointData = new List<WayPoint>();
        for (int i = 0; i < waypoints.Length;i++)
        {
            waypointData.Add(new WayPoint( waypoints[i]));
        }
        pathComponent.initialize(waypointData,this,dif);
    }

    // Update is called once per frame
    public void die()
    {
        GlobalVariables.Instance.levelCon.incramentScore();
        Destroy(this.gameObject);
        dead = true;

    }

    public void hit()
    {
        dead = true;
        GlobalVariables.Instance.levelCon.incramentScore();
        Hashtable ht = iTween.Hash("from", 0, "to", -90, "time", .25f, "onupdate", "rotateTo");
        iTween.ValueTo(gameObject, ht);

    }

    private bool calledYetDead = false;
    void rotateTo (float value)
    {
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = value;
        transform.eulerAngles = eulerAngles;
        if (value < -89 && calledYetDead == false)
        {
            calledYetDead = true;
            StartCoroutine(dieIn3Seconds());
        }
    }

    IEnumerator dieIn3Seconds ()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

}

  public class WayPoint {
        public Vector3 target;
        public bool targetReached = false;
        
        public WayPoint(Vector3 _target)
        {
            target = _target;
        }

    }
