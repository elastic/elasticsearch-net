using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HasChildQueryDescriptor<object>>))]
	public interface IHasChildQuery : IQuery
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ChildScoreType? ScoreType { get; set; }

		[JsonProperty("min_children")]
		int? MinChildren { get; set; }

		[JsonProperty("max_children")]
		int? MaxChildren { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainerDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeConverter<InnerHits>))]
		IInnerHits InnerHits { get; set; }
	}
	
	public class HasChildQuery : QueryBase, IHasChildQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public TypeName Type { get; set; }
		public ChildScoreType? ScoreType { get; set; }
		public int? MinChildren { get; set; }
		public int? MaxChildren { get; set; }
		public IQueryContainer Query { get; set; }
		public IInnerHits InnerHits { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.HasChild = this;
		internal static bool IsConditionless(IHasChildQuery q) => q.Query == null || q.Query.IsConditionless;
	}

	public class HasChildQueryDescriptor<T> 
		: QueryDescriptorBase<HasChildQueryDescriptor<T>, IHasChildQuery>
		, IHasChildQuery where T : class
	{
		private IHasChildQuery Self => this;
		bool IQuery.Conditionless => HasChildQuery.IsConditionless(this);
		TypeName IHasChildQuery.Type { get; set; }
		ChildScoreType? IHasChildQuery.ScoreType { get; set; }
		int? IHasChildQuery.MinChildren { get; set; }
		int? IHasChildQuery.MaxChildren { get; set; }
		IQueryContainer IHasChildQuery.Query { get; set; }
		IInnerHits IHasChildQuery.InnerHits { get; set; }

		public HasChildQueryDescriptor()
		{
			Self.Type = TypeName.Create<T>();
		}

		public HasChildQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryContainerDescriptor<T>();
			Self.Query = querySelector(q);
			return this;
		}

		public HasChildQueryDescriptor<T> Type(string type)
		{
			Self.Type = type;
			return this;
		}

		public HasChildQueryDescriptor<T> Score(ChildScoreType? scoreType)
		{
			Self.ScoreType = scoreType;
			return this;
		}

		public HasChildQueryDescriptor<T> MinChildren(int minChildren)
		{
			Self.MinChildren = minChildren;
			return this;
		}

		public HasChildQueryDescriptor<T> MaxChildren(int maxChildren)
		{
			Self.MaxChildren = maxChildren;
			return this;
		}

		public HasChildQueryDescriptor<T> InnerHits()
		{
			Self.InnerHits = new InnerHits();
			return this;
		}

		public HasChildQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> innerHitsSelector)
		{
			if (innerHitsSelector == null) return this;
			Self.InnerHits = innerHitsSelector(new InnerHitsDescriptor<T>());
			return this;
		}
	}
}
