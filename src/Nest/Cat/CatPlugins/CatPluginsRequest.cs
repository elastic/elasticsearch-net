using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatPluginsRequest : IRequest<CatPluginsRequestParameters> { }

	public partial class CatPluginsRequest : PathRequestBase<CatPluginsRequestParameters>, ICatPluginsRequest { }

	public partial class CatPluginsDescriptor : RequestDescriptorBase<CatPluginsDescriptor, CatPluginsRequestParameters>, ICatPluginsRequest { }
}
