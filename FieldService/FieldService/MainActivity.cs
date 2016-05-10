using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace FieldService {
	[Activity(Label = "Field Service", MainLauncher = true, Icon = "@drawable/icon", 
		ConfigurationChanges = ConfigChanges.Orientation|ConfigChanges.KeyboardHidden,
		ScreenOrientation = ScreenOrientation.Portrait)]

	public class MainActivity : Activity, GestureDetector.IOnGestureListener	{

		#region GestureListener Implementation
		//Reference: http://developer.xamarin.com/recipes/android/other_ux/gestures/create_a_gesture_listener/
		private GestureDetector gestureDetector;

		public void OnLongPress(MotionEvent e) {}

		public void OnShowPress(MotionEvent e) {}

		public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)	{
			return false;
		}

		public bool OnSingleTapUp(MotionEvent e)	{
			return false;
		}

		public bool OnDown(MotionEvent e)	{
			return false;
		}

		public virtual bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)	{
			if (e1.GetX() + 6 < e2.GetX() && Math.Abs(velocityX) >= 300) {
				StartActivity(typeof(ViewData));
			} else if (e1.GetX() > e2.GetX() + 6 && Math.Abs(velocityX) >= 300) {
				StartActivity(typeof(EnterData));
			}
			return true;
		}

		public override bool OnTouchEvent(MotionEvent e)	{
			gestureDetector.OnTouchEvent(e);
			return true;
		}
		#endregion

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			Button enter_button = FindViewById<Button>(Resource.Id.enter_button);
			Button view_button = FindViewById<Button>(Resource.Id.view_button);
			Button extra_button = FindViewById<Button>(Resource.Id.extra_button);

			gestureDetector = new GestureDetector(this);

			extra_button.Click += delegate {
				StartActivity(typeof(ExtraView));
			};

			enter_button.Click += delegate {
				StartActivity(typeof(EnterData));
			};

			view_button.Click += delegate {
				StartActivity(typeof(ViewData));
			};
		}
	}
}


