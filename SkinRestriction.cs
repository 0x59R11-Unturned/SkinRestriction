using Rocket.Core.Plugins;
using Rocket.Unturned.Permissions;
using SDG.Unturned;
using Steamworks;
using System;
using System.Linq;
using System.Reflection;
using Logger = Rocket.Core.Logging.Logger;
using Random = System.Random;

namespace Tortellio.SkinRestriction
{
    public class SkinRestriction : RocketPlugin<Config>
    {
        public static SkinRestriction Instance;
        public static string PluginName = "SkinRestriction";
        public static string PluginVersion = " 1.1.0";


        private Random _random = new Random();
        private FieldInfo _skinField =
            typeof(SteamPending).GetField("_skin", BindingFlags.Instance | BindingFlags.NonPublic);
        
        protected override void Load()
        {
            Instance = this;
            Logger.Log("SkinRestriction has been loaded!");
            Logger.Log(PluginName + PluginVersion, ConsoleColor.Yellow);
            Logger.Log("Made by Tortellio and fixed by nn653", ConsoleColor.Yellow);
            UnturnedPermissions.OnJoinRequested += new UnturnedPermissions.JoinRequested(OnPlayerConnect);
        }

        protected override void Unload()
        {
            Instance = null;
            Logger.Log("SkinRestriction has been unloaded!");
            UnturnedPermissions.OnJoinRequested -= new UnturnedPermissions.JoinRequested(OnPlayerConnect);
        }

        public void OnPlayerConnect(CSteamID Player, ref ESteamRejection? rejection)
        {
            foreach (SteamPending sPlayer in Provider.pending)
            {
                bool checkPlayer = sPlayer.playerID.steamID == Player;
                if (checkPlayer)
                {
                    bool checkBypass = Configuration.Instance.ExceptsPlayers
                        .FirstOrDefault(p => string.Equals(p, sPlayer.playerID.steamID.ToString(), StringComparison.InvariantCulture)) != null;
                    if (checkBypass)
                    {
                        return;
                    }
                    
                    #region SkinType
                    if (!Configuration.Instance.AllowItemSkin)
                    {
                        sPlayer.skinItems = Array.Empty<int>();
                        sPlayer.packageSkins = Array.Empty<ulong>();
                    }
                    if (!Configuration.Instance.AllowHatSkin)
                    {
                        sPlayer.packageHat = 0UL;
                        sPlayer.hatItem = 0;
                    }
                    if (!Configuration.Instance.AllowMaskSkin)
                    {
                        sPlayer.maskItem = 0;
                        sPlayer.packageMask = 0UL;
                    }
                    if (!Configuration.Instance.AllowGlassesSkin)
                    {
                        sPlayer.packageGlasses = 0UL;
                        sPlayer.glassesItem = 0;
                    }
                    if (!Configuration.Instance.AllowShirtSkin)
                    {
                        sPlayer.shirtItem = 0;
                        sPlayer.packageShirt = 0UL;
                    }
                    if (!Configuration.Instance.AllowVestSkin)
                    {
                        sPlayer.vestItem = 0;
                        sPlayer.packageVest = 0UL;
                    }
                    if (!Configuration.Instance.AllowBackpackSkin)
                    {
                        sPlayer.packageBackpack = 0UL;
                        sPlayer.backpackItem = 0;
                    }
                    if (!Configuration.Instance.AllowPantsSkin)
                    {
                        sPlayer.pantsItem = 0;
                        sPlayer.packagePants = 0UL;
                    }
                    #endregion

                    
                    var colors = Configuration.Instance.OverrideColors.Length;
                    if (Configuration.Instance.OverrideSkinColor && colors > 0)
                    {
                        string newColorHex;
                        if (colors == 1)
                        {
                            newColorHex = Configuration.Instance.OverrideColors[0];
                        }
                        else
                        {
                            newColorHex = Configuration.Instance.OverrideColors[_random.Next(colors)];
                        }
                        
                        var newColor = Palette.hex(newColorHex);
                        _skinField.SetValue(sPlayer, newColor);
                    }
                }
            }
        }

    }
}
