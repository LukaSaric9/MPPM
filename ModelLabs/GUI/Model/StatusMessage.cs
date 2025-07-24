using GUI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GUI.Model
{
    public class StatusMessage : BindableBase
    {
        private string message;
        private string backgroundColor;

        public string Message
        {
            get => message;

            set
            {
                if(message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public string BackgroundColor
        {
            get => backgroundColor;

            set
            {
                if(backgroundColor != value)
                {
                    backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        public StatusMessage(string message, string backgroundColor)
        {
            this.message = message;
            this.backgroundColor = backgroundColor;
        }
    }
}
