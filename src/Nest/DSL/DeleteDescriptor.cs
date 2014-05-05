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
	[DescriptorFor("Delete")]
	public partial class DeleteDescriptor<T> : DocumentPathDescriptorBase<DeleteDescriptor<T>, T, DeleteRequestParameters>
		, IPathInfo<DeleteRequestParameters>
		where T : class
	{
		ElasticsearchPathInfo<DeleteRequestParameters> IPathInfo<DeleteRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			return pathInfo;
		}
	}
}
