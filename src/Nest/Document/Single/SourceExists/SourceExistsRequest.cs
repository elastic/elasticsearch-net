namespace Nest
{
	public partial interface ISourceExistsRequest { }

	public interface ISourceExistsRequest<T> : ISourceExistsRequest where T : class { }

	public partial class SourceExistsRequest { }

	public partial class SourceExistsRequest<T> where T : class { }

	[DescriptorFor("ExistsSource")]
	public partial class SourceExistsDescriptor<T> where T : class { }
}
