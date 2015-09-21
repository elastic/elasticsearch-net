using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	//This does not represent an actual dedicated API endpoint in elasticsearch 

	public interface IUnregisterPercolatorRequest : IRequest<DeleteRequestParameters> { }

	//TODO port complex route values logic

	//internal static class UnregisterPercolatorPathInfo
	//{
	//	public static void Update(IConnectionSettingsValues settings, RouteValues pathInfo)
	//	{
	//		//deleting a percolator in elasticsearch < 1.0 is actually deleting a document in a 
	//		//special _percolator index where the passed index is actually a type
	//		//the name is actually the id, we rectify that here
	//		pathInfo.Index = pathInfo.Index;
	//		pathInfo.Id = pathInfo.Name;
	//		pathInfo.Type = ".percolator";
	//		pathInfo.HttpMethod = HttpMethod.DELETE;
	//	}
	//}

	public class UnregisterPercolatorRequest : RequestBase<DeleteRequestParameters>, IUnregisterPercolatorRequest
	{
	}

	public class UnregisterPercolatorDescriptor<T>
		: RequestDescriptorBase<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters>, IUnregisterPercolatorRequest
		where T : class
	{
	}
}
