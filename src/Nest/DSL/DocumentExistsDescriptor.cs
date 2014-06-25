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
	public partial class DocumentExistsDescriptor<T> : DocumentPathDescriptorBase<DocumentExistsDescriptor<T>, T, DocumentExistsRequestParameters>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DocumentExistsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
}
