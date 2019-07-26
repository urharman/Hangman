using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections;
using Android.Content;
using System.IO;

namespace HelloWorld
{
	[Activity (Label = " ")]			
	public class Hangman : Activity
	{
        int i = 0;
        int TotalScore;
        int failedAttempts = 0;
        ImageView[] hangmanBodyArray;
        GridView gridView;
        ArrayAdapter adapter;
        ArrayList gridKeyboard;
        String currentWord;
        TextView scoreview;
        TextView MainText;
        Button Button_SubmitScore;
        LinearLayout AnswerLayout;

        public static String[] PlayerList;
        public TextView[] charView;
        
        public static int highscore;
        public static String PlayerName;
        public static String summary;

        String[] questions = {"JAPAN'S FAMOUS MOTOR COMPANY", "GOOGLE CEO's FIRST NAME?", "CAPITAL OF ITALY", "CAPITAL OF INDIA?","METAL SYMBOL LIQUID AT ROOM TEMP"};
        String[] answers = {"HONDA","SUNDAR", "ROME","DELHI","Hg"};

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
                        SetContentView(Resource.Layout.GameLayout);

            gridView = FindViewById<GridView>(Resource.Id.gridView);
            getData();
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, gridKeyboard);
            gridView.Adapter = adapter;
            gridView.ItemClick += gridView_ItemClick;

            AnswerLayout = (LinearLayout) FindViewById(Resource.Id.currentWordLayout);
            sequence();

            MainText = (TextView)FindViewById(Resource.Id.question);
             
            Button_SubmitScore = (Button) FindViewById(Resource.Id.submit);

            Button_SubmitScore.Click +=delegate {
                write();
                MainActivity.editText.Text = "";
                Toast.MakeText(this, "Final Score: " + TotalScore, ToastLength.Long).Show();
                this.Finish();
            };
            
            MainText.Text = questions[i];
            scoreview = (TextView)FindViewById(Resource.Id.score);
            
            play();

        }

    void gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            String c = gridKeyboard[e.Position].ToString();
            gridKeyboard[e.Position] = " ";

            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, gridKeyboard);
            gridView.Adapter = adapter;

            if (answers[i].Contains(c))
            {
                currentWord = currentWord + c;

                for(int n = 0; n < answers[i].Length; n++)
                {
                    if (charView[n].Text == c)
                    {
                        charView[n].SetTextColor(Android.Graphics.Color.White);
                    }
                }

                if (answers[i].Length == currentWord.Length)
                {
                    getData();
                    adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, gridKeyboard);
                    gridView.Adapter = adapter;
                    Toast.MakeText(this, "Bravo!", ToastLength.Short).Show();
                    i++;
                    TotalScore++;
                    scoreview.Text = MainActivity.playerName+" Score: " + TotalScore.ToString();
                    PlayerName = MainActivity.playerName;
                    highscore = TotalScore;
                    MainText.Text = questions[i];
                    currentWord = "";
                    AnswerLayout.RemoveAllViews();
                    sequence();
                    failedAttempts = 0;
                    play();
                }
            }
            else
            {
                hangmanBodyArray[failedAttempts].Visibility = ViewStates.Visible;
                failedAttempts++;
                if (failedAttempts == 6)
                {
                    Toast.MakeText(this, "Wrong answer", ToastLength.Short).Show();
                    Toast.MakeText(this, "Answer is: "+answers[i], ToastLength.Long).Show();
                    Toast.MakeText(this, MainActivity.playerName+" Total Score: " + TotalScore, ToastLength.Long).Show();
                    PlayerName = MainActivity.playerName;
                    MainActivity.editText.Text = "";
                    highscore = TotalScore;
                    write();
                    this.Finish();
                    i++;
                }
            }

        }

        //FILL DATA
        private void getData()
        {
            gridKeyboard = new ArrayList();
            gridKeyboard.Add("A");
            gridKeyboard.Add("B");
            gridKeyboard.Add("C");
            gridKeyboard.Add("D");
            gridKeyboard.Add("E");
            gridKeyboard.Add("F");
            gridKeyboard.Add("G");
            gridKeyboard.Add("H");
            gridKeyboard.Add("I");
            gridKeyboard.Add("J");
            gridKeyboard.Add("K");
            gridKeyboard.Add("L");
            gridKeyboard.Add("M");
            gridKeyboard.Add("N");
            gridKeyboard.Add("O");
            gridKeyboard.Add("P");
            gridKeyboard.Add("Q");
            gridKeyboard.Add("R");
            gridKeyboard.Add("S");
            gridKeyboard.Add("T");
            gridKeyboard.Add("U");
            gridKeyboard.Add("V");
            gridKeyboard.Add("W");
            gridKeyboard.Add("X");
            gridKeyboard.Add("Y");
            gridKeyboard.Add("Z");

        }

        private void play()
        {
            hangmanBodyArray = new ImageView[6];
            hangmanBodyArray[0] = (ImageView) FindViewById(Resource.Id.head);
            hangmanBodyArray[1] = (ImageView)FindViewById(Resource.Id.body);
            hangmanBodyArray[2] = (ImageView)FindViewById(Resource.Id.left_arm);
            hangmanBodyArray[3] = (ImageView)FindViewById(Resource.Id.right_arm);
            hangmanBodyArray[4] = (ImageView)FindViewById(Resource.Id.left_leg);
            hangmanBodyArray[5] = (ImageView)FindViewById(Resource.Id.right_leg);

            for (int p = 0; p < 6; p++)
            {
                hangmanBodyArray[p].Visibility = ViewStates.Invisible;
            }

        }

        public void write()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = System.IO.Path.Combine(path, "newlog.txt");


            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(PlayerName+": "+highscore);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                PlayerList = new string[5];
                PlayerList = content.Split('\n');

            }
        }

        private void sequence()
        {
            charView = new TextView[answers[i].Length];

            for (int k = 0; k < answers[i].Length; k++)
            {
                charView[k] = new TextView(this);
                charView[k].Text = "" + answers[i][k];
                charView[k].SetTextColor(Android.Graphics.Color.Black);
                AnswerLayout.AddView(charView[k]);
            }
        }
    }
}

