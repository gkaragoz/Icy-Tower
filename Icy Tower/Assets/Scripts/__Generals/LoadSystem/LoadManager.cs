using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

    public Action<string> OnPlayFabAuthenticationSuccess;   //1
    public Action<string> OnPlayFabAuthenticationFailed;    //1
    public Action<string> OnGPGSAuthenticationSuccess;      //2
    public Action<string> OnGPGSAuthenticationFailed;       //2

    public Action OnAccountLoaded;                          //3
    public Action OnPoolLoaded;                             //4
    public Action OnSceneReady;                             //5

    private AsyncOperation _asyncOperation;

    public void AuthanticateToPlayFab() {
        AuthenticationManager.instance.AuthenticateToPlayFab(
            (actionResult, actionMessage) => {
                if (actionResult) {
                    OnPlayFabAuthenticationSuccess.Invoke(actionMessage);
                } else {
                    OnPlayFabAuthenticationFailed?.Invoke(actionMessage);
                }
            });
    }

    public void AuthanticateToGPGS() {
        AuthenticationManager.instance.AuthenticateToGPGS(
            (actionResult, actionMessage) => {
                if (actionResult) {
                    OnGPGSAuthenticationSuccess?.Invoke(actionMessage);
                } else {
                    OnGPGSAuthenticationFailed?.Invoke(actionMessage);
                }
            });
    }

    public void OpenLoadedScene() {
        AllowSceneActivation(true);
    }

    public void LoadAccount() {
        Account.instance.Init();

        OnAccountLoaded?.Invoke();
    }

    public void LoadScene() {
        StartCoroutine(ILoadAsync());
    }

    public void LoadPool() {
        ObjectPooler.instance.InitializePool("Platform");
        ObjectPooler.instance.InitializePool("Wall");
        ObjectPooler.instance.InitializePool("Key");

        foreach (string goldType in (string[])Enum.GetNames(typeof(GoldHolderTypes))) {
            ObjectPooler.instance.InitializePool(goldType, true);
        }

        foreach (string vfxType in (string[])Enum.GetNames(typeof(VFXTypes))) {
            ObjectPooler.instance.InitializePool(vfxType);
        }

        foreach (string collectable in (string[])Enum.GetNames(typeof(Collectables))) {
            ObjectPooler.instance.InitializePool(collectable);
        }

        foreach (string soundfxtype in (string[])Enum.GetNames(typeof(SoundFXTypes))) {
            ObjectPooler.instance.InitializePool(soundfxtype);
        }

        OnPoolLoaded?.Invoke();
    }

    private IEnumerator ILoadAsync() {
        yield return null;

        //Begin to load the Scene you specify
        _asyncOperation = SceneManager.LoadSceneAsync("Scene");

        //Don't let the Scene activate until you allow it to
        _asyncOperation.allowSceneActivation = false;

        //When the load is still in progress, output the Text and progress bar
        while (!_asyncOperation.isDone) {
            // Check if the load has finished
            if (_asyncOperation.progress >= 0.9f) {
                //OnSceneReady?.Invoke();
                break;
            }
            yield return null;
        }
    }

    private void AllowSceneActivation(bool activation) {
        _asyncOperation.allowSceneActivation = activation;
    }

}
