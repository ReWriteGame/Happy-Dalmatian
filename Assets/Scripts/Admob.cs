using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Abmob", menuName = "ScriptableObjects/AdmobInter")]
public class Admob : ScriptableObject
{
    private InterstitialAd interstitial;

    [SerializeField] private string interstitialIdAndroid;
    [SerializeField] private string interstitialIdIphone;



    [Range(0, 20), SerializeField] private int staticDisplayFrequency = 5;
    [Range(0, 20), SerializeField] private int randomDisplayFrequency = 5;

    private int counterForDisplay = 0;


    #region AdMethods
    public void requestInterstitialAd()
    {
#if UNITY_ANDROID
        string adUnitId = interstitialIdAndroid;
#elif UNITY_IPHONE
        string adUnitId = interstitialIdIphone;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(interstitialIdAndroid);

        // Events
        interstitial.OnAdClosed += destroyInterAd;


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }


    private void destroyInterAd(object a, EventArgs args)
    {
        interstitial?.Destroy();
    }
    private void OnDestroy()
    {
        interstitial?.Destroy();
    }
    public void showInterAd()
    {
        requestInterstitialAd();
        if (interstitial.IsLoaded())
            interstitial.Show();
    }
    #endregion



    public void staticShowAd()
    {
        counterForDisplay++;
        if (counterForDisplay >= staticDisplayFrequency)
        {
            counterForDisplay = 0;
            showInterAd();
        }
    }
    public void randomShowAd()
    {
        if (UnityEngine.Random.Range(0, randomDisplayFrequency) == 0)
            showInterAd();
    }
}