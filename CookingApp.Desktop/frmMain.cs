using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookingApp.Desktop
{
    public partial class frmMain : Form
    {
        private readonly System.Windows.Forms.Timer timer;

        public frmMain()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Tick += (sender, e) =>
            {
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            };

            timer.Interval = 1000;
            timer.Start();
        }

        


        private void btnStartCookingSync_Click(object sender, EventArgs e)
        {
            PrepareScreenForStarting();

            var sw = Stopwatch.StartNew();

            CrackEggs();
            MixEggs();
            AddSalt();
            TurnOnStove();
            HeatPan();
            PourOil();
            AddEgg();
            Cook();
            Serve();

            sw.Stop();
            AddLog();
            AddLog($"Total Cooking Duration: {sw.ElapsedMilliseconds:0} MS");
        }

        private async void btnAsync_Click(object sender, EventArgs e)
        {
            PrepareScreenForStarting();

            var sw = Stopwatch.StartNew();

            //This Tasks must run one after another
            var eggTaskGroup = await CrackEggsAsync()
                .ContinueWith(async _ =>
                {
                    await MixEggsAsync();
                    await AddSaltAsync();
                }, TaskScheduler.FromCurrentSynchronizationContext());

            //This Tasks must run one after another
            var painTaskGroup = await TurnOnStoveAsync()
                .ContinueWith(async _ =>
                {
                    await HeatPantAsync();
                    await PourOilAsync();
                    await AddEggAsync();
                }, TaskScheduler.FromCurrentSynchronizationContext());

            // A task that represents the completion of all of the supplied tasks
            await Task.WhenAll(eggTaskGroup, painTaskGroup);


            await CookAsync();
            await ServeAsync();



            sw.Stop();
            AddLog();
            AddLog($"Toplam Yemek Pişirme Süresi: {sw.ElapsedMilliseconds:0} MS");
        }


        #region Sync Methods

        public void CrackEggs()
        {
            Task.Delay(500).Wait();
            AddLog("Eggs Cracked");
            AdjustButtons(1);
        }

        public void MixEggs()
        {
            Task.Delay(750).Wait();
            AddLog("Eggs Mixed");
            AdjustButtons(2);

            // 1) --------            ------------
            // 2)         ------------
        }

        public void AddSalt()
        {
            Task.Delay(200).Wait();
            AddLog("Salt Added");
            AdjustButtons(3);
        }

        public void TurnOnStove()
        {
            Task.Delay(500).Wait();
            AddLog("Stove Turned On");
            AdjustButtons(4);
        }

        public void HeatPan()
        {
            Task.Delay(1000).Wait();
            AddLog("Pan Heated");
            AdjustButtons(5);
        }

        public void PourOil()
        {
            Task.Delay(750).Wait();
            AddLog("Oil Poured Into The Pan");
            AdjustButtons(6);
        }

        public void AddEgg()
        {
            Task.Delay(750).Wait();
            AddLog("Eggs Poured into the Pan");
            AdjustButtons(7);
        }

        public void Cook()
        {
            Task.Delay(2000).Wait();
            AddLog("Eggs Cooked");
            AdjustButtons(8);
        }

        public void Serve()
        {
            Task.Delay(750).Wait();
            AddLog("Eggs Served");
            AdjustButtons(9);
        }

        #endregion

        #region Async Methods

        public async Task CrackEggsAsync()
        {
            await Task.Delay(500);
            AddLog("Eggs Cracked");
            AdjustButtons(1);
        }

        public async Task MixEggsAsync()
        {
            await Task.Delay(750);
            AddLog("Eggs Mixed");
            AdjustButtons(2);
        }

        public async Task AddSaltAsync()
        {
            await Task.Delay(200);
            AddLog("Salt Added");
            AdjustButtons(3);
        }

        public async Task TurnOnStoveAsync()
        {
            await Task.Delay(500);
            AddLog("Stove Turned On");
            AdjustButtons(4);
        }

        public async Task HeatPantAsync()
        {
            await Task.Delay(1000);
            AddLog("Pan Heated");
            AdjustButtons(5);
        }

        public async Task PourOilAsync()
        {
            await Task.Delay(750);
            AddLog("Oil Poured Into The Pan");
            AdjustButtons(6);
        }

        public async Task AddEggAsync()
        {
            await Task.Delay(750);
            AddLog("Eggs Poured into the Pan");
            AdjustButtons(7);
        }

        public async Task CookAsync()
        {
            await Task.Delay(200);
            AddLog("Eggs Cooked");
            AdjustButtons(8);
        }

        public async Task ServeAsync()
        {
            await Task.Delay(750);
            AddLog("Eggs Served");
            AdjustButtons(9);
        }

        #endregion



        private void AddLog(string logStr = "")
        {
            if (!string.IsNullOrEmpty(logStr))
                logStr = $"[{DateTime.Now:dd:MM.yyyy HH:mm:ss}] - {logStr}";

            lbLogs.Items.Add(logStr);
            lbLogs.TopIndex = lbLogs.Items.Count - 1; // Locate the last row
        }

        private void PrepareScreenForStarting()
        {
            foreach (Control control in pnlButtons.Controls)
            {
                if (control is Button btn)
                    btn.BackColor = SystemColors.Control;
            }

            pnlButtons.Update();

            lbLogs.Items.Clear();
        }

        private void AdjustButtons(int step)
        {
            Button btn = pnlButtons.Controls[$"btnStep{step}"] as Button;

            btn.BackColor = Color.LimeGreen;
        }
    }
}
