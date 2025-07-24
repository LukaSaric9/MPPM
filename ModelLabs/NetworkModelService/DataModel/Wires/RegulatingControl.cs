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
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode mPhase;
        private float tRange;
        private float tValue;
        private long terminal = 0;
        private List<long> regSchedules = new List<long>();

        public bool Discrete { get =>  discrete; set => discrete = value; }

        public RegulatingControlModeKind Mode { get => mode; set => mode = value; }

        public PhaseCode MPhase { get => mPhase; set => mPhase = value; }

        public float TRange { get => tRange; set => tRange = value; }

        public float TValue { get => tValue; set => tValue = value; }

        public long Terminal { get => terminal; set => terminal = value; }

        public List<long> RegSchedules { get => regSchedules; set => regSchedules = value; }

        public RegulatingControl(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl r = (RegulatingControl)obj;
                return (CompareHelper.CompareLists(r.RegSchedules,this.RegSchedules,true) && r.Terminal == this.Terminal && r.Discrete == this.Discrete 
                    && r.Mode == this.Mode && r.MPhase == this.MPhase && r.TRange == this.TRange && r.TValue == this.TValue);
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
            switch(property)
            {
                case ModelCode.REG_CONTROL_DISCRETE:
                case ModelCode.REG_CONTROL_MODE:
                case ModelCode.REG_CONTROL_MONITPHASE:
                case ModelCode.REG_CONTROL_TARGETR:
                case ModelCode.REG_CONTROL_TARGETV:
                case ModelCode.REG_CONTROL_TERMINAL:
                case ModelCode.REG_CONTROL_RS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch(property.Id)
            {
                case ModelCode.REG_CONTROL_DISCRETE:
                    property.SetValue(Discrete);
                    break;
                case ModelCode.REG_CONTROL_MODE:
                    property.SetValue((short)Mode);
                    break;
                case ModelCode.REG_CONTROL_MONITPHASE:
                    property.SetValue((short)MPhase);
                    break;
                case ModelCode.REG_CONTROL_TARGETR:
                    property.SetValue(TRange);
                    break;
                case ModelCode.REG_CONTROL_TARGETV:
                    property.SetValue(TValue);
                    break;
                case ModelCode.REG_CONTROL_TERMINAL:
                    property.SetValue(Terminal);
                    break;
                case ModelCode.REG_CONTROL_RS:
                    property.SetValue(RegSchedules);
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
                case ModelCode.REG_CONTROL_DISCRETE:
                    Discrete = property.AsBool();
                    break;
                case ModelCode.REG_CONTROL_MODE:
                    Mode = (RegulatingControlModeKind)property.AsEnum();
                    break;
                case ModelCode.REG_CONTROL_MONITPHASE:
                    MPhase = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.REG_CONTROL_TARGETR:
                    TRange = property.AsFloat();
                    break;
                case ModelCode.REG_CONTROL_TARGETV:
                    TValue = property.AsFloat();
                    break;
                case ModelCode.REG_CONTROL_TERMINAL:
                    Terminal = property.AsReference();
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }
        #endregion

        #region IReference Implementation

        public override bool IsReferenced
        {
            get
            {
                return RegSchedules.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if(Terminal != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.REG_CONTROL_TERMINAL] = new List<long>();
                references[ModelCode.REG_CONTROL_TERMINAL].Add(Terminal);
            }
            if (RegSchedules != null && RegSchedules.Count != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.REG_CONTROL_RS] = RegSchedules.GetRange(0, RegSchedules.Count);
            }

            base.GetReferences(references, refType);

        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch(referenceId)
            {
                case ModelCode.REG_SCHEDULE_RC:
                    RegSchedules.Add(globalId);
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
                case ModelCode.REG_SCHEDULE_RC:
                    if(RegSchedules.Contains(globalId))
                    {
                        RegSchedules.Remove(globalId);
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
