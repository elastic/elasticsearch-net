namespace Nest
{
	public partial interface IGetRequest { }

	public interface IGetRequest<T> : IGetRequest where T : class { }

	public partial class GetRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class GetRequest<T>
		where T : class
	{
		private object AutoRouteDocument() => null;
	}

	public partial class GetDescriptor<T> where T : class
	{
		public GetDescriptor<T> ExecuteOnLocalShard() => Preference("_local");

		public GetDescriptor<T> ExecuteOnPrimary() => Preference("_primary");

		private object AutoRouteDocument() => null;
	}
}
