using UnityEngine;

namespace Megamenu.Features.Host
{
    public class ServerStress : MonoBehaviour
    {
        private static bool _isSpamming = false;
        private static int _reportDelayCounter = 0;
        private const int ReportSpamInterval = 5;

        // This is the public method our button will call
        public static void StartReportSpam()
        {
            if (AmongUsClient.Instance.IsInGame && ShipStatus.Instance == null)
            {
                _isSpamming = true;
            }
        }

        void Update()
        {
            if (!_isSpamming) return;

            if (ShipStatus.Instance != null || !AmongUsClient.Instance.IsInGame)
            {
                _isSpamming = false;
                return;
            }

            if (_reportDelayCounter <= 0)
            {
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (player != null && player.Data != null)
                    {
                        // --- THIS IS THE FIX ---
                        // The ReportDeadBody method requires the player's ".Data" property (a PlayerInfo object).
                        PlayerControl.LocalPlayer.ReportDeadBody(player.Data);
                        // --- END OF FIX ---
                        _reportDelayCounter = ReportSpamInterval;
                        return;
                    }
                }
            }
            else
            {
                _reportDelayCounter--;
            }
        }
    }
}