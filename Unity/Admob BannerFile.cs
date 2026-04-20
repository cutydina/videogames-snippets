using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class Banner : MonoBehaviour
{
    [SerializeField] private string adUnitBanner;

  BannerView bannerView;

	public void Start()
	{
		 // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
        // This callback is called once the MobileAds SDK is initialized.
            RequestBanner();
        });
	}

	public void RequestBanner()
  {
      Debug.Log("Creating banner view");

      // If we already have a banner, destroy the old one.
      if (bannerView != null)
      {
          bannerView.Destroy();
      }

      // Create a 320x50 banner at bottom of the screen
      bannerView = new BannerView(adUnitBanner, AdSize.Banner, AdPosition.Bottom);

    var adRequest = new AdRequest();

    Debug.Log("Loading banner ad.");
    bannerView.LoadAd(adRequest);

  }

  private void ListenToAdEvents()
{
    // Raised when an ad is loaded into the banner view.
    bannerView.OnBannerAdLoaded += () =>
    {
        Debug.Log("Banner view loaded an ad with response : "
            + bannerView.GetResponseInfo());
    };
    // Raised when an ad fails to load into the banner view.
    bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
    {
        Debug.LogError("Banner view failed to load an ad with error : "
            + error);
            RequestBanner();
    };
    // Raised when the ad is estimated to have earned money.
    bannerView.OnAdPaid += (AdValue adValue) =>
    {
        Debug.Log(string.Format("Banner view paid {0} {1}.",
            adValue.Value,
            adValue.CurrencyCode));
    };
    // Raised when an impression is recorded for an ad.
    bannerView.OnAdImpressionRecorded += () =>
    {
        Debug.Log("Banner view recorded an impression.");
    };
    // Raised when a click is recorded for an ad.
    bannerView.OnAdClicked += () =>
    {
        Debug.Log("Banner view was clicked.");
    };
    // Raised when an ad opened full screen content.
    bannerView.OnAdFullScreenContentOpened += () =>
    {
        Debug.Log("Banner view full screen content opened.");
    };
    // Raised when the ad closed full screen content.
    bannerView.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("Banner view full screen content closed.");
    };
}


}
