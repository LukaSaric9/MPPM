using FTN.Common;
using GalaSoft.MvvmLight.Messaging;
using GUI.Commands;
using GUI.Helper;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class GetValuesViewModel : BindableBase
    {
        public MyICommand GetValuesCommand { get; set; }
        public MyICommand ResetFormCommand { get; set; }
        public MyICommand ClearProperties {  get; set; }

        private GetValuesCommands commands = new GetValuesCommands();

        private List<DMSType> types = new List<DMSType>();
        private ObservableCollection<long> gids = new ObservableCollection<long>();
        private ObservableCollection<ModelCode> properties = new ObservableCollection<ModelCode>();
        private ObservableCollection<PropertyView> listedProperties = new ObservableCollection<PropertyView>();

        private DMSType selectedType;
        private long selectedGid = -1;

        public ObservableCollection<ModelCode> ModelList { get; set; }
        public ObservableCollection<ModelCode> SelectedModels { get; set; }

        public ObservableCollection<PropertyView> ListedProperties
        {
            get => listedProperties;

            set
            {
                if(value != listedProperties)
                {
                    listedProperties = value;
                    OnPropertyChanged("ListedProperties");
                }
            }
        }

        public long SelectedGid
        {
            get => selectedGid;
            set
            {
                if (selectedGid != value)
                {
                    selectedGid = value;
                    ListedProperties.Clear();
                    OnPropertyChanged("SelectedGid");
                }
            }
        }

        public DMSType SelectedType
        {
            get => selectedType;
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged("SelectedType");

                    try
                    {
                        Gids.Clear();
                        if (selectedType != DMSType.MASK_TYPE)
                        {
                            Gids = commands.GetGIDs(selectedType);
                            Properties = new ObservableCollection<ModelCode>(
                                MainWindowViewModel.modelResourceDesc.GetAllPropertyIds(SelectedType));
                        }
                        SelectedModels.Clear();
                        SelectedGid = -1;
                        ListedProperties.Clear();
                    }
                    catch
                    {
                        if (Gids != null)
                        {
                            Gids.Clear();
                            SelectedGid = -1;
                        }
                    }
                }
            }
        }

        public ObservableCollection<long> Gids
        {
            get => gids;
            private set
            {
                if (gids != value)
                {
                    gids = value;
                    OnPropertyChanged("Gids");
                }
            }
        }
        public System.Collections.IList SelectedItems
        {
            get
            {
                return SelectedModels;
            }
            set
            {
                foreach (ModelCode model in value)
                {
                    if (!SelectedModels.Contains(model))
                    {
                        SelectedModels.Add(model);
                    }
                }
            }
        }

        public List<DMSType> Types
        {
            get => types;
            set
            {
                if (types != value)
                {
                    types = value;
                    OnPropertyChanged("Types");
                }

            }
        }

        public ObservableCollection<ModelCode> Properties
        {
            get => properties;
            set
            {
                if (properties != value)
                {
                    properties = value;
                    OnPropertyChanged("Properties");
                }
            }
        }


        public GetValuesViewModel()
        {
            if(types !=  null && types.Count == 0)
            {
                Types = MainWindowViewModel.modelResourceDesc.AllDMSTypes.ToList();
                Types.Remove(DMSType.MASK_TYPE);
            }

            ModelList = new ObservableCollection<ModelCode>();
            SelectedModels = new ObservableCollection<ModelCode>();

            ClearProperties = new MyICommand(ClearPropertiesCollection);
            ResetFormCommand = new MyICommand(ResetAllForm);
            GetValuesCommand = new MyICommand(GetValuesFromNMSCriteria);

            ResetAllForm();
        }

        private void ClearPropertiesCollection()
        {
            SelectedModels.Clear();
        }

        private void ResetAllForm()
        {
            SelectedModels.Clear();
            SelectedType = (DMSType)1;
            SelectedGid = -1;
            ListedProperties.Clear();
            Messenger.Default.Send(new StatusMessage("Criteria has been resetted.", "SteelBlue"));
        }

        public void GetValuesFromNMSCriteria()
        {
            if (SelectedType == DMSType.MASK_TYPE)
            {
                Messenger.Default.Send(new StatusMessage("You didn't choose a DMS type!", "Firebrick"));
                return;
            }
            if (SelectedGid == -1)
            {
                Messenger.Default.Send(new StatusMessage("You didn't choose a Global ID!", "Firebrick"));
                return;
            }
            if (SelectedModels.Count == 0)
            {
                Messenger.Default.Send(new StatusMessage("You must choose atleast one property!", "Firebrick"));
                return;
            }

            Messenger.Default.Send(new StatusMessage("Executing query. Please wait...", "SteelBlue"));
            ListedProperties = commands.GetValues(selectedGid, SelectedModels.ToList());
        }

    }
}
