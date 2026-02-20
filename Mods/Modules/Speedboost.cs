using System;
using System.Collections.Generic;
using System.Text;
using VividV2.Classes.Buttons;
using VividV2.Mods;

namespace VividV2ExtensionTemplate.Mods.Modules
{
    internal class Speedboost : Module
    {
        private Variable SpeedBoostAmount = new Variable("Speedboost Amount", 1.0f, 0.0f, 10.0f);
        public Speedboost() : base("Speedboost", Categories.Movement)
        {
            AddVariable(SpeedBoostAmount);
        }
    }
}
