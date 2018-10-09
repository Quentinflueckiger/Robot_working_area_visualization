using UnityEngine;

public class KukaBehaviour : MonoBehaviour {

    private Animator _animator;
	// Use this for initialization
	void Start () {

        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("space"))
            _animator.SetTrigger("Grab_Left2");
	}
}
