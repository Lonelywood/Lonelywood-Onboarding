using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using System.Collections.Generic;

namespace Lonelywood.Onboarding.Android
{
    [Register("Lonelywood.OnboardingLayout")]
    public sealed class LonelywoodOnboardingLayout : RelativeLayout
    {
        private readonly ViewPager _viewPager;
        private OnboardingFragmentPagerAdapter _viewPagerAdapter;

        public LonelywoodOnboardingLayout(Context context, IAttributeSet attrs) : base(context, attrs) {
            Visibility = ViewStates.Gone;

            _viewPager = (ViewPager) Inflate(context, Resource.Layout.onboarding_view_pager, null);
            AddView(_viewPager);
        }

        public List<OnboardingFragment> Fragments { get; } = new List<OnboardingFragment>();
        public OnboardingPageTransformer PageTransformer { get; set; } = new OnboardingPageTransformer();

        public void Show(FragmentManager fragmentManager) {
            _viewPagerAdapter = new OnboardingFragmentPagerAdapter(fragmentManager, Fragments);

            _viewPager.Adapter = _viewPagerAdapter;
            _viewPager.SetPageTransformer(true, PageTransformer);

            PageTransformer.TransformPageEvent += OnTranformPageEvent;
            Visibility = ViewStates.Visible;
        }

        private void OnTranformPageEvent(object sender, TransformPageEventArgs e) {
            _viewPagerAdapter.GetFragmentByView(e.Page)?.Animate(e.Position);
        }
    }
}