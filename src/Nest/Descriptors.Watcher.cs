/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
using Elastic.Transport;
using Elasticsearch.Net;
using Nest.Utf8Json;
using Elasticsearch.Net.Specification.WatcherApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	///<summary>Descriptor for Acknowledge <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-ack-watch.html</para></summary>
	public partial class AcknowledgeWatchDescriptor : RequestDescriptorBase<AcknowledgeWatchDescriptor, AcknowledgeWatchRequestParameters, IAcknowledgeWatchRequest>, IAcknowledgeWatchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherAcknowledge;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_watcher/watch/{watch_id}/_ack</summary>
		///<param name = "watchId">this parameter is required</param>
		public AcknowledgeWatchDescriptor(Id watchId): base(r => r.Required("watch_id", watchId))
		{
		}

		///<summary>/_watcher/watch/{watch_id}/_ack/{action_id}</summary>
		///<param name = "watchId">this parameter is required</param>
		///<param name = "actionId">Optional, accepts null</param>
		public AcknowledgeWatchDescriptor(Id watchId, Ids actionId): base(r => r.Required("watch_id", watchId).Optional("action_id", actionId))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected AcknowledgeWatchDescriptor(): base()
		{
		}

		// values part of the url path
		Id IAcknowledgeWatchRequest.WatchId => Self.RouteValues.Get<Id>("watch_id");
		Ids IAcknowledgeWatchRequest.ActionId => Self.RouteValues.Get<Ids>("action_id");
		///<summary>A comma-separated list of the action ids to be acked</summary>
		public AcknowledgeWatchDescriptor ActionId(Ids actionId) => Assign(actionId, (a, v) => a.RouteValues.Optional("action_id", v));
	// Request parameters
	}

	///<summary>Descriptor for Activate <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-activate-watch.html</para></summary>
	public partial class ActivateWatchDescriptor : RequestDescriptorBase<ActivateWatchDescriptor, ActivateWatchRequestParameters, IActivateWatchRequest>, IActivateWatchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherActivate;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_watcher/watch/{watch_id}/_activate</summary>
		///<param name = "watchId">this parameter is required</param>
		public ActivateWatchDescriptor(Id watchId): base(r => r.Required("watch_id", watchId))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected ActivateWatchDescriptor(): base()
		{
		}

		// values part of the url path
		Id IActivateWatchRequest.WatchId => Self.RouteValues.Get<Id>("watch_id");
	// Request parameters
	}

	///<summary>Descriptor for Deactivate <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-deactivate-watch.html</para></summary>
	public partial class DeactivateWatchDescriptor : RequestDescriptorBase<DeactivateWatchDescriptor, DeactivateWatchRequestParameters, IDeactivateWatchRequest>, IDeactivateWatchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherDeactivate;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_watcher/watch/{watch_id}/_deactivate</summary>
		///<param name = "watchId">this parameter is required</param>
		public DeactivateWatchDescriptor(Id watchId): base(r => r.Required("watch_id", watchId))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected DeactivateWatchDescriptor(): base()
		{
		}

		// values part of the url path
		Id IDeactivateWatchRequest.WatchId => Self.RouteValues.Get<Id>("watch_id");
	// Request parameters
	}

	///<summary>Descriptor for Delete <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-delete-watch.html</para></summary>
	public partial class DeleteWatchDescriptor : RequestDescriptorBase<DeleteWatchDescriptor, DeleteWatchRequestParameters, IDeleteWatchRequest>, IDeleteWatchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		///<summary>/_watcher/watch/{id}</summary>
		///<param name = "id">this parameter is required</param>
		public DeleteWatchDescriptor(Id id): base(r => r.Required("id", id))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected DeleteWatchDescriptor(): base()
		{
		}

		// values part of the url path
		Id IDeleteWatchRequest.Id => Self.RouteValues.Get<Id>("id");
	// Request parameters
	}

	///<summary>Descriptor for Execute <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-execute-watch.html</para></summary>
	public partial class ExecuteWatchDescriptor : RequestDescriptorBase<ExecuteWatchDescriptor, ExecuteWatchRequestParameters, IExecuteWatchRequest>, IExecuteWatchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherExecute;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		///<summary>/_watcher/watch/{id}/_execute</summary>
		///<param name = "id">Optional, accepts null</param>
		public ExecuteWatchDescriptor(Id id): base(r => r.Optional("id", id))
		{
		}

		///<summary>/_watcher/watch/_execute</summary>
		public ExecuteWatchDescriptor(): base()
		{
		}

		// values part of the url path
		Id IExecuteWatchRequest.Id => Self.RouteValues.Get<Id>("id");
		///<summary>Watch ID</summary>
		public ExecuteWatchDescriptor Id(Id id) => Assign(id, (a, v) => a.RouteValues.Optional("id", v));
		// Request parameters
		///<summary>indicates whether the watch should execute in debug mode</summary>
		public ExecuteWatchDescriptor Debug(bool? debug = true) => Qs("debug", debug);
	}

	///<summary>Descriptor for Get <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-get-watch.html</para></summary>
	public partial class GetWatchDescriptor : RequestDescriptorBase<GetWatchDescriptor, GetWatchRequestParameters, IGetWatchRequest>, IGetWatchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherGet;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_watcher/watch/{id}</summary>
		///<param name = "id">this parameter is required</param>
		public GetWatchDescriptor(Id id): base(r => r.Required("id", id))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected GetWatchDescriptor(): base()
		{
		}

		// values part of the url path
		Id IGetWatchRequest.Id => Self.RouteValues.Get<Id>("id");
	// Request parameters
	}

	///<summary>Descriptor for Put <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-put-watch.html</para></summary>
	public partial class PutWatchDescriptor : RequestDescriptorBase<PutWatchDescriptor, PutWatchRequestParameters, IPutWatchRequest>, IPutWatchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherPut;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		///<summary>/_watcher/watch/{id}</summary>
		///<param name = "id">this parameter is required</param>
		public PutWatchDescriptor(Id id): base(r => r.Required("id", id))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected PutWatchDescriptor(): base()
		{
		}

		// values part of the url path
		Id IPutWatchRequest.Id => Self.RouteValues.Get<Id>("id");
		// Request parameters
		///<summary>Specify whether the watch is in/active by default</summary>
		public PutWatchDescriptor Active(bool? active = true) => Qs("active", active);
		///<summary>only update the watch if the last operation that has changed the watch has the specified primary term</summary>
		public PutWatchDescriptor IfPrimaryTerm(long? ifprimaryterm) => Qs("if_primary_term", ifprimaryterm);
		///<summary>only update the watch if the last operation that has changed the watch has the specified sequence number</summary>
		public PutWatchDescriptor IfSequenceNumber(long? ifsequencenumber) => Qs("if_seq_no", ifsequencenumber);
		///<summary>Explicit version number for concurrency control</summary>
		public PutWatchDescriptor Version(long? version) => Qs("version", version);
	}

	///<summary>Descriptor for Start <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-start.html</para></summary>
	public partial class StartWatcherDescriptor : RequestDescriptorBase<StartWatcherDescriptor, StartWatcherRequestParameters, IStartWatcherRequest>, IStartWatcherRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherStart;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	// values part of the url path
	// Request parameters
	}

	///<summary>Descriptor for Stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-stats.html</para></summary>
	public partial class WatcherStatsDescriptor : RequestDescriptorBase<WatcherStatsDescriptor, WatcherStatsRequestParameters, IWatcherStatsRequest>, IWatcherStatsRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherStats;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_watcher/stats</summary>
		public WatcherStatsDescriptor(): base()
		{
		}

		///<summary>/_watcher/stats/{metric}</summary>
		///<param name = "metric">Optional, accepts null</param>
		public WatcherStatsDescriptor(Metrics metric): base(r => r.Optional("metric", metric))
		{
		}

		// values part of the url path
		Metrics IWatcherStatsRequest.Metric => Self.RouteValues.Get<Metrics>("metric");
		///<summary>Controls what additional stat metrics should be include in the response</summary>
		public WatcherStatsDescriptor Metric(Metrics metric) => Assign(metric, (a, v) => a.RouteValues.Optional("metric", v));
		// Request parameters
		///<summary>Emits stack traces of currently running watches</summary>
		public WatcherStatsDescriptor EmitStacktraces(bool? emitstacktraces = true) => Qs("emit_stacktraces", emitstacktraces);
	}

	///<summary>Descriptor for Stop <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/watcher-api-stop.html</para></summary>
	public partial class StopWatcherDescriptor : RequestDescriptorBase<StopWatcherDescriptor, StopWatcherRequestParameters, IStopWatcherRequest>, IStopWatcherRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.WatcherStop;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	// values part of the url path
	// Request parameters
	}
}