using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitMultiplier v1mult;
        private UnitMultiplier v2mult;
        private UnitSymbol v1unit;
        private UnitSymbol v2unit;

        public DateTime StartTime { get => startTime; set => startTime = value; }

        public UnitMultiplier V1mult { get => v1mult; set => v1mult = value; }

        public UnitMultiplier V2mult { get => v2mult; set => v2mult = value; }

        public UnitSymbol V1unit { get => v1unit; set => v1unit = value; }

        public UnitSymbol V2unit { get => v1unit; set => v1unit = value; }

        public BasicIntervalSchedule(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if(base.Equals(obj))
            {
                BasicIntervalSchedule b = (BasicIntervalSchedule)obj;
                return (b.StartTime == this.StartTime && b.V1mult == this.V1mult && b.V2mult == this.V2mult && b.V1unit == this.V1unit && b.V2unit == b.V2unit);
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
                case ModelCode.BASINT_SCHEDULE_STIME:
                case ModelCode.BASINT_SCHEDULE_V1MULT:
                case ModelCode.BASINT_SCHEDULE_V2MULT:
                case ModelCode.BASINT_SCHEDULE_V1UNIT:
                case ModelCode.BASINT_SCHEDULE_V2UNIT:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.BASINT_SCHEDULE_STIME:
                    prop.SetValue(StartTime);
                    break;
                case ModelCode.BASINT_SCHEDULE_V1MULT:
                    prop.SetValue((short)V1mult);
                    break;
                case ModelCode.BASINT_SCHEDULE_V2MULT:
                    prop.SetValue((short)V2mult);
                    break;
                case ModelCode.BASINT_SCHEDULE_V1UNIT:
                    prop.SetValue((short)V1unit);
                    break;
                case ModelCode.BASINT_SCHEDULE_V2UNIT:
                    prop.SetValue((short)V2unit);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASINT_SCHEDULE_STIME:
                    StartTime = property.AsDateTime();
                    break;
                case ModelCode.BASINT_SCHEDULE_V1MULT:
                    V1mult = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.BASINT_SCHEDULE_V2MULT:
                    V2mult = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.BASINT_SCHEDULE_V1UNIT:
                    V2unit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.BASINT_SCHEDULE_V2UNIT:
                    V2unit = (UnitSymbol)property.AsEnum();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        #endregion IReference implementation
    }

}
