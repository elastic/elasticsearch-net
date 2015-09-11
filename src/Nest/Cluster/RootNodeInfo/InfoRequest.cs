using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IInfoRequest : IRequest<InfoRequestParameters> { }

	public partial class InfoRequest : PathRequestBase<InfoRequestParameters>, IInfoRequest { }

	[DescriptorFor("Info")]
	public partial class InfoDescriptor : PathDescriptorBase<InfoDescriptor, InfoRequestParameters>, IInfoRequest { }
}
