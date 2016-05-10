using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace FieldService {
	[Activity(Label = "Extra View", Icon = "@drawable/icon",  
		ConfigurationChanges = ConfigChanges.Orientation|ConfigChanges.KeyboardHidden,
		ScreenOrientation = ScreenOrientation.Portrait)]	

	public class ExtraView : MainActivity {

		private GestureDetector gestureDetector;

		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)	{
			if (e1.GetX() + 6 < e2.GetX() && Math.Abs(velocityX) >= 300) {
				Finish(); 
				StartActivity(typeof(EnterData));
			} else if (e1.GetX() > e2.GetX() + 6 && Math.Abs(velocityX) >= 300) {
				Finish(); 
				StartActivity(typeof(ViewData));
			}
			return true;
		}

		public override bool OnTouchEvent(MotionEvent e)	{
			gestureDetector.OnTouchEvent(e);
			return true;
		}

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.ExtraView);

			Button Pens_button = FindViewById<Button>(Resource.Id.pens_button);
			Button CDs_button = FindViewById<Button>(Resource.Id.cds_button);
			Button Pamphlets_button = FindViewById<Button>(Resource.Id.pamphlets_button);

			gestureDetector = new GestureDetector(this);

			Pens_button.Click += delegate {
				ItemView.Item_name = "Pens";
				StartActivity(typeof(ItemView));
			};

			CDs_button.Click += delegate {
				ItemView.Item_name = "CDs";
				StartActivity(typeof(ItemView));
			};

			Pamphlets_button.Click += delegate {
				ItemView.Item_name = "Pamphlets";
				StartActivity(typeof(ItemView));
			};
		}
	}
}

