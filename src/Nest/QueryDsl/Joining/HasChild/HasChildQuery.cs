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
		TypeNameMarker Type { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ChildScoreType? ScoreType { get; set; }

		[JsonProperty("min_children")]
		int? MinChildren { get; set; }

		[JsonProperty("max_children")]
		int? MaxChildren { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeConverter<InnerHits>))]
		IInnerHits InnerHits { get; set; }
	}
	
	public class HasChildQuery : PlainQuery, IHasChildQuery
	{
		public string Name { get; set; }
		bool IQuery.Conditionless => IsConditionless(this);
		public TypeNameMarker Type { get; set; }
		public ChildScoreType? ScoreType { get; set; }
		public int? MinChildren { get; set; }
		public int? MaxChildren { get; set; }
		public IQueryContainer Query { get; set; }
		public IInnerHits InnerHits { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.HasChild = this;
		internal static bool IsConditionless(IHasChildQuery q) => q.Query == null || q.Query.IsConditionless;
	}

	public class HasChildQueryDescriptor<T> : IHasChildQuery where T : class
	{
		private IHasChildQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => HasChildQuery.IsConditionless(this);
		TypeNameMarker IHasChildQuery.Type { get; set; }
		ChildScoreType? IHasChildQuery.ScoreType { get; set; }
		int? IHasChildQuery.MinChildren { get; set; }
		int? IHasChildQuery.MaxChildren { get; set; }
		IQueryContainer IHasChildQuery.Query { get; set; }
		IInnerHits IHasChildQuery.InnerHits { get; set; }

		public HasChildQueryDescriptor()
		{
			Self.Type = TypeNameMarker.Create<T>();
		}

		public HasChildQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public HasChildQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
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
