using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IUnregisterPercolatorRequest : IRequest<DeleteRequestParameters> { }

	public interface IUnregisterPercolatorRequest<T> : IUnregisterPercolatorRequest where T : class { }

	internal static class UnregisterPercolatorPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, RequestPath pathInfo)
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

	public partial class UnregisterPercolatorRequest : RequestBase<DeleteRequestParameters>, IUnregisterPercolatorRequest
	{
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			UnregisterPercolatorPathInfo.Update(settings, pathInfo);
		}
	}

	public partial class UnregisterPercolatorDescriptor<T>
		: RequestDescriptorBase<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters>, IUnregisterPercolatorRequest
		where T : class
	{
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			UnregisterPercolatorPathInfo.Update(settings, pathInfo);
		}
	}
}
