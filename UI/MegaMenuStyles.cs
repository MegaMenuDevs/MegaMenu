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

            MenuSkin.window.normal.background = CreateTexture(new Color(0.1f, 0.1f, 0.1f, 0.95f));
            MenuSkin.window.onNormal.background = MenuSkin.window.normal.background;
            MenuSkin.window.border = new RectOffset(4, 4, 4, 4);

            GUIStyle headerStyle = new GUIStyle
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                fixedHeight = 30,
                normal = { textColor = Color.white }
            };
            MenuSkin.window.title = headerStyle;

            MenuSkin.box.normal.background = CreateTexture(new Color(0.15f, 0.15f, 0.15f, 0.95f));
            MenuSkin.box.padding = new RectOffset(10, 10, 10, 10);
            
            MenuSkin.toggle = new GUIStyle(GUI.skin.toggle)
            {
                fontSize = 12,
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(20, 0, 0, 0), // Indent text for indicator
                fixedHeight = 22,
                normal = { textColor = new Color(0.9f, 0.9f, 0.9f) },
                onNormal = { textColor = Color.white },
                hover = { textColor = Color.white },
                onHover = { textColor = Color.white },
                active = { textColor = Color.white },
                onActive = { textColor = Color.white }
            };
            
            MenuSkin.customStyles = new GUIStyle[]
            {
                new GUIStyle { normal = { background = CreateTexture(new Color(0.2f, 0.7f, 1f)) } },
                
                new GUIStyle(GUI.skin.button)
                {
                    alignment = TextAnchor.MiddleLeft,
                    fontStyle = FontStyle.Bold,
                    fontSize = 12,
                    fixedHeight = 22,
                    normal = { textColor = new Color(0.8f, 0.8f, 0.8f), background = CreateTexture(new Color(0.25f, 0.25f, 0.25f)) },
                    hover = { textColor = Color.white, background = CreateTexture(new Color(0.3f, 0.3f, 0.3f)) },
                    active = { textColor = Color.white }
                }
            };

            IsInitialized = true;
        }

        public static GUIStyle GetStyle(string name)
        {
            switch (name)
            {
                case "Indicator": return MenuSkin.customStyles[0];
                case "SubmenuHeader": return MenuSkin.customStyles[1];
                default: return GUI.skin.label;
            }
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