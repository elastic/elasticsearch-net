using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteRepositoryRequest { }

	public partial class DeleteRepositoryRequest { }

	[DescriptorFor("SnapshotDeleteRepository")]
	public partial class DeleteRepositoryDescriptor { }
}
