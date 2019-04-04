using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HasParentQueryDescriptor<object>>))]
	public interface IHasParentQuery : IQuery
	{
		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		[JsonProperty("inner_hits")]
		IInnerHits InnerHits { get; set; }

		[JsonProperty("parent_type")]
		TypeName ParentType { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent
		/// document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		[JsonProperty("score")]
		bool? Score { get; set; }
	}

	public class HasParentQuery : QueryBase, IHasParentQuery
	{
		public bool? IgnoreUnmapped { get; set; }
		public IInnerHits InnerHits { get; set; }
		public TypeName ParentType { get; set; }
		public QueryContainer Query { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent
		/// document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		public bool? Score { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.HasParent = this;

		internal static bool IsConditionless(IHasParentQuery q) => q.Query == null || q.Query.IsConditionless || q.ParentType == null;
	}

	public class HasParentQueryDescriptor<T>
		: QueryDescriptorBase<HasParentQueryDescriptor<T>, IHasParentQuery>
			, IHasParentQuery where T : class
	{
		public HasParentQueryDescriptor() => Self.ParentType = TypeName.Create<T>();

		protected override bool Conditionless => HasParentQuery.IsConditionless(this);
		bool? IHasParentQuery.IgnoreUnmapped { get; set; }
		IInnerHits IHasParentQuery.InnerHits { get; set; }
		TypeName IHasParentQuery.ParentType { get; set; }
		QueryContainer IHasParentQuery.Query { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent
		/// document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		bool? IHasParentQuery.Score { get; set; }

		public HasParentQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public HasParentQueryDescriptor<T> ParentType(string type) => Assign(type, (a, v) => a.ParentType = v);

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent
		/// document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		public HasParentQueryDescriptor<T> Score(bool? score = true) => Assign(score, (a, v) => a.Score = v);

		public HasParentQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(selector.InvokeOrDefault(new InnerHitsDescriptor<T>()), (a, v) => a.InnerHits = v);

		public HasParentQueryDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) =>
			Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);
	}
}
