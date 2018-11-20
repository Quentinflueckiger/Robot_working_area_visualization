using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour {

    public void SpeedUp() {
        if (Time.timeScale < 2f)
            Time.timeScale += 0.1f;
    }

    public void SpeedDown() {
        if (Time.timeScale > 0.1f)
            Time.timeScale -= 0.1f;
    }
}
