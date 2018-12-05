using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HasParentQueryDescriptor<object>))]
	public interface IHasParentQuery : IQuery
	{
		[DataMember(Name ="ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		[DataMember(Name ="inner_hits")]
		IInnerHits InnerHits { get; set; }

		[DataMember(Name = "parent_type")]
		RelationName ParentType { get; set; }

		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent
		/// document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		[DataMember(Name ="score")]
		bool? Score { get; set; }
	}

	public class HasParentQuery : QueryBase, IHasParentQuery
	{
		public bool? IgnoreUnmapped { get; set; }
		public IInnerHits InnerHits { get; set; }
		public RelationName ParentType { get; set; }
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
		public HasParentQueryDescriptor() => Self.ParentType = RelationName.Create<T>();

		protected override bool Conditionless => HasParentQuery.IsConditionless(this);
		bool? IHasParentQuery.IgnoreUnmapped { get; set; }
		IInnerHits IHasParentQuery.InnerHits { get; set; }
		RelationName IHasParentQuery.ParentType { get; set; }
		QueryContainer IHasParentQuery.Query { get; set; }

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent
		/// document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		bool? IHasParentQuery.Score { get; set; }

		public HasParentQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.Query = selector?.Invoke(new QueryContainerDescriptor<T>()));

		public HasParentQueryDescriptor<T> ParentType(string type) => Assign(a => a.ParentType = type);

		/// <summary>
		/// Determines whether the score of the matching parent document is aggregated into the child documents belonging to the matching parent
		/// document.
		/// The default is false which ignores the score from the parent document.
		/// </summary>
		public HasParentQueryDescriptor<T> Score(bool? score = true) => Assign(a => a.Score = score);

		public HasParentQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(a => a.InnerHits = selector.InvokeOrDefault(new InnerHitsDescriptor<T>()));

		public HasParentQueryDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) =>
			Assign(a => a.IgnoreUnmapped = ignoreUnmapped);
	}
}
