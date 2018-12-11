/// <summary>
/// Filename: ChangeScene.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///     This script handles the change between scenes.
/// </summary>
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public void GoToMainScene(){

        SceneManager.LoadScene(1);
    }
}
