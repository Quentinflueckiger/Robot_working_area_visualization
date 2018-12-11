/// <summary>
/// Filename: SetTextValue.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///     This script handle the text change of a slider.
/// </summary>

using UnityEngine.UI;
using UnityEngine;

public class SetTextValue : MonoBehaviour {

    public Text sliderValue;
    public Slider slider;
    public string additionalText;

    // Update is called once per frame
    void Update()
    {
        // Set the text for the slider header
        sliderValue.text = additionalText + slider.value.ToString("0");
    }
}
