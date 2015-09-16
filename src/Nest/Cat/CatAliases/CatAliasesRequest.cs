using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatAliasesRequest : IRequest<CatAliasesRequestParameters> { }

	public partial class CatAliasesRequest : RequestBase<CatAliasesRequestParameters>, ICatAliasesRequest { }

	public partial class CatAliasesDescriptor : RequestDescriptorBase<CatAliasesDescriptor, CatAliasesRequestParameters>, ICatAliasesRequest { }
}
