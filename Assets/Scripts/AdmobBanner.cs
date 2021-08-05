using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class AdmobBanner : MonoBehaviour
{
    [SerializeField] private string bannerIdAndroid;
    [SerializeField] private string bannerIdIphone;
    [SerializeField] private AdPosition bannerPosition;
    //[SerializeField] private Vector2 bannerSize;


    private BannerView bannerView;
    void Start()
    {
        requestBanner();
        DontDestroyOnLoad(this.gameObject);
    }

    //////////////////////////////////////////////////

    public void requestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = bannerIdAndroid;
#elif UNITY_IPHONE
            string adUnitId = bannerIdIphone;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, bannerPosition);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

}
