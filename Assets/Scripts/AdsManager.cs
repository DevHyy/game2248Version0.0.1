using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : Singleton<AdsManager> {
  BannerView _bannerView;
  private string _adBannerUnitId = "ca-app-pub-1931191948345927/7915247833";

  private InterstitialAd _interstitialAd;
  private string _adInterstitialUnitId = "ca-app-pub-1931191948345927/5887796391";


  private RewardedAd _rewardedAd;
  private string _adRewardUnitId = "ca-app-pub-3940256099942544/5224354917";

  public Text OdulText;
  public bool isBanneHide;
  public bool isInterstitialAdHide;
  void Start() {
    // Initialize the Google Mobile Ads SDK.
    MobileAds.Initialize((InitializationStatus initStatus) => {
      // This callback is called once the MobileAds SDK is initialized.
    });

    //LoadBannerAd();
    //LoadInterstitialAd();
    //LoadRewardedAd();
  }

  #region Banner

  public void CreateBannerView() {
    Debug.Log("Creating banner view");

    // If we already have a banner, destroy the old one.
    if (_bannerView != null) {
      DestroyBannerView();
    }

    // Create a 320x50 banner at top of the screen
    _bannerView = new BannerView(_adBannerUnitId, AdSize.SmartBanner, AdPosition.Bottom);
  }

  public void DestroyBannerView() {
    if (_bannerView != null) {
      Debug.Log("Destroying banner view.");
      _bannerView.Destroy();
      _bannerView = null;
    }
  }

  public void LoadBannerAd() {
    // create an instance of a banner view first.
    if (_bannerView == null) {
      CreateBannerView();
    }

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    Debug.Log("Loading banner ad.");
    _bannerView.LoadAd(adRequest);
  }

  public void HideBanner() {
    isBanneHide = !isBanneHide;
    _bannerView.Hide();
    if (isBanneHide) {
      _bannerView.Show();
    }
    else {
      _bannerView.Hide();
    }
  }

  private void ListenToAdEvents() {
    // Raised when an ad is loaded into the banner view.
    _bannerView.OnBannerAdLoaded += () => {
      Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
    };
    // Raised when an ad fails to load into the banner view.
    _bannerView.OnBannerAdLoadFailed += (LoadAdError error) => {
      Debug.LogError("Banner view failed to load an ad with error : "
                     + error);
    };
    // Raised when the ad is estimated to have earned money.
    _bannerView.OnAdPaid += (AdValue adValue) => {
      Debug.Log(String.Format("Banner view paid {0} {1}.",
        adValue.Value,
        adValue.CurrencyCode));
    };
    // Raised when an impression is recorded for an ad.
    _bannerView.OnAdImpressionRecorded += () => { Debug.Log("Banner view recorded an impression."); };
    // Raised when a click is recorded for an ad.
    _bannerView.OnAdClicked += () => { Debug.Log("Banner view was clicked."); };
    // Raised when an ad opened full screen content.
    _bannerView.OnAdFullScreenContentOpened += () => { Debug.Log("Banner view full screen content opened."); };
    // Raised when the ad closed full screen content.
    _bannerView.OnAdFullScreenContentClosed += () => { Debug.Log("Banner view full screen content closed."); };
  }

  #endregion

  #region InterstitialAd

  public void LoadInterstitialAd() {
    // Clean up the old ad before loading a new one.
    if (_interstitialAd != null) {
      _interstitialAd.Destroy();
      _interstitialAd = null;
    }

    Debug.Log("Loading the interstitial ad.");

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    InterstitialAd.Load(_adInterstitialUnitId, adRequest,
      (InterstitialAd ad, LoadAdError error) => {
        // if error is not null, the load request failed.
        if (error != null || ad == null) {
          Debug.LogError("interstitial ad failed to load an ad " +
                         "with error : " + error);
          return;
        }

        Debug.Log("Interstitial ad loaded with response : "
                  + ad.GetResponseInfo());

        _interstitialAd = ad;
      });
  }
  public void ShowInterstitialAd() {
    if (_interstitialAd != null && _interstitialAd.CanShowAd()) {
      Debug.Log("Showing interstitial ad.");
      _interstitialAd.Show();
      isInterstitialAdHide = true; // Set to true when the ad is shown
      RegisterReloadHandler(_interstitialAd);
    }
    else {
      Debug.LogError("Interstitial ad is not ready yet.");
      isInterstitialAdHide = false; // Set to false if there is no ad to show
    }
  }

  public void RegisterEventHandlers(InterstitialAd interstitialAd) {
    // Raised when the ad is estimated to have earned money.
    interstitialAd.OnAdPaid += (AdValue adValue) => {
      Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
        adValue.Value,
        adValue.CurrencyCode));
    };
    // Raised when an impression is recorded for an ad.
    interstitialAd.OnAdImpressionRecorded += () => { Debug.Log("Interstitial ad recorded an impression."); };
    // Raised when a click is recorded for an ad.
    interstitialAd.OnAdClicked += () => { Debug.Log("Interstitial ad was clicked."); };
    // Raised when an ad opened full screen content.
    interstitialAd.OnAdFullScreenContentOpened += () => { Debug.Log("Interstitial ad full screen content opened."); };
    // Raised when the ad closed full screen content.
    interstitialAd.OnAdFullScreenContentClosed += () => { Debug.Log("Interstitial ad full screen content closed."); };
    // Raised when the ad failed to open full screen content.
    interstitialAd.OnAdFullScreenContentFailed += (AdError error) => {
      Debug.LogError("Interstitial ad failed to open full screen content " +
                     "with error : " + error);
    };
  }

  public void RegisterReloadHandler(InterstitialAd interstitialAd) {
    // Raised when the ad closed full screen content.
    interstitialAd.OnAdFullScreenContentClosed += () => {
      Debug.Log("Interstitial Ad full screen content closed.");

      // Set to false when the ad is closed
      isInterstitialAdHide = false;

      // Reload the ad so that we can show another as soon as possible.
      LoadInterstitialAd();
    };
    // Raised when the ad failed to open full screen content.
    interstitialAd.OnAdFullScreenContentFailed += (AdError error) => {
      Debug.LogError("Interstitial ad failed to open full screen content " +
                     "with error : " + error);

      // Reload the ad so that we can show another as soon as possible.
      LoadInterstitialAd();
    };
  }
  
  public bool CheckisInterstitialAd() {
    LoadInterstitialAd();
    ShowInterstitialAd();
    if (AdsManager.Instance.isInterstitialAdHide == false) {
      return false;
    }
    else {
      return true;
    }
  }
  
  #endregion

  #region RewardedAd

  public void LoadRewardedAd() {
    // Clean up the old ad before loading a new one.
    if (_rewardedAd != null) {
      _rewardedAd.Destroy();
      _rewardedAd = null;
    }

    Debug.Log("Loading the rewarded ad.");

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    RewardedAd.Load(_adRewardUnitId, adRequest,
      (RewardedAd ad, LoadAdError error) => {
        // if error is not null, the load request failed.
        if (error != null || ad == null) {
          Debug.LogError("Rewarded ad failed to load an ad " +
                         "with error : " + error);
          return;
        }

        Debug.Log("Rewarded ad loaded with response : "
                  + ad.GetResponseInfo());

        _rewardedAd = ad;
      });
  }

  public void ShowRewardedAd() {
    const string rewardMsg =
      "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

    if (_rewardedAd != null && _rewardedAd.CanShowAd()) {
      _rewardedAd.Show((Reward reward) => {
        OdulText.text = "Watch the ad, congratulations.";
        Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
      });
      RegisterReloadHandler(_rewardedAd);
    }
  }

  private void RegisterReloadHandler(RewardedAd ad) {
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () => {
      Debug.Log("Rewarded Ad full screen content closed.");

      // Reload the ad so that we can show another as soon as possible.
      LoadRewardedAd();
    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) => {
      Debug.LogError("Rewarded ad failed to open full screen content " +
                     "with error : " + error);

      // Reload the ad so that we can show another as soon as possible.
      LoadRewardedAd();
    };
  }

  #endregion
  
}