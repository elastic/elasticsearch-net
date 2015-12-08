using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ElasticsearchServerException : Exception
	{
		public ElasticsearchError Error { get; set; }
		public int StatusCode { get; set; }
	}

	public interface IRootCause
	{

	}

	public class ElasticsearchError : IRootCause
	{
		public List<RootCause> RootCause { get; set; }
	}

	public class RootCause : IRootCause
	{

	}
}
