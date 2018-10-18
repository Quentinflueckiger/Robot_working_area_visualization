using System;
using UnityEngine;

public class KukaBehaviour : MonoBehaviour {

    private Animator _animator;

    private int randomIndex;
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
