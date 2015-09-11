using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatAllocationRequest : IRequest<CatAllocationRequestParameters> { }

	public partial class CatAllocationRequest : PathRequestBase<CatAllocationRequestParameters>, ICatAllocationRequest { }

	public partial class CatAllocationDescriptor : RequestDescriptorBase<CatAllocationDescriptor, CatAllocationRequestParameters>, ICatAllocationRequest { }
}
