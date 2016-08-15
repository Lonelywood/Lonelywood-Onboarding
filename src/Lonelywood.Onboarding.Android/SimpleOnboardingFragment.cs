using Android.OS;
using Android.Views;

namespace Lonelywood.Onboarding.Android
{
    public class SimpleOnboardingFragment : OnboardingFragment
    {
        public static readonly string ArgLayout = "layout";

        private int _layout;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            if (Arguments != null) {
                _layout = Arguments.GetInt(ArgLayout);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            return inflater.Inflate(_layout, null, false);
        }

        public static OnboardingFragment NewInstance(int layout) {
            var result = new SimpleOnboardingFragment();

            var bundle = new Bundle();
            bundle.PutInt(ArgLayout, layout);
            result.Arguments = bundle;

            return result;
        }

        public override void Animate(float position) {
            // Do nothing
        }
    }
}