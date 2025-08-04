using UnityEngine;
using System.Collections.Generic;

namespace Megamenu.UI
{
    #region Data Models
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

    public static class MenuDefinition
    {
        public static List<MenuWindow> GetMenuWindows()
        {
            System.Action<string> logAction = (name) => Debug.Log($"Toggle '{name}' was changed.");

            return new List<MenuWindow>
            {
                new MenuWindow("Player", new Rect(20, 20, 220, 200),
                    toggles: new List<ToggleItem>
                    {
                        new ToggleItem("NoClip", val => logAction("NoClip")),
                        new ToggleItem("SpeedHack", val => logAction("SpeedHack"))
                    },
                    submenus: new List<Submenu>
                    {
                        new Submenu("Teleport", new List<ToggleItem>
                        {
                            new ToggleItem("to Cursor", val => logAction("Teleport to Cursor")),
                            new ToggleItem("to Player", val => logAction("Teleport to Player"))
                        })
                    }
                ),

                new MenuWindow("ESP", new Rect(250, 20, 220, 500),
                    toggles: new List<ToggleItem>
                    {
                        new ToggleItem("See Roles", val => logAction("See Roles")),
                        new ToggleItem("See Ghosts", val => logAction("See Ghosts")),
                        new ToggleItem("No Shadows", val => logAction("No Shadows")),
                        new ToggleItem("Reveal Votes", val => logAction("Reveal Votes"))
                    },
                    submenus: new List<Submenu>
                    {
                        new Submenu("Camera", new List<ToggleItem>
                        {
                            new ToggleItem("Zoom Out", val => logAction("Zoom Out")),
                            new ToggleItem("Spectate", val => logAction("Spectate")),
                            new ToggleItem("Freecam", val => logAction("Freecam"))
                        }),
                        new Submenu("Tracers", new List<ToggleItem>
                        {
                            new ToggleItem("Crewmates", val => logAction("Crew Tracers")),
                            new ToggleItem("Impostors", val => logAction("Impostor Tracers")),
                            new ToggleItem("Ghosts", val => logAction("Ghost Tracers")),
                            new ToggleItem("Dead Bodies", val => logAction("Body Tracers")),
                            new ToggleItem("Color-based", val => logAction("Color-based Tracers"))
                        }),
                        new Submenu("Minimap", new List<ToggleItem>
                        {
                            new ToggleItem("Crewmates", val => logAction("Minimap Crew")),
                            new ToggleItem("Impostors", val => logAction("Minimap Impostors")),
                            new ToggleItem("Ghosts", val => logAction("Minimap Ghosts")),
                            new ToggleItem("Color-based", val => logAction("Minimap Color"))
                        })
                    }
                ),

                new MenuWindow("Roles", new Rect(480, 20, 220, 600),
                    toggles: new List<ToggleItem> { new ToggleItem("Set Fake Role", val => logAction("Set Fake Role")) },
                    submenus: new List<Submenu>
                    {
                        new Submenu("Impostor", new List<ToggleItem> { new ToggleItem("Kill Reach", val => logAction("Kill Reach")) }),
                        new Submenu("Shapeshifter", new List<ToggleItem> { new ToggleItem("No SS Animation", val => logAction("No SS Animation")), new ToggleItem("Endless SS Duration", val => logAction("Endless SS")) }),
                        new Submenu("Crewmate", new List<ToggleItem> { new ToggleItem("Complete My Tasks", val => logAction("Complete Tasks")) }),
                        new Submenu("Tracker", new List<ToggleItem> { new ToggleItem("Endless Tracking", val => logAction("Endless Tracking")) }),
                        new Submenu("Engineer", new List<ToggleItem> { new ToggleItem("Endless Vent Time", val => logAction("Endless Vent Time")) }),
                        new Submenu("Scientist", new List<ToggleItem> { new ToggleItem("Endless Battery", val => logAction("Endless Battery")) })
                    }
                ),

                new MenuWindow("Ship", new Rect(710, 20, 220, 400),
                    toggles: new List<ToggleItem>
                    {
                        new ToggleItem("Unfixable Lights", val => logAction("Unfixable Lights")),
                        new ToggleItem("Report Body", val => logAction("Report Body")),
                        new ToggleItem("Close Meeting", val => logAction("Close Meeting"))
                    },
                    submenus: new List<Submenu>
                    {
                        new Submenu("Sabotage", new List<ToggleItem> { new ToggleItem("Reactor", val => logAction("Sabotage Reactor")) /*...etc*/ }),
                        new Submenu("Vents", new List<ToggleItem> { new ToggleItem("Unlock Vents", val => logAction("Unlock Vents")) /*...etc*/ })
                    }
                ),

                new MenuWindow("Chat", new Rect(940, 20, 220, 150),
                    toggles: new List<ToggleItem>
                    {
                        new ToggleItem("Enable Chat", val => logAction("Enable Chat")),
                        new ToggleItem("Unlock Textbox", val => logAction("Unlock Textbox"))
                    }
                ),

                new MenuWindow("Host-Only", new Rect(1170, 20, 220, 250),
                    toggles: new List<ToggleItem>
                    {
                        new ToggleItem("Kill While Vanished", val => logAction("Kill Vanished")),
                        new ToggleItem("Kill Anyone", val => logAction("Kill Anyone")),
                        new ToggleItem("No Kill Cooldown", val => logAction("No Kill CD"))
                    },
                    submenus: new List<Submenu>
                    {
                        new Submenu("Murder", new List<ToggleItem> { new ToggleItem("Kill Player", val => logAction("Kill Player")) /*...etc*/ })
                    }
                ),

                new MenuWindow("Passive", new Rect(1400, 20, 220, 180),
                    toggles: new List<ToggleItem>
                    {
                        new ToggleItem("Free Cosmetics", val => logAction("Free Cosmetics"), true),
                        new ToggleItem("Avoid Penalties", val => logAction("Avoid Penalties"), true),
                        new ToggleItem("Unlock Extra Features", val => logAction("Unlock Features"), true)
                    }
                )
            };
        }
    }
}