using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class ActionIds : IUrlParameter
	{
		private readonly List<string> _actionIds;

		public ActionIds(IEnumerable<string> actionIds) => _actionIds = actionIds?.ToList() ?? new List<string>();

		public ActionIds(string actionIds) => _actionIds = actionIds.IsNullOrEmpty()
			? new List<string>()
			: actionIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim())
				.ToList();

		private string DebugDisplay => GetString(null);

		//TODO explicit implemtation
		public string GetString(IConnectionConfigurationValues settings) => string.Join(",", _actionIds);

		public static implicit operator ActionIds(string actionIds) => new ActionIds(actionIds);

		public static implicit operator ActionIds(string[] actionIds) => new ActionIds(actionIds);
	}
}
