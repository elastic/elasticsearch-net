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
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? Score { get; set; }

		[JsonProperty("filter")]
		IQueryContainer Filter { get; set; }

		[JsonProperty("query")]
		IQueryContainer Query { get; set; }

		[JsonProperty("path")]
		FieldName Path { get; set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeJsonConverter<InnerHits>))]
		IInnerHits InnerHits { get; set; }

	}

	public class NestedQuery : QueryBase, INestedQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public NestedScore? Score { get; set; }
		public IQueryContainer Filter { get; set; }
		public IQueryContainer Query { get; set; }
		public FieldName Path { get; set; }
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
		NestedScore? INestedQuery.Score { get; set; }
		IQueryContainer INestedQuery.Filter { get; set; }
		IQueryContainer INestedQuery.Query { get; set; }
		FieldName INestedQuery.Path { get; set; }
		IInnerHits INestedQuery.InnerHits { get; set; }

		public NestedQueryDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Filter = selector(new QueryContainerDescriptor<T>()));

		public NestedQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Query = selector(new QueryContainerDescriptor<T>()));

		public NestedQueryDescriptor<T> Score(NestedScore score) => Assign(a => a.Score = score);

		public NestedQueryDescriptor<T> Path(string path) => Assign(a => a.Path = path);

		public NestedQueryDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);

		public NestedQueryDescriptor<T> InnerHits() => Assign(a => a.InnerHits = new InnerHits());

		public NestedQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector) => 
			Assign(a => a.InnerHits = selector(new InnerHitsDescriptor<T>()));	
	}
}
