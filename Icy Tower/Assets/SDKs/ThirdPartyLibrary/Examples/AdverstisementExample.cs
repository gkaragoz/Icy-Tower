using UnityEngine;
using Library.Advertisement.Admob;
using System;
using GoogleMobileAds.Api;
using Library.Advertisement.UnityAd;

public class AdverstisementExample : MonoBehaviour
{
    private VideoADs _videoADs;

    private BannerAD _bannerADs;

    private RewardedVideoAD _rewardedADs;

    void Awake()
    {
         _bannerADs = new BannerAD();

         _videoADs = new VideoADs();

        _rewardedADs = new RewardedVideoAD();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) // Interstitial
        {
            _videoADs.LoadAndShowVideoAD();
        }

        else if (Input.GetKey("s"))
        {
            _bannerADs.LoadAndShowBannerAd();
        }

        else if (Input.GetKey("d"))
        {
            _rewardedADs.LoadAndShowRewardedVideoAd();
        }
        
    }

}
