namespace Nest
{
	public partial interface IGetRequest { }
	public interface IGetRequest<T> : IGetRequest where T : class { }

	public partial class GetRequest 
	{
	}

	public partial class GetRequest<T> 
		where T : class
	{
	}

	public partial class GetDescriptor<T> where T : class
	{
		public GetDescriptor<T> ExecuteOnPrimary() => this.Preference("_primary");

		public GetDescriptor<T> ExecuteOnLocalShard() => this.Preference("_local");
	}
}
