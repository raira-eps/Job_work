using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scratchMouseDemo : MonoBehaviour
{
    public Camera camera;
    
    public void MoveToMouse()
    {
        float distance = transform.position.z - camera.transform.position.z;
        Vector3 targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        transform.position = camera.ScreenToWorldPoint(targetPos);
    }
}