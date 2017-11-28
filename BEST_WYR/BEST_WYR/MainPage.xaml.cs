using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using PCLStorage;

namespace BEST_WYR
{
    public partial class MainPage : ContentPage
    {
        List<Scenario> Scenario;
        string json;
        Random r = new Random();
        string api = "http://progco.fatcow.com/sefdstuff/apis/WYR_database.txt";
        View start, game;
        string lFolder = "testvdvfeb";
        string lFile = "saveData13.txt";
        string localData = "[{'scene':'listen to only Christmas music for the rest of your life','category':'music'},{'scene':'make out with a stalker that has threatened your life, for 30 minutes','category':'romance'},{'scene':'only be able to eat/drink protien shakes for the rest of your life','category':'diet'},{'scene':'never be able to use any Google products ever again','category':'internet'},{'scene':'not kill someone but have everyone think you did','category':'crime'},{'scene':'have all limbs surgically removed','category':'surgery'},{'scene':'get severe hiccups every 15 minutes','category':'annoying'},{'scene':'be buried alive with a blowtorch & man eating ants','category':'death'},{'scene':'have a deep itch that takes more than 20 scratches that show up every minute','category':'annoying'},{'scene':'need permission before being able to do absolutely anything','category':'annoying'},{'scene':'know the exact place & time of your death','category':'death'},{'scene':'never be able to wear socks and shoes ever again','category':'pain'},{'scene':'jump from a 1 story roof & land onto scattered legos barefoot','category':'pain'},{'scene':'pee your pants once a week at a random time','category':'annoying'},{'scene':'work 4 days a week but work 20 hour days','category':'annoying'},{'scene':'have absolutely no one you have met or will meet like or respect you','category':'pain'},{'scene':'bare knuckle fight a pissed off Mike Tyson in his prime','category':'pain'},{'scene':'only be able to use the internet 20 minutes a day','category':'internet'},{'scene':'have to pay the government a tax for every word you speak','category':'annoying'},{'scene':'have to eat a very moldy meal once a week','category':'diet'},{'scene':'lick the seat of every porta potty at Cochella','category':'diet'},{'scene':'intentionally break a glass bottle on your own head not covered by insurance','category':'pain'},{'scene':'erase every computer from history','category':'internet'},{'scene':'have to pay EVERYTHING in nothing but pennies for the rest of your life','category':'annoying'},{'scene':'get kicked in the genitalia everytime you use a noun','category':'pain'},{'scene':'gain super-human sense of smell, but only for horrible vomit enducing smells','category':'gross'},{'scene':'have ANY teeth you have fall out if you dont whistle for at least 1 out of evey 4 hours','category':'annoying'},{'scene':'be 1/100 humans to survive a major catestrophic event','category':'pain'},{'scene':'not able remember anything new from this point on & have your memory reset every 6 hours','category':'pain'},{'scene':'volunteer for a pointless space mission that has a 25% chance of returning to Earth','category':'death'},{'scene':'have something installed into your eyes that permanently show advertisments that block 80% of your vision','category':'annoying'},{'scene':'be limited to communicate one word a minute','category':'annoying'},{'scene':'have your total bank balances taken away from you every 24 hours','category':'annoying'},{'scene':'replace every cat & dog on Earth with a brain eating zombie','category':'death'},{'scene':'have everything you consume with water be replaced with dirty bath water','category':'diet'},{'scene':'jam a tooth pick into your big toe nail & kick the wall as hard as you can','category':'pain'},{'scene':'hear all voices & sounds at an annoying nails-on-chalkboard high pitch','category':'pain'},{'scene':'have a childhood bully embarrassingly come out of nowhere & pants you anytime you try to make a good impression','category':'annoying'},{'scene':'have all the favorite parts of songs you like be replaced with the Amber Alert noise, & you forget it will happen every time','category':'music'},{'scene':'have to lick a persons face every time they say a sentence to you','category':'gross'},{'scene':'have to drink nothing but earwax whenever you get thirsty','category':'gross'},{'scene':'have to step on a set bear trap until it slams shut on your leg every 9 months ','category':'pain'},{'scene':'be stuck on a space station alone with no communication but have lifetime supply of canned beans & water, with 100% certainty of rescue, but not sure if it will be in 1 hour or 25 years','category':'death'},{'scene':'have to rip out all of your hair & every finger & toe nail once a year with no anesthetics ','category':'pain'}]";

        // 
        public MainPage()
        {
            InitializeComponent();
            FetchData();

            start = this.FindByName<View>("Start");

            Button sButt = this.FindByName<Button>("sBut");

            CheckFile();

            game = this.FindByName<View>("Game");
            game.IsVisible = false;

        }

        /// <summary>
        /// Loads the database from file
        /// </summary>
        /// <param name="rf"></param>
        /// <returns></returns>
        async Task<string> loadDataBase()
        {
            IFolder rf = FileSystem.Current.LocalStorage;
            IFolder pf = rf.GetFolderAsync(lFolder).Result;
            IFile iF = pf.GetFileAsync(lFile).Result;
            return iF.ReadAllTextAsync().Result;
        }

