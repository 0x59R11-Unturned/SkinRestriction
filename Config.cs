using System.Xml.Serialization;
using Rocket.API;
using UnityEngine;

namespace Tortellio.SkinRestriction
{
    public class Config : IRocketPluginConfiguration
    {
        public bool AllowItemSkin;
        public bool AllowHatSkin;
        public bool AllowMaskSkin;
        public bool AllowGlassesSkin;
        public bool AllowBackpackSkin;
        public bool AllowShirtSkin;
        public bool AllowVestSkin;
        public bool AllowPantsSkin;

        public bool OverrideSkinColor;
        [XmlArrayItem("Item")] public string[] OverrideColors;
        [XmlArrayItem("PlayerId")] public string[] ExceptsPlayers;
        
        public void LoadDefaults()
        {
            AllowItemSkin = false;
            AllowHatSkin = false;
            AllowMaskSkin = false;
            AllowGlassesSkin = false;
            AllowBackpackSkin = false;
            AllowShirtSkin = false;
            AllowVestSkin = false;
            AllowPantsSkin = false;

            OverrideSkinColor = true;
            OverrideColors = new string[]
            {
                "#FF0000",
            };

            ExceptsPlayers = new string[]
            {
                "76561198122202652",
            };
        }
    }
}