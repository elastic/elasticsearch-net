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
		internal IReadOnlyList<string> Ids => _actionIds;

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

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => string.Join(",", this._actionIds);

		public static implicit operator ActionIds(string actionIds) => actionIds.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new ActionIds(list);

		public static implicit operator ActionIds(string[] actionIds) => actionIds.IsEmpty()  ? null : new ActionIds(actionIds);

		public bool Equals(ActionIds other)
		{
			if (this.Ids == null && other.Ids == null) return true;
			if (this.Ids == null || other.Ids == null) return false;
			return this.Ids.Count == other.Ids.Count && !this.Ids.Except(other.Ids).Any();
		}

		public override bool Equals(object obj) => obj is ActionIds other && Equals(other);

		public override int GetHashCode() => this._actionIds.GetHashCode();

		public static bool operator ==(ActionIds left, ActionIds right) => Equals(left, right);

		public static bool operator !=(ActionIds left, ActionIds right) => !Equals(left, right);
	}
}
