namespace Nest
{
	public partial interface IGetRequest { }

	public partial interface IGetRequest<TDocument> where TDocument : class { }

	public partial class GetRequest { }

	public partial class GetRequest<TDocument> where TDocument : class { }

	public partial class GetDescriptor<TDocument> where TDocument : class
	{
		public GetDescriptor<TDocument> ExecuteOnPrimary() => Preference("_primary");

		public GetDescriptor<TDocument> ExecuteOnLocalShard() => Preference("_local");
	}
}
