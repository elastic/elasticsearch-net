// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// Creates a new named collection of auto-follow patterns against the remote cluster specified
	/// in the request body. Newly created indices on the remote cluster matching any of the specified patterns
	/// will be automatically configured as follower indices.
	/// </summary>
	[MapsApi("ccr.put_auto_follow_pattern.json")]
	[ReadAs(typeof(CreateAutoFollowPatternRequest))]
	public partial interface ICreateAutoFollowPatternRequest : IAutoFollowPattern { }

	/// <inheritdoc cref="ICreateAutoFollowPatternRequest"/>
	public partial class CreateAutoFollowPatternRequest
	{
		/// <inheritdoc cref="IAutoFollowPattern.RemoteCluster"/>
		public string RemoteCluster { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public IEnumerable<string> LeaderIndexPatterns { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.Settings"/>
		public IIndexSettings Settings { get; set; }
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
		IIndexSettings IAutoFollowPattern.Settings { get; set; }
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
		public CreateAutoFollowPatternDescriptor RemoteCluster(string remoteCluster) => Assign(remoteCluster, (a, v) => a.RemoteCluster = v);

		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public CreateAutoFollowPatternDescriptor LeaderIndexPatterns(IEnumerable<string> leaderIndexPatterns) =>
			Assign(leaderIndexPatterns, (a, v) => a.LeaderIndexPatterns = v);

		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public CreateAutoFollowPatternDescriptor LeaderIndexPatterns(params string[] leaderIndexPatterns) =>
			Assign(leaderIndexPatterns, (a, v) => a.LeaderIndexPatterns = v);

		/// <inheritdoc cref="IAutoFollowPattern.FollowIndexPattern"/>
		public CreateAutoFollowPatternDescriptor FollowIndexPattern(string followIndexPattern) =>
			Assign(followIndexPattern, (a, v) => a.FollowIndexPattern = v);

		/// <inheritdoc cref="IAutoFollowPattern.Settings"/>
		public CreateAutoFollowPatternDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(selector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestOperationCount"/>
		public CreateAutoFollowPatternDescriptor MaxReadRequestOperationCount(int? maxReadRequestOperationCount) =>
			Assign(maxReadRequestOperationCount, (a, v) => a.MaxReadRequestOperationCount = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingReadRequests"/>
		public CreateAutoFollowPatternDescriptor MaxOutstandingReadRequests(long? maxOutstandingReadRequests) =>
			Assign(maxOutstandingReadRequests, (a, v) => a.MaxOutstandingReadRequests = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestSize"/>
		public CreateAutoFollowPatternDescriptor MaxReadRequestSize(string maxReadRequestSize) =>
			Assign(maxReadRequestSize, (a, v) => a.MaxReadRequestSize = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestOperationCount"/>
		public CreateAutoFollowPatternDescriptor MaxWriteRequestOperationCount(int? maxWriteRequestOperationCount) =>
			Assign(maxWriteRequestOperationCount, (a, v) => a.MaxWriteRequestOperationCount = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestSize"/>
		public CreateAutoFollowPatternDescriptor MaxWriteRequestSize(string maxWriteRequestSize) =>
			Assign(maxWriteRequestSize, (a, v) => a.MaxWriteRequestSize = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingWriteRequests"/>
		public CreateAutoFollowPatternDescriptor MaxOutstandingWriteRequests(int? maxOutstandingWriteRequests) =>
			Assign(maxOutstandingWriteRequests, (a, v) => a.MaxOutstandingWriteRequests = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferCount"/>
		public CreateAutoFollowPatternDescriptor MaxWriteBufferCount(int? maxWriteBufferCount) =>
			Assign(maxWriteBufferCount, (a, v) => a.MaxWriteBufferCount = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferSize"/>
		public CreateAutoFollowPatternDescriptor MaxWriteBufferSize(string maxWriteBufferSize) =>
			Assign(maxWriteBufferSize, (a, v) => a.MaxWriteBufferSize = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxRetryDelay"/>
		public CreateAutoFollowPatternDescriptor MaxRetryDelay(Time maxRetryDelay) => Assign(maxRetryDelay, (a, v) => a.MaxRetryDelay = v);

		/// <inheritdoc cref="IAutoFollowPattern.MaxPollTimeout"/>
		public CreateAutoFollowPatternDescriptor MaxPollTimeout(Time maxPollTimeout) => Assign(maxPollTimeout, (a, v) => a.MaxPollTimeout = v);
	}
}
