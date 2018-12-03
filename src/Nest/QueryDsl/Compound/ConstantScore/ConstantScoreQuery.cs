using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ConstantScoreQueryDescriptor<object>))]
	public interface IConstantScoreQuery : IQuery
	{
		[DataMember(Name ="filter")]
		QueryContainer Filter { get; set; }
	}

	public class ConstantScoreQuery : QueryBase, IConstantScoreQuery
	{
		public QueryContainer Filter { get; set; }
		public string Lang { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public string Script { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.ConstantScore = this;

		internal static bool IsConditionless(IConstantScoreQuery q) => q.Filter.NotWritable();
	}

	public class ConstantScoreQueryDescriptor<T>
		: QueryDescriptorBase<ConstantScoreQueryDescriptor<T>, IConstantScoreQuery>
			, IConstantScoreQuery where T : class
	{
		protected override bool Conditionless => ConstantScoreQuery.IsConditionless(this);
		QueryContainer IConstantScoreQuery.Filter { get; set; }

		public ConstantScoreQueryDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.Filter = selector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
