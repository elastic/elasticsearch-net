// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("eql.get_status.json")]
	[ReadAs(typeof(EqlSearchStatusRequest))]
	public partial interface IEqlSearchStatusRequest { }

	public partial class EqlSearchStatusRequest { }

	public partial class EqlSearchStatusDescriptor { }
}
