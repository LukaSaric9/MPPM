using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;
using GalaSoft.MvvmLight.Messaging;
using GUI.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GUI.Commands
{
    public class ImportApplyRemoveDataCommands
    {
        private CIMAdapter adapter = new CIMAdapter();
        private Delta nmsDelta = null;
        private string xml_path = "";

        public void ImportData()
        {
            ShowOpenCIMXMLFileDialog();
            ConvertCIMXMLToDMSNetworkModelDelta();
            ApplyDMSNetworkModelDelta();
        }

        #region HELPER METHODS TO OPEN FILE DIALOG AND IMPORT DATA TO DELTA
        private void ShowOpenCIMXMLFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open CIM Document File..";
            openFileDialog.Filter = "CIM-XML Files|*.xml;*.txt;*.rdf;*.xmi|All Files|*.*";
            openFileDialog.RestoreDirectory = true;

            bool? dialogResponse = openFileDialog.ShowDialog();

            if (dialogResponse == true)
            {
                xml_path = openFileDialog.FileName;
                Messenger.Default.Send(new StatusMessage($"Selected file: {xml_path}", "SteelBlue"));
            }
            else
            {
                xml_path = "";
                Messenger.Default.Send(new StatusMessage("No file selected.", "Firebrick"));
            }
        }

        private void ConvertCIMXMLToDMSNetworkModelDelta()
        {
            ////SEND CIM/XML to ADAPTER
            try
            {
                if (xml_path == "")
                {
                    Messenger.Default.Send(new StatusMessage("You must choose valid CIM/XML file!", "Firebrick"));
                    return;
                }

                nmsDelta = null;
                using (FileStream fs = File.Open(xml_path, FileMode.Open))
                {
                    nmsDelta = adapter.CreateDelta(fs, SupportedProfiles.RegulationSchedule, out _);
                }

                if (nmsDelta != null)
                {
                    //// export delta to file
                    using (XmlTextWriter xmlWriter = new XmlTextWriter(".\\deltaExport.xml", Encoding.UTF8))
                    {
                        xmlWriter.Formatting = Formatting.Indented;
                        nmsDelta.ExportToXml(xmlWriter);
                        xmlWriter.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                Messenger.Default.Send(new StatusMessage(String.Format("An error occurred. {0}", e.Message), "Firebrick"));
            }
        }

        private void ApplyDMSNetworkModelDelta()
        {
            //// APPLY Delta
            if (nmsDelta != null)
            {
                try
                {
                    string log = adapter.ApplyUpdates(nmsDelta);
                    nmsDelta = null;
                    Messenger.Default.Send(new StatusMessage("Import & Apply Delta has been completed succesfully!", "SteelBlue"));
                }
                catch (Exception e)
                {
                    Messenger.Default.Send(new StatusMessage(String.Format("An error occurred. {0}", e.Message), "Firebrick"));
                }
            }
            else
            {
                Messenger.Default.Send(new StatusMessage("No data is imported into delta object.", "Firebrick"));
            }
        }
        #endregion
    }
}
