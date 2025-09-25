using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreamentPlanReport.ViewModels
{
	public class MainViewModel
	{
		// pass in the patientInfoViewMOdel via constructor injextion
		public MainViewModel(PatientInfoViewModel patientInfoViewModel, PlanInfoViewModel planInfoVewModel, CTInfoViewModel cTInfoViewModel)
		{
			//set a property for PateintInfoViewModel for the MainView.xaml to bind to
			PatientInfoViewModel = patientInfoViewModel;
			PlanInfoViewModel = planInfoVewModel;
			CTInfoViewModel = cTInfoViewModel;
		}

		//this property is what the MainView.xaml PatientView usercontorl binds to for its datacontext.
		public PatientInfoViewModel PatientInfoViewModel { get; }
		public PlanInfoViewModel PlanInfoViewModel { get; }
		public CTInfoViewModel CTInfoViewModel { get; }
	}
}
