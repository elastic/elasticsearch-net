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
	public partial class SnapshotDescriptor : IndicesOptionalPathDescriptor<SnapshotDescriptor, SnapshotRequestParameters>
		, IPathInfo<SnapshotRequestParameters>
	{
		ElasticsearchPathInfo<SnapshotRequestParameters> IPathInfo<SnapshotRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<SnapshotRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
