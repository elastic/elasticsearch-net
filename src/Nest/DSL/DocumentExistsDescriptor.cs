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
	[DescriptorFor("Exists")]
	public partial class IndexDescriptor<T> : DocumentPathDescriptorBase<IndexDescriptor<T>, T, IndexRequestParameters>
		, IPathInfo<IndexRequestParameters>
		where T : class
	{
		ElasticsearchPathInfo<IndexRequestParameters> IPathInfo<IndexRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = this._Id.IsNullOrEmpty() ? PathInfoHttpMethod.POST : PathInfoHttpMethod.PUT;
			return pathInfo;
		}
	}
}
