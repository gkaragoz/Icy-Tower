using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorfulGoldHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject _yellow;
    [SerializeField]
    private GameObject _red;
    [SerializeField]
    private GameObject _green;
    [SerializeField]
    private float _redRate;
    [SerializeField]
    private float _greenRate;

    /// <summary>
    /// rate counting works like that
    ///  0 ....... _redRate ..... _greenRate...........100
    ///    RedGold         GreenGold        YellowGold
    ///    
    /// Yo have to be careful about the rates. There is no rate correction
    /// </summary>
   
    private void Start()
    {
        _yellow.SetActive(false);
        _red.SetActive(false);
        _green.SetActive(false);

        _greenRate += _redRate;

        ShowColorfulGold();
    }

    public void ShowColorfulGold()
    {
        float rate = Random.Range(0, 100);
        if (rate<_redRate)
        {
            _red.SetActive(true);
            

        }
        else if (rate<_greenRate)
        {
            _green.SetActive(true);
            

        }
        else
        {
            _yellow.SetActive(true);
            

        }
    }

}
