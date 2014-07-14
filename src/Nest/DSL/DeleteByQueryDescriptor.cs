using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteByQueryRequest : IQueryPath<DeleteByQueryRequestParameters>
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	public interface IDeleteByQueryRequest<T> : IDeleteByQueryRequest where T : class {}

	internal static class DeleteByQueryPathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo, IDeleteByQueryRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			//query works a bit different in that if all types and all indices are specified the root 
			//needs to be /_all/_query not just /_query
			if (pathInfo.Index.IsNullOrEmpty() && pathInfo.Type.IsNullOrEmpty()
				&& request.AllIndices.GetValueOrDefault(false)
				&& request.AllTypes.GetValueOrDefault(false))
				pathInfo.Index = "_all";

		}
	}
	

	public partial class DeleteByQueryRequest : QueryPathBase<DeleteByQueryRequestParameters>, IDeleteByQueryRequest
	{
		public IQueryContainer Query { get; set; }

		/// <summary>
		/// Sents a delete query to _all indices
		/// </summary>
		public DeleteByQueryRequest()
		{
			this.AllIndices = true;
			this.AllTypes = true;
		}
	
		public DeleteByQueryRequest(IndexNameMarker index, TypeNameMarker type = null)
		{
			this.Indices = new [] { index };
			if (type != null)
				this.Types = new[] { type };
			else this.AllTypes = true;
		}

		public DeleteByQueryRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null)
		{
			this.Indices = indices;
			this.AllTypes = !types.HasAny();
			this.Types = types;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryRequest<T> : QueryPathBase<DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest where T : class
	{
		public IQueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryDescriptor<T> 
		: QueryPathDescriptorBase<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest
		where T : class
	{
		private IDeleteByQueryRequest Self { get { return this; } }

		IQueryContainer IDeleteByQueryRequest.Query { get; set; }

		public DeleteByQueryDescriptor<T> MatchAll()
		{
			Self.Query = new QueryDescriptor<T>().MatchAll();
			return this;
		}

		public DeleteByQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}
	}
}
