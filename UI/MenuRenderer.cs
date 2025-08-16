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
                // --- THIS IS THE FINAL FIX ---
                // We use a lambda expression `(id) => { ... }` to explicitly define the
                // window function. This resolves the IL2CPP compiler ambiguity.
                _windows[i].WindowRect = GUI.Window(
                    _windows[i].Id,
                    _windows[i].WindowRect,
                    (GUI.WindowFunction)((int id) => DrawWindowContents(id)), // The lambda expression
                    _windows[i].Title
                );
                // --- END OF FIX ---
            }
        }

        private void DrawWindowContents(int id)
        {
            MenuWindow window = _windows.FirstOrDefault(w => w.Id == id);
            if (window == null) return;

            GUILayout.BeginScrollView(window.ScrollPosition, GUI.skin.box);
            
            foreach (var action in window.Actions)
            {
                DrawButton(action);
            }
            GUILayout.EndScrollView();

            // Make the window draggable
            GUI.DragWindow();
        }

        private void DrawButton(ActionItem item)
        {
            if (GUILayout.Button(item.Label))
            {
                item.OnPressed?.Invoke();
            }
        }
    }
}