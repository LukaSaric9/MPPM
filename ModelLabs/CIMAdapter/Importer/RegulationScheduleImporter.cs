using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    public class RegulationScheduleImporter
    {
        /// <summary> Singleton </summary>
        private static RegulationScheduleImporter ptImporter = null;
        private static object singletoneLock = new object();

        private ConcreteModel concreteModel;
        private Delta delta;
        private ImportHelper importHelper;
        private TransformAndLoadReport report;


        #region Properties
        public static RegulationScheduleImporter Instance
        {
            get
            {
                if (ptImporter == null)
                {
                    lock (singletoneLock)
                    {
                        if (ptImporter == null)
                        {
                            ptImporter = new RegulationScheduleImporter();
                            ptImporter.Reset();
                        }
                    }
                }
                return ptImporter;
            }
        }

        public Delta NMSDelta
        {
            get
            {
                return delta;
            }
        }
        #endregion Properties


        public void Reset()
        {
            concreteModel = null;
            delta = new Delta();
            importHelper = new ImportHelper();
            report = null;
        }

        public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
        {
            LogManager.Log("Importing RegulationSchedule Elements...", LogLevel.Info);
            report = new TransformAndLoadReport();
            concreteModel = cimConcreteModel;
            delta.ClearDeltaOperations();

            if ((concreteModel != null) && (concreteModel.ModelMap != null))
            {
                try
                {
                    // convert into DMS elements
                    ConvertModelAndPopulateDelta();
                }
                catch (Exception ex)
                {
                    string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
                    LogManager.Log(message);
                    report.Report.AppendLine(ex.Message);
                    report.Success = false;
                }
            }
            LogManager.Log("Importing RegulationSchedule Elements - END.", LogLevel.Info);
            return report;
        }

        /// <summary>
        /// Method performs conversion of network elements from CIM based concrete model into DMS model.
        /// </summary>
        private void ConvertModelAndPopulateDelta()
        {
            LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            ImportConductingEquipments();
            ImportTerminals();
            ImportRegulatingControls();
            ImportTapChangers();
            ImportDayTypes();
            ImportSeasons();
            ImportTapSchedules();
            ImportRegulationSchedules();

            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
        }

        #region Import

        private void ImportTerminals()
        {
            SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
            if(cimTerminals !=  null )
            {
                foreach(KeyValuePair<string,object> cimTerminalPair in cimTerminals)
                {
                    FTN.Terminal cimTerminal = cimTerminalPair.Value as FTN.Terminal;

                    ResourceDescription rd = CreateTerminalResourceDescription(cimTerminal);
                    if(rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateTerminalResourceDescription(FTN.Terminal cimTerminal)
        {
            ResourceDescription rd = null;
            if (cimTerminal != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTerminal.ID, gid);

                ////populate ResourceDescription
                RegulationScheduleConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportRegulatingControls()
        {
            SortedDictionary<string, object> cimRegulatingControls = concreteModel.GetAllObjectsOfType("FTN.RegulatingControl");
            if (cimRegulatingControls != null)
            {
                foreach (KeyValuePair<string, object> cimRegControlPair in cimRegulatingControls)
                {
                    FTN.RegulatingControl cimRegulatingControl = cimRegControlPair.Value as FTN.RegulatingControl;

                    ResourceDescription rd = CreateRegulatingControlResourceDescription(cimRegulatingControl);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Regulating Control ID = ").Append(cimRegulatingControl.ID).Append(" SUCCESSFULY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimRegulatingControl.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateRegulatingControlResourceDescription(FTN.RegulatingControl cimRegulatingControl)
        {
            ResourceDescription rd = null;
            if (cimRegulatingControl != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGCONTROL, importHelper.CheckOutIndexForDMSType(DMSType.REGCONTROL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegulatingControl.ID, gid);

                ////populate ResourceDescription
                RegulationScheduleConverter.PopulateRegulatingControlProperties(cimRegulatingControl, rd,importHelper,report);
            }
            return rd;
        }

        private void ImportConductingEquipments()
        {
            SortedDictionary<string, object> cimEquipments = concreteModel.GetAllObjectsOfType("FTN.ConductingEquipment");
            if (cimEquipments != null)
            {
                foreach (KeyValuePair<string, object> pair in cimEquipments)
                {
                    FTN.ConductingEquipment cimObj = pair.Value as FTN.ConductingEquipment;
                    ResourceDescription rd = CreateConductingEquipmentResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ConductingEquipment ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ConductingEquipment ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateConductingEquipmentResourceDescription(FTN.ConductingEquipment cimObj)
        {
            ResourceDescription rd = null;
            if (cimObj != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CONDEQUIPMENT, importHelper.CheckOutIndexForDMSType(DMSType.CONDEQUIPMENT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimObj.ID, gid);
                RegulationScheduleConverter.PopulateConductingEquipmentProperties(cimObj, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportDayTypes()
        {
            SortedDictionary<string, object> cimDayTypes = concreteModel.GetAllObjectsOfType("FTN.DayType");
            if (cimDayTypes != null)
            {
                foreach (var pair in cimDayTypes)
                {
                    FTN.DayType cimObj = pair.Value as FTN.DayType;
                    ResourceDescription rd = CreateDayTypeResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("DayType ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("DayType ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateDayTypeResourceDescription(FTN.DayType cimObj)
        {
            ResourceDescription rd = null;
            if (cimObj != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DAYTYPE, importHelper.CheckOutIndexForDMSType(DMSType.DAYTYPE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimObj.ID, gid);
                RegulationScheduleConverter.PopulateDayTypeProperties(cimObj, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportSeasons()
        {
            SortedDictionary<string, object> cimSeasons = concreteModel.GetAllObjectsOfType("FTN.Season");
            if (cimSeasons != null)
            {
                foreach (var pair in cimSeasons)
                {
                    FTN.Season cimObj = pair.Value as FTN.Season;
                    ResourceDescription rd = CreateSeasonResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Season ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Season ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateSeasonResourceDescription(FTN.Season cimObj)
        {
            ResourceDescription rd = null;
            if (cimObj != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SEASON, importHelper.CheckOutIndexForDMSType(DMSType.SEASON));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimObj.ID, gid);
                RegulationScheduleConverter.PopulateSeasonProperties(cimObj, rd,importHelper, report);
            }
            return rd;
        }

        private void ImportTapChangers()
        {
            SortedDictionary<string, object> cimTapChangers = concreteModel.GetAllObjectsOfType("FTN.TapChanger");
            if (cimTapChangers != null)
            {
                foreach (var pair in cimTapChangers)
                {
                    FTN.TapChanger cimObj = pair.Value as FTN.TapChanger;
                    ResourceDescription rd = CreateTapChangerResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("TapChanger ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("TapChanger ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateTapChangerResourceDescription(FTN.TapChanger cimObj)
        {
            ResourceDescription rd = null;
            if (cimObj != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPCHANGER, importHelper.CheckOutIndexForDMSType(DMSType.TAPCHANGER));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimObj.ID, gid);
                RegulationScheduleConverter.PopulateTapChangerProperties(cimObj, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportTapSchedules()
        {
            SortedDictionary<string, object> cimTapSchedules = concreteModel.GetAllObjectsOfType("FTN.TapSchedule");
            if (cimTapSchedules != null)
            {
                foreach (var pair in cimTapSchedules)
                {
                    FTN.TapSchedule cimObj = pair.Value as FTN.TapSchedule;
                    ResourceDescription rd = CreateTapScheduleResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("TapSchedule ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("TapSchedule ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateTapScheduleResourceDescription(FTN.TapSchedule cimObj)
        {
            ResourceDescription rd = null;
            if (cimObj != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.TAPSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimObj.ID, gid);
                RegulationScheduleConverter.PopulateTapScheduleProperties(cimObj, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportRegulationSchedules()
        {
            SortedDictionary<string, object> cimSchedules = concreteModel.GetAllObjectsOfType("FTN.RegulationSchedule");
            if (cimSchedules != null)
            {
                foreach (var pair in cimSchedules)
                {
                    FTN.RegulationSchedule cimObj = pair.Value as FTN.RegulationSchedule;
                    ResourceDescription rd = CreateRegulationScheduleResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegulationSchedule ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegulationSchedule ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                    report.Report.AppendLine();
                }
            }
        }

        private ResourceDescription CreateRegulationScheduleResourceDescription(FTN.RegulationSchedule cimObj)
        {
            ResourceDescription rd = null;
            if (cimObj != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATIONSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULATIONSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimObj.ID, gid);
                RegulationScheduleConverter.PopulateRegulationScheduleProperties(cimObj, rd, importHelper, report);
            }
            return rd;
        }

        #endregion Import
    }
}

