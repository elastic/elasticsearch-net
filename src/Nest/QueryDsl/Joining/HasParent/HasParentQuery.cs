using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HasParentQueryDescriptor<object>>))]
	public interface IHasParentQuery : IQuery
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		[JsonProperty("score")]
		bool? Score { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("inner_hits")]
		IInnerHits InnerHits { get; set; }

	}

	public class HasParentQuery : QueryBase, IHasParentQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public TypeName Type { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		public bool? Score{ get; set; }
		public QueryContainer Query { get; set; }
		public IInnerHits InnerHits { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.HasParent = this;
		internal static bool IsConditionless(IHasParentQuery q) => q.Query == null || q.Query.IsConditionless || q.Type == null;
	}

	public class HasParentQueryDescriptor<T> 
		: QueryDescriptorBase<HasParentQueryDescriptor<T>, IHasParentQuery>
		, IHasParentQuery where T : class
	{
		protected override bool Conditionless => HasParentQuery.IsConditionless(this);
		TypeName IHasParentQuery.Type { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		bool? IHasParentQuery.Score { get; set; }
		IInnerHits IHasParentQuery.InnerHits { get; set; }
		QueryContainer IHasParentQuery.Query { get; set; }

		public HasParentQueryDescriptor() { Self.Type = TypeName.Create<T>(); }

		public HasParentQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.Query = selector?.Invoke(new QueryContainerDescriptor<T>()));

		public HasParentQueryDescriptor<T> Type(string type) => Assign(a => a.Type = type);

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		public HasParentQueryDescriptor<T> Score(bool score) => Assign(a => a.Score = score);

		public HasParentQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(a => a.InnerHits = selector.InvokeOrDefault(new InnerHitsDescriptor<T>()));
	}
}
