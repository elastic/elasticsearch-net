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
