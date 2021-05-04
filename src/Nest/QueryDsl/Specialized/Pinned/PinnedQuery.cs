// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Promotes selected documents to rank higher than those matching a given query. This feature is typically used to
	/// guide searchers to curated documents that are promoted over and above any "organic" matches for a search.
	/// The promoted or "pinned" documents are identified using the document IDs stored in the _id field.
	/// <para />
	/// Available in Elasticsearch 7.4.0+ with at least basic license level
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(PinnedQuery))]
	public interface IPinnedQuery : IQuery
	{
		/// <summary>
		/// An array of document IDs listed in the order they are to appear in results.
		/// </summary>
		[DataMember(Name = "ids")]
		IEnumerable<Id> Ids { get; set; }

		/// <summary>
		/// Any choice of query used to rank documents which will be ranked below the "pinned" document ids.
		/// </summary>
		[DataMember(Name = "organic")]
		QueryContainer Organic { get; set; }
	}

	/// <inheritdoc cref="IPinnedQuery"/>
	public class PinnedQuery : QueryBase, IPinnedQuery
	{
		/// <inheritdoc />
		public IEnumerable<Id> Ids { get; set; }

		/// <inheritdoc />
		public QueryContainer Organic { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Pinned = this;

		internal static bool IsConditionless(IPinnedQuery q) => !q.Ids.HasAny() && q.Organic.IsConditionless();
	}

	/// <inheritdoc cref="IPinnedQuery"/>
	public class PinnedQueryDescriptor<T>
		: QueryDescriptorBase<PinnedQueryDescriptor<T>, IPinnedQuery>
			, IPinnedQuery
		where T : class
	{
		protected override bool Conditionless => PinnedQuery.IsConditionless(this);
		IEnumerable<Id> IPinnedQuery.Ids { get; set; }
		QueryContainer IPinnedQuery.Organic { get; set; }

		/// <inheritdoc cref="IPinnedQuery.Ids"/>
		public PinnedQueryDescriptor<T> Ids(params Id[] ids) =>
			Assign(ids, (a, v) => a.Ids = v);

		/// <inheritdoc cref="IPinnedQuery.Ids"/>
		public PinnedQueryDescriptor<T> Ids(IEnumerable<Id> ids) =>
			Assign(ids, (a, v) => a.Ids = v);

		/// <inheritdoc cref="IPinnedQuery.Ids"/>
		public PinnedQueryDescriptor<T> Ids(IEnumerable<string> ids) =>
			Assign(ids?.Select(i => (Id)i), (a, v) => a.Ids = v);

		/// <inheritdoc cref="IPinnedQuery.Ids"/>
		public PinnedQueryDescriptor<T> Ids(IEnumerable<long> ids) =>
			Assign(ids?.Select(i => (Id)i), (a, v) => a.Ids = v);

		/// <inheritdoc cref="IPinnedQuery.Ids"/>
		public PinnedQueryDescriptor<T> Ids(IEnumerable<Guid> ids) =>
			Assign(ids?.Select(i => (Id)i), (a, v) => a.Ids = v);

		/// <inheritdoc cref="IPinnedQuery.Organic"/>
		public PinnedQueryDescriptor<T> Organic(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Organic = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
