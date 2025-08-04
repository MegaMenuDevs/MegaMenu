using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Megamenu.UI
{
    public class MenuRenderer : MonoBehaviour
    {
        private bool _isGuiActive = true;
        private List<MenuWindow> _windows;
        private KeyCode _menuKeybind = KeyCode.Insert;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _windows = MenuDefinition.GetMenuWindows();
        }

        void Update()
        {
            if (Input.GetKeyDown(_menuKeybind))
            {
                _isGuiActive = !_isGuiActive;
            }
        }

        void OnGUI()
        {
            if (!_isGuiActive) return;

            MegamenuStyles.Initialize();
            GUI.skin = MegamenuStyles.MenuSkin;

            for (int i = 0; i < _windows.Count; i++)
            {
                _windows[i].WindowRect = GUILayout.Window(_windows[i].Id, _windows[i].WindowRect, DrawWindowContents, _windows[i].Title);
            }
        }

        private void DrawWindowContents(int id)
        {
            MenuWindow window = _windows.FirstOrDefault(w => w.Id == id);
            if (window == null) return;

            window.ScrollPosition = GUILayout.BeginScrollView(window.ScrollPosition, GUI.skin.box);

            foreach (var toggle in window.Toggles)
            {
                DrawToggle(toggle);
            }

            if (window.Toggles.Any() && window.Submenus.Any())
            {
                GUILayout.Space(10);
            }

            foreach (var submenu in window.Submenus)
            {
                DrawSubmenu(submenu);
            }

            GUILayout.EndScrollView();
        }

        private void DrawToggle(ToggleItem item)
        {
            bool newState = GUILayout.Toggle(item.IsOn, " " + item.Label);
            if (newState != item.IsOn)
            {
                item.IsOn = newState;
                item.OnToggled?.Invoke(newState);
            }

            if (item.IsOn)
            {
                Rect toggleRect = GUILayoutUtility.GetLastRect();
                GUI.Box(new Rect(toggleRect.x + 5, toggleRect.y + 4, 4, toggleRect.height - 8), GUIContent.none, MegamenuStyles.GetStyle("Indicator"));
            }
        }

        private void DrawSubmenu(Submenu submenu)
        {
            string expander = submenu.IsExpanded ? "▼ " : "► ";
            if (GUILayout.Button(expander + submenu.Title, MegamenuStyles.GetStyle("SubmenuHeader")))
            {
                submenu.IsExpanded = !submenu.IsExpanded;
            }

            if (submenu.IsExpanded)
            {
                GUILayout.BeginVertical(GUI.skin.box);
                foreach (var toggle in submenu.Toggles)
                {
                    DrawToggle(toggle);
                }
                GUILayout.EndVertical();
            }
        }
    }
}