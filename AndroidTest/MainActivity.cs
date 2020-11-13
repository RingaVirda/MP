using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using AndroidTest.Fragments;

namespace AndroidTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",
        MainLauncher = true, Description = "@string/org_name",
        Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(null);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var bottomNav = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNav.NavigationItemSelected += NavItemSelected;

            if (savedInstanceState != null)
            {
                switch (currentSelected)
                {
                    case 0:
                        LoadNavigation(Resource.Id.navigation_movies);
                        return;
                    case 1:
                        LoadNavigation(Resource.Id.navigation_else);
                        return;
                }
            }

            LoadNavigation(Resource.Id.navigation_movies);
        }

        private void NavItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadNavigation(e.Item.ItemId);
        }

        private int currentSelected = 0;

        private void LoadNavigation(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_movies:
                    fragment = MoviesListFragment.NewInstance();
                    currentSelected = 0;
                    break;
                case Resource.Id.navigation_else:
                    fragment = ElseFragment.NewInstance();
                    currentSelected = 1;
                    break;
            }

            if (fragment == null)
                return;
            
            LoadFragment(fragment);
        }

        public void LoadFragment(Fragment fragment)
        {
            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}