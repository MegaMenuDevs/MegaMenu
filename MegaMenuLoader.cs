using BepInEx;
using Megamenu.UI;
using Megamenu.Features.Host;
using UnityEngine;

[BepInPlugin("com.megamenu.loader", "Megamenu", "1.0.0")]
public class MegamenuLoader : BaseUnityPlugin
{
    void Awake()
    {
        // This makes the UI appear
        gameObject.AddComponent<MenuRenderer>();

        // This makes the feature logic run
        gameObject.AddComponent<ServerStress>();

        Logger.LogInfo("Megamenu Loaded! UI and features are active.");
    }
}