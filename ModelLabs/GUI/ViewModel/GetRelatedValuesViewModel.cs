using FTN.Common;
using GalaSoft.MvvmLight.Messaging;
using GUI.Commands;
using GUI.Helper;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class GetRelatedValuesViewModel : BindableBase
    {
            private readonly GetRelatedValuesCommands _commands = new GetRelatedValuesCommands();

            public MyICommand ClearSelectedPropertiesCommand { get; set; }
            public MyICommand ResetFormCommand { get; set; }
            public MyICommand ExecuteGetRelatedValuesCommand { get; set;  }

            public ObservableCollection<ModelCode> AvailableProperties { get; private set; } = new ObservableCollection<ModelCode>();
            public ObservableCollection<ModelCode> SelectedProperties { get; private set; } = new ObservableCollection<ModelCode>();
            public ObservableCollection<ModelCode> AvailableReferences { get; private set; } = new ObservableCollection<ModelCode>();
            public ObservableCollection<ModelCode> TypePropertyCodes { get; private set; } = new ObservableCollection<ModelCode>();
            public ObservableCollection<ModelCode> ModelCodes { get; private set; } = new ObservableCollection<ModelCode>();

            public ObservableCollection<long> AvailableGids { get; private set; } = new ObservableCollection<long>();
            public ObservableCollection<PropertiesView> ResultProperties { get; private set; } = new ObservableCollection<PropertiesView>();
            public List<DMSType> AvailableTypes { get; private set; } = new List<DMSType>();

            private DMSType _selectedType;
            public DMSType SelectedType
            {
                get => _selectedType;
                set
                {
                    if (_selectedType != value)
                    {
                        _selectedType = value;
                        UpdateOnSelectedTypeChange();
                        OnPropertyChanged("SelectedType");
                    }
                }
            }

            private long _selectedGid = -1;
            public long SelectedGid
            {
                get => _selectedGid;
                set
                {
                    _selectedGid = value;
                    ResultProperties.Clear();
                    OnPropertyChanged("SelectedGid");
                }
            }

            private ModelCode _selectedReference;
            public ModelCode SelectedReference
            {
                get => _selectedReference;
                set
                {
                    _selectedReference = value;
                    UpdateAvailableProperties();
                    OnPropertyChanged("SelectedReference");
                }
            }

            public GetRelatedValuesViewModel()
            {
                LoadAvailableTypes();

                ClearSelectedPropertiesCommand = new MyICommand(() => SelectedProperties.Clear());
                ResetFormCommand = new MyICommand(ResetForm);
                ExecuteGetRelatedValuesCommand = new MyICommand(ExecuteGetRelatedValues);

                ResetForm();
            }

            private void LoadAvailableTypes()
            {
                var allTypes = MainWindowViewModel.modelResourceDesc.AllDMSTypes.ToList();
                allTypes.Remove(DMSType.MASK_TYPE);

                AvailableTypes = allTypes
                    .Where(type => MainWindowViewModel.modelResourceDesc.GetAllPropertyIds(type)
                        .Any(mc => IsReference(mc)))
                    .ToList();
            }

            private void ResetForm()
            {
                SelectedProperties.Clear();
                ResultProperties.Clear();
                AvailableReferences.Clear();
                TypePropertyCodes.Clear();
                AvailableProperties.Clear();
                AvailableGids.Clear();

                SelectedType = AvailableTypes.FirstOrDefault();
                SelectedGid = -1;
                SelectedReference = 0;

                Messenger.Default.Send(new StatusMessage("Criteria has been reset.", "SteelBlue"));
            }

            private void UpdateOnSelectedTypeChange()
            {
                try
                {
                    AvailableGids = _commands.GetGIDs(SelectedType);

                    if (AvailableGids.Any())
                    {
                        SelectedGid = AvailableGids.First();
                    }
                    else
                    {
                        SelectedGid = -1;
                    }

                TypePropertyCodes = new ObservableCollection<ModelCode>(
                        MainWindowViewModel.modelResourceDesc.GetAllPropertyIds(SelectedType)
                    );
                    AvailableReferences = GetReferenceModelCodes();
                }
                catch
                {
                    AvailableGids.Clear();
                    SelectedGid = -1;
                }

                SelectedProperties.Clear();
                ResultProperties.Clear();
                AvailableProperties.Clear();
                SelectedReference = 0;

                OnPropertyChanged(nameof(AvailableReferences));
                OnPropertyChanged(nameof(AvailableGids));
            }

        private void UpdateAvailableProperties()
        {
            SelectedProperties.Clear();

            try
            {
                string[] parts = SelectedReference.ToString().Split('_');
                string typePart = parts[parts.Length - 1]; // Last part

                string modelCodeName;

                // First try to parse it directly as ModelCode
                if (Enum.TryParse(typePart, true, out ModelCode parsedCode) &&
                    Enum.IsDefined(typeof(ModelCode), parsedCode))
                {
                    modelCodeName = typePart;
                }
                // If that fails, try mapped name
                else if (ReferenceToModelCodeName.TryGetValue(typePart, out string mappedName))
                {
                    modelCodeName = mappedName;
                }
                else
                {
                    throw new Exception($"Unknown model code reference: {typePart}");
                }

                var targetTypeCode = MainWindowViewModel.modelResourceDesc.GetModelCodeFromModelCodeName(modelCodeName);

                AvailableProperties = new ObservableCollection<ModelCode>(
                    MainWindowViewModel.modelResourceDesc.GetAllPropertyIds(targetTypeCode));
            }
            catch
            {
                AvailableProperties.Clear();
            }

            OnPropertyChanged(nameof(AvailableProperties));
        }


        private ObservableCollection<ModelCode> GetReferenceModelCodes()
            {
                var references = new ObservableCollection<ModelCode>();

                foreach (var mc in MainWindowViewModel.modelResourceDesc.GetAllPropertyIds(SelectedType))
                {
                    if (IsReference(mc))
                        references.Add(mc);
                }

                return references;
            }

            private static bool IsReference(ModelCode mc)
            {
                long code = (long)mc;
                return (code & 0x9) == 0x9 || (code & 0x19) == 0x19;
            }

            private void ExecuteGetRelatedValues()
            {
                if (SelectedType == DMSType.MASK_TYPE)
                {
                    ShowError("You didn't choose a DMS type!");
                    return;
                }
                if (SelectedGid == -1)
                {
                    ShowError("You didn't choose a Global ID!");
                    return;
                }
                if (SelectedReference == 0)
                {
                    ShowError("You must choose a valid reference!");
                    return;
                }
                if (!SelectedProperties.Any())
                {
                    ShowError("You must choose at least one property!");
                    return;
                }

                Messenger.Default.Send(new StatusMessage("Executing query. Please wait...", "SteelBlue"));

                var association = new Association
                {
                    PropertyId = SelectedReference,
                    Type = 0
                };

                ResultProperties = _commands.GetRelatedValues(SelectedGid, SelectedProperties.ToList(), association, SelectedReference);
                OnPropertyChanged(nameof(ResultProperties));
            }

            private void ShowError(string message)
            {
                Messenger.Default.Send(new StatusMessage(message, "Firebrick"));
            }

        private static readonly Dictionary<string, string> ReferenceToModelCodeName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
            { "REGCONTROL", "REG_CONTROL" },
            { "TERMINALS", "TERMINAL" },
            { "RS", "REG_SCHEDULE" },
            { "TS", "TAPSCHEDULE" },
            { "TC", "TAPCHANGER" },
            { "DT", "DAYTYPE" },
            { "SE", "SEASON" },
            { "SDTS", "SEASONDT_SCHEDULE" },
            { "RC", "REG_CONTROL" },
        };

    }
}

