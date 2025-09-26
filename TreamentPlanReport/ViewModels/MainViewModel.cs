using DoseMetricExample.Helpers;
using DVHPlot.ViewModels;
using DVHPlot.Views;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using TreamentPlanReport.Views;

namespace TreamentPlanReport.ViewModels
{
	public class MainViewModel
	{

		public RelayCommand PrintCommand { get; set; }
		public RelayCommand ARIAPostCommand { get; set; }

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
			PrintCommand = new RelayCommand(OnPrint);
			ARIAPostCommand = new RelayCommand(OnARIAPost);
		}

		private void OnARIAPost(object obj)
		{
			throw new NotImplementedException();
		}

		private void OnPrint(object obj)
		{
			// flow document printing style
			FlowDocument fd = new FlowDocument { FontSize = 12, FontFamily = new System.Windows.Media.FontFamily("Franklin Gothic") };
			fd.Blocks.Add(new Paragraph(new Run("Treatment Plan Report")));
			fd.Blocks.Add(new Paragraph(new Run("Patient Info:")) { FontWeight = FontWeights.Bold });
			fd.Blocks.Add(new BlockUIContainer(new PatientView { DataContext = PatientInfoViewModel }));
			fd.Blocks.Add(new Paragraph(new Run("Plan Info:")) { FontWeight = FontWeights.Bold });
			fd.Blocks.Add(new BlockUIContainer(new PlanInfoView { DataContext = PlanInfoViewModel }));
			fd.Blocks.Add(new Paragraph(new Run("CT Info:")) { FontWeight = FontWeights.Bold });
			fd.Blocks.Add(new BlockUIContainer(new CTImageView { DataContext = CTInfoViewModel }));
			fd.Blocks.Add(new Paragraph(new Run("Reference Point:")) { FontWeight = FontWeights.Bold });
			fd.Blocks.Add(new BlockUIContainer(new ReferencePointView { DataContext = ReferencePointViewModel }));
			fd.Blocks.Add(new Paragraph(new Run("Patient Shifts:")) { FontWeight = FontWeights.Bold });
			fd.Blocks.Add(new BlockUIContainer(new PatientShiftView { DataContext = PatientShiftViewModel }));
			foreach (var field in FieldViewModel.Fields)
			{
				fd.Blocks.Add(new BlockUIContainer(new FieldDetailsView { DataContext = field }));
			}
			BitmapSource bmp = new PngExporter().ExportToBitmap(DVHViewModel.DVHPlotModel);
			fd.Blocks.Add(new BlockUIContainer(new System.Windows.Controls.Image
			{
				Source = bmp,
				Height = 600,
				Width = 725
			}));
			System.Windows.Controls.PrintDialog printer = new System.Windows.Controls.PrintDialog();
			//printer.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
			fd.PageHeight = 1056;
			fd.PageWidth = 816;
			fd.PagePadding = new System.Windows.Thickness(50);
			fd.ColumnGap = 0;
			fd.ColumnWidth = 816;
			IDocumentPaginatorSource source = fd;
			if (printer.ShowDialog() == true)
			{
				printer.PrintDocument(source.DocumentPaginator, "TreatmentPlanReport");
			}
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
