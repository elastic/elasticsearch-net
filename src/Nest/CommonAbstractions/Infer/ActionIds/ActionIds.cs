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

		public ActionIds(IEnumerable<string> actionIds) => _actionIds = actionIds?.ToList();

		public ActionIds(string actionIds)
		{
			if (!actionIds.IsNullOrEmptyCommaSeparatedList(out var arr))
				_actionIds = arr.ToList();
		}

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		public bool Equals(ActionIds other)
		{
			if (other == null) return false;
			if (_actionIds == null && other._actionIds == null) return true;
			if (_actionIds == null || other._actionIds == null) return false;

			return _actionIds.Count == other._actionIds.Count &&
				_actionIds.OrderBy(id => id).SequenceEqual(other._actionIds.OrderBy(id => id));
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", _actionIds ?? Enumerable.Empty<string>());

		public static implicit operator ActionIds(string actionIds) =>
			actionIds.IsNullOrEmptyCommaSeparatedList(out var arr) ? null : new ActionIds(arr);

		public static implicit operator ActionIds(string[] actionIds) =>
			actionIds.IsEmpty() ? null : new ActionIds(actionIds);

		public override bool Equals(object obj) => obj is ActionIds other && Equals(other);

		public override int GetHashCode()
		{
			if (_actionIds == null) return 0;
			unchecked
			{
				var hc = 0;
				foreach (var id in _actionIds.OrderBy(id => id))
					hc = hc * 17 + id.GetHashCode();
				return hc;
			}
		}

		public static bool operator ==(ActionIds left, ActionIds right) => Equals(left, right);

		public static bool operator !=(ActionIds left, ActionIds right) => !Equals(left, right);
	}
}
