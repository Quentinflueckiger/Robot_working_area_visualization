/**
*   Filename: KukaBehaviour.cs
*   Author: Flückiger Quentin
*   
*   Description:
*       This script handle the text change of a slider.
*   
**/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KukaBehaviour : MonoBehaviour {

    public Button animateButton;
    public Slider animateSlider;
    public Material rayCastMaterial;

    [Space(10)]
    [Header("Position Helper")]
    public GameObject basePositionHelper;

    public GameObject bicepsPositionHelperFirst;
    public GameObject bicepsPositionHelperSecond;

    public GameObject foreArmPositionHelperFirst;
    public GameObject foreArmPositionHelperSecond;
    public GameObject foreArmPositionHelperThird;

    public GameObject handPositionHelper;

    [Space(10)]
    [Header("Parameters")]
    public int vectorModifier = 10;

    private Animator _animator;

    private bool status = false;
    private int randomIndex;
    private bool isInAnimation;
    private bool isInQueue;
    private static Stack<int> randomQueue = new Stack<int>();
    private int frameCounter = 0;
    private GameObject[] positionHelper;
    private Vector3[] positionHelperOldArray;
    private int numberOfHelper = 7;
    private int totalNumberOfAnimation = 12;

    // Use this for initialization
    void Start () {

        isInAnimation = false;
        isInQueue = false;
        _animator = GetComponent<Animator>();
        positionHelper = new GameObject[numberOfHelper];
        positionHelperOldArray = new Vector3[numberOfHelper];
        AssignPositionHelperValue();
        AssignPositionHelperOldValue();
    }

    // Update is called once per frame
    void Update () {
        
        // Play a random animation on space pressed
        if (Input.GetKey("space") && !isInQueue)
        {

            if (!isInAnimation )
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


    }

    // LateUpdate is called once per frame, after all Update functions
    private void LateUpdate()
    {
        if (isInAnimation)
        {
            DrawDebuggRay();
        }

        if (frameCounter == 60)
            frameCounter = 1;
        else
            frameCounter++;
    }

    /** This method is used to prepare a stack filled with random int.
     *  These int will be used to play animation in a sequence.
     *  The amount of random number is based on the animationSlider's value.
     * 
     */
    public void PrepareSequenceOfRandomAnimation()
    {

        isInQueue = true;
        
        for (int i = 0; i < (int)animateSlider.value; i++)
        {
            randomQueue.Push(UnityEngine.Random.Range(0, totalNumberOfAnimation));
        }
        ButtonSliderChangeVisibility();

    }

    /** Set the trigger for the animator with a random int.
     * 
     */
    private void Animate()
    {
        isInAnimation = true;
        randomIndex = UnityEngine.Random.Range(0, totalNumberOfAnimation);
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
        _animator.speed = 1.0f;
        _animator.SetInteger("AnimParam", 0);
        isInAnimation = false;
        Debug.Log("OUT");
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

    /**
     * 
     */
    private void DrawDebuggRay()
    {

        for (int i = 0; i < numberOfHelper; i++)
        {
            Debug.DrawLine(positionHelper[i].transform.position,
                positionHelper[i].transform.position +
                (positionHelper[i].transform.position - positionHelperOldArray[i]) * vectorModifier,
                Color.red);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(positionHelper[i].transform.position,
                                       positionHelper[i].transform.position +
                                       (positionHelper[i].transform.position - positionHelperOldArray[i]) * vectorModifier,
                                      100.0f);

            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j].collider.gameObject.CompareTag("Box"))
                    hits[j].collider.GetComponent<Renderer>().material = rayCastMaterial;
            }
        }

        if (frameCounter % 3 == 0)
            AssignPositionHelperOldValue();
    }

    /**
     * 
     */
    private void AssignPositionHelperOldValue()
    {
        for (int i = 0; i < numberOfHelper; i++)
        {
            positionHelperOldArray[i] = positionHelper[i].transform.position;
        }
    }

    /** Initialise an array filled with the amount of game object used to gather information about the position and speed.
     * 
     */
    private void AssignPositionHelperValue()
    {
        positionHelper[0] = basePositionHelper;
        positionHelper[1] = bicepsPositionHelperFirst;
        positionHelper[2] = bicepsPositionHelperSecond;
        positionHelper[3] = foreArmPositionHelperFirst;
        positionHelper[4] = foreArmPositionHelperSecond;
        positionHelper[5] = foreArmPositionHelperThird;
        positionHelper[6] = handPositionHelper;
    }
}

/*      
            RaycastHit hit;
            Ray ray = new Ray(positionHelper[i].transform.position,
                    positionHelper[i].transform.position +
                    (positionHelper[i].transform.position - positionHelperOldArray[i]) * vectorModifier);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.isTrigger)
                {
                    hit.collider.GetComponent<Renderer>().material = rayCastMaterial;
                }
            }
 */