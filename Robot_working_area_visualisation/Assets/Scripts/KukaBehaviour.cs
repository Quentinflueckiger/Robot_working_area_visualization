using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KukaBehaviour : MonoBehaviour {

    /*
    public Collider baseCollider;
    public Collider bicepsCollider;
    public Collider forearmCollider;
    public Collider handCollider;*/

    public Button animateButton;
    public Slider animateSlider;

    [Space(10)]
    [Header("Position Helper")]
    public GameObject basePositionHelper;

    public GameObject bicepsPositionHelperFirst;
    public GameObject bicepsPositionHelperSecond;

    public GameObject foreArmPositionHelperFirst;
    public GameObject foreArmPositionHelperSecond;
    public GameObject foreArmPositionHelperThird;

    public GameObject handPositionHelper;

    private Animator _animator;

    private bool status =false;
    private int randomIndex;
    private bool isInAnimation;
    private bool isInQueue;
    private static Stack<int> randomQueue = new Stack<int>();
    private int frameCounter = 0;

    private Vector3 foreArmPositionHelperSecondOld;

    // Use this for initialization
    void Start () {

        isInAnimation = false;
        isInQueue = false;
        _animator = GetComponent<Animator>();
        foreArmPositionHelperSecondOld = foreArmPositionHelperSecond.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        
        // Play a random animation on space pressed
        if (Input.GetKey("space"))
        {

            if (!isInAnimation && !isInQueue)
                Animate();
        }

        // If there are something in the stack execute this part
        else if (isInQueue)
        {
            // Test if we are already playing an animation
            // the second part is used to wait because I encounter some problem if I wasn't waiting between two animations
           if (!isInAnimation && frameCounter % 3 == 0)
           {
               
                if (randomQueue.Count == 0)
                {
                    isInQueue = false;
                    ButtonSliderChangeVisibility();
                }
                    
                else
                {
                    Animate(randomQueue.Pop()); 
                }
                    
            }
        }

        if (frameCounter == 60)
            frameCounter = 1;
        else
            frameCounter++;
    }

    private void LateUpdate()
    {
        if (isInAnimation)
        {
            DrawDebuggRay();
        }
    }

    /** This method is used to prepare a stack filled with random int.
     *  These int will be used to play animation in a sequence.
     *  The amount of random number is based on the animationSlider's value.
     * 
     */
    public void PrepareSequenceOfRandomAnimation()
    {

        isInQueue = true;
        int numberOfAnimation = (int)animateSlider.value;
        
        for (int i = 0; i < numberOfAnimation; i++)
        {
            randomQueue.Push(UnityEngine.Random.Range(0, 12));
        }
        ButtonSliderChangeVisibility();

    }

    /** Set the trigger for the animator with a random int.
     * 
     */
    private void Animate()
    {
        isInAnimation = true;
        randomIndex = UnityEngine.Random.Range(0, 12);
        _animator.SetInteger("AnimParam", randomIndex);
    }

    /** Set the trigger for the animator based on an input int.
     * 
     * @animationNumber the int used for the trigger
    */
    private void Animate(int animationNumber)
    {
        isInAnimation = true;
        _animator.SetInteger("AnimParam", animationNumber);
    }

    /** Reset the Animator Parameter at the end of the animation.
     * 
     */   
    public void AnimationEnded()
    {
        _animator.SetInteger("AnimParam", 0);
        isInAnimation = false;
    }

    /** Switch the slider and the button's visibility.
     * 
     */ 
    private void ButtonSliderChangeVisibility()
    {
        animateButton.gameObject.SetActive(status);
        animateSlider.gameObject.SetActive(status);
        status = !status ;
    }

    private void DrawDebuggRay()
    {

        Debug.Log("Vector diff " + (foreArmPositionHelperSecond.transform.position - foreArmPositionHelperSecondOld));
        Debug.DrawLine(foreArmPositionHelperSecond.transform.position, 
                foreArmPositionHelperSecond.transform.position + 
                (foreArmPositionHelperSecond.transform.position - foreArmPositionHelperSecondOld),
                Color.red);
        if (frameCounter % 2 == 0)
            foreArmPositionHelperSecondOld = foreArmPositionHelperSecond.transform.position;
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
}
