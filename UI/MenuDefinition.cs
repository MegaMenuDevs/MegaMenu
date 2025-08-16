using UnityEngine;
using System.Collections.Generic;
using Megamenu.Features.Host;

namespace Megamenu.UI
{
    #region Data Models

    // --- FIX 1: RESTORE THE ACTIONITEM CLASS ---
    /// <summary>
    /// Represents a single-press button that performs an action.
    /// </summary>
    public class ActionItem
    {
        public string Label { get; }
        public System.Action OnPressed { get; }
        public ActionItem(string label, System.Action onPressed)
        {
            Label = label;
            OnPressed = onPressed;
        }
    }

    public class MenuWindow
    {
        private static int _nextId = 0;
        public int Id { get; } = _nextId++;
        public string Title { get; }
        public Rect WindowRect { get; set; }
        public Vector2 ScrollPosition { get; set; }
        
        // --- FIX 2: ADD THE ACTIONS LIST BACK ---
        public List<ActionItem> Actions { get; } = new List<ActionItem>();

        public MenuWindow(string title, Rect initialRect, List<ActionItem> actions = null)
        {
            Title = title;
            WindowRect = initialRect;
            if (actions != null) Actions = actions;
        }
    }
    #endregion

    public static class MenuDefinition
    {
        public static List<MenuWindow> GetMenuWindows()
        {
            return new List<MenuWindow>
            {
                new MenuWindow("Megamenu", new Rect(20, 20, 220, 120),
                    // --- FIX 3: CHANGE THE TOGGLE TO THE CORRECT BUTTON ---
                    actions: new List<ActionItem>
                    {
                        new ActionItem("Crash (Report Spam)", ServerStress.StartReportSpam)
                    }
                )
            };
        }
    }
}