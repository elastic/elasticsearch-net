using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatNodesRequest : IRequest<CatNodesRequestParameters> { }

	public partial class CatNodesRequest : RequestBase<CatNodesRequestParameters>, ICatNodesRequest { }

	public partial class CatNodesDescriptor : RequestDescriptorBase<CatNodesDescriptor, CatNodesRequestParameters>, ICatNodesRequest { }
}
