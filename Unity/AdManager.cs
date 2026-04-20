using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
//Here we can add our AdUnits
    [Header("Ad Unit IDs")]
    [SerializeField] private string adUnitBanner;
    [SerializeField] private string adUnitInterstitial;

    BannerView bannerView;
    InterstitialAd interstitialAd;

    void Start()
    {
    // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
         // This callback is called once the MobileAds SDK is initialized.
        });
    }

    public void RequestBanner()
    {
        Debug.Log("Creating and loading banner view");

        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        bannerView = new BannerView(_adUnitBanner, AdSize.Banner, AdPosition.Bottom);
        var adRequest = new AdRequest();
        bannerView.LoadAd(adRequest);
        ListenToBannerAdEvents();
        bannerView.Show();
    }

    public void RequestInterstitial()
    {
        LoadInterstitialAd();
    }

    private void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        AdRequest adRequest = new AdRequest();
        InterstitialAd.Load(_adUnitInterstitial, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Failed to load interstitial ad: " + error.GetMessage());
                    return;
                }
                interstitialAd = ad;
                ListenToInterstitialAdEvents();
                Debug.Log("Interstitial ad loaded successfully.");
            });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready yet.");
        }
    }

    private void ListenToBannerAdEvents()
    {
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad.");
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad: " + error);
            RequestBanner();
        };
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Banner view paid {0} {1}.", adValue.Value, adValue.CurrencyCode));
        };
        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    private void ListenToInterstitialAdEvents()
    {
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad closed.");
            LoadInterstitialAd();
        };

        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Failed to show interstitial ad: " + error);
        };
    }
}
