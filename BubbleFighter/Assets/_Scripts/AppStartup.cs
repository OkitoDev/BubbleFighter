using System.Linq;
using Menu;
using UnityEngine;

public static class AppStartup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void OnAfterSceneLoadRuntimeMethod()
    {
        InitializeDisabledMenuElements();
    }

    // Menu elements needs to subscribe to game pause/unpause events.
    // This is not possible if they are disabled by default. 
    // Therefore, after a scene is loaded, we need to find them and initialize those manually.
    private static void InitializeDisabledMenuElements()
    {
        var disabledMenuElements = Resources.FindObjectsOfTypeAll<MenuElement>().Where(g => g.gameObject.activeSelf == false);

        foreach (var disabledElement in disabledMenuElements)
        {
            disabledElement.Initialize();
        }
    }
}