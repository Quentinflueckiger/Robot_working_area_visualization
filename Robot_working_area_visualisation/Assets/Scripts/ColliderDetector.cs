/// <summary>
/// Filename: ColliderDetector.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///     This script handle the text change of a slider.
/// </summary>
using UnityEngine;

public class ColliderDetector : MonoBehaviour {

    public Material freeMaterial;
    public Material occupiedMaterial;

   /// <summary>
   /// Change the material of a gameobject if it's tag is "Box" and the robot enter in collision with it.
   /// </summary>
   /// <param name="other">The object it collides with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
            other.GetComponent<Renderer>().material = occupiedMaterial;
    }

    /// <summary>
    /// Change the material of a gameobject if it's tag is "Box" and the robot exit a collision with it.
    /// </summary>
    /// <param name="other">The object it collides with.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
            other.GetComponent<Renderer>().material = freeMaterial;
    }

    /// <summary>
    /// Change the material of a gameobject if it's tag is "Box" and the robot stays in contact with it.
    /// </summary>
    /// <param name="other">The object it collides with.</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
            other.GetComponent<Renderer>().material = occupiedMaterial;
    }

}
