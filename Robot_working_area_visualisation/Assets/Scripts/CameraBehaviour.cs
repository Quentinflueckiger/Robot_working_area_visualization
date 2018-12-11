/// <summary>
/// Filename: CameraBehaviour.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///    This script let an object rotates around a target with the inputs "w,a,s,d".
/// </summary>
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject target;       // The target to rotate around

    public float maxUpCamera;       // The maximum position y of the camera
    public float maxDownCamera;     // The minimum position y of the camera

	// Update is called once per frame
	void Update () {

        if (Input.GetKey("w"))
        {
            if (transform.position.y < maxUpCamera)
                transform.position += new Vector3(0, .1f, 0);
        }
        else if (Input.GetKey("s"))
        {
            if (transform.position.y > maxDownCamera)
                transform.position += new Vector3(0, -.1f, 0);
        }
        else if (Input.GetKey("a"))
            transform.RotateAround(target.transform.position, Vector3.up, 2f);
        else if (Input.GetKey("d"))
            transform.RotateAround(target.transform.position, Vector3.down, 2f);

        transform.LookAt(target.transform);
    }
}
