using System;
using System.Collections.Generic;
using System.Text;
using VividV2.Classes.Buttons;

namespace VividV2ExtensionTemplate.Mods
{
    internal class Categories
    {
        // to create categories just copy these and change the variable name and the string passed.
        public static readonly Category Example = Category.Register("Example Category");

        // you can also add parent categorys by using the second parameter of the Register method
        public static readonly Category ParentCategory = Category.Register("Parent Category");
        public static readonly Category ChildCategory = Category.Register("Child Category", ParentCategory);

        // if you dont want a category to show up in the home page of the menu you can use the third parameter of the Register method
        public static readonly Category HiddenCategory = Category.Register("Hidden Category", null, true);
    }
}
