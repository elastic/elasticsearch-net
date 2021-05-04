// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(AutoDateHistogramAggregation))]
	public interface IAutoDateHistogramAggregation : IBucketAggregation
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name = "buckets")]
		int? Buckets { get; set; }

		[DataMember(Name = "format")]
		string Format { get; set; }

		[DataMember(Name = "missing")]
		DateTime? Missing { get; set; }

		[DataMember(Name = "offset")]
		string Offset { get; set; }

		[DataMember(Name = "params")]
		IDictionary<string, object> Params { get; set; }

		[DataMember(Name = "script")]
		IScript Script { get; set; }

		[DataMember(Name = "time_zone")]
		string TimeZone { get; set; }

		/// <summary>
		/// Specify the minimum rounding interval that should be used. This can make the collection process
		/// more efficient, as the aggregation will not attempt to round at any interval lower than this.
		/// </summary>
		[DataMember(Name = "minimum_interval")]
		MinimumInterval? MinimumInterval { get; set; }
	}

	public class AutoDateHistogramAggregation : BucketAggregationBase, IAutoDateHistogramAggregation
	{
		private string _format;

		internal AutoDateHistogramAggregation() { }

		public AutoDateHistogramAggregation(string name) : base(name) { }

		public Field Field { get; set; }

		public int? Buckets { get; set; }

		//see: https://github.com/elastic/elasticsearch/issues/9725
		public string Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}

		public DateTime? Missing { get; set; }
		public string Offset { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public IScript Script { get; set; }
		public string TimeZone { get; set; }

		public MinimumInterval? MinimumInterval { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.AutoDateHistogram = this;
	}

	public class AutoDateHistogramAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<AutoDateHistogramAggregationDescriptor<T>, IAutoDateHistogramAggregation, T>
			, IAutoDateHistogramAggregation
		where T : class
	{
		private string _format;

		Field IAutoDateHistogramAggregation.Field { get; set; }

		int? IAutoDateHistogramAggregation.Buckets { get; set; }

		//see: https://github.com/elastic/elasticsearch/issues/9725
		string IAutoDateHistogramAggregation.Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(Self.Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}

		DateTime? IAutoDateHistogramAggregation.Missing { get; set; }

		string IAutoDateHistogramAggregation.Offset { get; set; }

		IDictionary<string, object> IAutoDateHistogramAggregation.Params { get; set; }

		IScript IAutoDateHistogramAggregation.Script { get; set; }

		string IAutoDateHistogramAggregation.TimeZone { get; set; }

		MinimumInterval? IAutoDateHistogramAggregation.MinimumInterval { get; set; }

		public AutoDateHistogramAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public AutoDateHistogramAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public AutoDateHistogramAggregationDescriptor<T> Buckets(int? buckets) => Assign(buckets, (a, v) => a.Buckets = v);

		public AutoDateHistogramAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public AutoDateHistogramAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public AutoDateHistogramAggregationDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		public AutoDateHistogramAggregationDescriptor<T> TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		public AutoDateHistogramAggregationDescriptor<T> Offset(string offset) => Assign(offset, (a, v) => a.Offset = v);

		public AutoDateHistogramAggregationDescriptor<T> Missing(DateTime? missing) => Assign(missing, (a, v) => a.Missing = v);

		/// <inheritdoc cref="IAutoDateHistogramAggregation.MinimumInterval"/>
		public AutoDateHistogramAggregationDescriptor<T> MinimumInterval(MinimumInterval? minimumInterval) => Assign(minimumInterval, (a, v) => a.MinimumInterval = v);
	}
}
