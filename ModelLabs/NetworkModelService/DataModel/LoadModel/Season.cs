using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class Season : IdentifiedObject
    {
        private DateTime startDate;
        private DateTime endDate;
        private List<long> sdts = new List<long>();

        public DateTime StartDate { get =>  startDate; set => startDate = value; }

        public DateTime EndDate { get => endDate; set => endDate = value; }

        public List<long> Sdts { get => sdts; set => sdts = value; }

        public Season(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Season s = (Season)obj;
                return (CompareHelper.CompareLists(s.Sdts, this.Sdts, true) && s.StartDate == this.StartDate && s.EndDate == this.EndDate);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess Implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SEASON_STARTDATE:
                case ModelCode.SEASON_ENDDATE:
                case ModelCode.SEASON_SDTS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEASON_STARTDATE:
                    property.SetValue(StartDate);
                    break;
                case ModelCode.SEASON_ENDDATE:
                    property.SetValue(EndDate);
                    break;
                case ModelCode.SEASON_SDTS:
                    property.SetValue(Sdts);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch(property.Id)
            {
                case ModelCode.SEASON_STARTDATE:
                    StartDate = property.AsDateTime();
                    break;
                case ModelCode.SEASON_ENDDATE:
                    EndDate = property.AsDateTime();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion

        #region IReference Implementation

        public override bool IsReferenced
        {
            get
            {
                return Sdts.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (Sdts != null && Sdts.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASON_SDTS] = Sdts.GetRange(0, Sdts.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SEASONDT_SCHEDULE_SE:
                    Sdts.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SEASONDT_SCHEDULE_SE:
                    if (Sdts.Contains(globalId))
                    {
                        Sdts.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }
        #endregion
    }
}
