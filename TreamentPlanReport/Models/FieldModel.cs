using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace TreamentPlanReport.Models
{
	public class FieldModel
	{
		//Field Properties
		public string FieldID { get; set; }
		public string FieldName { get; set; }
		public string MachineID { get; set; }
		public string MachineType { get; set; }
		public string Technique { get; set; }
		public string MLCType { get; set; }
		public string EnergyMode { get; set; }
		public double DoseRate { get; set; }

		//Geometric Properties
		public string Gantry { get; set; }
		public double Collimator { get; set; }
		public double Couch { get; set; }
		public double X1 { get; set; }
		public double X2 { get; set; }
		public double FieldX { get; set; }
		public double Y1 { get; set; }
		public double Y2 { get; set; }
		public double FieldY { get; set; }
		public string Isocenter { get; set; }
		public double SSD { get; set; }
		public double MU { get; set; }
		//DRR Image
		public BitmapSource DRR { get; set; }
		public FieldModel(Beam beam, PlanSetup plan)
		{
			//_beam = beam;
			int varianScale = beam.TreatmentUnit.MachineScaleDisplayName.Contains("Carian") ? -1 : 1;
			FieldID = beam.Id;
			FieldName = beam.Name;
			MachineID = beam.TreatmentUnit.Id;
			MachineType = beam.TreatmentUnit.MachineModelName;
			Technique = beam.Technique.Id;
			MLCType = beam.MLCPlanType.ToString();
			EnergyMode = beam.EnergyModeDisplayName;
			DoseRate = beam.DoseRate;
			Gantry = GetGantry(beam);
			Collimator = beam.ControlPoints.First().CollimatorAngle;
			Couch = beam.ControlPoints.First().PatientSupportAngle;
			X1 = beam.ControlPoints.Min(cp => cp.JawPositions.X1) / 10.0;
			X2 = beam.ControlPoints.Max(cp => cp.JawPositions.X2) / 10.0;
			FieldX = X2 - X1;
			Y1 = beam.ControlPoints.Min(cp => cp.JawPositions.Y1) / 10.0;
			Y2 = beam.ControlPoints.Max(cp => cp.JawPositions.Y2) / 10.0;
			FieldY = Y2 - Y1;
			Isocenter = GetIsocenter(beam, plan);
			//SSD = beam.Meterset.Value;
			SSD = beam.SSD / 10.0;
			MU = beam.Meterset.Value;
			DRR = BuildDRR(beam);
		}

		public BitmapSource BuildDRR(Beam beam)
		{
			if (beam.ReferenceImage == null) { return null; }
			var drr = beam.ReferenceImage;
			int[,] pixels = new int[drr.YSize, drr.XSize];
			drr.GetVoxels(0, pixels);//get image pixels out of ESAPI.
			int[] flat_pixels = new int[drr.YSize * drr.XSize];
			//lay out pixels into single array
			for (int i = 0; i < drr.YSize; i++)
			{
				for (int ii = 0; ii < drr.XSize; ii++)
				{
					flat_pixels[i + drr.XSize * ii] = pixels[i, ii];
				}
			}
			//translate into byte array
			var drr_max = flat_pixels.Max();
			var drr_min = flat_pixels.Min();
			PixelFormat format = PixelFormats.Gray8;//low res image, but only 1 byte per pixel. 
			int stride = (drr.XSize * format.BitsPerPixel + 7) / 8;
			byte[] image_bytes = new byte[stride * drr.YSize];
			for (int i = 0; i < flat_pixels.Length; i++)
			{
				double value = flat_pixels[i];
				image_bytes[i] = Convert.ToByte(255 * ((value - drr_min) / (drr_max - drr_min)));
			}
			//build the bitmapsource.
			return BitmapSource.Create(drr.XSize, drr.YSize, 25.4 / drr.XRes, 25.4 / drr.YRes,
					format, null, image_bytes, stride);
		}

		private string GetIsocenter(Beam beam, PlanSetup plan)
		{
			var isocenter = beam.IsocenterPosition;
			var userIso = plan.StructureSet.Image.DicomToUser(isocenter, plan);
			return $"({userIso.x / 10.0:F1}, {userIso.y / 10.0:F1}, {userIso.z / 10.0:F1})";
		}

		private string GetGantry(Beam beam)
		{
			if (beam.Technique.Id.Contains("ARC"))
			{
				return $"{beam.ControlPoints.First().GantryAngle}" +
						$" {(beam.GantryDirection == GantryDirection.Clockwise ? "CW" : "CCW")} " +
						$"{beam.ControlPoints.Last().GantryAngle}";
			}
			return beam.ControlPoints.First().GantryAngle.ToString();
		}
	}
}
