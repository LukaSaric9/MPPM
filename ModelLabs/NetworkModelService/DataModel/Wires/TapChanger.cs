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
    public class TapChanger : PowerSystemResource
    {
        private List<long> tSchedules = new List<long>();

        public List<long> TSchedules { get => tSchedules; set => tSchedules = value; }
        public TapChanger(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if(base.Equals(obj))
            {
                TapChanger t = (TapChanger)obj;
                return (CompareHelper.CompareLists(t.TSchedules,this.TSchedules,true));
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
            switch(property)
            {
                case ModelCode.TAPCHANGER_TS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch(property.Id)
            {
                case ModelCode.TAPCHANGER_TS:
                    property.SetValue(TSchedules);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return TSchedules.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (TSchedules != null && TSchedules.Count != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPCHANGER_TS] = TSchedules.GetRange(0, TSchedules.Count);  
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch(referenceId)
            {
                case ModelCode.TAPSCHEDULE_TC:
                    TSchedules.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch(referenceId)
            {
                case ModelCode.TAPSCHEDULE_TC:
                    if(TSchedules.Contains(globalId))
                    {
                        TSchedules.Remove(globalId);
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

        #endregion IReference implementation

    }
}
