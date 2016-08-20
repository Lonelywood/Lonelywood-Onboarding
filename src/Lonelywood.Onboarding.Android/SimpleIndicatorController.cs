using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Support.V4.Content;
using System.Collections.Generic;
using Android.Graphics.Drawables;

namespace Lonelywood.Onboarding.Android
{
    public class SimpleIndicatorController : IIndicatorController
    {
        private Drawable _selectedDrawable;
        private Drawable _unselectedDrawable;
        private readonly int _indicatorLayout;
        private readonly int _selectedIndicator;
        private readonly int _unselectedIndicator;
        private readonly List<AppCompatImageView> _indicators = new List<AppCompatImageView>();

        public SimpleIndicatorController(int selectedIndicator, int unselectedIndicator, int indicatorLayout = -1) {
            _selectedIndicator = selectedIndicator;
            _unselectedIndicator = unselectedIndicator;

            _indicatorLayout = indicatorLayout != -1 ? indicatorLayout : Resource.Layout.onboarding_simple_indicator_layout;
        }

        public View Initialize(Context context, int slideCount) {
            var result = View.Inflate(context, _indicatorLayout, null);

            var linearLayout = result.FindViewById<LinearLayout>(Resource.Id.lonelywood_onboarding_simple_indicator_layout);

            _selectedDrawable = ContextCompat.GetDrawable(context, _selectedIndicator);
            _unselectedDrawable = ContextCompat.GetDrawable(context, _unselectedIndicator);
            for (int i = 0; i < slideCount; i++) {
                var indicator = new AppCompatImageView(context);
                indicator.SetImageDrawable(_unselectedDrawable);

                var parameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                parameters.SetMargins(8,2,8,2);
                linearLayout.AddView(indicator, parameters);
                _indicators.Add(indicator);
            }

            _indicators[0].SetImageDrawable(_selectedDrawable);

            return result;
        }

        public void SelectPosition(int position) {
            foreach (var imageView in _indicators) {
                imageView.SetImageDrawable(_unselectedDrawable);
            }

            _indicators[position].SetImageDrawable(_selectedDrawable);
        }
    }
}