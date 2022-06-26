using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Shop : MonoBehaviour, IUnityAdsInitializationListener
{
    public int[] boxprices;
    public int[] coinprices;
    public int[] coinsrewards;
    public string myGameIdAndroid = "4524227";
    public string myGameIdIOS = "4524227";
    public string myVideoPlacement = "rewardedVideo";
    public string myAdStatus = "";
    public bool adStarted;
    public bool adCompleted;
    private bool testMode = true;
    ShowOptions options = new ShowOptions();
    private void Start()
    {
        //Advertisement.AddListener(this);
    }
    public void OnUnityAdsDidError(string message)
    {
        myAdStatus = message;
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    { 
        adCompleted = showResult == ShowResult.Finished;
        GameManager.instance.FAadd("A", 25);
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        adStarted = true;
    }
    public void OnUnityAdsReady(string placementId)
    {
        if(!adStarted)
        {
            Advertisement.Show(myVideoPlacement, options);
        }
    }
    public void BuyBox(int i)
    {
        if(GameManager.instance.gem >= boxprices[i])
        {
            GameManager.instance.FAadd("B", -boxprices[i]);
            Chests.instance.fromShop = true;
            Chests.instance.OpenChest(i);
        }
    }
    public void BuyCoins(int i)
    {
        if(GameManager.instance.gem >= coinprices[i])
        {
            GameManager.instance.FAadd("B", -coinprices[i]);
            GameManager.instance.FAadd("A", coinsrewards[i]);
        }
    }
    public void ShowRewarded()
    {
        #if UNITY_IOS
            Advertisement.Initialize(myGameIdIOS, testMode);
        #else
            Advertisement.Initialize(myGameIdAndroid, testMode);
        #endif
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
