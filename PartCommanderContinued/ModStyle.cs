﻿// ModStyle.cs
// Skin and style settings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PartCommanderContinued
{
    public class ModStyle
    {
        public GUISkin skin;
        public Dictionary<string, GUIStyle> guiStyles;
        public int fontSize = 12;
        public int minWidth = 110;
        public int minHeight = 100;

        public ModStyle()
        {
            guiStyles = new Dictionary<string, GUIStyle>();

            skin = GameObject.Instantiate(HighLogic.Skin) as GUISkin;

            skin.button.padding = new RectOffset() { left = 3, right = 3, top = 3, bottom = 3 };
            skin.button.wordWrap = true;
            skin.button.fontSize = fontSize;

            skin.label.padding.top = 0;
            skin.label.fontSize = fontSize;

            skin.verticalScrollbar.fixedWidth = 10f;

            skin.window.onNormal.textColor = skin.window.normal.textColor = XKCDColors.Green_Yellow;
            skin.window.onHover.textColor = skin.window.hover.textColor = XKCDColors.YellowishOrange;
            skin.window.onFocused.textColor = skin.window.focused.textColor = Color.red;
            skin.window.onActive.textColor = skin.window.active.textColor = Color.blue;
            skin.window.padding.left = skin.window.padding.right = skin.window.padding.bottom = 2;
            skin.window.fontSize = (fontSize + 2);
            skin.window.padding = new RectOffset() { left = 1, top = 5, right = 1, bottom = 1 };

            Texture2D blackBackground = new Texture2D(1, 1);
            blackBackground.SetPixel(0, 0, Color.black);
            blackBackground.Apply();

            Log.Info("Before first call to GetToggleButtonStyle");
            guiStyles["resizeButton"] = GetToggleButtonStyle("resize", 20, 20, true);
            guiStyles["symLockButton"] = GetToggleButtonStyle("symlock", 20, 20, false);
            guiStyles["azButton"] = GetToggleButtonStyle("az", 20, 20, false);
            guiStyles["search"] = GetToggleButtonStyle("search", 20, 20, false);
            guiStyles["filter"] = GetToggleButtonStyle("filter", 20, 20, false);
            guiStyles["closeButton"] = GetToggleButtonStyle("close", 15, 15, true);
            guiStyles["popoutButton"] = GetToggleButtonStyle("popout", 20, 20, true);
            guiStyles["resourcesButton"] = GetToggleButtonStyle("resources", 20, 20, false);
            guiStyles["tempButton"] = GetToggleButtonStyle("temp", 20, 20, false);
            guiStyles["aeroButton"] = GetToggleButtonStyle("aero", 20, 20, false);
            guiStyles["left"] = GetToggleButtonStyle("left", 20, 20, true);
            guiStyles["right"] = GetToggleButtonStyle("right", 20, 20, true);
            guiStyles["settings"] = GetToggleButtonStyle("settings", 20, 20, false);

            guiStyles["titleLabel"] = new GUIStyle();
            guiStyles["titleLabel"].name = "titleLabel";
            guiStyles["titleLabel"].fontSize = fontSize + 3;
            guiStyles["titleLabel"].fontStyle = FontStyle.Bold;
            guiStyles["titleLabel"].alignment = TextAnchor.MiddleCenter;
            guiStyles["titleLabel"].wordWrap = true;
            guiStyles["titleLabel"].normal.textColor = Color.yellow;
            guiStyles["titleLabel"].padding = new RectOffset() { left = 20, right = 20, top = 0, bottom = 0 };

            guiStyles["categoryLabel"] = new GUIStyle();
            guiStyles["categoryLabel"].name = "categoryLabel";
            guiStyles["categoryLabel"].fontSize = fontSize + 1;
            guiStyles["categoryLabel"].fontStyle = FontStyle.Bold;
            guiStyles["categoryLabel"].alignment = TextAnchor.MiddleCenter;
            guiStyles["categoryLabel"].wordWrap = true;
            guiStyles["categoryLabel"].normal.textColor = XKCDColors.Orange;

            guiStyles["tooltip"] = new GUIStyle();
            guiStyles["tooltip"].name = "tooltip";
            guiStyles["tooltip"].fontSize = fontSize+3;
            guiStyles["tooltip"].wordWrap = true;
            guiStyles["tooltip"].alignment = TextAnchor.MiddleCenter;
            guiStyles["tooltip"].normal.textColor = Color.yellow;
            guiStyles["tooltip"].normal.background = blackBackground;

        }
        private WWW _imagetex;

        public Texture2D GetImage(String path, int width, int height)
        {
            Log.Info("GetImage, path: " + path);
            // Due to the image dimensions, they aren't loaded in to the KSP database properly, 
            // the code below now does that directly

            //Texture2D img = new Texture2D(width, height, TextureFormat.ARGB32, false);
            //img = GameDatabase.Instance.GetTexture(path, false);
            
            
            Log.Info("Loading image manually");
            _imagetex = new WWW(KSPUtil.ApplicationRootPath.Replace("\\", "/") +  "/GameData/" + path + ".png");
            Texture2D img = _imagetex.texture;
            _imagetex.Dispose();
            
            return img;
        }

        public GUIStyle GetToggleButtonStyle(string styleName, int width, int height, bool hover)
        {
            GUIStyle myStyle = new GUIStyle();
            Log.Info("GetToggleButtonStyle, styleName: " + styleName);
            Texture2D styleOff = GetImage("PartCommanderContinued/textures/" + styleName + "_off", width, height);
            Texture2D styleOn = GetImage("PartCommanderContinued/textures/" + styleName + "_on", width, height);

            myStyle.name = styleName + "Button";
            myStyle.padding = new RectOffset() { left = 0, right = 0, top = 0, bottom = 0 };
            myStyle.border = new RectOffset() { left = 0, right = 0, top = 0, bottom = 0 };
            myStyle.margin = new RectOffset() { left = 0, right = 0, top = 2, bottom = 2 };
            myStyle.normal.background = styleOff;
            myStyle.onNormal.background = styleOn;
            if (hover)
            {
                myStyle.hover.background = styleOn;
            }
            myStyle.active.background = styleOn;
            myStyle.fixedWidth = width;
            myStyle.fixedHeight = height;
            return myStyle;
        }
    }
}
