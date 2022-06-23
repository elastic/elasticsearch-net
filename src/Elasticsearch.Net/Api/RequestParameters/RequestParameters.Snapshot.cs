// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net.Specification.SnapshotApi
{
	///<summary>Request options for CleanupRepository <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/clean-up-snapshot-repo-api.html</para></summary>
	public class CleanupRepositoryRequestParameters : RequestParameters<CleanupRepositoryRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => false;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for Clone <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class CloneSnapshotRequestParameters : RequestParameters<CloneSnapshotRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
		public override bool SupportsBody => true;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	///<summary>Request options for Snapshot <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class SnapshotRequestParameters : RequestParameters<SnapshotRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
		public override bool SupportsBody => true;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Should this request wait until the operation has completed before returning</summary>
		public bool? WaitForCompletion
		{
			get => Q<bool? >("wait_for_completion");
			set => Q("wait_for_completion", value);
		}
	}

	///<summary>Request options for CreateRepository <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class CreateRepositoryRequestParameters : RequestParameters<CreateRepositoryRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
		public override bool SupportsBody => true;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}

		///<summary>Whether to verify the repository after creation</summary>
		public bool? Verify
		{
			get => Q<bool? >("verify");
			set => Q("verify", value);
		}
	}

	///<summary>Request options for Delete <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class DeleteSnapshotRequestParameters : RequestParameters<DeleteSnapshotRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.DELETE;
		public override bool SupportsBody => false;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	///<summary>Request options for DeleteRepository <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class DeleteRepositoryRequestParameters : RequestParameters<DeleteRepositoryRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.DELETE;
		public override bool SupportsBody => false;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for Get <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class GetSnapshotRequestParameters : RequestParameters<GetSnapshotRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		public override bool SupportsBody => false;
		///<summary>Whether to ignore unavailable snapshots, defaults to false which means a SnapshotMissingException is thrown</summary>
		public bool? IgnoreUnavailable
		{
			get => Q<bool? >("ignore_unavailable");
			set => Q("ignore_unavailable", value);
		}

		///<summary>Whether to include the repository name in the snapshot info. Defaults to true.</summary>
		public bool? IncludeRepository
		{
			get => Q<bool? >("include_repository");
			set => Q("include_repository", value);
		}

		///<summary>Whether to include details of each index in the snapshot, if those details are available. Defaults to false.</summary>
		public bool? IndexDetails
		{
			get => Q<bool? >("index_details");
			set => Q("index_details", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Whether to show verbose snapshot info or only show the basic info found in the repository index blob</summary>
		public bool? Verbose
		{
			get => Q<bool? >("verbose");
			set => Q("verbose", value);
		}
	}

	///<summary>Request options for GetRepository <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class GetRepositoryRequestParameters : RequestParameters<GetRepositoryRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		public override bool SupportsBody => false;
		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	///<summary>Request options for AnalyzeRepository <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class AnalyzeRepositoryRequestParameters : RequestParameters<AnalyzeRepositoryRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => false;
		///<summary>Number of blobs to create during the test. Defaults to 100.</summary>
		public long? BlobCount
		{
			get => Q<long? >("blob_count");
			set => Q("blob_count", value);
		}

		///<summary>Number of operations to run concurrently during the test. Defaults to 10.</summary>
		public long? Concurrency
		{
			get => Q<long? >("concurrency");
			set => Q("concurrency", value);
		}

		///<summary>Whether to return detailed results or a summary. Defaults to 'false' so that only the summary is returned.</summary>
		public bool? Detailed
		{
			get => Q<bool? >("detailed");
			set => Q("detailed", value);
		}

		///<summary>
		/// Number of nodes on which to perform an early read on a blob, i.e. before writing has completed. Early reads are rare actions so the
		/// 'rare_action_probability' parameter is also relevant. Defaults to 2.
		///</summary>
		public long? EarlyReadNodeCount
		{
			get => Q<long? >("early_read_node_count");
			set => Q("early_read_node_count", value);
		}

		///<summary>Maximum size of a blob to create during the test, e.g '1gb' or '100mb'. Defaults to '10mb'.</summary>
		public string MaxBlobSize
		{
			get => Q<string>("max_blob_size");
			set => Q("max_blob_size", value);
		}

		///<summary>Maximum total size of all blobs to create during the test, e.g '1tb' or '100gb'. Defaults to '1gb'.</summary>
		public string MaxTotalDataSize
		{
			get => Q<string>("max_total_data_size");
			set => Q("max_total_data_size", value);
		}

		///<summary>Probability of taking a rare action such as an early read or an overwrite. Defaults to 0.02.</summary>
		public long? RareActionProbability
		{
			get => Q<long? >("rare_action_probability");
			set => Q("rare_action_probability", value);
		}

		///<summary>Whether to rarely abort writes before they complete. Defaults to 'true'.</summary>
		public bool? RarelyAbortWrites
		{
			get => Q<bool? >("rarely_abort_writes");
			set => Q("rarely_abort_writes", value);
		}

		///<summary>Number of nodes on which to read a blob after writing. Defaults to 10.</summary>
		public long? ReadNodeCount
		{
			get => Q<long? >("read_node_count");
			set => Q("read_node_count", value);
		}

		///<summary>Seed for the random number generator used to create the test workload. Defaults to a random value.</summary>
		public long? Seed
		{
			get => Q<long? >("seed");
			set => Q("seed", value);
		}

		///<summary>Explicit operation timeout. Defaults to '30s'.</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for Restore <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class RestoreRequestParameters : RequestParameters<RestoreRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => true;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Should this request wait until the operation has completed before returning</summary>
		public bool? WaitForCompletion
		{
			get => Q<bool? >("wait_for_completion");
			set => Q("wait_for_completion", value);
		}
	}

	///<summary>Request options for Status <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class SnapshotStatusRequestParameters : RequestParameters<SnapshotStatusRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		public override bool SupportsBody => false;
		///<summary>Whether to ignore unavailable snapshots, defaults to false which means a SnapshotMissingException is thrown</summary>
		public bool? IgnoreUnavailable
		{
			get => Q<bool? >("ignore_unavailable");
			set => Q("ignore_unavailable", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	///<summary>Request options for VerifyRepository <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para></summary>
	public class VerifyRepositoryRequestParameters : RequestParameters<VerifyRepositoryRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => false;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}
}