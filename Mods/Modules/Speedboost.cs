using System;
using System.Collections.Generic;
using System.Text;
using VividV2.Classes.Buttons;
using VividV2.Core;
using VividV2.Mods;

namespace VividV2ExtensionTemplate.Mods.Modules
{
    internal class Speedboost : Module
    {

        // to initialize the module do this
        // the first string will be the name shown in the menu
        // the second is the category, that is where the mod will be located
        // the third bool is if it is toggleable, set to true to be toggleable or false to not be toggleable
        // if it is toggleable OnEnable will be called each time it is pressed
        // if it is toggleable OnDisable will never be called
        // Update will will always be called no matter if it is or isnt toggleable
        public Speedboost() : base("Speedboost", Categories.Movement, true)
        {
            // to add variables first initialize the variable
            Variable SpeedBoostAmount = new Variable("Speedboost Amount", 1.0f, 0.0f, 10.0f);
            // and the add it to the module by using AddVariable
            AddVariable(SpeedBoostAmount);

            // you can get variables by using GetVariable with the name (WARNING: the name is case sensitive)
            GetVariable("Speedboost Amount");
        }


        public override void Update()
        {
            // this will run every frame

            if (Enabled)
            {
                // use the Enabled boolean to check weather the mod is enabled in update
            }
        }

        public override void OnDisable()
        {
            // this will be called every time the module is disabled
            Logger.LogError
        }

        public override void OnEnable()
        {
            // this will be called every time the module is enabled
        }

        // other useful methods are:
        // SetEnabled which will set the mod the be enabled or disabled
        // Logger.Log and Logger.LogError which will log messages, (IMPORTANT: make sure to use VividV2.Core.Logger and not the bepinex one)


    }
}
