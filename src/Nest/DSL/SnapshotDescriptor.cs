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
	[DescriptorFor("IndicesSnapshotIndex")]
	public partial class SnapshotDescriptor : IndicesOptionalPathDescriptor<SnapshotDescriptor, SnapshotQueryString>
		, IPathInfo<SnapshotQueryString>
	{
		ElasticSearchPathInfo<SnapshotQueryString> IPathInfo<SnapshotQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<SnapshotQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
