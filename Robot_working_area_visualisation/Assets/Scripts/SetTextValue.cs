using UnityEngine.UI;
using UnityEngine;

public class SetTextValue : MonoBehaviour {

    public Text sliderValue;
    public Slider slider;

    void Update()
    {

        sliderValue.text = slider.value.ToString("0");
    }
}
