// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A values source that is equivalent to a simple Geo aggregation.
	/// </summary>
	public interface IGeoTileGridCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		/// The zoom of the key used to define cells/buckets in the results.
		/// </summary>
		[DataMember(Name ="precision")]
		GeoTilePrecision? Precision { get; set; }
	}

	/// <inheritdoc cref="IGeoTileGridCompositeAggregationSource" />
	public class GeoTileGridCompositeAggregationSource : CompositeAggregationSourceBase, IGeoTileGridCompositeAggregationSource
	{
		public GeoTileGridCompositeAggregationSource(string name) : base(name) { }

		/// <inheritdoc />
		public GeoTilePrecision? Precision { get; set; }

		/// <inheritdoc />
		protected override string SourceType => "geotile_grid";
	}

	/// <inheritdoc cref="IGeoTileGridCompositeAggregationSource" />
	public class GeoTileGridCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<GeoTileGridCompositeAggregationSourceDescriptor<T>, IGeoTileGridCompositeAggregationSource, T>,
			IGeoTileGridCompositeAggregationSource
	{
		public GeoTileGridCompositeAggregationSourceDescriptor(string name) : base(name, "geotile_grid") { }

		GeoTilePrecision? IGeoTileGridCompositeAggregationSource.Precision { get; set; }

		/// <inheritdoc cref="IGeoTileGridCompositeAggregationSource.Precision" />
		public GeoTileGridCompositeAggregationSourceDescriptor<T> Precision(GeoTilePrecision? precision) =>
			Assign(precision, (a, v) => a.Precision = v);
	}
}
