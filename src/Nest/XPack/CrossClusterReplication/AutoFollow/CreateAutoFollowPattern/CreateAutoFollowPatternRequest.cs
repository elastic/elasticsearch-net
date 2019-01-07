using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// This API creates a new named collection of auto-follow patterns against the remote cluster specified
	/// in the request body. Newly created indices on the remote cluster matching any of the specified patterns
	/// will be automatically configured as follower indices.
	/// </summary>
	[MapsApi("ccr.put_auto_follow_pattern.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<CreateAutoFollowPatternRequest>))]
	public partial interface ICreateAutoFollowPatternRequest : IAutoFollowPattern { }

	/// <inheritdoc cref="ICreateAutoFollowPatternRequest"/>
	public partial class CreateAutoFollowPatternRequest
	{
		/// <inheritdoc cref="IAutoFollowPattern.RemoteCluster"/>
		public string RemoteCluster { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public IEnumerable<string> LeaderIndexPatterns { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.FollowIndexPattern"/>
		public string FollowIndexPattern { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestOperationCount"/>
		public int? MaxReadRequestOperationCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingReadRequests"/>
		public long? MaxOutstandingReadRequests { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestSize"/>
		public string MaxReadRequestSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestOperationCount"/>
		public int? MaxWriteRequestOperationCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestSize"/>
		public string MaxWriteRequestSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingWriteRequests"/>
		public int? MaxOutstandingWriteRequests { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferCount"/>
		public int? MaxWriteBufferCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferSize"/>
		public string MaxWriteBufferSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxRetryDelay"/>
		public Time MaxRetryDelay { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxPollTimeout"/>
		public Time MaxPollTimeout { get; set; }
	}

	/// <inheritdoc cref="ICreateAutoFollowPatternRequest"/>
	public partial class CreateAutoFollowPatternDescriptor
	{
		string IAutoFollowPattern.RemoteCluster { get; set; }
		IEnumerable<string> IAutoFollowPattern.LeaderIndexPatterns { get; set; }
		string IAutoFollowPattern.FollowIndexPattern { get; set; }
		int? IAutoFollowPattern.MaxReadRequestOperationCount { get; set; }
		long? IAutoFollowPattern.MaxOutstandingReadRequests { get; set; }
		string IAutoFollowPattern.MaxReadRequestSize { get; set; }
		int? IAutoFollowPattern.MaxWriteRequestOperationCount { get; set; }
		string IAutoFollowPattern.MaxWriteRequestSize { get; set; }
		int? IAutoFollowPattern.MaxOutstandingWriteRequests { get; set; }
		int? IAutoFollowPattern.MaxWriteBufferCount { get; set; }
		string IAutoFollowPattern.MaxWriteBufferSize { get; set; }
		Time IAutoFollowPattern.MaxRetryDelay { get; set; }
		Time IAutoFollowPattern.MaxPollTimeout { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.RemoteCluster"/>
		public CreateAutoFollowPatternDescriptor RemoteCluster(string remoteCluster) => Assign(a => a.RemoteCluster = remoteCluster);

		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public CreateAutoFollowPatternDescriptor LeaderIndexPatterns(IEnumerable<string> leaderIndexPatterns) =>
			Assign(a => a.LeaderIndexPatterns = leaderIndexPatterns);

		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public CreateAutoFollowPatternDescriptor LeaderIndexPatterns(params string[] leaderIndexPatterns) =>
			Assign(a => a.LeaderIndexPatterns = leaderIndexPatterns);

		/// <inheritdoc cref="IAutoFollowPattern.FollowIndexPattern"/>
		public CreateAutoFollowPatternDescriptor FollowIndexPattern(string followIndexPattern) =>
			Assign(a => a.FollowIndexPattern = followIndexPattern);

		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestOperationCount"/>
		public CreateAutoFollowPatternDescriptor MaxReadRequestOperationCount(int? maxReadRequestOperationCount) =>
			Assign(a => a.MaxReadRequestOperationCount = maxReadRequestOperationCount);

		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingReadRequests"/>
		public CreateAutoFollowPatternDescriptor MaxOutstandingReadRequests(long? maxOutstandingReadRequests) =>
			Assign(a => a.MaxOutstandingReadRequests = maxOutstandingReadRequests);

		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestSize"/>
		public CreateAutoFollowPatternDescriptor MaxReadRequestSize(string maxReadRequestSize) =>
			Assign(a => a.MaxReadRequestSize = maxReadRequestSize);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestOperationCount"/>
		public CreateAutoFollowPatternDescriptor MaxWriteRequestOperationCount(int? maxWriteRequestOperationCount) =>
			Assign(a => a.MaxWriteRequestOperationCount = maxWriteRequestOperationCount);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestSize"/>
		public CreateAutoFollowPatternDescriptor MaxWriteRequestSize(string maxWriteRequestSize) =>
			Assign(a => a.MaxWriteRequestSize = maxWriteRequestSize);

		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingWriteRequests"/>
		public CreateAutoFollowPatternDescriptor MaxOutstandingWriteRequests(int? maxOutstandingWriteRequests) =>
			Assign(a => a.MaxOutstandingWriteRequests = maxOutstandingWriteRequests);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferCount"/>
		public CreateAutoFollowPatternDescriptor MaxWriteBufferCount(int? maxWriteBufferCount) =>
			Assign(a => a.MaxWriteBufferCount = maxWriteBufferCount);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferSize"/>
		public CreateAutoFollowPatternDescriptor MaxWriteBufferSize(string maxWriteBufferSize) =>
			Assign(a => a.MaxWriteBufferSize = maxWriteBufferSize);

		/// <inheritdoc cref="IAutoFollowPattern.MaxRetryDelay"/>
		public CreateAutoFollowPatternDescriptor MaxRetryDelay(Time maxRetryDelay) => Assign(a => a.MaxRetryDelay = maxRetryDelay);

		/// <inheritdoc cref="IAutoFollowPattern.MaxPollTimeout"/>
		public CreateAutoFollowPatternDescriptor MaxPollTimeout(Time maxPollTimeout) => Assign(a => a.MaxPollTimeout = maxPollTimeout);
	}
}
