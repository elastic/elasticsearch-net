// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("delete.json")]
	public partial interface IDeleteRequest { }

	// ReSharper disable once UnusedMember.Global
	// ReSharper disable once UnusedTypeParameter
	public partial interface IDeleteRequest<TDocument> where TDocument : class { }

	// ReSharper disable once UnusedMember.Global
	public partial class DeleteRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class DeleteRequest<TDocument> where TDocument : class { }

	// ReSharper disable once UnusedTypeParameter
	public partial class DeleteDescriptor<TDocument> where TDocument : class { }
}
