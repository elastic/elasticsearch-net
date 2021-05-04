// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HasChildQueryDescriptor<object>))]
	public interface IHasChildQuery : IQuery
	{
		[DataMember(Name ="ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		[DataMember(Name ="inner_hits")]
		IInnerHits InnerHits { get; set; }

		/// <summary>
		/// Specify how many child documents are allowed to match.
		/// </summary>
		[DataMember(Name ="max_children")]
		int? MaxChildren { get; set; }

		[DataMember(Name ="min_children")]
		int? MinChildren { get; set; }

		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		[DataMember(Name ="score_mode")]
		ChildScoreMode? ScoreMode { get; set; }

		[DataMember(Name = "type")]
		RelationName Type { get; set; }
	}

	public class HasChildQuery : QueryBase, IHasChildQuery
	{
		public bool? IgnoreUnmapped { get; set; }
		public IInnerHits InnerHits { get; set; }

		/// <summary>
		/// Specify how many child documents are allowed to match.
		/// </summary>
		public int? MaxChildren { get; set; }

		public int? MinChildren { get; set; }
		public QueryContainer Query { get; set; }
		public ChildScoreMode? ScoreMode { get; set; }
		public RelationName Type { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.HasChild = this;

		internal static bool IsConditionless(IHasChildQuery q) => q.Query == null || q.Query.IsConditionless || q.Type == null;
	}

	public class HasChildQueryDescriptor<T>
		: QueryDescriptorBase<HasChildQueryDescriptor<T>, IHasChildQuery>
			, IHasChildQuery where T : class
	{
		public HasChildQueryDescriptor() => Self.Type = RelationName.Create<T>();

		protected override bool Conditionless => HasChildQuery.IsConditionless(this);
		bool? IHasChildQuery.IgnoreUnmapped { get; set; }
		IInnerHits IHasChildQuery.InnerHits { get; set; }

		/// <summary>
		/// Specify how many child documents are allowed to match.
		/// </summary>
		int? IHasChildQuery.MaxChildren { get; set; }

		int? IHasChildQuery.MinChildren { get; set; }
		QueryContainer IHasChildQuery.Query { get; set; }
		ChildScoreMode? IHasChildQuery.ScoreMode { get; set; }
		RelationName IHasChildQuery.Type { get; set; }

		public HasChildQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public HasChildQueryDescriptor<T> Type(string type) => Assign(type, (a, v) => a.Type = v);

		public HasChildQueryDescriptor<T> ScoreMode(ChildScoreMode? scoreMode) => Assign(scoreMode, (a, v) => a.ScoreMode = v);

		public HasChildQueryDescriptor<T> MinChildren(int? minChildren) => Assign(minChildren, (a, v) => a.MinChildren = v);

		/// <summary>
		/// Specify how many child documents are allowed to match.
		/// </summary>
		public HasChildQueryDescriptor<T> MaxChildren(int? maxChildren) => Assign(maxChildren, (a, v) => a.MaxChildren = v);

		public HasChildQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(selector.InvokeOrDefault(new InnerHitsDescriptor<T>()), (a, v) => a.InnerHits = v);

		public HasChildQueryDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) =>
			Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);
	}
}
