/// <summary>
/// Filename: KukaBehaviour.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///     This script handles the animation of the robot and listen to keyboard input.
/// </summary>
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KukaBehaviour : MonoBehaviour {

    public Button animateButton;
    public Slider animateSlider;

    private Animator _animator;
    private bool status = false;
    private int randomIndex;
    private bool isInAnimation;
    private bool isInQueue;
    private static Stack<int> randomQueue = new Stack<int>();
    private int frameCounter = 0;
    private int totalNumberOfAnimation = 12;
    private PredictionBehaviour predictionBehaviour;
    private bool predict = true;

    // Use this for initialization
    void Start () {

        isInAnimation = false;
        isInQueue = false;
        _animator = GetComponent<Animator>();
        predictionBehaviour = GetComponent<PredictionBehaviour>();
    }

    // Update is called once per frame
    void Update () {

        // Play a random animation on space pressed
        if (Input.GetKey("space") && !isInQueue)
        {

            if (!isInAnimation)
                Animate();
        }

        // Reload the scene, to avoid geting stuck
        else if (Input.GetKey("r"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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

    // LateUpdate is called once per frame, after all Update functions.
    private void LateUpdate()
    {
        // Checks if the robot is in animation and calls the debug ray accordingly.
        if (isInAnimation && predict)
        {
            predictionBehaviour.DrawDebugRay();
        }

        if (frameCounter == 60)
            frameCounter = 1;
        else
            frameCounter++;
    }

    /// <summary>
    /// This method is used to prepare a stack filled with random int.
    /// These int will be used to play animation in a sequence.
    /// The amount of random number is based on the animationSlider's value.
    /// </summary>
    public void PrepareSequenceOfRandomAnimation()
    {

        isInQueue = true;
        
        for (int i = 0; i < (int)animateSlider.value; i++)
        {
            randomQueue.Push(UnityEngine.Random.Range(0, totalNumberOfAnimation));
        }
        ButtonSliderChangeVisibility();

    }

    // Set the trigger for the animator with a random int.
    private void Animate()
    {
        isInAnimation = true;
        randomIndex = UnityEngine.Random.Range(0, totalNumberOfAnimation);
        _animator.SetInteger("AnimParam", randomIndex);
    }

    /// <summary>
    /// Set the trigger for the animator based on an input int.
    /// </summary>
    /// <param name="animationNumber">the int used for the trigger</param>
    private void Animate(int animationNumber)
    {
        isInAnimation = true;
        _animator.SetInteger("AnimParam", animationNumber);
    }

    // Reset the Animator Parameter at the end of the animation.
    public void AnimationEnded()
    {
        _animator.speed = 1.0f;
        _animator.SetInteger("AnimParam", 0);
        isInAnimation = false;
        Debug.Log("OUT");
    }

    // Switch the slider and the button's visibility.
    private void ButtonSliderChangeVisibility()
    {
        animateButton.gameObject.SetActive(status);
        animateSlider.gameObject.SetActive(status);
        status = !status ;
    }

    // Switch boolean predict, which is used to notify if one need to predict the movements.
    public void SwitchPredict()
    {
        predict = !predict;
    }
}