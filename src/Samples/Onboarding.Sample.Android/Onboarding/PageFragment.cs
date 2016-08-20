using Android.OS;
using Android.Util;
using Android.Views;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Lonelywood.Onboarding.Android;
using Square.Picasso;

namespace Onboarding.Sample.Android.Onboarding
{
    public class PageFragment : OnboardingFragment
    {
        public static readonly string ArgLayout = "layout";
        public static readonly string ArgBackground = "background";

        private int _layout;
        private int _background;

        private AppCompatImageView _backgroundImageView;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            if (Arguments != null) {
                _layout = Arguments.GetInt(ArgLayout);
                _background = Arguments.GetInt(ArgBackground);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            var view =  inflater.Inflate(_layout, null, false);

            _backgroundImageView = view.FindViewById<AppCompatImageView>(Resource.Id.onboarding_background_page_image);

            var dims = new DisplayMetrics();
            Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>().DefaultDisplay.GetMetrics(dims);

            Picasso.With(Context)
                .Load(_background)
                .Resize(dims.WidthPixels, dims.HeightPixels)
                .CenterCrop()
                .Into(_backgroundImageView);

            return view;
        }

        public static OnboardingFragment NewInstance(int layout, int background) {
            var result = new PageFragment();

            var bundle = new Bundle();
            bundle.PutInt(ArgLayout, layout);
            bundle.PutInt(ArgBackground, background);
            result.Arguments = bundle;

            return result;
        }

        public override void Animate(float position) {

        }
    }
}