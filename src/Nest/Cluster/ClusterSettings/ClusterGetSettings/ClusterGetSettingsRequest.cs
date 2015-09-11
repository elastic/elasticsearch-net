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
	
	public partial class ClusterGetSettingsRequest : PathRequestBase<ClusterGetSettingsRequestParameters>, IClusterGetSettingsRequest { }

	public partial class ClusterGetSettingsDescriptor : RequestDescriptorBase<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters>
		, IClusterGetSettingsRequest { }
}
