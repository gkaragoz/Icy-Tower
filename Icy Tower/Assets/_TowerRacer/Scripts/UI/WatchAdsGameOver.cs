using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdsGameOver : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverMenuPanel;



    [SerializeField]
    Slider _adsSlider;

    bool timerStart;
    float sliderValue = 0;


    private void Start()
    {
        StartTimerRevive();
    }


    public void StartTimerRevive()
    {
        sliderValue = 0;
        timerStart = true;
        StartCoroutine(SliderTimer());
    }

    IEnumerator SliderTimer()
    {
        while (timerStart)
        {
            sliderValue += Time.deltaTime/2;
            _adsSlider.value = sliderValue;
            if (sliderValue>=1)
            {

                transform.gameObject.SetActive(false);
                gameOverMenuPanel.SetActive(true);
                timerStart = false;
                    
            }
            yield return null;
        }
    }


}
