using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class ActionIds : IUrlParameter, IEquatable<ActionIds>
	{
		private readonly List<string> _actionIds;

		public ActionIds(IEnumerable<string> actionIds) => _actionIds = actionIds?.ToList() ?? new List<string>();

		public ActionIds(string actionIds) => _actionIds = actionIds.IsNullOrEmpty()
			? new List<string>()
			: actionIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim())
				.ToList();

		internal IReadOnlyList<string> Ids => _actionIds;

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		public bool Equals(ActionIds other)
		{
			if (Ids == null && other.Ids == null) return true;
			if (Ids == null || other.Ids == null) return false;

			return Ids.Count == other.Ids.Count && !Ids.Except(other.Ids).Any();
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => string.Join(",", _actionIds);

		public static implicit operator ActionIds(string actionIds) =>
			actionIds.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new ActionIds(list);

		public static implicit operator ActionIds(string[] actionIds) => actionIds.IsEmpty() ? null : new ActionIds(actionIds);

		public override bool Equals(object obj) => obj is ActionIds other && Equals(other);

		public override int GetHashCode() => _actionIds.GetHashCode();

		public static bool operator ==(ActionIds left, ActionIds right) => Equals(left, right);

		public static bool operator !=(ActionIds left, ActionIds right) => !Equals(left, right);
	}
}
