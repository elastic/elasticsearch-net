using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Elasticsearch.Net.Connection
{
	public class ElasticsearchServerException : Exception
	{
		public int Status { get; set; }
		public string Error { get; set; }
		public string ExceptionType { get; set; }
	}
}
