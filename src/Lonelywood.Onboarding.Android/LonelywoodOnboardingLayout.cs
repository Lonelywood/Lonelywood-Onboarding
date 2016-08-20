using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace Lonelywood.Onboarding.Android
{
    [Register("Lonelywood.OnboardingLayout")]
    public sealed class LonelywoodOnboardingLayout : RelativeLayout
    {
        private readonly Context _context;
        private ViewPager _viewPager;
        private AppCompatButton _nextButton;
        private ViewGroup _buttonNextLayout;
        private AppCompatButton _skipButton;
        private ViewGroup _buttonSkipLayout;
        private AppCompatButton _finishButton;
        private ViewGroup _buttonFinishLayout;
        private OnboardingFragmentPagerAdapter _viewPagerAdapter;

        public LonelywoodOnboardingLayout(Context context, IAttributeSet attrs) : base(context, attrs) {
            _context = context;
            Visibility = ViewStates.Gone;
        }

        public List<OnboardingFragment> Fragments { get; } = new List<OnboardingFragment>();
        public OnboardingPageTransformer PageTransformer { get; set; } = new OnboardingPageTransformer();
        public ICommand ButtonNextCommand { get; set; }
        public ICommand ButtonSkipCommand { get; set; }
        public ICommand ButtonFinishCommand { get; set; }
        public int ButtonNextLayout { get; set; } = Resource.Layout.onboarding_next_button;
        public int ButtonSkipLayout { get; set; } = Resource.Layout.onboarding_skip_button;
        public int ButtonFinishLayout { get; set; } = Resource.Layout.onboarding_finish_button;

        public void Show(FragmentManager fragmentManager) {
            _viewPagerAdapter = new OnboardingFragmentPagerAdapter(fragmentManager, Fragments);

            InflateElements();

            _viewPager.Adapter = _viewPagerAdapter;
            _viewPager.SetPageTransformer(true, PageTransformer);

            PageTransformer.TransformPageEvent += OnTranformPageEvent;

            AddViews();

            RegisterForEvents();

            Visibility = ViewStates.Visible;
        }

        private void InflateElements() {
            var inflater = (LayoutInflater) _context.GetSystemService(Context.LayoutInflaterService);

            _viewPager = (ViewPager) inflater.Inflate(Resource.Layout.onboarding_view_pager, null, false);

            _buttonNextLayout = (ViewGroup) inflater.Inflate(ButtonNextLayout, null, false);
            _nextButton = _buttonNextLayout.FindViewById<AppCompatButton>(Resource.Id.lonelywood_onboarding_next_button);

            if (_nextButton == null) throw new ArgumentException("ButtonNextLayout must have AppCompatButton with lonelywood_onboarding_next_button id.");

            _buttonSkipLayout = (ViewGroup) inflater.Inflate(ButtonSkipLayout, null, false);
            _skipButton = _buttonSkipLayout.FindViewById<AppCompatButton>(Resource.Id.lonelywood_onboarding_skip_button);

            if (_skipButton == null) throw new ArgumentException("ButtonSkipLayout must have AppCompatButton with lonelywood_onboarding_skip_button id.");

            _buttonFinishLayout = (ViewGroup) inflater.Inflate(ButtonFinishLayout, null, false);
            _finishButton = _buttonFinishLayout.FindViewById<AppCompatButton>(Resource.Id.lonelywood_onboarding_finish_button);
            _finishButton.Visibility = ViewStates.Gone;

            if (_finishButton == null) throw new ArgumentException("ButtonFinishLayout must have AppCompatButton with lonelywood_onboarding_finish_button id.");

        }

        private void AddViews() {
            AddView(_viewPager);
            AddView(_buttonNextLayout);
            AddView(_buttonSkipLayout);
            AddView(_buttonFinishLayout);
        }

        private void RegisterForEvents() {
            _nextButton.Click += (sender, args) => {
                if (_viewPager.CurrentItem + 1 < _viewPagerAdapter.Count)
                    _viewPager.SetCurrentItem(_viewPager.CurrentItem + 1, true);

                ButtonNextCommand?.Execute(_viewPager.CurrentItem);
            };

            _skipButton.Click += (sender, args) => {
                Visibility = ViewStates.Gone;
                ButtonSkipCommand?.Execute(null);
            };

            _finishButton.Click += (sender, args) => {
                Visibility = ViewStates.Gone;
                ButtonFinishCommand?.Execute(null);
            };

            _viewPager.PageSelected += OnPageSelected;
        }

        private void OnPageSelected(object sender, ViewPager.PageSelectedEventArgs e) {
            _nextButton.Visibility = e.Position == _viewPagerAdapter.Count -1 ? ViewStates.Gone : ViewStates.Visible;
            _skipButton.Visibility = e.Position == _viewPagerAdapter.Count - 1 ? ViewStates.Gone : ViewStates.Visible;
            _finishButton.Visibility = e.Position == _viewPagerAdapter.Count - 1 ? ViewStates.Visible : ViewStates.Gone;
        }

        private void OnTranformPageEvent(object sender, TransformPageEventArgs e) {
            _viewPagerAdapter.GetFragmentByView(e.Page)?.Animate(e.Position);
        }
    }
}