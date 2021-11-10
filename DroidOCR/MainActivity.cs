using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Tesseract.Droid;

namespace DroidOCR
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            };

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // It's a good idea that you check this before accessing the data
            if (requestCode == 0 && resultCode == Result.Ok)
            {
                //get the image bitmap from the intent extras
                var image = (Bitmap)data.Extras.Get("data");

                // you might also like to check whether image is null or not
                // if (image == null) do something

                //convert bitmap into byte array
                byte[] bitmapData;
                using (var stream = new MemoryStream())
                {
                    image.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();
                }
                RunOCR(bitmapData);
     
            }
            // if you got here something bad happened...
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //RunOCR();
        }
        protected async void RunOCR(byte[] imagebyte)
        {
            TesseractApi api = new TesseractApi(this, AssetsDeployment.OncePerVersion);
            await api.Init("pol");
            await api.SetImage(imagebyte);
            string text = api.Text;

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Text = text;
        }


    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
