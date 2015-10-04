using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Text;

namespace iHelpful_Tirkx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TirkxWindow_Loaded(object sender, RoutedEventArgs e)
        {

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            //notifyIcon.Click += new EventHandler(notifyIcon_Click);
            //notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            //notifyIcon.Icon = IconHandles["QuickLaunch"];

            BackgroundWorker getListAnime = new BackgroundWorker();
            getListAnime.WorkerReportsProgress = true;
            getListAnime.WorkerSupportsCancellation = false;
            getListAnime.DoWork += getListAnime_DoWork;
            getListAnime.ProgressChanged += getListAnime_ProgressChanged;
            getListAnime.RunWorkerCompleted += getListAnime_RunWorkerCompleted;
            getListAnime.RunWorkerAsync();
        }

        void getListAnime_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Anime> listTirkx = new List<Anime>();

            Tirkx WebMain = new Tirkx();
            WebMain.Login("dvgamer","dvg7po8ai");

            string HashIings = "";
            //GET /main/tirkx_anime_list_home.php HTTP/1.1
            //Host: forum.tirkx.com
            //Connection: keep-alive
            //Cache-Control: max-age=0
            //Accept: */*
            //User-Agent: Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.107 Safari/537.36
            //X-Requested-With: XMLHttpRequest
            //Referer: http://forum.tirkx.com/main/index.php
            //Accept-Encoding: gzip,deflate,sdch
            //Accept-Language: th-TH,th;q=0.8,en-US;q=0.6,en;q=0.4,ca;q=0.2,es;q=0.2,fr;q=0.2,it;q=0.2,ko;q=0.2,ru;q=0.2,zh-CN;q=0.2,zh;q=0.2,zh-TW;q=0.2,ja;q=0.2
            //Cookie: bb_lastvisit=1391255717; HstCfa2334126=1391255725439; HstCmu2334126=1391255725439; bb_lastactivity=0; bb_userid=22200; bb_password=8c6e72b898af1e4152d21345090a3203; stCla2334126=1392408099840; HstPn2334126=4; HstPt2334126=127; HstCnv2334126=28; HstCns2334126=45
            //    */




        }

        void getListAnime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        void getListAnime_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage  < 1)
            {
                preDataProcess.Visibility = Visibility.Visible;
                preDataBlack.Visibility = Visibility.Visible;
            }
            else if (e.ProgressPercentage > 99)
            {
                preDataProcess.Visibility = Visibility.Hidden;
                preDataBlack.Visibility = Visibility.Hidden;
            }
            preDataProcess.Width = (this.Width * e.ProgressPercentage) / 100;

        }

        #region Title Panel MouseMove Event
        Point CurrentMouse;
        Point CurrentLocation;
        Boolean MouseMoveWindows = false;
        private void PanelTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrentMouse = new Point(e.GetPosition(this).X, e.GetPosition(this).Y);
            CurrentLocation = new Point(this.Top, this.Left);
            MouseMoveWindows = true;
        }
        private void PanelTitle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseMoveWindows = false;
        }
        private void PanelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMoveWindows)
            {
                Point locationPlus = new Point(e.GetPosition(this).X - CurrentMouse.X, e.GetPosition(this).Y - CurrentMouse.Y);
                this.Left += locationPlus.X;
                this.Top += locationPlus.Y;
                CurrentLocation = new Point(this.Top, this.Left);
            }
        }

        private void btnMinimum_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        private void btnDonate_Click(object sender, RoutedEventArgs e)
        {
            PanelDonate.Visibility = Visibility.Visible;
            ListNewAnime.Visibility = Visibility.Hidden;
        }

        private void btnQueues_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOngoing_Click(object sender, RoutedEventArgs e)
        {
            PanelDonate.Visibility = Visibility.Hidden;
            ListNewAnime.Visibility = Visibility.Visible;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            PanelDonate.Visibility = Visibility.Hidden;
            ListNewAnime.Visibility = Visibility.Visible;

        }
    }
}


    public class Anime
    {
        public String Filename { get; set; }
        public String Fansub { get; set; }

        public Anime(string filename, string fansub)
        {
            this.Filename = filename;
            this.Fansub = fansub;
        }
    }

    public class AnimeLists : List<Anime>
    {
        public AnimeLists()
        {


            //String listAnimeString = TirkxSocket.SocketSendReceive("forum.tirkx.com", "/main/tirkx_anime_list_home.php");
            //Console.WriteLine(listAnimeString);
            this.Add(new Anime("Fate Kaleid Liner Prisma Illya - 07 (1280x720 H264 AC3 TH)", "CaliburnCouvert & NoGod-FS"));
            this.Add(new Anime("Fantasista Doll - 04 (1280x720 x264 AAC-TH)", "VirusNeko-FS"));
            this.Add(new Anime("Kami nomi zo Shiru Sekai Megami Hen - 07 (TX 1280x720 x264 AC3)", "ASP-FS"));
        }
    }