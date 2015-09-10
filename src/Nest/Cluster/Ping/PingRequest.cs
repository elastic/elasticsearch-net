using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPingRequest : IRequest<PingRequestParameters> { }

	public partial class PingRequest : BasePathRequest<PingRequestParameters>, IPingRequest { }

	public partial class PingDescriptor : BasePathDescriptor<PingDescriptor, PingRequestParameters>, IPingRequest { }
}
