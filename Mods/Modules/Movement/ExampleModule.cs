using System;
using System.Collections.Generic;
using System.Text;
using VividV2.Classes.Buttons;
using VividV2.Classes.Buttons.Variables;
using VividV2.Classes.Enums;
using VividV2.Core;
using VividV2.Mods;

namespace VividV2ExtensionTemplate.Mods.Modules.Movement
{
    internal class ExampleModule : Module
    {
        #region Constructor

        // to initialize the module do this
        // the first string will be the name shown in the menu
        // the second is the category, that is where the mod will be located
        // the third bool is if it is toggleable, set to true to be toggleable or false to not be toggleable
        // if it is toggleable OnEnable will be called each time it is pressed
        // if it is toggleable OnDisable will never be called
        // Update will will always be called no matter if it is or isnt toggleable
        public ExampleModule() : base("Example", Categories.Example, true)
        {
            #region Variables

            #region Adding Variables

            // to add variables first initialize the variable
            // and the add it to the module by using AddVariable
            FloatVariable exampleVariable = new FloatVariable("Example Variable", 1f, 0f, 10f);
            AddVariable(exampleVariable);

            #endregion Adding Variables

            #region Getting Variables

            // you can get variables by using GetVariable with the name along with the type of Variable (WARNING: the name is case sensitive)
            GetVariable<FloatVariable>("Example Variable");

            #endregion Getting Variables

            #region Other Variables
            // Other types of variables include:

            #region Bool Variable

            // Bool Variable
            // First parameter is the name.
            // Second parameter is the default value.
            BoolVariable boolVariable = new BoolVariable("Bool Variable", true);

            #endregion Bool Variable

            #region Int Variable

            // Int Variable
            // First parameter is the name.
            // Second parameter is the default value.
            // Third parameter is the minimum value.
            // Fourth parameter is the maximum value.
            IntVariable intVariable = new IntVariable("Int Variable", 5, 0, 10);

            #endregion Int Variable

            #region Float Variable

            // Float Variable
            // First parameter is the name.
            // Second parameter is the default value.
            // Third parameter is the minimum value.
            // Fourth parameter is the maximum value.
            FloatVariable floatVariable = new FloatVariable("Float Variable", 1.0f, 0.0f, 10.0f);

            #endregion Float Variable

            #region String Variable

            // String Variable
            // First parameter is the name.
            // Second parameter is an array of options for the variable.
            // The default value will be the first option in the array.
            StringVariable stringVariable = new StringVariable("String Variable", new string[] { "Option 1", "Option 2", "Option 3" });

            #endregion String Variable

            #region Color Variable

            // Color Variable
            // First parameter is the name.
            // Second parameter is the default value using UnityEngine.Color.
            // Use RGBA or RGB values.
            ColorVariable colorVariable = new ColorVariable("Color Variable", new UnityEngine.Color(1f, 0f, 0f));

            #endregion Color Variable

            #region Keybind Variable

            // Keybind Variable
            // First parameter is the name.
            // Second parameter is the type of keybind:
            // KeybindType.Joystick, KeybindType.BothHands, or KeybindType.SingleHand.
            // Third parameter is the default hand it will use.
            // Fourth parameter is the default button it will use.
            // If the type is KeybindType.Joystick then the fourth parameter is ignored
            // and it will use the joystick on the selected hand.

            #region Both Hands
            // KeybindType.BothHands

            // Lets the user select a button type only.
            // The selected button will work on BOTH hands.
            //
            // Example:
            // - Primary
            // - Secondary
            // - Grip
            // - Trigger
            //
            // Pressed will return true if the selected button
            // is pressed on either the left OR right hand.
            //
            // Example:
            // If Button = Trigger:
            // - Left Trigger pressed  -> true
            // - Right Trigger pressed -> true

            KeybindVariable keybindVariable = new KeybindVariable("Keybind Variable", KeybindType.BothHands);

            #endregion Both Hands

            #region Single Hand

            // KeybindType.SingleHand

            // Lets the user select BOTH a hand and a button.
            //
            // Example values:
            // - Left Trigger
            // - Right Grip
            // - Left Primary
            //
            // Pressed will only return true if the selected
            // hand AND button are pressed.
            //
            // Example:
            // If Value = Right Grip:
            // - Right Grip pressed -> true
            // - Left Grip pressed  -> false

            KeybindVariable keybindVariable2 = new KeybindVariable("Keybind Variable 2", KeybindType.SingleHand);

            #endregion Single Hand

            #region Joystick

            // KeybindType.Joystick

            // Lets the user select either the left or right joystick.
            //
            // The button parameter is ignored for this type.
            //
            // Use JoystickValue to get the Vector2 input.
            //
            // Example:
            // JoystickValue.x = horizontal movement
            // JoystickValue.y = vertical movement

            KeybindVariable joystickVariable = new KeybindVariable("Joystick Variable", KeybindType.Joystick, HandType.Right);

            #endregion Joystick

            #endregion Keybind Variable

            #endregion Other Variables


            AddVariable(boolVariable);
            AddVariable(intVariable);
            AddVariable(floatVariable);
            AddVariable(stringVariable);
            AddVariable(colorVariable);
            AddVariable(keybindVariable);
            AddVariable(keybindVariable2);
            AddVariable(joystickVariable);

            #endregion Variables
        }

        #endregion Constructor

        public override void Update()
        {
            // This will run every frame no matter if the mod is enabled or not

            if (Enabled)
            {
                // Use the Enabled boolean to check weather the mod is enabled in update
            }
        }

        public override void LateUpdate()
        {
            // This will run every LateUpdate frame no matter if the mod is enabled or not

            if (Enabled)
            {
                // Use the Enabled boolean to check weather the mod is enabled in LateUpdate
            }
        }



        public override void OnDisable()
        {
            // This will be called every time the module is disabled
        }

        public override void OnEnable()
        {
            // This will be called every time the module is enabled
        }

        // other useful methods are:
        // SetEnabled which will set the mod the be enabled or disabled
        // Logger.Log and Logger.LogError which will log messages, (IMPORTANT: make sure to use VividV2.Core.Logger and not the bepinex one)


    }
}
