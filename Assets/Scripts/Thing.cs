using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    private Vector3 lastPoint;
    Transform topThing;
    bool _placedOnThing;
    private void Start()
    {
        lastPoint = transform.position;
    }
    public void Place()
    {
        if (_placedOnThing)
        {
            transform.parent = topThing;
            topThing = null;
        }
    }
    public void TryToPlaceOnThing(Thing thing)
    {
        var upperThings = thing.transform.GetComponentsInChildren<Thing>();
        transform.position = GetUpperPosition(upperThings[upperThings.Length - 1].transform);
        topThing = upperThings[upperThings.Length - 1].transform;
        _placedOnThing = true;
    }
    private Vector3 GetUpperPosition(Transform targetTransform)
    {
        return new Vector3(targetTransform.position.x,targetTransform.position.y + targetTransform.lossyScale.y/2 + transform.lossyScale.y / 2,targetTransform.position.z);
    }
    public bool TryToPlaceOnSurface(Surface surface,Vector3 point)
    {
        _placedOnThing = false;
        var placePoint = surface.GetPlacePoint(transform, point,out bool needToRotate);
        if (placePoint != Vector3.zero)
        {
            transform.position = placePoint + new Vector3(0, transform.lossyScale.y/2,0);
            if (needToRotate)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            lastPoint = transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetLastPoint()
    {
        _placedOnThing = false;
         transform.position = lastPoint;
    }
}
