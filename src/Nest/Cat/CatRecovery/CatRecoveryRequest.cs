using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatRecoveryRequest : IRequest<CatRecoveryRequestParameters> { }

	public partial class CatRecoveryRequest : RequestBase<CatRecoveryRequestParameters>, ICatRecoveryRequest { }

	public partial class CatRecoveryDescriptor : RequestDescriptorBase<CatRecoveryDescriptor, CatRecoveryRequestParameters>, ICatRecoveryRequest { }
}
