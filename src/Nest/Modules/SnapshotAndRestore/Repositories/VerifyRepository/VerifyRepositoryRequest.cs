using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IVerifyRepositoryRequest { }

	public partial class VerifyRepositoryRequest { }

	[DescriptorFor("SnapshotVerifyRepository")]
	public partial class VerifyRepositoryDescriptor { }
}
