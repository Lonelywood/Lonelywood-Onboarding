using System;
using System.Collections;
using System.Collections.Generic;
using Android.Views;
using Android.Runtime;
using Android.Support.V4.App;

namespace Lonelywood.Onboarding.Android
{
    public class OnboardingFragmentPagerAdapter : FragmentPagerAdapter
    {
        private readonly OnboardingFragment[] _fragments;
        private readonly Hashtable _retainedFragments = new Hashtable();

        public OnboardingFragmentPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public OnboardingFragmentPagerAdapter(FragmentManager fragmentManager) : base(fragmentManager) { }
        public OnboardingFragmentPagerAdapter(FragmentManager fragmentManager, List<OnboardingFragment> fragments) : base(fragmentManager) {
            _fragments = fragments.ToArray();
        }

        public override int Count => _fragments.Length;

        public override Fragment GetItem(int position) {
            if (position >= Count) return null;

            return _retainedFragments.ContainsKey(position) ? (OnboardingFragment)_retainedFragments[position] : _fragments[position];
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position) {
            var fragment = (Fragment) base.InstantiateItem(container, position);
            _retainedFragments.Add(position, fragment);
            return fragment;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj) {
            if (_retainedFragments.ContainsKey(position))
                _retainedFragments.Remove(position);

            base.DestroyItem(container, position, obj);
        }

        public OnboardingFragment GetFragmentAt(int position) {
            return _retainedFragments.ContainsKey(position) ? (OnboardingFragment)_retainedFragments[position] : null;
        }

        public OnboardingFragment GetFragmentByView(View view) {
            foreach (DictionaryEntry fragment in _retainedFragments) {
                if (((OnboardingFragment) fragment.Value).View == view)
                    return (OnboardingFragment) fragment.Value;
            }

            return null;
        }
    }
}