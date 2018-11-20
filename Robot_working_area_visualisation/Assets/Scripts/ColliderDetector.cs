using UnityEngine;

public class ColliderDetector : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision with " + col.transform.name);
    }
}
