using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace TreamentPlanReport.Models
{
	public class ReferencePointModel
	{
		public ReferencePoint ReferencePoint { get; set; }
		public string LocationX { get; set; }
		public string LocationY { get; set; }
		public string LocationZ { get; set; }
		public string DailyDoseLimit { get; set; }
		public string PatientVolumeId { get; set; }
		public string Id { get; set; }

		public ReferencePointModel(ReferencePoint referencePoint, PlanSetup ps)
		{
			ReferencePoint = referencePoint;
			// Replace this line in ReferencePointModel constructor:
			// PatientVolumeId = referencePoint.PatientVolumeId;

			// With the following, which sets PatientVolumeId to "-" or another appropriate default value
			PatientVolumeId = "NA";
			//PatientVolumeId = referencePoint.PatientVolumeId.ToString();
			DailyDoseLimit = referencePoint.DailyDoseLimit.ToString();
			Id = referencePoint.Id.ToString();
			LocationX = "-";
			LocationY = "-";
			LocationZ = "-";


			if (referencePoint.HasLocation(ps))
			{
				var vec = referencePoint.GetReferencePointLocation(ps);
				//LocationX = vec.x.ToString();
				//LocationY = vec.y.ToString();
				//LocationZ = vec.z.ToString();
				LocationX = $"{vec.x:F2}";  // Format to 2 decimals
				LocationY = $"{vec.y:F2}";  // Format to 2 decimals
				LocationZ = $"{vec.z:F2}";  // Format to 2 decimals
																		//MessageBox.Show($"Ref point {Id} has location");
			}
		}
	}
}
