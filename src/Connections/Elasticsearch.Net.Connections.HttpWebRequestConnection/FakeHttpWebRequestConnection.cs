//THIS FILE is included as <None/> on windows platforms and won't actually compile.

namespace Elasticsearch.Net.Connections.HttpWebRequestConnection
{
	/// <summary>
	/// This web request implementation is only loaded for non windows platforms
	/// through csproj inclusion/exclusion. This allows us to reference and build this
	/// project on all platforms. The test project will only randomly select to use this
	/// connection implemention on windows platforms. 
	/// </summary>
	public class HttpWebRequestConnection : HttpConnection
	{
		
	}
}
