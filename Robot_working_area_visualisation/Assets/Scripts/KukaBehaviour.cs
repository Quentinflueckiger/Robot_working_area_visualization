using UnityEngine;
using System.Collections;

public class KukaBehaviour : MonoBehaviour {

    private Animator _animator;

    private int randomIndex;

	// Use this for initialization
	void Start () {

        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        //Debug.Log(Random.Range(1, 13));
        if (Input.GetKey("space"))
        {
            randomIndex = Random.Range(0, 12);
            _animator.SetInteger("AnimParam", randomIndex);
        }
        
    }

    // Reset the Animator Parameter at the end of the animation
    public void AnimationEnded()
    {

        _animator.SetInteger("AnimParam", 0);
    }
}
