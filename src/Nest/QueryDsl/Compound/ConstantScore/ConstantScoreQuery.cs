// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ConstantScoreQuery))]
	public interface IConstantScoreQuery : IQuery
	{
		[DataMember(Name ="filter")]
		QueryContainer Filter { get; set; }
	}

	[DataContract]
	public class ConstantScoreQuery : QueryBase, IConstantScoreQuery
	{
		public QueryContainer Filter { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.ConstantScore = this;

		internal static bool IsConditionless(IConstantScoreQuery q) => q.Filter.NotWritable();
	}

	[DataContract]
	public class ConstantScoreQueryDescriptor<T>
		: QueryDescriptorBase<ConstantScoreQueryDescriptor<T>, IConstantScoreQuery>
			, IConstantScoreQuery where T : class
	{
		protected override bool Conditionless => ConstantScoreQuery.IsConditionless(this);
		QueryContainer IConstantScoreQuery.Filter { get; set; }

		public ConstantScoreQueryDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
