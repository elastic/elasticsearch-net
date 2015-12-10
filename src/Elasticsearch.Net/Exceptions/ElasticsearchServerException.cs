using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ElasticsearchServerException : Exception
	{
		public ElasticsearchServerException(SerializationInfo info, StreamingContext context)
			: this() { }

		public ElasticsearchServerException()
			: base("Elasticsearch returned an error.") { }

		public ElasticsearchError Error { get; set; }
		public int StatusCode { get; set; }
	}

	public interface IRootCause
	{
		string Type { get; set; }
		string Reason { get; set; }
		string ResourceId { get; set; }
		string ResourceType { get; set; }
		string Index { get; set; }
	}

	public class ElasticsearchError : IRootCause
	{
		public string Index { get; set; }
		public string Reason { get; set; }
		public string ResourceId { get; set; }
		public string ResourceType { get; set; }
		public string Type { get; set; }
		public List<RootCause> RootCause { get; set; }
	}

	public class RootCause : IRootCause
	{
		public string Index { get; set; }
		public string Reason { get; set; }
		public string ResourceId { get; set; }
		public string ResourceType { get; set; }
		public string Type { get; set; }
	}
}
