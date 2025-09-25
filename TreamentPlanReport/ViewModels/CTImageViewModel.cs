using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;


namespace TreamentPlanReport.ViewModels
{
	public class CTInfoViewModel
	{
		// ====== Data per image =======
		public string CtId { get; set; }

		//public string CtName { get; set; }

		//public string CtDescription { get; set; }

		public string CtSeriesId { get; set; }

		public string CtImageType { get; set; }

		public string CtAcquisitionDateTime { get; set; }

		public string CtNumberOfSlices { get; set; }

		public string CtSliceResolution { get; set; }

		public string CtSliceSize { get; set; }
		public string CtDescription { get; private set; }
		public string CTOrientation { get; set; }

		public string ImagingDeviceId { get; set; }

		public CTInfoViewModel(Image img)
		{
			//CtId = $"Show Id {ctDetails.bShowId}\nShow acq date {ctDetails.bShowAcquisitionDateTime}";
			// Harvest data from CT
			CtId = img.Id.Length > 1 ? img.Id : "-";
			//CtName = img.Name.Length > 1 ? img.Name : "-";
			CtNumberOfSlices = img.ZSize.ToString();
			CtSeriesId = img.Series.Id ?? "-";
			CtSliceResolution = $"{img.ZRes} mm";
			CtSliceSize = $"{img.XSize}px X {img.YSize}px";
			//CtDescription = img.Comment.Length > 1 ? img.Comment : "-";
			CtImageType = img.Series.Modality.ToString();
			CtAcquisitionDateTime = img.Series.Images.FirstOrDefault(I => I.ZSize == 1).CreationDateTime.ToString();
			CTOrientation = GetImageOrientation(img.ImagingOrientation);
			ImagingDeviceId = img.Series.ImagingDeviceId;


		}

		private string GetImageOrientation(PatientOrientation v)
		{
			switch (v)
			{
				case PatientOrientation.NoOrientation:
					return "No Orientation";
				case PatientOrientation.Sitting:
					return "Sitting";
				case PatientOrientation.FeetFirstDecubitusLeft:
					return "FF Decubitus Left";
				case PatientOrientation.FeetFirstDecubitusRight:
					return "FF Decubitus Right";
				case PatientOrientation.FeetFirstProne:
					return "FFP";
				case PatientOrientation.FeetFirstSupine:
					return "FFS";
				case PatientOrientation.HeadFirstDecubitusLeft:
					return "HF Decubitus Left";
				case PatientOrientation.HeadFirstDecubitusRight:
					return "HF Decubitus Right";
				case PatientOrientation.HeadFirstProne:
					return "HFP";
				case PatientOrientation.HeadFirstSupine:
					return "HFS";
				default:
					return "Unknown";
			}
		}
	}
}
