using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TreamentPlanReport.ViewModels;
using TreamentPlanReport.Views;
using esapi = VMS.TPS.Common.Model.API;

namespace TreamentPlanReport
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			// To make this an ESAPI App, we must call CreatApplication
			using (esapi.Application app = esapi.Application.CreateApplication())
			{
				var patient = app.OpenPatientById("RapidPlan-01");
				//LINQ
				// Collections.LinqWxpression(placeholder => placeholder.[properties][condition])
				var course = patient.Courses.FirstOrDefault(c => c.Id == "Demo");
				var plan = course.PlanSetups.FirstOrDefault(p => p.Id == "IMRT Calc");
				//Create a mainwindow to hold our patientview.
				var mainWindow = new MainView();
				mainWindow.DataContext = new MainViewModel(new PatientInfoViewModel(patient), new PlanInfoViewModel(plan), new CTInfoViewModel(plan.StructureSet.Image), new ReferencePointViewModel(plan),
					new PatientShiftViewModel(plan));
				//create an instance of the patient View.
				var patientView = new PatientView();
				//Set the DataContext of the patient view to an instance of the PatientInfoViewModel.
				//DataContext defines where the XAML finds its binding source.
				//patientView.DataContext = new PatientInfoViewModel(patient);
				//mainWindow.Content = patientView;
				mainWindow.ShowDialog();

			}

		}
   }
}
