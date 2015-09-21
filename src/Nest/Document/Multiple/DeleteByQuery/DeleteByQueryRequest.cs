using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteByQueryRequest 
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	public interface IDeleteByQueryRequest<T> : IDeleteByQueryRequest where T : class { }

	public partial class DeleteByQueryRequest 
	{
		public IQueryContainer Query { get; set; }

	}
	
	//TODO inherit from deleteybyqueryrequest
	public partial class DeleteByQueryRequest<T> : RequestBase<DeleteByQueryRequestParameters>, IDeleteByQueryRequest where T : class
	{
		public IQueryContainer Query { get; set; }

	}

	public partial class DeleteByQueryDescriptor<T> where T : class
	{
		private IDeleteByQueryRequest Self => this;

		IQueryContainer IDeleteByQueryRequest.Query { get; set; }

		public DeleteByQueryDescriptor()
			: base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()))
		{ }

		public DeleteByQueryDescriptor(Indices indices, Types types)
			: base(p => p.Required(indices).Required(types))
		{ }

		public DeleteByQueryDescriptor(Indices indices)
			: base(p => p.Required(indices).Required(Types.Single<T>()))
		{ }

		public DeleteByQueryDescriptor(Types types)
			: base(p => p.Required(types).Required(Indices.Single<T>()))
		{ }


		public DeleteByQueryDescriptor<T> MatchAll()
		{
			Self.Query = new QueryContainerDescriptor<T>().MatchAll();
			return this;
		}

		public DeleteByQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryContainerDescriptor<T>());
			return this;
		}
	}
}
