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
		, IPathInfo<DocumentExistsRequestParameters>
		where T : class
	{
		ElasticsearchPathInfo<DocumentExistsRequestParameters> IPathInfo<DocumentExistsRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<DocumentExistsRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
			return pathInfo;
		}
	}
}
