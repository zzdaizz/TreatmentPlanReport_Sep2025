using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreamentPlanReport.Models;
using VMS.TPS.Common.Model.API;

namespace TreamentPlanReport.ViewModels
{
	public class FieldViewModel
	{
		public List<FieldModel> Fields { get; set; }

		public FieldModel SelectedField { get; set; }

		public FieldViewModel(PlanSetup plan)

		{
			Fields = new List<FieldModel>();

			foreach (var beam in plan.Beams.Where(b => !b.IsSetupField))
			{
				Fields.Add(new FieldModel(beam, plan));
			}

		}
	}
}
