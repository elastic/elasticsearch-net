using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
    public interface IUnregisterPercolatorRequest : IIndexNamePath<DeleteRequestParameters> { }

    public interface IUnregisterPercolatorRequest<T> : IUnregisterPercolatorRequest where T : class { }

    internal static class UnregisterPercolatorPathInfo
    {
        public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
        {
            //deleting a percolator in elasticsearch < 1.0 is actually deleting a document in a 
            //special _percolator index where the passed index is actually a type
            //the name is actually the id, we rectify that here
            pathInfo.Index = pathInfo.Index;
            pathInfo.Id = pathInfo.Name;
            pathInfo.Type = ".percolator";
            pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
        }
    }

    public partial class UnregisterPercolatorRequest : IndexNamePathBase<DeleteRequestParameters>, IUnregisterPercolatorRequest
    {
        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
        {
            UnregisterPercolatorPathInfo.Update(settings, pathInfo);
        }
    }

    public partial class UnregisterPercolatorRequest<T> : IndexNamePathBase<DeleteRequestParameters>, IUnregisterPercolatorRequest<T>
        where T : class
    {
        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
        {
            UnregisterPercolatorPathInfo.Update(settings, pathInfo);
        }
    }

	public partial class UnregisterPercolatorDescriptor : IndexNamePathDescriptor<UnregisterPercolatorDescriptor, DeleteRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
            UnregisterPercolatorPathInfo.Update(settings, pathInfo);
		}
	}
}
