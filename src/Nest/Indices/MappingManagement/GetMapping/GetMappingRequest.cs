// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("indices.get_mapping.json")]
	public partial interface IGetMappingRequest { }

	public partial class GetMappingRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class GetMappingDescriptor<TDocument> where TDocument : class { }
}
