// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public interface IAggregationRange
	{
		[DataMember(Name ="from")]
		double? From { get; set; }

		[DataMember(Name ="key")]
		string Key { get; set; }

		[DataMember(Name ="to")]
		double? To { get; set; }
	}

	/// <inheritdoc />
	public class AggregationRange : IAggregationRange
	{
		public double? From { get; set; }
		public string Key { get; set; }
		public double? To { get; set; }
	}

	/// <inheritdoc />
	public class AggregationRangeDescriptor : DescriptorBase<AggregationRangeDescriptor, IAggregationRange>, IAggregationRange
	{
		double? IAggregationRange.From { get; set; }
		string IAggregationRange.Key { get; set; }
		double? IAggregationRange.To { get; set; }

		public AggregationRangeDescriptor Key(string key) => Assign(key, (a, v) => a.Key = v);

		public AggregationRangeDescriptor From(double? from) => Assign(from, (a, v) => a.From = v);

		public AggregationRangeDescriptor To(double? to) => Assign(to, (a, v) => a.To = v);
	}
}
