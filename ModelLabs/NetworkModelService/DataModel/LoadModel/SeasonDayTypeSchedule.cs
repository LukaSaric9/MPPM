using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class SeasonDayTypeSchedule : RegularIntervalSchedule
    {
        private long season = 0;
        private long dayType = 0;

        public long Season { get =>  season; set => season = value; }
        public long DayType { get => dayType; set => dayType = value; }

        public SeasonDayTypeSchedule(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeasonDayTypeSchedule s = (SeasonDayTypeSchedule)obj;
                return (s.Season == this.Season && s.DayType == this.DayType);
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
                case ModelCode.SEASONDT_SCHEDULE_DT:
                case ModelCode.SEASONDT_SCHEDULE_SE:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SEASONDT_SCHEDULE_DT:
                    prop.SetValue(DayType);
                    break;
                case ModelCode.SEASONDT_SCHEDULE_SE:
                    prop.SetValue(Season);
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
                case ModelCode.SEASONDT_SCHEDULE_DT:
                    DayType = property.AsReference();
                    break;
                case ModelCode.SEASONDT_SCHEDULE_SE:
                    Season = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if(DayType != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASONDT_SCHEDULE_DT] = new List<long>();
                references[ModelCode.SEASONDT_SCHEDULE_DT].Add(DayType);
            }
            if (Season != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASONDT_SCHEDULE_SE] = new List<long>();
                references[ModelCode.SEASONDT_SCHEDULE_SE].Add(Season);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
