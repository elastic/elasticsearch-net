// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TopMetricsAggregation))]
	public interface ITopMetricsAggregation : IMetricAggregation
	{
		/// <summary>
		/// Metrics selects the fields of the "top" document to return. You can request a single metric or multiple metrics.
		/// </summary>
		[DataMember(Name ="metrics")]
		IList<ITopMetricsValue> Metrics { get; set; }

		/// <summary>
		/// Return the top few documents worth of metrics using this parameter.
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }

		/// <summary>
		/// The sort field in the metric request functions exactly the same as the sort field in the search request except:
		/// * It can’t be used on  binary, flattened, ip, keyword, or text fields.
		/// * It only supports a single sort value so which document wins ties is not specified.
		/// </summary>
		[DataMember(Name ="sort")]
		IList<ISort> Sort { get; set; }
	}

	public class TopMetricsAggregation : MetricAggregationBase, ITopMetricsAggregation
	{
		internal TopMetricsAggregation() { }

		public TopMetricsAggregation(string name) : base(name, null) { }

		/// <inheritdoc cref="ITopMetricsAggregation.Metrics" />
		public IList<ITopMetricsValue> Metrics { get; set; }

		/// <inheritdoc cref="ITopMetricsAggregation.Size" />
		public int? Size { get; set; }

		/// <inheritdoc cref="ITopMetricsAggregation.Sort" />
		public IList<ISort> Sort { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.TopMetrics = this;
	}

	public class TopMetricsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<TopMetricsAggregationDescriptor<T>, ITopMetricsAggregation, T>, ITopMetricsAggregation where T : class
	{
		int? ITopMetricsAggregation.Size { get; set; }

		IList<ISort> ITopMetricsAggregation.Sort { get; set; }

		IList<ITopMetricsValue> ITopMetricsAggregation.Metrics { get; set; }

		/// <inheritdoc cref="ITopMetricsAggregation.Size" />
		public TopMetricsAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) =>
			a.Size = v);

		/// <inheritdoc cref="ITopMetricsAggregation.Sort" />
		public TopMetricsAggregationDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sortSelector) =>
			Assign(sortSelector, (a, v) =>
				a.Sort = v?.Invoke(new SortDescriptor<T>())?.Value);

		/// <inheritdoc cref="ITopMetricsAggregation.Metrics" />
		public TopMetricsAggregationDescriptor<T> Metrics(Func<TopMetricsValuesDescriptor<T>, IPromise<IList<ITopMetricsValue>>> TopMetricsValueSelector) =>
			Assign(TopMetricsValueSelector, (a, v) =>
				a.Metrics = v?.Invoke(new TopMetricsValuesDescriptor<T>())?.Value);
	}
}
