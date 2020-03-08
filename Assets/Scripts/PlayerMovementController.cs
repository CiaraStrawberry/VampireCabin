using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Has the data needed to move the player character depending on input and their progress through the game.
/// </summary>
public class PlayerMovementController : MonoBehaviour {

    [SerializeField]
    private GameObject playerCharacter;

    [SerializeField]
    private GameObject PlayerAimingObject;

    private Vector3 currentMoveTarget;

    private Vector3 currentOrigin;

    public void movePlayerCharacterToPosition(Vector3 target)
    {
        currentMoveTarget = target;
        currentOrigin = playerCharacter.transform.position;
        Hashtable ht = iTween.Hash("from", 0, "to", 1, "time", 6f, "onupdate", "movePlayerCharacter");
        iTween.ValueTo(gameObject, ht);
    }

    private void movePlayerCharacter(float val)
    {
      //  Debug.Log(val);
        Vector3 newPos = Vector3.Lerp(currentOrigin, currentMoveTarget, val);
        newPos.y = 1.6f;
        playerCharacter.transform.position = newPos;

    }

    // Update is called once per frame
    void Update () {
        Vector3 thingPlayerLookingAt = getThingPlayerPointingAt();
        PlayerAimingObject.transform.LookAt(thingPlayerLookingAt);
	}


    private Vector3 getThingPlayerPointingAt ()
    {
        Ray mseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(mseRay, out hit))
        {
            return hit.point;
        }
        return new Vector3(0, 0, 0);
    }

    public void rotateUp()
    {
       // adjustPlayerRotation(1,0);
    }

    public void rotateDown ()
    {
      //  adjustPlayerRotation(-1, 0);
    }

    public void rotateLeft ()
    {
        adjustPlayerRotation(0,-1f);
    }

    public void rotateRight()
    {
        adjustPlayerRotation(0,1f);
    }

    public void adjustPlayerRotation (float x ,float y)
    {
     Vector3 oldPos = playerCharacter.transform.eulerAngles;
        oldPos.x += x;
        oldPos.y += y;
        playerCharacter.transform.eulerAngles = oldPos;
    }

}
