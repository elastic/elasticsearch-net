using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IUnregisterPercolatorRequest : IIndexNamePath<DeleteRequestParameters> { }

	public interface IUnregisterPercolatorRequest<T> : IUnregisterPercolatorRequest where T : class { }

	internal static class UnregisterPercolatorPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, RequestPath<DeleteRequestParameters> pathInfo)
		{
			//deleting a percolator in elasticsearch < 1.0 is actually deleting a document in a 
			//special _percolator index where the passed index is actually a type
			//the name is actually the id, we rectify that here
			pathInfo.Index = pathInfo.Index;
			pathInfo.Id = pathInfo.Name;
			pathInfo.Type = ".percolator";
			pathInfo.HttpMethod = HttpMethod.DELETE;
		}
	}

	public partial class UnregisterPercolatorRequest : IndexNamePathBase<DeleteRequestParameters>, IUnregisterPercolatorRequest
	{
		public UnregisterPercolatorRequest(IndexName index, string name) : base(index, name)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<DeleteRequestParameters> pathInfo)
		{
			UnregisterPercolatorPathInfo.Update(settings, pathInfo);
		}
	}

	public partial class UnregisterPercolatorDescriptor<T>
		: IndexNamePathDescriptor<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters, T>, IUnregisterPercolatorRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<DeleteRequestParameters> pathInfo)
		{
			UnregisterPercolatorPathInfo.Update(settings, pathInfo);
		}
	}
}
