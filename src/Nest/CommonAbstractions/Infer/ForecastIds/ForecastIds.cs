using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class ForecastIds : IUrlParameter, IEquatable<ForecastIds>
	{
		public static ForecastIds All => new ForecastIds("_all");

		private readonly List<string> _forecastIds;

		public ForecastIds(IEnumerable<string> forecastIds) => _forecastIds = forecastIds?.ToList() ?? new List<string>();

		public ForecastIds(string forecastIds) => _forecastIds = forecastIds.IsNullOrEmpty()
			? new List<string>()
			: forecastIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim())
				.ToList();

		internal IReadOnlyList<string> Ids => _forecastIds;

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		public bool Equals(ForecastIds other)
		{
			if (Ids == null && other.Ids == null) return true;
			if (Ids == null || other.Ids == null) return false;

			return Ids.Count == other.Ids.Count && !Ids.Except(other.Ids).Any();
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => string.Join(",", _forecastIds);

		public static implicit operator ForecastIds(string forecastIds) =>
			forecastIds.IsNullOrEmptyCommaSeparatedList(out var list) ? new ForecastIds(null) : new ForecastIds(list);

		public static implicit operator ForecastIds(string[] forecastIds) =>
			forecastIds.IsEmpty() ? new ForecastIds(null) : new ForecastIds(forecastIds);

		public override bool Equals(object obj) => obj is ForecastIds other && Equals(other);

		public override int GetHashCode() => _forecastIds.OrderBy(s => s).GetHashCode();

		public static bool operator ==(ForecastIds left, ForecastIds right) => Equals(left, right);

		public static bool operator !=(ForecastIds left, ForecastIds right) => !Equals(left, right);
	}
}
