using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace FieldService {
	[Activity(Label = "View", Icon = "@drawable/icon",  
		ConfigurationChanges = ConfigChanges.Orientation|ConfigChanges.KeyboardHidden,
		ScreenOrientation = ScreenOrientation.Portrait)]		

	public class ViewData : MainActivity {

		private GestureDetector gestureDetector;

		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)	{
			if (e1.GetX() + 6 < e2.GetX() && Math.Abs(velocityX) >= 300) {
				Finish(); 
				StartActivity(typeof(ExtraView));
			} else if (e1.GetX() > e2.GetX() + 6 && Math.Abs(velocityX) >= 300) {
				Finish(); 
			}
			return true;
		}

		public override bool OnTouchEvent(MotionEvent e)	{
			gestureDetector.OnTouchEvent(e);
			return true;
		}

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.View);

			TextView Pen_output = FindViewById<TextView>(Resource.Id.PensNum);
			TextView Pamphlet_output = FindViewById<TextView>(Resource.Id.PamphletsNum);
			TextView CD_output = FindViewById<TextView>(Resource.Id.CDsNum);

			gestureDetector = new GestureDetector(this);

			//Data.Service.RefreshCache();
			Data.SumOfRecords();
			Pen_output.Text = Data.PensTotal.ToString();
			CD_output.Text = Data.CdsTotal.ToString();
			Pamphlet_output.Text = Data.PamphletsTotal.ToString();
		}
	}
}

