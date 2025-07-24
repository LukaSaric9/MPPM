using FTN.Common;
using GalaSoft.MvvmLight.Messaging;
using GUI.Helper;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace GUI.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<Window> CloseWindowCommand { get; private set; }
        public MyICommand<string> NavCommand { get; private set; }

        private string statusMessage = "GDA Client is ready. All systems are operative!";
        private string backgroundColor = "SteelBlue";
        private string currentTime;

        public HomeViewModel homeViewModel;
        public GetValuesViewModel getValuesViewModel;
        public GetExtentValuesViewModel getExtentViewModel;
        public GetRelatedValuesViewModel getRelatedValuesViewModel;
        private BindableBase currentViewModel;

        public static ModelResourcesDesc modelResourceDesc = new ModelResourcesDesc();

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);

            getValuesViewModel = new GetValuesViewModel();
            homeViewModel = new HomeViewModel();
            getExtentViewModel = new GetExtentValuesViewModel();
            getRelatedValuesViewModel = new GetRelatedValuesViewModel();
            CurrentViewModel = homeViewModel;

            Messenger.Default.Register<StatusMessage>(this, SetMessage);

            CloseWindowCommand = new MyICommand<Window>(CloseWindow);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            timer.Start();
        }

        public string StatusMessage
        {
            get { return statusMessage; }
            set
            {
                if (statusMessage != value)
                {
                    statusMessage = value;
                    OnPropertyChanged("StatusMessage");
                }
            }
        }

        public string BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                if (backgroundColor != value)
                {
                    backgroundColor = value;
                    OnPropertyChanged("BackgroundColor");
                }
            }
        }

        public string CurrentTime
        {
            get => currentTime;
            set
            {
                if (currentTime != value)
                {
                    currentTime = value;
                    OnPropertyChanged(nameof(CurrentTime));
                }
            }
        }

        public void SetMessage(StatusMessage message)
        {
            if (message != null)
            {
                StatusMessage = message.Message;
                BackgroundColor = message.BackgroundColor;
            }
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    Messenger.Default.Send(new StatusMessage("Welcome to GDA Client - All services are operating normally.", "SteelBlue"));
                    CurrentViewModel = homeViewModel;
                    break;
                case "get":
                    CurrentViewModel = getValuesViewModel;
                    break;
                case "extent":
                    CurrentViewModel = getExtentViewModel;
                    break;
                case "related":
                    CurrentViewModel = getRelatedValuesViewModel;
                    break;
            }
        }

        private void CloseWindow(Window MainWindow)
        {
            MainWindow.Close();
        }
    }
}
