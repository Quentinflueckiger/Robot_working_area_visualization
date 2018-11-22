/**
*   Filename: SpeedController.cs
*   Author: Flückiger Quentin
*   
*   Description:
*       This script handles the speed of animations.
*   
**/
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