        /// <summary>
        /// This checks to see if the file even exists
        /// </summary>
        /// <param name="rf"></param>
        /// <returns></returns>
        async Task CheckFile()
        {
            print("Morpheus!");

            // Grab ref to root directory
            IFolder root = FileSystem.Current.LocalStorage;
            print(root.Path);

            // check if the data folder exists
            bool fCheck1 = (root.CheckExistsAsync(lFolder).Result == ExistenceCheckResult.FolderExists);
            print((fCheck1) ? "data folder exists" : "data folder = no");

            if(!fCheck1)
            {
                print("now creating it...");

                IFolder dataFo = await root.CreateFolderAsync(lFolder, CreationCollisionOption.ReplaceExisting);
                print("data folder creation success");

                print("starting for file");
                IFile dataFi = await dataFo.CreateFileAsync(lFile, CreationCollisionOption.ReplaceExisting);
                await dataFi.WriteAllTextAsync(localData);
                print("file created! in");
            }
            else
            {
                print("checking for file");
                IFolder dataFo = root.GetFolderAsync(lFolder).Result;
                print("got data fo ref");

                bool fCheck2 = (dataFo.CheckExistsAsync(lFile).Result == ExistenceCheckResult.FileExists);
                print((fCheck2) ? "data file exists": "data file = no");

                // 
                if (!fCheck2)
                {
                print("starting for file");
                    IFile dataFi = await dataFo.CreateFileAsync(lFile, CreationCollisionOption.ReplaceExisting);
                    await dataFi.WriteAllTextAsync(localData);
                    print("file created! out");
                }
            }
        }

        /// <summary>
        /// Save your data onto your data file
        /// </summary>
        /// <param name="inp"></param>
        async void SaveData(string inp)
        {
            IFolder root = FileSystem.Current.LocalStorage;
                IFolder dataFo = root.GetFolderAsync(lFolder).Result;
                    IFile dataFi = dataFo.GetFileAsync(lFile).Result;
            await dataFi.WriteAllTextAsync(inp);
        }

        /// <summary>
        /// For printing to console debugging
        /// </summary>
        /// <param name="Fold1"></param>
        private static void print(object Fold1)
        {
            System.Diagnostics.Debug.WriteLine(Fold1);
        }


        // 
        void Next(object sender, EventArgs args)
        {
            Button a = this.FindByName<Button>("A");
            Button b = this.FindByName<Button>("B");

            try
            {
                int r1 = r.Next(0, Scenario.Count);
                int r2 = r.Next(0, Scenario.Count);

                while (r2 == r1)
                {
                    r2 = r.Next(0, Scenario.Count);
                }

                a.Text = Scenario[r1].scene;
                b.Text = Scenario[r2].scene;
            }
            catch
            {
                Scenario = JsonConvert.DeserializeObject<List<Scenario>>(localData);

                int r1 = r.Next(0, Scenario.Count);
                int r2 = r.Next(0, Scenario.Count);

                while (r2 == r1)
                {
                    r2 = r.Next(0, Scenario.Count);
                }

                a.Text = Scenario[r1].scene;
                b.Text = Scenario[r2].scene;
            }
        }

        // 
        void Begin(object sender, EventArgs args)
        {
            start.VerticalOptions = LayoutOptions.Start;
            start.IsVisible = false;
            game.IsVisible = true;
                Next();
        }

        // 
        void Next()
        {
            Button a = this.FindByName<Button>("A");
            Button b = this.FindByName<Button>("B");

            try
            {
                int r1 = r.Next(0, Scenario.Count);
                int r2 = r.Next(0, Scenario.Count);

                while (r2 == r1)
                {
                    r2 = r.Next(0, Scenario.Count);
                }

                a.Text = Scenario[r1].scene;
                b.Text = Scenario[r2].scene;
            }
            catch
            {
                Scenario = JsonConvert.DeserializeObject<List<Scenario>>(localData);

                int r1 = r.Next(0, Scenario.Count);
                int r2 = r.Next(0, Scenario.Count);

                while (r2 == r1)
                {
                    r2 = r.Next(0, Scenario.Count);
                }

                a.Text = Scenario[r1].scene;
                b.Text = Scenario[r2].scene;
            }
        }

        async Task<string> FetchData()
        {
            try
            {
                json = await new HttpClient().GetStringAsync(api);
                Scenario = JsonConvert.DeserializeObject<List<Scenario>>(json);
                SaveData(json);
                print(loadDataBase().Result);
            }
           catch
            {
                json = loadDataBase().Result;
                Scenario = JsonConvert.DeserializeObject<List<Scenario>>(json);
                print(json);
            }

            return json;
        }
    }


    public class Scenario
    {
        public string scene { get; set; }
        public string category { get; set; }
    }

}