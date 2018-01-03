using Elasticsearch.Net;

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
		private object AutoRouteDocument() => null;
		
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
