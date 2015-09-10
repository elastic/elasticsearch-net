using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterGetSettingsRequest : IRequest<ClusterGetSettingsRequestParameters> { 
	}
	
	public partial class ClusterGetSettingsRequest : BasePathRequest<ClusterGetSettingsRequestParameters>, IClusterGetSettingsRequest { }

	public partial class ClusterGetSettingsDescriptor : BasePathDescriptor<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters>
		, IClusterGetSettingsRequest { }
}
