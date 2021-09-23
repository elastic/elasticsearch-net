// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public partial class QueryContainer
	{
		public QueryContainer() { }

		public QueryContainer(QueryBase query) : this()
		{
			if (query == null)
				return;

			//if (query.IsStrict && !query.IsWritable)
			//	throw new ArgumentException("Query is conditionless but strict is turned on");

			query.WrapInContainer(this);
		}

		[JsonIgnore]
		private IQueryContainer _self => this;

		[JsonIgnore]
		internal IQuery ContainedQuery { get; set; }

		private T Set<T>(T value) where T : IQuery
		{
			if (ContainedQuery != null)
				throw new Exception(
					$"Cannot assign {typeof(T).Name} to {nameof(QueryContainer)}. "
					+ $"It can only hold a single query and already contains a {ContainedQuery.GetType().Name}");

			ContainedQuery = value;
			return value;
		}
	}

	public partial class QueryContainerDescriptor<T>
	{
		private QueryContainer WrapInContainer<TQuery, TQueryInterface>(
			Func<TQuery, TQueryInterface> create,
			Action<TQueryInterface, IQueryContainer> assign
		) where TQuery : class, TQueryInterface, IQuery, new()
			where TQueryInterface : class, IQuery
		{
			// Invoke the create delegate before assigning container; the create delegate
			// may mutate the current QueryContainerDescriptor<T> instance such that it
			// contains a query. See https://github.com/elastic/elasticsearch-net/issues/2875
			var query = create.InvokeOrDefault(new TQuery());

			var container = ContainedQuery == null
				? this
				: new QueryContainerDescriptor<T>();

			IQueryContainer c = container;
			//c.IsVerbatim = query.IsVerbatim;
			//c.IsStrict = query.IsStrict;
			assign(query, container);
			container.ContainedQuery = query;

			//if query is writable (not conditionless or verbatim): return a container that holds the query
			if (query.IsWritable)
				return container;

			////query is conditionless but marked as strict, throw exception
			//if (query.IsStrict)
			//	throw new ArgumentException("Query is conditionless but strict is turned on");

			//query is conditionless return an empty container that can later be rewritten
			return null;
		}
	}
}
