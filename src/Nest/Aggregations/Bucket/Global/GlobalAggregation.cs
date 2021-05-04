// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GlobalAggregation))]
	public interface IGlobalAggregation : IBucketAggregation { }

	public class GlobalAggregation : BucketAggregationBase, IGlobalAggregation
	{
		internal GlobalAggregation() { }

		public GlobalAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Global = this;
	}

	public class GlobalAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GlobalAggregationDescriptor<T>, IGlobalAggregation, T>
			, IGlobalAggregation
		where T : class { }
}
