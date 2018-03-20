using System;
using UIKit;
using Xamarin.Forms;
using BEST_WYR.iOS;

[assembly: ResolutionGroupName("SEFD")]
[assembly: ExportEffect(typeof(ButtonWrapTextEffect), "ButtonWrapEffect")]
namespace BEST_WYR.iOS
{
    public class ButtonWrapTextEffect : Xamarin.Forms.Platform.iOS.PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(Control is UIButton button))
                return;

            button.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
}
