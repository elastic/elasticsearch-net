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
	public partial class SnapshotDescriptor : IndicesOptionalPathDescriptorBase<SnapshotDescriptor, SnapshotQueryString>
	{
		internal new ElasticSearchPathInfo<SnapshotQueryString> ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<SnapshotQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
