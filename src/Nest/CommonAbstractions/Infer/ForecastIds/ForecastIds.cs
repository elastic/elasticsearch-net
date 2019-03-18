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
		public static ForecastIds All { get; } = new ForecastIds("_all");

		private readonly List<string> _forecastIds;

		public ForecastIds(IEnumerable<string> forecastIds) => _forecastIds = forecastIds?.ToList() ?? new List<string>();

		public ForecastIds(string forecastIds) => _forecastIds = forecastIds.IsNullOrEmptyCommaSeparatedList(out var ids)
			? new List<string>()
			: ids.ToList();

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		public bool Equals(ForecastIds other)
		{
			if (other == null) return false;
			if (_forecastIds == null && other._forecastIds == null) return true;
			if (_forecastIds == null || other._forecastIds == null) return false;

			return _forecastIds.Count == other._forecastIds.Count && !_forecastIds.Except(other._forecastIds).Any();
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => string.Join(",", _forecastIds);

		public static implicit operator ForecastIds(string forecastIds) => new ForecastIds(forecastIds);

		public static implicit operator ForecastIds(string[] forecastIds) => new ForecastIds(forecastIds);

		public override bool Equals(object obj) => obj is ForecastIds other && Equals(other);

		public override int GetHashCode()
		{
			_forecastIds.Sort();
			unchecked
			{
				var hc = 0;
				foreach (var id in _forecastIds)
					hc = hc * 17 + id.GetHashCode();
				return hc;
			}
		}

		public static bool operator ==(ForecastIds left, ForecastIds right) => Equals(left, right);

		public static bool operator !=(ForecastIds left, ForecastIds right) => !Equals(left, right);
	}
}
