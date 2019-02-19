namespace Nest
{
	public partial interface IGetRequest { }

	public partial interface IGetRequest<TDocument> where TDocument : class { }

	public partial class GetRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class GetRequest<TDocument> where TDocument : class
	{
		private object AutoRouteDocument() => null;
	}

	public partial class GetDescriptor<TDocument> where TDocument : class
	{
		private object AutoRouteDocument() => null;

		public GetDescriptor<TDocument> ExecuteOnPrimary() => Preference("_primary");

		public GetDescriptor<TDocument> ExecuteOnLocalShard() => Preference("_local");
	}
}
