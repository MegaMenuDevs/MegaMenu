using UnityEngine;

namespace Megamenu.UI
{
    public static class MegamenuStyles
    {
        public static GUISkin MenuSkin { get; private set; }
        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            if (IsInitialized) return;

            MenuSkin = ScriptableObject.CreateInstance<GUISkin>();

            // Main Window Style
            MenuSkin.window.normal.background = CreateTexture(new Color(0.1f, 0.1f, 0.1f, 0.95f));
            MenuSkin.window.onNormal.background = MenuSkin.window.normal.background;
            // --- FIX 1 ---
            MenuSkin.window.border = new RectOffset { left = 4, right = 4, top = 4, bottom = 4 };

            // --- FIX 2 ---
            // We modify the existing window style instead of assigning to .title
            MenuSkin.window.fontSize = 14;
            MenuSkin.window.fontStyle = FontStyle.Bold;
            MenuSkin.window.alignment = TextAnchor.MiddleCenter;
            MenuSkin.window.fixedHeight = 30;
            MenuSkin.window.normal.textColor = Color.white;

            // Content Area Style
            MenuSkin.box.normal.background = CreateTexture(new Color(0.15f, 0.15f, 0.15f, 0.95f));
            // --- FIX 3 ---
            MenuSkin.box.padding = new RectOffset { left = 10, right = 10, top = 10, bottom = 10 };
            
            // Toggle/Button styles are unchanged and work fine
            // We just need a default button style for our action item
            MenuSkin.button = new GUIStyle(GUI.skin.button) { fixedHeight = 22 };
        }

        private static Texture2D CreateTexture(Color color)
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }
    }
}