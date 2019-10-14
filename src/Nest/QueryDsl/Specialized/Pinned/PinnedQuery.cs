using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PinnedQuery))]
	public interface IPinnedQuery : IQuery
	{
		[DataMember(Name = "ids")]
		IEnumerable<Id> Ids { get; set; }

		[DataMember(Name = "organic")]
		QueryContainer Organic { get; set; }
	}

	public class PinnedQuery : QueryBase, IPinnedQuery
	{
		public IEnumerable<Id> Ids { get; set; }

		public QueryContainer Organic { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Pinned = this;

		internal static bool IsConditionless(IPinnedQuery q) => !q.Ids.HasAny() && q.Organic.IsConditionless();
	}

	public class PinnedQueryDescriptor<T>
		: QueryDescriptorBase<PinnedQueryDescriptor<T>, IPinnedQuery>
			, IPinnedQuery
		where T : class
	{
		protected override bool Conditionless => PinnedQuery.IsConditionless(this);
		IEnumerable<Id> IPinnedQuery.Ids { get; set; }
		QueryContainer IPinnedQuery.Organic { get; set; }

		public PinnedQueryDescriptor<T> Ids(params Id[] ids) => Assign(ids, (a, v) => a.Ids = v);

		public PinnedQueryDescriptor<T> Ids(IEnumerable<Id> ids) => Ids(ids?.ToArray());

		public PinnedQueryDescriptor<T> Ids(params string[] ids) => Assign(ids?.Select(v => (Id)v), (a, v) => a.Ids = v);

		public PinnedQueryDescriptor<T> Ids(IEnumerable<string> ids) => Ids(ids.ToArray());

		public PinnedQueryDescriptor<T> Ids(params long[] ids) => Assign(ids?.Select(v => (Id)v), (a, v) => a.Ids = v);

		public PinnedQueryDescriptor<T> Ids(IEnumerable<long> ids) => Ids(ids.ToArray());

		public PinnedQueryDescriptor<T> Ids(params Guid[] ids) => Assign(ids?.Select(v => (Id)v), (a, v) => a.Ids = v);

		public PinnedQueryDescriptor<T> Ids(IEnumerable<Guid> ids) => Ids(ids.ToArray());

		public PinnedQueryDescriptor<T> Organic(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Organic = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
