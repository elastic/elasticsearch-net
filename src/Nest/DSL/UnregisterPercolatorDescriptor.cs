using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class UnregisterPercolatorDescriptor 
		: IndexNamePathDescriptor<UnregisterPercolatorDescriptor, DeleteQueryString>
		, IPathInfo<DeleteQueryString>
	{
		ElasticsearchPathInfo<DeleteQueryString> IPathInfo<DeleteQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			//deleting a percolator in elasticsearch < 1.0 is actually deleting a document in a 
			//special _percolator index where the passed index is actually a type
			//the name is actually the id, we rectify that here

			var pathInfo = base.ToPathInfo<DeleteQueryString>(settings, new DeleteQueryString());
			pathInfo.Type = pathInfo.Index;
			pathInfo.Id = pathInfo.Name;
			pathInfo.Index = "_percolator";
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;

			return pathInfo;
		}
	}
}
