// 📁 Megamenu/UI/MenuDefinition.cs

using UnityEngine;
using System.Collections.Generic;
using Megamenu.Features.Host; // We need this to link our UI to the feature logic

namespace Megamenu.UI
{
    #region Data Models
    // These are the building blocks for the UI. They are required for the renderer to work.

    public class ToggleItem
    {
        public string Label { get; }
        public bool IsOn { get; set; }
        public System.Action<bool> OnToggled { get; }
        public ToggleItem(string label, System.Action<bool> onToggled, bool initialState = false)
        {
            Label = label;
            IsOn = initialState;
            OnToggled = onToggled;
        }
    }

    public class Submenu
    {
        public string Title { get; }
        public bool IsExpanded { get; set; }
        public List<ToggleItem> Toggles { get; }
        public Submenu(string title, List<ToggleItem> toggles, bool startsExpanded = false)
        {
            Title = title;
            Toggles = toggles;
            IsExpanded = startsExpanded;
        }
    }

    public class MenuWindow
    {
        private static int _nextId = 0;
        public int Id { get; } = _nextId++;
        public string Title { get; }
        public Rect WindowRect { get; set; }
        public List<ToggleItem> Toggles { get; } = new List<ToggleItem>();
        public List<Submenu> Submenus { get; } = new List<Submenu>();
        public Vector2 ScrollPosition { get; set; }

        public MenuWindow(string title, Rect initialRect, List<ToggleItem> toggles = null, List<Submenu> submenus = null)
        {
            Title = title;
            WindowRect = initialRect;
            if (toggles != null) Toggles = toggles;
            if (submenus != null) Submenus = submenus;
        }
    }
    #endregion

    /// <summary>
    /// Contains the static definition of the entire menu structure.
    /// This file now only defines ONE window with ONE toggle for our single feature.
    /// </summary>
    public static class MenuDefinition
    {
        public static List<MenuWindow> GetMenuWindows()
        {
            // The list of windows now only contains a single entry.
            return new List<MenuWindow>
            {
                // A single, small window for our feature.
                // We've moved it to the top-left of the screen for simplicity.
                new MenuWindow("Megamenu", new Rect(20, 20, 220, 120),
                    toggles: new List<ToggleItem>
                    {
                        // The one and only toggle. This connects directly to our ServerStress logic.
                        new ToggleItem("Crash (Report Spam)", 
                                       (isEnabled) => ServerStress.IsCrashSpamReportEnabled = isEnabled)
                    }
                )
            };
        }
    }
}