using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class controls how the light swings back and forth, could have used a better method with Itween or something, but given the time contstraints this had to do.
/// </summary>
public class SwingingFunctionality : MonoBehaviour
{
    [SerializeField] private float progress;

    [SerializeField] private float speed;

    [SerializeField] private Transform point1;

    [SerializeField] private Transform point2;

    [SerializeField] private Transform thingToMove;

    private bool increasing = true;

    void Update()
    {
        if (increasing) progress += Time.deltaTime * speed;
        else progress -= Time.deltaTime * speed;
        if (progress > 1 && increasing)
        {
            increasing = false;
        }

        if (progress < 0 && !increasing)
        {
            increasing = true;
        }

        updatePosition(progress);
    }

    void updatePosition (float _progress)
    {
        Quaternion newRot = Quaternion.Lerp(point1.rotation, point2.rotation, _progress);
        thingToMove.transform.rotation = newRot;
    }
}
