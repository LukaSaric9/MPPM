using GalaSoft.MvvmLight.Messaging;
using GUI.Commands;
using GUI.Helper;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        private string title;
        private string subtitle;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Subtitle
        {
            get { return subtitle; }
            set
            {
                subtitle = value;
                OnPropertyChanged("Subtitle");
            }
        }

        public MyICommand ImportXml {  get; set; }
        public MyICommand ResetAll {  get; set; }

        public HomeViewModel()
        {
            Title = "GDA Client";
            Subtitle = "Use navigation for different GDA methods!";

            ImportXml = new MyICommand(ImportXmlData);
            ResetAll = new MyICommand(ClearData);
        }

        private void ImportXmlData()
        {
            new ImportApplyRemoveDataCommands().ImportData();
        }

        private void ClearData()
        {
            try
            {
                string solutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
                string dataPath = Path.Combine(solutionDirectory, "NetworkModelData.data");

                if (File.Exists(dataPath))
                {
                    File.Delete(dataPath);
                    Messenger.Default.Send(new StatusMessage("NetworkModelData.data file deleted successfully.", "SteelBlue"));
                }
                else
                {
                    Messenger.Default.Send(new StatusMessage("NetworkModelData.data file does not exist.", "Firebrick"));
                }
            }
            catch (Exception ex)
            {
                Messenger.Default.Send(new StatusMessage($"An error occurred: {ex.Message}", "Firebrick"));
            }
        }
    }
}
