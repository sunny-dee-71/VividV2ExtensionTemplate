using System;
using System.Collections.Generic;
using System.Text;
using VividV2.Classes;
using VividV2.Classes.Enums;

namespace VividV2ExtensionTemplate.Mods.Animations.Close
{
    public class ExampleCloseAnimation : MenuAnimation
    {
        public ExampleCloseAnimation() : base("ExampleCloseAnimation", MenuAnimationType.MenuClose)
        {
        }

        public override MenuAnimator Animate(float AnimationPercentage, MenuAnimator target)
        {
            target.Scale = target.Scale - (target.Scale * (AnimationPercentage));

            return target;
        }
    }
}
