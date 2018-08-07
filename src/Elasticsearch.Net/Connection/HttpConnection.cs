#if FEATURE_HTTPWEBREQUEST
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
	public class HttpConnection : HttpWebRequestConnection
	{
	}
}
#endif
