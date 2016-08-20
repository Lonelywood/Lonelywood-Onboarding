using Android.Views;
using Android.Content;

namespace Lonelywood.Onboarding.Android
{
    public interface IIndicatorController
    {
        View Initialize(Context context, int slideCount);
        void SelectPosition(int position);
    }
}