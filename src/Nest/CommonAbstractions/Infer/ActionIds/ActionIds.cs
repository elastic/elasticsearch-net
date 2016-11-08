using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public class ActionIds : IUrlParameter
	{
		private readonly List<string> _actionIds;

		public ActionIds(IEnumerable<string> actionIds)
		{
			this._actionIds = actionIds?.ToList() ?? new List<string>();
		}

		public ActionIds(string actionIds)
		{
			this._actionIds = actionIds.IsNullOrEmpty()
				? new List<string>()
				: actionIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
						   .Select(s => s.Trim())
						   .ToList();
		}

		public string GetString(IConnectionConfigurationValues settings) => string.Join(",", this._actionIds);

		public static implicit operator ActionIds(string actionIds) => new ActionIds(actionIds);

		public static implicit operator ActionIds(string[] actionIds) => new ActionIds(actionIds);
	}
}
