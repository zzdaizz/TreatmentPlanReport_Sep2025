using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace TreamentPlanReport.ViewModels
{
	public class PatientInfoViewModel
	{
		// define properties for UI binding
		public string PatientId { get; set; }
		public string PatientLastName { get; set; }
		public string PatientFirstName { get; set; }
		public string DateOfBirth { get; set; }
		public string Hospital { get; set; }
		public string PrimaryOncologist { get; set; }

		//create constructor
		//constructor shares the same name as the classname
		//constructor has no return type
		//constructor is a great place to initialize properties (and pass in parameters
		public PatientInfoViewModel(Patient patient)
		{
			PatientId = patient.Id;
			PatientLastName = patient.LastName;

			//String interpolation: $"text is written as seen {variables are evaluated in braces}"
			//Ternary operator: condition ? value if true : value if false
			PatientFirstName = $"{patient.FirstName}{(String.IsNullOrEmpty(patient.MiddleName) ? "" : $", {patient.MiddleName[0]}")}";
			DateOfBirth = patient.DateOfBirth == null ? "No DOB" : patient.DateOfBirth.Value.ToShortDateString();
			Hospital = patient.Hospital.Id;
			PrimaryOncologist = String.IsNullOrEmpty(patient.PrimaryOncologistName) ? "No Oncologist" : patient.PrimaryOncologistName;
		



	}
	}
}
