using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{

    public Vector3 GetPlacePoint(Transform thingTransform,Vector3 point,out bool needToRotate)
    {
        needToRotate = false;
        float placePointX;
        float placePointZ;
        if (thingTransform.localScale.x <= transform.lossyScale.x && thingTransform.localScale.z <= transform.lossyScale.z || thingTransform.localScale.z <= transform.lossyScale.x && thingTransform.localScale.x <= transform.lossyScale.z)
        {
            if (point.x > transform.position.x + (transform.lossyScale.x/2 -thingTransform.localScale.x/2))
            {
                if (point.x > transform.position.x + (transform.lossyScale.x / 2 - thingTransform.localScale.z / 2) && thingTransform.localScale.z < thingTransform.localScale.x)
                {
                    placePointX = transform.position.x + (transform.lossyScale.x / 2 - thingTransform.localScale.z / 2);
                    needToRotate = true;
                }
                else
                {
                placePointX = transform.position.x + (transform.lossyScale.x / 2 - thingTransform.localScale.x / 2);
                }
            }
            else if (point.x < transform.position.x - (transform.lossyScale.x / 2 - thingTransform.localScale.x / 2))
            {
                if (point.x < transform.position.x - (transform.lossyScale.x / 2 - thingTransform.localScale.z / 2) && thingTransform.localScale.z < thingTransform.localScale.x)
                {
                    needToRotate = true;
                    placePointX = transform.position.x - (transform.lossyScale.x / 2 - thingTransform.localScale.z / 2);
                }
                else
                {
                    placePointX = transform.position.x - (transform.lossyScale.x / 2 - thingTransform.localScale.x / 2);
                }
            }
            else
            {
                placePointX = point.x;
            }
            if (point.z > transform.position.z + (transform.lossyScale.z / 2 - thingTransform.localScale.z / 2))
            {
                if (point.z > transform.position.z + (transform.lossyScale.z / 2 - thingTransform.localScale.x / 2) && thingTransform.localScale.x< thingTransform.localScale.z)
                {
                    needToRotate = true;
                    placePointZ = transform.position.z + (transform.lossyScale.z / 2 - thingTransform.localScale.x / 2);
                }
                else
                {
                    placePointZ = transform.position.z + (transform.lossyScale.z / 2 - thingTransform.localScale.z / 2);
                }
            }
            else if (point.z < transform.position.z - (transform.lossyScale.z / 2 - thingTransform.localScale.z / 2))
            {
                if (point.z < transform.position.z - (transform.lossyScale.z / 2 - thingTransform.localScale.x / 2) && thingTransform.localScale.x < thingTransform.localScale.z)
                {
                    needToRotate = true;
                    placePointZ = transform.position.z - (transform.lossyScale.z / 2 - thingTransform.localScale.x / 2);
                }
                else
                {
                    placePointZ = transform.position.z - (transform.lossyScale.z / 2 - thingTransform.localScale.z / 2);
                }
            }
            else
            {
                placePointZ = point.z;
            }
            point = new Vector3(placePointX, point.y, placePointZ);
            return point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
