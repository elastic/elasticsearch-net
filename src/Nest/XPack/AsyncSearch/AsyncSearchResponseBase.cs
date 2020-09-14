using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IAsyncSearchResponse<TDocument> : IResponse where TDocument : class
	{
		[DataMember(Name = "id")]
		string Id { get; }

		[DataMember(Name = "is_partial")]
		bool IsPartial { get; }

		[DataMember(Name = "start_time_in_millis")]
		long StartTimeInMilliseconds { get; }

		[IgnoreDataMember]
		DateTimeOffset StartTime { get; }

		[DataMember(Name = "is_running")]
		bool IsRunning { get; }

		[DataMember(Name = "expiration_time_in_millis")]
		long ExpirationTimeInMilliseconds { get; }

		[IgnoreDataMember]
		DateTimeOffset ExpirationTime { get; }

		[DataMember(Name = "response")]
		AsyncSearch<TDocument> Response { get; }
	}

	[DataContract]
	public abstract class AsyncSearchResponseBase<TDocument>
		: ResponseBase, IAsyncSearchResponse<TDocument> where TDocument: class
	{
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		[DataMember(Name = "is_partial")]
		public bool IsPartial { get; internal set; }

		[DataMember(Name = "start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[IgnoreDataMember]
		public DateTimeOffset StartTime => DateTimeUtil.UnixEpoch.AddMilliseconds(StartTimeInMilliseconds);

		[DataMember(Name = "is_running")]
		public bool IsRunning { get; internal set; }

		[DataMember(Name = "expiration_time_in_millis")]
		public long ExpirationTimeInMilliseconds { get; internal set; }

		[IgnoreDataMember]
		public DateTimeOffset ExpirationTime => DateTimeUtil.UnixEpoch.AddMilliseconds(ExpirationTimeInMilliseconds);

		[DataMember(Name = "response")]
		public AsyncSearch<TDocument> Response { get; internal set; }
	}
}
