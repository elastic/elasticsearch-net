#if !DOTNETCORE
using System.Net;

namespace Elasticsearch.Net
{
	/// <summary> The default IConnection implementation. Uses <see cref="HttpWebRequest" /> on the current .NET desktop framework.</summary>
	public class HttpConnection : HttpWebRequestConnection { }
}
#endif
