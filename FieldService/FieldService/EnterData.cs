using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Locations;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace FieldService {
	[Activity(Label = "Enter Data", Icon = "@drawable/icon", 
		ConfigurationChanges = ConfigChanges.Orientation|ConfigChanges.KeyboardHidden,
		ScreenOrientation = ScreenOrientation.Portrait)]	

	public class EnterData : MainActivity, ILocationListener	{

		#region LocationListener Implementation
		//Reference: http://developer.xamarin.com/guides/android/platform_features/maps_and_location/location/
		Location currentLocation = null;
		LocationManager locationManager;
		String locationProvider;

		public void OnLocationChanged(Location location) {
			currentLocation = location;
		}

		public void OnProviderDisabled(string provider) {}

		public void OnProviderEnabled(string provider) {}
	
		public void OnStatusChanged(string provider, Availability status, Bundle extras) {}

		protected override void OnResume()	{
				base.OnResume();
			if (!(locationProvider == String.Empty)) 
				locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
		}

		protected override void OnPause()	{
			base.OnPause();
			locationManager.RemoveUpdates(this);
		}

		private void InitializeLocationManager()	{
			locationManager = GetSystemService(LocationService) as LocationManager;
			Criteria criteriaForLocationService = new Criteria	{Accuracy = Accuracy.Fine};
			IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())	{
				locationProvider = acceptableLocationProviders.First();
			} else {
				locationProvider = String.Empty;
			}
		}
		#endregion

		private GestureDetector gestureDetector;

		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)	{
			if (e1.GetX() + 6 < e2.GetX() && Math.Abs(velocityX) >= 300) {
				Finish(); 
			} else if (e1.GetX() > e2.GetX() + 6 && Math.Abs(velocityX) >= 300) {
				Finish(); 
				StartActivity(typeof(ExtraView));
			}
			return true;
		}

		public override bool OnTouchEvent(MotionEvent e)	{
			gestureDetector.OnTouchEvent(e);
			return true;
		}

		private void InputCheck(EditText pens, EditText pamphlets, EditText cds)	{
			if(pens.Text == "")
				pens.Text = "0";
			if(pamphlets.Text == "")
				pamphlets.Text = "0";
			if(cds.Text == "")
				cds.Text = "0";
		}

		private void displayAlertDialog()	{
			RunOnUiThread(() =>	{
					AlertDialog.Builder builder;
					builder = new AlertDialog.Builder(this);
					builder.SetTitle("Data summitted");
					builder.SetMessage("The data have been summitted.");
					builder.SetCancelable(false);
					builder.SetPositiveButton("OK", delegate {Finish();});
					builder.Show();
				}
			);
		}

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.EnterData);

			Button summit = FindViewById<Button>(Resource.Id.summit);
			EditText pens_input = FindViewById<EditText>(Resource.Id.pensInput);
			EditText pamphlets_input = FindViewById<EditText>(Resource.Id.pamphletsInput);
			EditText cds_input = FindViewById<EditText>(Resource.Id.cdsInput);
			TextView GPS_status = FindViewById<TextView>(Resource.Id.GPSstatus);

			InitializeLocationManager();
			gestureDetector = new GestureDetector(this);

			if (locationProvider == String.Empty) {
				GPS_status.Text = "Your GPS is currently off.";
			}

			summit.Click += delegate {
				InputCheck(pens_input, pamphlets_input, cds_input);

				String coordinate;
				int Pamphlet_amount = Convert.ToInt32(pamphlets_input.Text);
				int Pen_amount = Convert.ToInt32(pens_input.Text);
				int CD_amount = Convert.ToInt32(cds_input.Text);

				if(currentLocation != null)	{	
					coordinate = "(" + currentLocation.Latitude + ", " + currentLocation.Longitude + ")";
				} else {
					coordinate = "Unknown Location";
				}
					
				Data.AddRecord(Pamphlet_amount, Pen_amount, CD_amount, coordinate);
				OnPause();
				displayAlertDialog();
			};
		}
	}
}

