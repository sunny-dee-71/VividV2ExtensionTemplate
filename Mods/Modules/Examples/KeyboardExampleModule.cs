using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VividV2.Classes.Buttons;
using VividV2.Classes.Utils;

namespace VividV2ExtensionTemplate.Mods.Modules.Examples
{
    public class KeyboardExampleModule : Module
    {
        public KeyboardExampleModule() : base("Keyboard Example", Categories.Example, true)
        {
        }

        public override void OnEnable()
        {
            // For using the keyboard you should do run it in an async method
            Task.Run(async () => await GetKeyboardInput());
        }

        private static async Task GetKeyboardInput()
        {
            string input = await KeyboardUtils.RequestString("Enter a string:");

            // Will be whatever the user has typed in the keyboard when they press enter

            // If you want constant updates on when user types each letter do
            void OnInputChanged(string newInput)
            {
                // The string passed here will be whatever the user has typed in the keyboard at that moment
            }
            string input2 = await KeyboardUtils.RequestString("Enter a string:", OnInputChanged);
            // The final string will still be whatever the user has typed in the keyboard when they press enter
        }
    }
}
