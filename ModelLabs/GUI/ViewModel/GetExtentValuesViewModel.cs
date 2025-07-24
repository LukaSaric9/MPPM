using FTN.Common;
using GalaSoft.MvvmLight.Messaging;
using GUI.Commands;
using GUI.Helper;
using GUI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class GetExtentValuesViewModel : BindableBase
    {
        #region Fields and Properties

        private readonly GetExtentValuesCommands _commands = new GetExtentValuesCommands();

        private List<DMSType> _types;
        private ObservableCollection<ModelCode> _properties = new ObservableCollection<ModelCode>();
        private ObservableCollection<PropertiesView> _listedEntities = new ObservableCollection<PropertiesView>();
        private DMSType _selectedType;

        public ObservableCollection<ModelCode> ModelList { get; } = new ObservableCollection<ModelCode>();
        public ObservableCollection<ModelCode> SelectedModels { get; } = new ObservableCollection<ModelCode>();

        public List<DMSType> Types
        {
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
            }
        }

        public DMSType SelectedType
        {
            get => _selectedType;
            set
            {
                if (_selectedType != value)
                {
                    _selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));
                    LoadPropertiesForSelectedType();
                }
            }
        }

        public ObservableCollection<ModelCode> Properties
        {
            get => _properties;
            set
            {
                _properties = value;
                OnPropertyChanged(nameof(Properties));
            }
        }

        public ObservableCollection<PropertiesView> ListedEntities
        {
            get => _listedEntities;
            set
            {
                _listedEntities = value;
                OnPropertyChanged(nameof(ListedEntities));
            }
        }

        public IList SelectedItems
        {
            get => SelectedModels;
            set
            {
                foreach (var model in value.Cast<ModelCode>())
                {
                    if (!SelectedModels.Contains(model))
                    {
                        SelectedModels.Add(model);
                    }
                }
            }
        }

        #endregion

        #region Commands

        public MyICommand ClearPropertiesCommand { get; }
        public MyICommand ResetAllCommand { get; }
        public MyICommand GetExtentValuesCommand { get; }

        #endregion

        #region Constructor

        public GetExtentValuesViewModel()
        {
            InitializeTypes();

            ClearPropertiesCommand = new MyICommand(ClearProperties);
            ResetAllCommand = new MyICommand(ResetAll);
            GetExtentValuesCommand = new MyICommand(ExecuteGetExtentValues);

            ResetAll();
        }

        #endregion

        #region Private Methods

        private void InitializeTypes()
        {
            Types = MainWindowViewModel.modelResourceDesc.AllDMSTypes
                .Where(t => t != DMSType.MASK_TYPE)
                .ToList();
        }

        private void LoadPropertiesForSelectedType()
        {
            try
            {
                Properties = new ObservableCollection<ModelCode>(
                    MainWindowViewModel.modelResourceDesc.GetAllPropertyIds(SelectedType));
                SelectedModels.Clear();
                ListedEntities.Clear();
            }
            catch
            {
                Messenger.Default.Send(new StatusMessage("Failed to load properties.", "Firebrick"));
            }
        }

        private void ClearProperties()
        {
            SelectedModels.Clear();
        }

        private void ResetAll()
        {
            SelectedModels.Clear();
            SelectedType = Types.FirstOrDefault();
            ListedEntities.Clear();
            Messenger.Default.Send(new StatusMessage("Criteria has been reset.", "SteelBlue"));
        }

        private void ExecuteGetExtentValues()
        {
            if (SelectedType == DMSType.MASK_TYPE)
            {
                Messenger.Default.Send(new StatusMessage("You didn't choose a DMS type!", "Firebrick"));
                return;
            }

            if (SelectedModels.Count == 0)
            {
                Messenger.Default.Send(new StatusMessage("You must choose at least one property!", "Firebrick"));
                return;
            }

            Messenger.Default.Send(new StatusMessage("Executing query. Please wait...", "SteelBlue"));

            ListedEntities = _commands.GetExtentValues(SelectedType, SelectedModels.ToList());
        }

        #endregion
    }
}
