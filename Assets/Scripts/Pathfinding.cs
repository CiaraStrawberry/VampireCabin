using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// moves the targetable enemies towords the player at a constant rate.
/// </summary>
public class Pathfinding : MonoBehaviour
{

    List<WayPoint> allpoints = new List<WayPoint>();

    public float speed = 30;

    private _Vampire baseClass;
    public void initialize(List<WayPoint> points,_Vampire _baseClass,ThingToSpawn.difficulty difficulty)
    {
        allpoints = points;
        baseClass = _baseClass;
        if (difficulty == ThingToSpawn.difficulty.easy) speed = 0.4f;
        if (difficulty == ThingToSpawn.difficulty.med) speed = 0.6f;
        if (difficulty == ThingToSpawn.difficulty.hard) speed = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseClass.dead) return;
        faceCenter();
        moveToTarget();
        checkDistance();
    }

    private void faceCenter()
    {
        Vector3 centerOfTheGame = GlobalVariables.Instance.center.transform.position;
        transform.LookAt(centerOfTheGame);
        Vector3 oldPos = transform.eulerAngles;
        oldPos.x = 0;
        transform.eulerAngles = oldPos;
    }

    private void moveToTarget()
    {
        WayPoint curPoint = getNextPoint();
        Vector3 newPos = Vector3.MoveTowards(transform.position,curPoint.target,Time.deltaTime * speed);
        transform.position = newPos;
    }

    private WayPoint getNextPoint ()
    {
        foreach(WayPoint point in allpoints)
        {
            if(!point.targetReached)
            {
                return point;
            }
        }
        return allpoints[allpoints.Count - 1];
    }

    private void checkDistance()
    {
        WayPoint curPoint = getNextPoint();
       // Debug.Log(Vector3.Distance(transform.position, curPoint.target));
        if(Vector3.Distance(transform.position, curPoint.target) < 2)
        {
            curPoint.targetReached = true;
            if(allPointsReached())
            {
                attackPlayer();
                die();
            }
        }
    }

    private bool allPointsReached ()
    {
        foreach (WayPoint point in allpoints)
        {
            if (!point.targetReached)
            {
                return false;
            }
        }
        return true;
    }

    private void attackPlayer ()
    {
        GlobalVariables.Instance.healthManager.loseHealth();
    }

    private void die ()
    {
        baseClass.die();
    }
}
