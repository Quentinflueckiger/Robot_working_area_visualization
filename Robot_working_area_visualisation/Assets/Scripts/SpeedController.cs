/// <summary>
/// Filename: SpeedController.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///     This script handles the speed of animations.
/// </summary>
using UnityEngine;

public class SpeedController : MonoBehaviour {

    /// <summary>
    /// Makes the in game time goes faster.
    /// </summary>
    public void SpeedUp() {
        if (Time.timeScale < 2f)
            Time.timeScale += 0.1f;
    }

    /// <summary>
    /// Makes the in game time goes slower.
    /// </summary>
    public void SpeedDown() {
        if (Time.timeScale > 0.1f)
            Time.timeScale -= 0.1f;
    }
}
