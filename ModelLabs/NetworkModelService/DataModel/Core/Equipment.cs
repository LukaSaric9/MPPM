using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Equipment : PowerSystemResource
	{
		private bool aggregate;
		private bool noInService;

		public bool Aggregate { get =>  aggregate; set => aggregate = value; }

		public bool NoInService { get => noInService; set => noInService = value; }
						
		public Equipment(long globalId) : base(globalId) { }
	
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Equipment x = (Equipment)obj;
				return (x.Aggregate == this.Aggregate && x.NoInService == this.NoInService);
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
				case ModelCode.EQUIPMENT_AGG:
				case ModelCode.EQUIPMENT_NIS:
		
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.EQUIPMENT_AGG:
					property.SetValue(Aggregate);
					break;

				case ModelCode.EQUIPMENT_NIS:
					property.SetValue(NoInService);
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
				case ModelCode.EQUIPMENT_AGG:					
					Aggregate = property.AsBool();
					break;

				case ModelCode.EQUIPMENT_NIS:
					NoInService = property.AsBool();
					break;
			
				default:
					base.SetProperty(property);
					break;
			}
		}		

		#endregion IAccess implementation
	}
}
