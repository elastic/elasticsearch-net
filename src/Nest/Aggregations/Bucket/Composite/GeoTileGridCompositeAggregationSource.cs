/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
