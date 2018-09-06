#if !DOTNETCORE
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	/// <summary> The default IConnection implementation. Uses <see cref="HttpWebRequest"/> on the current .NET desktop framework.</summary>
	public class HttpConnection : HttpWebRequestConnection
	{
	}
}
#endif
