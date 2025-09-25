using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreamentPlanReport.Models;
using VMS.TPS.Common.Model.API;

namespace TreamentPlanReport.ViewModels
{
	public class ReferencePointViewModel
	{
		private ExternalPlanSetup ExternalPlanSetup { get; set; }
		public ObservableCollection<ReferencePointModel> LRefPoints { get; set; }
		public ReferencePointViewModel(PlanSetup ps)
		{
			ExternalPlanSetup = ps as ExternalPlanSetup;

			LRefPoints = new ObservableCollection<ReferencePointModel>();

			// Construct list of reference points
			foreach (var rp in ExternalPlanSetup.ReferencePoints)
			{
				LRefPoints.Add(new ReferencePointModel(rp, ps));
			}

		}
	}
}
