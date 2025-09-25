using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TreamentPlanReport.Models;

namespace TreamentPlanReport.Converters
{
	public class PatientShiftConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//calculate image path from shift
			if (value == null)
			{
				return null;
			}
			var shift = value as PatientShiftModel;
			switch (shift.ShiftDirection)
			{
				//go through shift directions
				case ShiftDirectionEnum.XDir:
					if (Math.Abs(shift.Magnitude) < 0.01)
					{
						return null;
					}
					else if (shift.Magnitude < 0.0)
					{
						//get executing assembly path and couch direction from magnitude.
						return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "PatientShift", "CouchLeft.jpg");
					}
					else
					{
						return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "PatientShift", "CouchRight.jpg");
					}
				case ShiftDirectionEnum.YDir:
					if (Math.Abs(shift.Magnitude) < 0.01)
					{
						return null;
					}
					else if (shift.Magnitude < 0.0)
					{
						return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "PatientShift", "CouchDown.jpg");
					}
					else
					{
						return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "PatientShift", "CouchUp.jpg");
					}
				case ShiftDirectionEnum.ZDir:
					if (Math.Abs(shift.Magnitude) < 0.01)
					{
						return null;
					}
					else if (shift.Magnitude < 0.0)
					{
						return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "PatientShift", "CouchOut.jpg");
					}
					else
					{
						return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "PatientShift", "CouchIn.jpg");
					}
				default:
					return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
