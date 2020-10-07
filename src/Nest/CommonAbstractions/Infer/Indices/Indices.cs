// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(IndicesMultiSyntaxFormatter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Indices : Union<Indices.AllIndicesMarker, Indices.ManyIndices>, IUrlParameter
	{
		internal Indices(AllIndicesMarker all) : base(all) { }

		internal Indices(ManyIndices indices) : base(indices) { }

		internal Indices(IEnumerable<IndexName> indices) : base(new ManyIndices(indices)) { }

		/// <summary>All indices. Represents _all</summary>
		public static Indices All { get; } = new Indices(new AllIndicesMarker());

		/// <inheritdoc cref="All" />
		public static Indices AllIndices { get; } = All;

		private string DebugDisplay => Match(
			all => "_all",
			types => $"Count: {types.Indices.Count} [" + string.Join(",", types.Indices.Select((t, i) => $"({i + 1}: {t.DebugDisplay})")) + "]"
		);

		public override string ToString() => DebugDisplay;

		string IUrlParameter.GetString(ITransportConfigurationValues settings) => Match(
			all => "_all",
			many =>
			{
				if (!(settings is IConnectionSettingsValues nestSettings))
					throw new Exception(
						"Tried to pass index names on querysting but it could not be resolved because no nest settings are available");

				var infer = nestSettings.Inferrer;
				var indices = many.Indices.Select(i => infer.IndexName(i)).Distinct();
				return string.Join(",", indices);
			}
		);

		public static IndexName Index(string index) => index;

		public static IndexName Index(IndexName index) => index;

		public static IndexName Index<T>() => typeof(T);

		public static ManyIndices Index(IEnumerable<IndexName> indices) => new ManyIndices(indices);

		public static ManyIndices Index(params IndexName[] indices) => new ManyIndices(indices);

		public static ManyIndices Index(IEnumerable<string> indices) => new ManyIndices(indices);

		public static ManyIndices Index(params string[] indices) => new ManyIndices(indices);

		public static Indices Parse(string indicesString)
		{
			if (indicesString.IsNullOrEmptyCommaSeparatedList(out var indices)) return null;

			return indices.Contains("_all") ? All : Index(indices.Select(i => (IndexName)i));
		}

		public static implicit operator Indices(string indicesString) => Parse(indicesString);

		public static implicit operator Indices(ManyIndices many) => many == null ? null : new Indices(many);

		public static implicit operator Indices(string[] many) => many.IsEmpty() ? null : new ManyIndices(many);

		public static implicit operator Indices(IndexName[] many) => many.IsEmpty() ? null : new ManyIndices(many);

		public static implicit operator Indices(IndexName index) => index == null ? null : new ManyIndices(new[] { index });

		public static implicit operator Indices(Type type) => type == null ? null : new ManyIndices(new IndexName[] { type });

		public static bool operator ==(Indices left, Indices right) => Equals(left, right);

		public static bool operator !=(Indices left, Indices right) => !Equals(left, right);

		public override bool Equals(object obj)
		{
			if (!(obj is Indices other)) return false;

			return Match(
				all => other.Match(a => true, m => false),
				many => other.Match(
					a => false,
					m => EqualsAllIndices(m.Indices, many.Indices)
				)
			);
		}

		private static bool EqualsAllIndices(IReadOnlyList<IndexName> thisIndices, IReadOnlyList<IndexName> otherIndices)
		{
			if (thisIndices == null && otherIndices == null) return true;
			if (thisIndices == null || otherIndices == null) return false;

			return thisIndices.Count == otherIndices.Count && !thisIndices.Except(otherIndices).Any();
		}

		public override int GetHashCode() => Match(
			all => "_all".GetHashCode(),
			many => string.Concat(many.Indices.OrderBy(i => i.ToString())).GetHashCode()
		);

		public class AllIndicesMarker
		{
			internal AllIndicesMarker() { }
		}

		public class ManyIndices
		{
			private readonly List<IndexName> _indices = new List<IndexName>();

			internal ManyIndices(IEnumerable<IndexName> indices) => _indices.AddRange(indices.NotEmpty(nameof(indices)));

			internal ManyIndices(IEnumerable<string> indices) =>
				_indices.AddRange(indices.NotEmpty(nameof(indices)).Select(s => (IndexName)s));

			public IReadOnlyList<IndexName> Indices => _indices;

			public ManyIndices And<T>()
			{
				_indices.Add(typeof(T));
				return this;
			}
		}
	}
}
