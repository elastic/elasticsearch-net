// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("get_source.json")]
	[ResponseBuilderWithGeneric("SourceRequestResponseBuilder<TDocument>.Instance")]
	public partial interface ISourceRequest { }

	// ReSharper disable UnusedTypeParameter
	public partial interface ISourceRequest<TDocument> where TDocument : class { }

	public partial class SourceRequest { }

	// ReSharper disable UnusedTypeParameter
	public partial class SourceRequest<TDocument> where TDocument : class { }

	public partial class SourceDescriptor<TDocument> where TDocument : class
	{
		public SourceDescriptor<TDocument> ExecuteOnLocalShard() => Preference("_local");
	}
}
