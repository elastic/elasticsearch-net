using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteScriptRequest { }

	public partial class DeleteScriptRequest { }

	[DescriptorFor("ScriptDelete")]
	public partial class DeleteScriptDescriptor { }
}