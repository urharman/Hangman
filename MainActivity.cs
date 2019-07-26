using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;

namespace HelloWorld
{
	[Activity (Label = "Hangman", MainLauncher = true)]
	public class MainActivity : Activity
	{
        Button highs;
        public static EditText editText;
        public static String playerName;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainMenu);
            
			var buttonNewGame = FindViewById<Button> (Resource.Id.Button_NewGame);
			buttonNewGame.Click += (sender, e) => {
				Intent newGame= new Intent(this,typeof(Hangman));
                editText = (EditText)FindViewById(Resource.Id.player);
                playerName = (editText.Text).ToString();

                StartActivity(newGame);
			};

			var buttonExit = FindViewById<Button> (Resource.Id.Button_Exit);
			buttonExit.Click += (sender, e) => {
				this.Finish();
			};

        }
	}
}


