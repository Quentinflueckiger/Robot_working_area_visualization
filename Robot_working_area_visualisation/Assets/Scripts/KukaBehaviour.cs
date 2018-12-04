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
    public List<GameObject> listPositionHelper = new List<GameObject>(7);
    
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
    private Vector3[] positionHelperOldArray;
    private int numberOfHelper;
    private int totalNumberOfAnimation = 12;

    // Use this for initialization
    void Start () {

        isInAnimation = false;
        isInQueue = false;
        _animator = GetComponent<Animator>();
        numberOfHelper = listPositionHelper.Count;
        positionHelperOldArray = new Vector3[numberOfHelper];
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
            Debug.DrawLine(listPositionHelper[i].transform.position,
                listPositionHelper[i].transform.position +
                (listPositionHelper[i].transform.position - positionHelperOldArray[i]) * vectorModifier,
                Color.red);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(listPositionHelper[i].transform.position,
                                       listPositionHelper[i].transform.position +
                                       (listPositionHelper[i].transform.position - positionHelperOldArray[i]) * vectorModifier,
                                      100.0f);

            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j].collider.gameObject.CompareTag("Box"))
                    hits[j].collider.GetComponent<Renderer>().material = rayCastMaterial;
            }
        }

        //if (frameCounter % 3 == 0)
        AssignPositionHelperOldValue();
    }

    /**
     * 
     */
    private void AssignPositionHelperOldValue()
    {
        for (int i = 0; i < numberOfHelper; i++)
        {
            positionHelperOldArray[i] = listPositionHelper[i].transform.position;
        }
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