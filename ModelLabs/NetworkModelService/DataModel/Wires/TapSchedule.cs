using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TapSchedule : SeasonDayTypeSchedule
    {
        private long tChanger = 0;

        public long TChanger { get =>  tChanger; set => tChanger = value; }

        public TapSchedule(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapSchedule t = (TapSchedule)obj;
                return (t.TChanger == this.TChanger);
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

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.TAPSCHEDULE_TC:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TAPSCHEDULE_TC:
                    property.SetValue(TChanger);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TAPSCHEDULE_TC:
                    TChanger = property.AsReference();
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (TChanger != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPSCHEDULE_TC] = new List<long>();
                references[ModelCode.TAPSCHEDULE_TC].Add(TChanger);  
            }
            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
