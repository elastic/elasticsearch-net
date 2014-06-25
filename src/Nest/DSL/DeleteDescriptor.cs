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
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
}
