namespace Nest
{
	public partial interface IGetRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial interface IGetRequest<TDocument> where TDocument : class { }

	public partial class GetRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class GetRequest<TDocument> where TDocument : class { }

	public partial class GetDescriptor<TDocument> where TDocument : class
	{
		public GetDescriptor<TDocument> ExecuteOnLocalShard() => Preference("_local");
	}
}
