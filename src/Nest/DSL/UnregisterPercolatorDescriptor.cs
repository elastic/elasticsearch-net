using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class UnregisterPercolatorDescriptor : IndexNamePathDescriptor<UnregisterPercolatorDescriptor, DeleteRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
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
}
