using BepInEx;
using BepInEx.Unity.IL2CPP; // Correct namespace for BasePlugin
using Megamenu.Features.Host;
using Megamenu.UI;

namespace Megamenu; // Using a simple namespace like the example

// These attributes are from the working example and are essential.
[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
public partial class Plugin : BasePlugin // Must be public partial class : BasePlugin
{
    /// <summary>
    /// This is the correct entry point for an IL2CPP plugin.
    /// It runs once when the plugin is loaded.
    /// </summary>
    public override void Load()
    {
        // --- THIS IS THE CRITICAL CHANGE ---
        // The working example shows that BasePlugin has a built-in AddComponent method.
        // We do NOT need to create our own GameObject. This is the idiomatic way.
        AddComponent<MenuRenderer>();
        AddComponent<ServerStress>();

        Log.LogInfo("Megamenu Loaded! UI and features are active.");
    }
}