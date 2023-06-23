using SingletonBehaviour;
using UnityEngine;

public class LoadingScreen : SingletonBehaviour<LoadingScreen>
{
    [SerializeField]
    private GameObject _loadingScreen;

    [SerializeField]
    private bool _startOn;

    private void Awake() => ShowLoadingScreen(_startOn);

    public void ShowLoadingScreen(bool show) => _loadingScreen.SetActive(show);
}
