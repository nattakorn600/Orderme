using Android.App;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using oderme.Droid.Services;
using oderme.Services;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(GpsDependencyService))]
namespace oderme.Droid.Services
{
    public class GpsDependencyService : IGpsDependencyService
    {

        public bool IsGpsEnable()
        {
            bool value = false;
            Android.Locations.LocationManager manager = (Android.Locations.LocationManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.LocationService);
            if (!manager.IsProviderEnabled(Android.Locations.LocationManager.GpsProvider))
            {
                //gps disable
                value = false;
            }
            else
            {
                //Gps enable
                value = true;
            }
            return value;
        }

        public void OpenSettings()
        {
            turnOnGps();
            /*Intent intent = new Android.Content.Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intent);*/
        }
        public async void turnOnGps()
        {
            try
            {
                MainActivity activity = Forms.Context as MainActivity;

                GoogleApiClient googleApiClient = new GoogleApiClient.Builder(activity)
                    .AddApi(LocationServices.API).Build();
                googleApiClient.Connect();
                LocationRequest locationRequest = LocationRequest.Create();
                locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
                locationRequest.SetInterval(10000);
                locationRequest.SetFastestInterval(10000 / 2);

                LocationSettingsRequest.Builder
                        locationSettingsRequestBuilder = new LocationSettingsRequest.Builder()
                        .AddLocationRequest(locationRequest);
                locationSettingsRequestBuilder.SetAlwaysShow(false);
                LocationSettingsResult locationSettingsResult = await LocationServices.SettingsApi.CheckLocationSettingsAsync(
                    googleApiClient, locationSettingsRequestBuilder.Build());

                if (locationSettingsResult.Status.StatusCode == LocationSettingsStatusCodes.ResolutionRequired)
                {
                    locationSettingsResult.Status.StartResolutionForResult(activity, 0);
                }

            }
            catch (Exception ex)
            {
                var activity = (Activity)Forms.Context;
                activity.FinishAffinity();
            }
        }
    }
}