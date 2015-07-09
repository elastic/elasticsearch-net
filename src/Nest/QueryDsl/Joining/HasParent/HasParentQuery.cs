using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HasParentQueryDescriptor<object>>))]
	public interface IHasParentQuery : IQuery
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ParentScoreType? ScoreType { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainerDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeConverter<InnerHits>))]
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
		private IHasParentQuery Self { get { return this; }}
		bool IQuery.Conditionless => HasParentQuery.IsConditionless(this);
		TypeName IHasParentQuery.Type { get; set; }
		ParentScoreType? IHasParentQuery.ScoreType { get; set; }
		IInnerHits IHasParentQuery.InnerHits { get; set; }
		IQueryContainer IHasParentQuery.Query { get; set; }

		public HasParentQueryDescriptor()
		{
			Self.Type = TypeName.Create<T>();
		}

		public HasParentQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryContainerDescriptor<T>();
			Self.Query = querySelector(q);
			return this;
		}
		public HasParentQueryDescriptor<T> Type(string type)
		{
			Self.Type = type;
			return this;
		}

		public HasParentQueryDescriptor<T> Score(ParentScoreType? scoreType = ParentScoreType.Score)
		{
			Self.ScoreType = scoreType;
			return this;
		}

		public HasParentQueryDescriptor<T> InnerHits()
		{
			Self.InnerHits = new InnerHits();
			return this;
		}

		public HasParentQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> innerHitsSelector)
		{
			if (innerHitsSelector == null) return this;
			Self.InnerHits = innerHitsSelector(new InnerHitsDescriptor<T>());
			return this;
		}
	}
}
