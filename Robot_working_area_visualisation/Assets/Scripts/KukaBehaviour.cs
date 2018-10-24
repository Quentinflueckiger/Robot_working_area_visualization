using System;
using UnityEngine;
using UnityEngine.UI;

public class KukaBehaviour : MonoBehaviour {

    public Collider baseCollider;
    public Collider bicepsCollider;
    public Collider forearmCollider;
    public Collider handCollider;

    private Animator _animator;

    private int randomIndex;
    private Quaternion baseForearmRotation;
    private bool isInAnimation;
    private bool isInQueue;

    // Use this for initialization
    void Start () {

        isInAnimation = false;
        isInQueue = false;
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey("space"))
        {

            if (!isInAnimation && !isInQueue)
                AnimateRandomly();
        }
        DrawDebuggRay();

      /* else if (isInQueue)
        {
            // put the multiple animation here
        }*/
    }

    private void DrawDebuggRay()
    {
 
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

    public void PrepareSequenceOfRandomAnimation(Slider numberOfAnimationSlider)
    {
        if (!isInQueue)
        {
            isInQueue = true;
            int numberOfAnimation = (int)numberOfAnimationSlider.value;
            int[] animationQueue = new int[numberOfAnimation];

            Debug.Log("Input = " + numberOfAnimation);
            for (int i = 0; i < numberOfAnimation; i++)
            {
                animationQueue[i] = UnityEngine.Random.Range(0, 12);
            }

            MultipleAnimation(animationQueue);
        }
        
    }

    private void MultipleAnimation(int[] animationQueue)
    {
        for (int i = 0; i < animationQueue.Length; i++)
        {
            isInAnimation = true;
            Debug.Log("I'm in here !");
            _animator.SetInteger("AnimParam", animationQueue[i]);
        }
       
        isInQueue = false;
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

    public bool getIsInQueue()
    {
        return isInQueue;
    }
}
