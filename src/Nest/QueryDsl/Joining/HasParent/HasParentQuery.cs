using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HasParentQueryDescriptor<object>>))]
	public interface IHasParentQuery : IQuery
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ParentScoreType? ScoreType { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainerDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeJsonConverter<InnerHits>))]
		IInnerHits InnerHits { get; set; }

	}

	public class HasParentQuery : QueryBase, IHasParentQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public TypeName Type { get; set; }
		public ParentScoreType? ScoreType { get; set; }
		public IQueryContainer Query { get; set; }
		public IInnerHits InnerHits { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.HasParent = this;
		internal static bool IsConditionless(IHasParentQuery q) => q.Query == null || q.Query.IsConditionless;
	}

	public class HasParentQueryDescriptor<T> 
		: QueryDescriptorBase<HasParentQueryDescriptor<T>, IHasParentQuery>
		, IHasParentQuery where T : class
	{
		bool IQuery.Conditionless => HasParentQuery.IsConditionless(this);
		TypeName IHasParentQuery.Type { get; set; }
		ParentScoreType? IHasParentQuery.ScoreType { get; set; }
		IInnerHits IHasParentQuery.InnerHits { get; set; }
		IQueryContainer IHasParentQuery.Query { get; set; }

		public HasParentQueryDescriptor()
		{
			((IHasParentQuery)this).Type = TypeName.Create<T>();
		}

		public HasParentQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.Query = selector(new QueryContainerDescriptor<T>()));

		public HasParentQueryDescriptor<T> Type(string type) => Assign(a => a.Type = type);

		public HasParentQueryDescriptor<T> Score(ParentScoreType? scoreType = ParentScoreType.Score) => Assign(a => a.ScoreType = scoreType);

		public HasParentQueryDescriptor<T> InnerHits() => Assign(a => a.InnerHits = new InnerHits());

		public HasParentQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector) =>
			Assign(a => a.InnerHits = selector(new InnerHitsDescriptor<T>()));
	}
}
