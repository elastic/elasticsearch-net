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
	public partial class IndexDescriptor<T> : DocumentOptionalPathDescriptorBase<IndexDescriptor<T>, T, IndexRequestParameters>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			pathInfo.Index.ThrowIfNull("index");
			pathInfo.Type.ThrowIfNull("type");
			var id = this._Id;
			pathInfo.HttpMethod = id != null && id.IsNullOrEmpty() ? PathInfoHttpMethod.POST : PathInfoHttpMethod.PUT;
		}
	}
}
