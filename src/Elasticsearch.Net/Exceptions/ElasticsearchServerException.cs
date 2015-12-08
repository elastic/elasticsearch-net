using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ElasticsearchServerException : Exception
	{
		public Error Error { get; set; }
		public int StatusCode { get; set; }
	}

	public class Error
	{
		public List<RootCause> RootCause { get; set; }
	}

	public class RootCause
	{

	}
}
