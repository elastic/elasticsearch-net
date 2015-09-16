using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IInfoRequest : IRequest<InfoRequestParameters> { }

	public partial class InfoRequest : RequestBase<InfoRequestParameters>, IInfoRequest { }

	[DescriptorFor("Info")]
	public partial class InfoDescriptor : RequestDescriptorBase<InfoDescriptor, InfoRequestParameters>, IInfoRequest { }
}
