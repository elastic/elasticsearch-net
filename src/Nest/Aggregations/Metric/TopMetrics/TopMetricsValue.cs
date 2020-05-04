// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The configuration for a field or script that provides a value or weight
	/// for <see cref="TopMetricsAggregation" />
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TopMetricsValue))]
	public interface ITopMetricsValue
	{
		/// <summary>
		/// The field that values should be extracted from
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }
	}

	/// <inheritdoc />
	public class TopMetricsValue : ITopMetricsValue
	{
		internal TopMetricsValue() { }

		public TopMetricsValue(Field field) => Field = field;

		/// <inheritdoc />
		public Field Field { get; set; }
	}

	/// <inheritdoc cref="ITopMetricsAggregation" />
	public class TopMetricsValuesDescriptor<T> : DescriptorPromiseBase<TopMetricsValuesDescriptor<T>, IList<ITopMetricsValue>>
		where T : class
	{
		public TopMetricsValuesDescriptor() : base(new List<ITopMetricsValue>()) { }

		public TopMetricsValuesDescriptor<T> Field(Field field) => AddTopMetrics(new TopMetricsValue { Field = field });

		public TopMetricsValuesDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) =>
			AddTopMetrics(new TopMetricsValue { Field = field});

		private TopMetricsValuesDescriptor<T> AddTopMetrics(ITopMetricsValue TopMetrics) => TopMetrics == null ? this : Assign(TopMetrics, (a, v) => a.Add(v));
	}

}
