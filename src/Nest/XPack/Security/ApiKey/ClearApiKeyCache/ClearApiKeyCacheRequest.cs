// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("security.clear_api_key_cache")]
	[ReadAs(typeof(ClearApiKeyCacheRequest))]
	public partial interface IClearApiKeyCacheRequest { }

	public partial class ClearApiKeyCacheRequest { }

	public partial class ClearApiKeyCacheDescriptor { }
}
