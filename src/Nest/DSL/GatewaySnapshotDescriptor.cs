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
	[DescriptorFor("IndicesSnapshotIndex")]
	public partial class GatewaySnapshotDescriptor : IndicesOptionalPathDescriptor<GatewaySnapshotDescriptor, GatewaySnapshotRequestParameters>
		, IPathInfo<GatewaySnapshotRequestParameters>
	{
		ElasticsearchPathInfo<GatewaySnapshotRequestParameters> IPathInfo<GatewaySnapshotRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<GatewaySnapshotRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
