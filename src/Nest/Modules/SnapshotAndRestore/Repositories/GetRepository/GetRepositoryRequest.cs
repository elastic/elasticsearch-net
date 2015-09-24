using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetRepositoryRequest { }

	public partial class GetRepositoryRequest { }

	[DescriptorFor("SnapshotGetRepository")]
	public partial class GetRepositoryDescriptor { }
}
