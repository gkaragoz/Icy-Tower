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

    public void LoadAndShowVideoAD() {
        _videoADs.LoadAndShowVideoAD();
    }

    public void LoadAndShowBannderAD() {
        _bannerADs.LoadAndShowBannerAd();
    }

    public void LoadAndShowRewardedVideoAD() {
        _rewardedADs.LoadAndShowRewardedVideoAd();
    }

}
