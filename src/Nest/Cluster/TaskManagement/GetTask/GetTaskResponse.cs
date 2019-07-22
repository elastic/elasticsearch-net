using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetTaskResponse : ResponseBase
	{
		[DataMember(Name = "completed")]
		public bool Completed { get; internal set; }

		[DataMember(Name = "task")]
		public TaskInfo Task { get; internal set; }

		[DataMember(Name = "response")]
		internal LazyDocument Response { get; set; }

		public TResponse GetResponse<TResponse>() where TResponse : class, IResponse => Response?.As<TResponse>();
	}
}
