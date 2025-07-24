using FTN.Common;
using GUI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class PropertiesView : BindableBase
    {
        private ModelCode parentName;
        private List<PropertyView> properties = new List<PropertyView>();

        public PropertiesView() { }

        public ModelCode ParentName
        {
            get => parentName;

            set
            {
                if(parentName != value)
                {
                    parentName = value;
                    OnPropertyChanged(nameof(ParentName));
                }
            }
        }

        public List<PropertyView> Properties
        {
            get => properties;
            set
            {
                if (properties != value)
                {
                    properties = value;
                    OnPropertyChanged(nameof(Properties));
                }
            }
        }
    }
}
