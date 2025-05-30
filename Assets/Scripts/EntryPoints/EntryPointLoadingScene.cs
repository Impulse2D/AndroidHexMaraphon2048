using LoadingScene;
using Localization;
using UnityEngine;

public class EntryPointLoadingScene : MonoBehaviour
{
    [SerializeField] private LocalozationLoader _localozationLoader;
    [SerializeField] private AsyncGameSceneLoader _asyncGameSceneLoader;

    private void Start()
    {
        _localozationLoader.Init();
        _asyncGameSceneLoader.Init();
    }
}
