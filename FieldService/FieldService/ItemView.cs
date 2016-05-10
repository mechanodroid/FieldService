using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace FieldService {
	[Activity(Label = "Items Location", Icon = "@drawable/icon", 
		ConfigurationChanges = ConfigChanges.Orientation|ConfigChanges.KeyboardHidden,
		ScreenOrientation = ScreenOrientation.Portrait)]

	public class ItemView : MainActivity {
	
		private static string item_name;
		private string last_page, next_page;
		private GestureDetector gestureDetector;

		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)	{
			if (e1.GetX() + 6 < e2.GetX() && Math.Abs(velocityX) >= 300) {
				item_name = last_page;
			} else if (e1.GetX() > e2.GetX() + 6 && Math.Abs(velocityX) >= 300) {
				item_name = next_page;
			}
			Finish();
			StartActivity(typeof(ItemView));
			return true;
		}

		public override bool OnTouchEvent(MotionEvent e)	{
			gestureDetector.OnTouchEvent(e);
			return true;
		}

		public static string Item_name	{
			set	{ item_name = value; } 
		}

		private void initializeView(TextView title, TextView detail)	{
			title.Text = item_name;
			//Data.Service.RefreshCache();
			switch (item_name) {
				case "Pamphlets":
					last_page = "CDs";
					next_page = "Pens";
					detail.Text = Data.printItemLocation(0);
					break;
				case "Pens":
					last_page = "Pamphlets";
					next_page = "CDs";
					detail.Text = Data.printItemLocation(1);
					break;
				case "CDs":
					last_page = "Pens";
					next_page = "Pamphlets";
					detail.Text = Data.printItemLocation(2);
					break;
			}
		}

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.ItemView);

			TextView Item_Title = FindViewById<TextView>(Resource.Id.item_title);
			TextView Item_Location = FindViewById<TextView>(Resource.Id.item_location);

			Item_Location.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
			initializeView(Item_Title, Item_Location);
			gestureDetector = new GestureDetector(this);
		}
	}
}

