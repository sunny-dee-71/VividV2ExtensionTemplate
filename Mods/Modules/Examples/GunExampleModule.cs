using System;
using System.Collections.Generic;
using System.Text;
using VividV2.Classes.Buttons;
using VividV2.Classes.Utils;

namespace VividV2ExtensionTemplate.Mods.Modules.Examples
{
    public class GunExampleModule : Module
    {
        public GunExampleModule() : base("Gun Example", Categories.Example, true)
        {
        }

        public override void Update()
        {
            // For using the gun lib you should do
            // The bool is if you if you want to gun to snap to players, use if you are making a something that should select a player
            var info = GunLibUtils.UpdateGun(false);
            // UpdateGun return GunInfo which will contain
            // Trigger pressed, GunActive, RigAimedAt and GunPosition
            // Trigger pressed will be true if the trigger is pressed
            // GunActive will be true if the gun is active
            // RigAimedAt will be the VRRig that the gun is aimed at
            // GunPosition will be the position of the gun in world space.
        }
    }
}
