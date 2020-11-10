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
                        LoadFragment(Resource.Id.navigation_movies);
                        return;
                    case 1:
                        LoadFragment(Resource.Id.navigation_else);
                        return;
                }
            }
            
            LoadFragment(Resource.Id.navigation_movies);
        }

        private void NavItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        private int currentSelected = 0;
        private void LoadFragment(int id)
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

            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}