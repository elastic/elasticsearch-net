using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HasChildQueryDescriptor<object>>))]
	public interface IHasChildQuery : IQuery
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("score_mode")]
		ChildScoreMode? ScoreMode { get; set; }

		[JsonProperty("min_children")]
		int? MinChildren { get; set; }

		[JsonProperty("max_children")]
		int? MaxChildren { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("inner_hits")]
		IInnerHits InnerHits { get; set; }
	}
	
	public class HasChildQuery : QueryBase, IHasChildQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public TypeName Type { get; set; }
		public ChildScoreMode? ScoreMode { get; set; }
		public int? MinChildren { get; set; }
		public int? MaxChildren { get; set; }
		public QueryContainer Query { get; set; }
		public IInnerHits InnerHits { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.HasChild = this;
		internal static bool IsConditionless(IHasChildQuery q) => q.Query == null || q.Query.IsConditionless || q.Type == null;
	}

	public class HasChildQueryDescriptor<T> 
		: QueryDescriptorBase<HasChildQueryDescriptor<T>, IHasChildQuery>
		, IHasChildQuery where T : class
	{
		protected override bool Conditionless => HasChildQuery.IsConditionless(this);
		TypeName IHasChildQuery.Type { get; set; }
		ChildScoreMode? IHasChildQuery.ScoreMode { get; set; }
		int? IHasChildQuery.MinChildren { get; set; }
		int? IHasChildQuery.MaxChildren { get; set; }
		QueryContainer IHasChildQuery.Query { get; set; }
		IInnerHits IHasChildQuery.InnerHits { get; set; }

		public HasChildQueryDescriptor()
		{
			((IHasChildQuery)this).Type = TypeName.Create<T>();
		}

		public HasChildQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Query = selector?.Invoke(new QueryContainerDescriptor<T>()));

		public HasChildQueryDescriptor<T> Type(string type) => Assign(a => a.Type = type);

		public HasChildQueryDescriptor<T> ScoreMode(ChildScoreMode? scoreMode) => Assign(a => a.ScoreMode = scoreMode);

		public HasChildQueryDescriptor<T> MinChildren(int? minChildren) => Assign(a => a.MinChildren = minChildren);

		public HasChildQueryDescriptor<T> MaxChildren(int? maxChildren) => Assign(a => a.MaxChildren = maxChildren);

		public HasChildQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(a => a.InnerHits = selector.InvokeOrDefault(new InnerHitsDescriptor<T>()));
	}
}
