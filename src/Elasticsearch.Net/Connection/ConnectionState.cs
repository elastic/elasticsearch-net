using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Elasticsearch.Net
{
	class ConnectionState
	{
		public HttpWebRequest Connection { get; set; }
	}
}
