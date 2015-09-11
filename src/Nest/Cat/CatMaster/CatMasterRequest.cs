using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatMasterRequest : IRequest<CatMasterRequestParameters> { }

	public partial class CatMasterRequest : PathRequestBase<CatMasterRequestParameters>, ICatMasterRequest { }

	public partial class CatMasterDescriptor : PathDescriptorBase<CatMasterDescriptor, CatMasterRequestParameters>, ICatMasterRequest { }
}
