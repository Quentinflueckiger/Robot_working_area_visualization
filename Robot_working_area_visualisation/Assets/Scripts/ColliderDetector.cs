/**
*   Filename: ColliderDetector.cs
*   Author: Flückiger Quentin
*   
*   Description:
*       This script handle the text change of a slider.
*   
**/
using UnityEngine;

public class ColliderDetector : MonoBehaviour {

    public Material free;
    public Material occupied;

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
            other.GetComponent<Renderer>().material = occupied;
    }
                        
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
            other.GetComponent<Renderer>().material = free;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
            other.GetComponent<Renderer>().material = occupied;
    }

}
