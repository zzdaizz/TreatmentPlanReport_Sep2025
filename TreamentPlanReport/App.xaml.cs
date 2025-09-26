using DoseMetricExample.Helpers;
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
				string patientId = e.Args.FirstOrDefault().Split(';').First();
				string courseId = e.Args.FirstOrDefault().Split(';').ElementAt(1);
				string planId = e.Args.FirstOrDefault().Split(';').Last();

				var patient = app.OpenPatientById(patientId);
				if (patient == null)
				{
					MessageBox.Show("Please open a patient");
					app.Dispose();
					this.Shutdown();
					return;
		
				}
				//LINQ -> Language Integrated Query	(=> is a lambda operator)
				// Collections.LinqWxpression(placeholder => placeholder.[properties][condition])
				var course = patient.Courses.FirstOrDefault(c => c.Id == courseId);
				if (course == null)
				{
					MessageBox.Show("Please open a patient with a valid course");
					app.Dispose();
					this.Shutdown();
					return;

				}
				var plan = course.PlanSetups.FirstOrDefault(p => p.Id == planId);
				if (plan == null)
				{
					MessageBox.Show("Please open a patient with a valid plan");
					app.Dispose();
					this.Shutdown();
					return;

				}
				//Create a mainwindow to hold our patientview.
				var mainWindow = new MainView();
				EventHelper eventHelper = new EventHelper();
				mainWindow.DataContext = new MainViewModel(new PatientInfoViewModel(patient), 
					new PlanInfoViewModel(plan), 
					new CTInfoViewModel(plan.StructureSet.Image), 
					new ReferencePointViewModel(plan),
					new PatientShiftViewModel(plan), 
					new FieldViewModel(plan),
					new DVHPlot.ViewModels.DVHViewModel(plan, eventHelper),
					new DVHPlot.ViewModels.DVHSelectionViewModel(plan, eventHelper),
					app.CurrentUser);
				
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
