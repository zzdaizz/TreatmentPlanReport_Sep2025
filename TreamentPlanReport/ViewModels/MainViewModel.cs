using DoseMetricExample.Helpers;
using DVHPlot.ViewModels;
using DVHPlot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TreamentPlanReport.ViewModels
{
	public class MainViewModel
	{

		public RelayCommand PrintCommand { get; set; };
		public RelayCommand ARIAPostCommand { get; set; };

		// pass in the patientInfoViewMOdel via constructor injextion

		public MainViewModel(PatientInfoViewModel patientInfoViewModel, PlanInfoViewModel planInfoVewModel, CTInfoViewModel cTInfoViewModel, ReferencePointViewModel referencePointViewModel, PatientShiftViewModel patientViewModel,FieldViewModel fieldViewModel, DVHViewModel dvhViewModel, DVHSelectionViewModel dVHSelectionViewModel)
		{
		
			//set a property for PateintInfoViewModel for the MainView.xaml to bind to
			PatientInfoViewModel = patientInfoViewModel;
			PlanInfoViewModel = planInfoVewModel;
			CTInfoViewModel = cTInfoViewModel;
			ReferencePointViewModel = referencePointViewModel;	
			PatientShiftViewModel = patientViewModel;
			FieldViewModel = fieldViewModel;
			DVHViewModel = dvhViewModel;
			DVHSelectionViewModel = dVHSelectionViewModel;
		}

		//this property is what the MainView.xaml PatientView usercontorl binds to for its datacontext.
		public PatientInfoViewModel PatientInfoViewModel { get; }
		public PlanInfoViewModel PlanInfoViewModel { get; }
		public CTInfoViewModel CTInfoViewModel { get; }
		public ReferencePointViewModel ReferencePointViewModel { get; }
		public PatientShiftViewModel PatientShiftViewModel { get; }
		public FieldViewModel FieldViewModel { get; }
		public DVHViewModel DVHViewModel { get; }
		public DVHSelectionViewModel DVHSelectionViewModel { get; }
	}
}
