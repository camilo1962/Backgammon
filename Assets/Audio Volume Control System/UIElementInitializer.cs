using UnityEngine;
using UnityEngine.UI;

public class UIElementInitializer : MonoBehaviour
{
    public enum UIElementType { 
       SFX_Slider,
       SFX_Slider1,
        SFX_Slider2,
        SFX_Slider3,
        MUSIC_Slider
    }

    public UIElementType type;

    Slider slider;
    Slider slider2;
    Slider slider3;
    Slider slider4;

    private void Start()
    {
        switch (type)
        {
            case UIElementType.SFX_Slider:
                slider = GetComponent<Slider>();
                slider.value = AudioSettings.audioSettings.GetSFXVolume();
                break;
            case UIElementType.MUSIC_Slider:
                slider = GetComponent<Slider>();
                slider.value = AudioSettings.audioSettings.GetMusicVolume();
                break;
            case UIElementType.SFX_Slider1:
                slider2 = GetComponent<Slider>();
                slider2.value = AudioSettings.audioSettings.GetSFXVolume();
                break;
            case UIElementType.SFX_Slider2:
                slider3 = GetComponent<Slider>();
                slider3.value = AudioSettings.audioSettings.GetSFXVolume();
                break;
            case UIElementType.SFX_Slider3:
                slider4 = GetComponent<Slider>();
                slider4.value = AudioSettings.audioSettings.GetSFXVolume();
                break;

        }

    }

}
