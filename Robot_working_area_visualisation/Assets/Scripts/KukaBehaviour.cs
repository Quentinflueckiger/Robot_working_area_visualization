using System;
using UnityEngine;

public class KukaBehaviour : MonoBehaviour {

    public Collider baseCollider;
    public Collider bicepsCollider;
    public Collider forearmCollider;
    public Collider handCollider;
    public Rigidbody forearmRigidbody;

    private Animator _animator;

    private int randomIndex;
    private Quaternion baseForearmRotation;
    private bool isInAnimation;

	// Use this for initialization
	void Start () {

        isInAnimation = false;
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey("space"))
        {
            if (!isInAnimation)
                AnimateRandomly();
        }
        DrawDebuggRay();
    }

    private void DrawDebuggRay()
    {

        // Have to find a way to add velocity via the animation or so
        Debug.Log("Velocity " + forearmRigidbody.velocity);
        /*
        Debug.DrawLine(baseCollider.transform.position + (Vector3.up * 0.1f),
                baseCollider.transform.position + (Vector3.up * 0.1f) +
                (Vector3.down * 1), Color.red);

        Debug.DrawLine(bicepsCollider.transform.position + (Vector3.up * 0.1f),
                bicepsCollider.transform.position + (Vector3.up * 0.1f) +
                (Vector3.down * 1), Color.red);
        Debug.DrawRay(forearmCollider.transform.position, forearmCollider.transform.forward);

       Debug.DrawLine(forearmCollider.transform.position + (Vector3.up * 0.1f),
                forearmCollider.transform.position + (Vector3.up * 0.1f) +
                (Vector3.down * 1), Color.red);
        
        Debug.DrawLine(handCollider.transform.position + (Vector3.up * 0.1f),
                handCollider.transform.position + (Vector3.up * 0.1f) +
                (Vector3.down * 1), Color.red);*/
    }

    private void AnimateRandomly()
    {
        isInAnimation = true;
        randomIndex = UnityEngine.Random.Range(0, 12);
        _animator.SetInteger("AnimParam", randomIndex);
    }

    // Reset the Animator Parameter at the end of the animation
    public void AnimationEnded()
    {
        _animator.SetInteger("AnimParam", 0);
        isInAnimation = false;
    }
}
