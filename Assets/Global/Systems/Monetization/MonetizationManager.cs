using UnityEngine;
using UnityEngine.Advertisements;

public class MonetizationManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidID;
    [SerializeField] private string adUnitID;
    [SerializeField] private bool testMode;

    public void Initialize()
    {
        Advertisement.Initialize(androidID, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Monetization Init Complete");
        LoadAd();

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Monetization Init Failed - {error} - {message}");
    }

    public void LoadAd()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(adUnitID, this);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsAdLoaded(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }
    public void OnUnityAdsShowStart(string placementId) { }
}
