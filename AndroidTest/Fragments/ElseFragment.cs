
using Android.App;
using Android.OS;
using Android.Views;


namespace AndroidTest.Fragments
{
    public class ElseFragment : Fragment
    {
        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.else_fragment, container, false);
            return view;
        }

        public static Fragment NewInstance()
        {
            var bundle = new Bundle();
            return new ElseFragment {Arguments = bundle};
        }
    }
}