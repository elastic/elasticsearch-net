namespace Nest
{
	public partial interface ISourceRequest { }

	public interface ISourceRequest<T> : ISourceRequest where T : class { }

	public partial class SourceRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class SourceRequest<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}

	[DescriptorFor("GetSource")]
	public partial class SourceDescriptor<T> where T : class
	{
		public SourceDescriptor<T> ExecuteOnLocalShard() => Preference("_local");

		public SourceDescriptor<T> ExecuteOnPrimary() => Preference("_primary");

		private object AutoRouteDocument() => null;
	}
}
