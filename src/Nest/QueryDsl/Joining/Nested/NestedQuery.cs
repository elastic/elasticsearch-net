using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<NestedQueryDescriptor<object>>))]
	public interface INestedQuery : IQuery
	{
		[JsonProperty("score_mode")]
		NestedScoreMode? ScoreMode { get; set; }

		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }

		[JsonProperty("inner_hits")]
		IInnerHits InnerHits { get; set; }

	}

	public class NestedQuery : QueryBase, INestedQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public NestedScoreMode? ScoreMode { get; set; }
		public QueryContainer Filter { get; set; }
		public QueryContainer Query { get; set; }
		public Field Path { get; set; }
		public IInnerHits InnerHits { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Nested = this;
		internal static bool IsConditionless(INestedQuery q)
		{
			return (q.Query == null || q.Query.IsConditionless)
				&& (q.Filter == null || q.Filter.IsConditionless);
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class NestedQueryDescriptor<T> 
		: QueryDescriptorBase<NestedQueryDescriptor<T>, INestedQuery>
		, INestedQuery where T : class
	{
		bool IQuery.Conditionless => NestedQuery.IsConditionless(this);
		NestedScoreMode? INestedQuery.ScoreMode { get; set; }
		QueryContainer INestedQuery.Filter { get; set; }
		QueryContainer INestedQuery.Query { get; set; }
		Field INestedQuery.Path { get; set; }
		IInnerHits INestedQuery.InnerHits { get; set; }

		public NestedQueryDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Filter = selector(new QueryContainerDescriptor<T>()));

		public NestedQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Query = selector(new QueryContainerDescriptor<T>()));

		public NestedQueryDescriptor<T> ScoreMode(NestedScoreMode scoreMode) => Assign(a => a.ScoreMode = scoreMode);

		public NestedQueryDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public NestedQueryDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);

		public NestedQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) => 
			Assign(a => a.InnerHits = selector.InvokeOrDefault(new InnerHitsDescriptor<T>()));	
	}
}
