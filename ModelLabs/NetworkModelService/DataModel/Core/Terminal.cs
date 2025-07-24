using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {

        private List<long> regControls = new List<long>();
        public List<long> RegControls { get => regControls; set => regControls = value; }

        private long condEquipment = 0;
        public long CondEquipment { get => condEquipment; set => condEquipment = value; }

        public Terminal(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal t = (Terminal)obj;
                return (CompareHelper.CompareLists(t.RegControls, this.RegControls, true) && t.CondEquipment == this.CondEquipment);
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
                case ModelCode.TERMINAL_CONDEQ:
                case ModelCode.TERMINAL_REGCONTROL:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch(property.Id)
            {
                case ModelCode.TERMINAL_CONDEQ:
                    property.SetValue(CondEquipment);
                    break;
                case ModelCode.TERMINAL_REGCONTROL:
                    property.SetValue(RegControls);
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
                case ModelCode.TERMINAL_CONDEQ:
                    CondEquipment = property.AsReference();
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
                return (RegControls.Count != 0 || base.IsReferenced);
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if(CondEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONDEQ] = new List<long>();
                references[ModelCode.TERMINAL_CONDEQ].Add(CondEquipment);
            }

            if(RegControls != null && RegControls.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_REGCONTROL] = RegControls.GetRange(0,RegControls.Count);
            } 

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch(referenceId)
            {
                case ModelCode.REG_CONTROL_TERMINAL:
                    RegControls.Add(globalId); 
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
                case ModelCode.REG_CONTROL_TERMINAL:
                    if(RegControls.Contains(globalId))
                    {
                        RegControls.Remove(globalId);
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
