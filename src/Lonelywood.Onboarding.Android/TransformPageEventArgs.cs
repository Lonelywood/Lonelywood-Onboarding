using System;
using Android.Views;

namespace Lonelywood.Onboarding.Android
{
    public class TransformPageEventArgs : EventArgs
    {
        public View Page { get; set; }
        public float Position { get; set; }
    }
}