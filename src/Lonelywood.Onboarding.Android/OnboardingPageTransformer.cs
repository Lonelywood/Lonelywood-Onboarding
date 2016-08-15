using System;
using Android.Views;
using Android.Support.V4.View;

namespace Lonelywood.Onboarding.Android
{
    public class OnboardingPageTransformer : Java.Lang.Object, ViewPager.IPageTransformer
    {
        public event EventHandler<TransformPageEventArgs> TransformPageEvent;

        public void TransformPage(View page, float position) {
            TransformPageEvent?.Invoke(this, new TransformPageEventArgs {
                Page = page,
                Position = position
            });
        }
    }
}