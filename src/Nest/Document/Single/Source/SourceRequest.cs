namespace Nest
{
	public partial interface ISourceRequest { }

	public interface ISourceRequest<T> : ISourceRequest where T : class { }

	public partial class SourceRequest { }

	public partial class SourceRequest<T> where T : class { }

	[DescriptorFor("GetSource")]
	public partial class SourceDescriptor<T> where T : class
	{
		public SourceDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public SourceDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}
	}
}
