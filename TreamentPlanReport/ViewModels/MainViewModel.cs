using DoseMetricExample.Helpers;
using DVHPlot.ViewModels;
using DVHPlot.Views;
using Microsoft.Win32;
using OxyPlot.Wpf;
using PDFtoAria;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using TreamentPlanReport.Views;
using VMS.TPS.Common.Model.API;


namespace TreamentPlanReport.ViewModels
{
	public class MainViewModel
	{

		public RelayCommand PrintCommand { get; set; }
		public RelayCommand ARIAPostCommand { get; set; }

		// pass in the patientInfoViewMOdel via constructor injextion

		public MainViewModel(PatientInfoViewModel patientInfoViewModel, PlanInfoViewModel planInfoVewModel, CTInfoViewModel cTInfoViewModel, ReferencePointViewModel referencePointViewModel, PatientShiftViewModel patientViewModel,FieldViewModel fieldViewModel, DVHViewModel dvhViewModel, DVHSelectionViewModel dVHSelectionViewModel,
			User user)
		{

			//set a property for PateintInfoViewModel for the MainView.xaml to bind to
			_user = user;
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
			OpenFileDialog file = new OpenFileDialog();
			file.Filter = "PDF (*.pdf)|*.pdf";
			if (file.ShowDialog() == true)
			{
				var BinaryContent = File.ReadAllBytes(file.FileName);

				CustomInsertDocumentsParameter.PostDocumentData(PatientInfoViewModel.PatientId, _user,
						BinaryContent, "Plan Report",
						new VMS.OIS.ARIALocal.WebServices.Document.Contracts.DocumentType { DocumentTypeDescription = "Treatment Plan Report" });
			}
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

		private User _user;

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
