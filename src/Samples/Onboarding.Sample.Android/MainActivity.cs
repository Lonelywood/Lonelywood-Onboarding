using Android.OS;
using Android.App;
using Android.Content.PM;
using Android.Support.V7.App;
using Lonelywood.Onboarding.Android;
using Onboarding.Sample.Android.Onboarding;

namespace Onboarding.Sample.Android
{
    [Activity(Label = "Onboarding.Sample.Android", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main_activity);

            // Find onboarding layout
            var onboarding = FindViewById<LonelywoodOnboardingLayout>(Resource.Id.main_welcome_introduction);

            // Add pages to show at right positions
            onboarding.Fragments.Add(PageFragment.NewInstance(Resource.Layout.onboarding_page, Resource.Drawable.page_1));
            onboarding.Fragments.Add(PageFragment.NewInstance(Resource.Layout.onboarding_page, Resource.Drawable.page_2));
            onboarding.Fragments.Add(PageFragment.NewInstance(Resource.Layout.onboarding_page, Resource.Drawable.page_3));

            // Show onboarding layout
            onboarding.Show(SupportFragmentManager);
        }
    }
}