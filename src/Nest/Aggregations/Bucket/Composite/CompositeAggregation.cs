// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A multi-bucket aggregation that creates composite buckets from different sources.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(CompositeAggregation))]
	public interface ICompositeAggregation : IBucketAggregation
	{
		/// <summary>
		/// Used to retrieve the composite buckets that are after the
		/// last composite buckets returned in a previous round
		/// </summary>
		[DataMember(Name ="after")]
		CompositeKey After { get; set; }

		/// <summary>
		/// Defines how many composite buckets should be returned.
		/// Each composite bucket is considered as a single bucket so
		/// setting a size of 10 will return the first 10 composite buckets
		/// created from the values source.
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }

		/// <summary>
		/// Controls the sources that should be used to build the composite buckets
		/// </summary>
		[DataMember(Name ="sources")]
		IEnumerable<ICompositeAggregationSource> Sources { get; set; }
	}

	/// <inheritdoc cref="ICompositeAggregation" />
	public class CompositeAggregation : BucketAggregationBase, ICompositeAggregation
	{
		internal CompositeAggregation() { }

		public CompositeAggregation(string name) : base(name) { }

		/// <inheritdoc />
		public CompositeKey After { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }

		/// <inheritdoc />
		public IEnumerable<ICompositeAggregationSource> Sources { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Composite = this;
	}

	/// <inheritdoc cref="ICompositeAggregation" />
	public class CompositeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<CompositeAggregationDescriptor<T>, ICompositeAggregation, T>
			, ICompositeAggregation
		where T : class
	{
		CompositeKey ICompositeAggregation.After { get; set; }
		int? ICompositeAggregation.Size { get; set; }
		IEnumerable<ICompositeAggregationSource> ICompositeAggregation.Sources { get; set; }

		/// <inheritdoc cref="ICompositeAggregation.Sources" />
		public CompositeAggregationDescriptor<T> Sources(
			Func<CompositeAggregationSourcesDescriptor<T>, IPromise<IList<ICompositeAggregationSource>>> selector
		) =>
			Assign(selector, (a, v) => a.Sources = v?.Invoke(new CompositeAggregationSourcesDescriptor<T>())?.Value);

		/// <inheritdoc cref="ICompositeAggregation.Size" />
		public CompositeAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="ICompositeAggregation.After" />
		public CompositeAggregationDescriptor<T> After(CompositeKey after) => Assign(after, (a, v) => a.After = v);
	}
}
