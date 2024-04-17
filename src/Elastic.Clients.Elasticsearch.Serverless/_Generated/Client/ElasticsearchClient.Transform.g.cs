// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch.Serverless.TransformManagement;

public partial class TransformManagementNamespacedClient : NamespacedClientProxy
{
	/// <summary>
	/// <para>Initializes a new instance of the <see cref="TransformManagementNamespacedClient"/> class for mocking.</para>
	/// </summary>
	protected TransformManagementNamespacedClient() : base()
	{
	}

	internal TransformManagementNamespacedClient(ElasticsearchClient client) : base(client)
	{
	}

	/// <summary>
	/// <para>Deletes an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteTransformResponse> DeleteTransformAsync(DeleteTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<DeleteTransformRequest, DeleteTransformResponse, DeleteTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Deletes an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteTransformResponse> DeleteTransformAsync(DeleteTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteTransformRequestDescriptor, DeleteTransformResponse, DeleteTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Deletes an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteTransformResponse> DeleteTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteTransformRequestDescriptor, DeleteTransformResponse, DeleteTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Deletes an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteTransformResponse> DeleteTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<DeleteTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteTransformRequestDescriptor, DeleteTransformResponse, DeleteTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves configuration information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformResponse> GetTransformAsync(GetTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<GetTransformRequest, GetTransformResponse, GetTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves configuration information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformResponse> GetTransformAsync(GetTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformRequestDescriptor, GetTransformResponse, GetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves configuration information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformResponse> GetTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Names? transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformRequestDescriptor, GetTransformResponse, GetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves configuration information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformResponse> GetTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Names? transformId, Action<GetTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformRequestDescriptor, GetTransformResponse, GetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves configuration information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformResponse> GetTransformAsync(CancellationToken cancellationToken = default)
	{
		var descriptor = new GetTransformRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformRequestDescriptor, GetTransformResponse, GetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves configuration information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformResponse> GetTransformAsync(Action<GetTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetTransformRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformRequestDescriptor, GetTransformResponse, GetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves usage information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform-stats.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformStatsResponse> GetTransformStatsAsync(GetTransformStatsRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<GetTransformStatsRequest, GetTransformStatsResponse, GetTransformStatsRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves usage information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform-stats.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformStatsResponse> GetTransformStatsAsync(GetTransformStatsRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformStatsRequestDescriptor, GetTransformStatsResponse, GetTransformStatsRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves usage information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform-stats.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformStatsResponse> GetTransformStatsAsync(Elastic.Clients.Elasticsearch.Serverless.Names transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetTransformStatsRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformStatsRequestDescriptor, GetTransformStatsResponse, GetTransformStatsRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Retrieves usage information for transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/get-transform-stats.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetTransformStatsResponse> GetTransformStatsAsync(Elastic.Clients.Elasticsearch.Serverless.Names transformId, Action<GetTransformStatsRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetTransformStatsRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetTransformStatsRequestDescriptor, GetTransformStatsResponse, GetTransformStatsRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Previews a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/preview-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PreviewTransformResponse<TTransform>> PreviewTransformAsync<TTransform>(PreviewTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<PreviewTransformRequest, PreviewTransformResponse<TTransform>, PreviewTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Previews a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/preview-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PreviewTransformResponse<TTransform>> PreviewTransformAsync<TTransform>(PreviewTransformRequestDescriptor<TTransform> descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<PreviewTransformRequestDescriptor<TTransform>, PreviewTransformResponse<TTransform>, PreviewTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Previews a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/preview-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PreviewTransformResponse<TTransform>> PreviewTransformAsync<TTransform>(Elastic.Clients.Elasticsearch.Serverless.Id? transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new PreviewTransformRequestDescriptor<TTransform>(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<PreviewTransformRequestDescriptor<TTransform>, PreviewTransformResponse<TTransform>, PreviewTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Previews a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/preview-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PreviewTransformResponse<TTransform>> PreviewTransformAsync<TTransform>(Elastic.Clients.Elasticsearch.Serverless.Id? transformId, Action<PreviewTransformRequestDescriptor<TTransform>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new PreviewTransformRequestDescriptor<TTransform>(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<PreviewTransformRequestDescriptor<TTransform>, PreviewTransformResponse<TTransform>, PreviewTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Previews a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/preview-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PreviewTransformResponse<TTransform>> PreviewTransformAsync<TTransform>(CancellationToken cancellationToken = default)
	{
		var descriptor = new PreviewTransformRequestDescriptor<TTransform>();
		descriptor.BeforeRequest();
		return DoRequestAsync<PreviewTransformRequestDescriptor<TTransform>, PreviewTransformResponse<TTransform>, PreviewTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Previews a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/preview-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PreviewTransformResponse<TTransform>> PreviewTransformAsync<TTransform>(Action<PreviewTransformRequestDescriptor<TTransform>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new PreviewTransformRequestDescriptor<TTransform>();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<PreviewTransformRequestDescriptor<TTransform>, PreviewTransformResponse<TTransform>, PreviewTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Instantiates a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/put-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PutTransformResponse> PutTransformAsync(PutTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<PutTransformRequest, PutTransformResponse, PutTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Instantiates a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/put-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PutTransformResponse> PutTransformAsync<TDocument>(PutTransformRequestDescriptor<TDocument> descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<PutTransformRequestDescriptor<TDocument>, PutTransformResponse, PutTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Instantiates a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/put-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PutTransformResponse> PutTransformAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new PutTransformRequestDescriptor<TDocument>(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<PutTransformRequestDescriptor<TDocument>, PutTransformResponse, PutTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Instantiates a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/put-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PutTransformResponse> PutTransformAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<PutTransformRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new PutTransformRequestDescriptor<TDocument>(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<PutTransformRequestDescriptor<TDocument>, PutTransformResponse, PutTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Instantiates a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/put-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PutTransformResponse> PutTransformAsync(PutTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<PutTransformRequestDescriptor, PutTransformResponse, PutTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Instantiates a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/put-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PutTransformResponse> PutTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new PutTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<PutTransformRequestDescriptor, PutTransformResponse, PutTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Instantiates a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/put-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<PutTransformResponse> PutTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<PutTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new PutTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<PutTransformRequestDescriptor, PutTransformResponse, PutTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Resets an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/reset-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetTransformResponse> ResetTransformAsync(ResetTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<ResetTransformRequest, ResetTransformResponse, ResetTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Resets an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/reset-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetTransformResponse> ResetTransformAsync(ResetTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<ResetTransformRequestDescriptor, ResetTransformResponse, ResetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Resets an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/reset-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetTransformResponse> ResetTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new ResetTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<ResetTransformRequestDescriptor, ResetTransformResponse, ResetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Resets an existing transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/reset-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetTransformResponse> ResetTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<ResetTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new ResetTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<ResetTransformRequestDescriptor, ResetTransformResponse, ResetTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Schedules now a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/schedule-now-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ScheduleNowTransformResponse> ScheduleNowTransformAsync(ScheduleNowTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<ScheduleNowTransformRequest, ScheduleNowTransformResponse, ScheduleNowTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Schedules now a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/schedule-now-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ScheduleNowTransformResponse> ScheduleNowTransformAsync(ScheduleNowTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<ScheduleNowTransformRequestDescriptor, ScheduleNowTransformResponse, ScheduleNowTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Schedules now a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/schedule-now-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ScheduleNowTransformResponse> ScheduleNowTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new ScheduleNowTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<ScheduleNowTransformRequestDescriptor, ScheduleNowTransformResponse, ScheduleNowTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Schedules now a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/schedule-now-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ScheduleNowTransformResponse> ScheduleNowTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<ScheduleNowTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new ScheduleNowTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<ScheduleNowTransformRequestDescriptor, ScheduleNowTransformResponse, ScheduleNowTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Starts one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/start-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StartTransformResponse> StartTransformAsync(StartTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<StartTransformRequest, StartTransformResponse, StartTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Starts one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/start-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StartTransformResponse> StartTransformAsync(StartTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<StartTransformRequestDescriptor, StartTransformResponse, StartTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Starts one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/start-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StartTransformResponse> StartTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new StartTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<StartTransformRequestDescriptor, StartTransformResponse, StartTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Starts one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/start-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StartTransformResponse> StartTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<StartTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new StartTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<StartTransformRequestDescriptor, StartTransformResponse, StartTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Stops one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/stop-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StopTransformResponse> StopTransformAsync(StopTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<StopTransformRequest, StopTransformResponse, StopTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Stops one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/stop-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StopTransformResponse> StopTransformAsync(StopTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<StopTransformRequestDescriptor, StopTransformResponse, StopTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Stops one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/stop-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StopTransformResponse> StopTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Name transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new StopTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<StopTransformRequestDescriptor, StopTransformResponse, StopTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Stops one or more transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/stop-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<StopTransformResponse> StopTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Name transformId, Action<StopTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new StopTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<StopTransformRequestDescriptor, StopTransformResponse, StopTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates certain properties of a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/update-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateTransformResponse> UpdateTransformAsync(UpdateTransformRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<UpdateTransformRequest, UpdateTransformResponse, UpdateTransformRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Updates certain properties of a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/update-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateTransformResponse> UpdateTransformAsync<TDocument>(UpdateTransformRequestDescriptor<TDocument> descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateTransformRequestDescriptor<TDocument>, UpdateTransformResponse, UpdateTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates certain properties of a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/update-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateTransformResponse> UpdateTransformAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateTransformRequestDescriptor<TDocument>(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateTransformRequestDescriptor<TDocument>, UpdateTransformResponse, UpdateTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates certain properties of a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/update-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateTransformResponse> UpdateTransformAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<UpdateTransformRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateTransformRequestDescriptor<TDocument>(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateTransformRequestDescriptor<TDocument>, UpdateTransformResponse, UpdateTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates certain properties of a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/update-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateTransformResponse> UpdateTransformAsync(UpdateTransformRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateTransformRequestDescriptor, UpdateTransformResponse, UpdateTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates certain properties of a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/update-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateTransformResponse> UpdateTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateTransformRequestDescriptor(transformId);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateTransformRequestDescriptor, UpdateTransformResponse, UpdateTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates certain properties of a transform.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/update-transform.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateTransformResponse> UpdateTransformAsync(Elastic.Clients.Elasticsearch.Serverless.Id transformId, Action<UpdateTransformRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateTransformRequestDescriptor(transformId);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateTransformRequestDescriptor, UpdateTransformResponse, UpdateTransformRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Upgrades all transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/upgrade-transforms.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpgradeTransformsResponse> UpgradeTransformsAsync(UpgradeTransformsRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<UpgradeTransformsRequest, UpgradeTransformsResponse, UpgradeTransformsRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>Upgrades all transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/upgrade-transforms.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpgradeTransformsResponse> UpgradeTransformsAsync(UpgradeTransformsRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<UpgradeTransformsRequestDescriptor, UpgradeTransformsResponse, UpgradeTransformsRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Upgrades all transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/upgrade-transforms.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpgradeTransformsResponse> UpgradeTransformsAsync(CancellationToken cancellationToken = default)
	{
		var descriptor = new UpgradeTransformsRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequestAsync<UpgradeTransformsRequestDescriptor, UpgradeTransformsResponse, UpgradeTransformsRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Upgrades all transforms.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/upgrade-transforms.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpgradeTransformsResponse> UpgradeTransformsAsync(Action<UpgradeTransformsRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpgradeTransformsRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpgradeTransformsRequestDescriptor, UpgradeTransformsResponse, UpgradeTransformsRequestParameters>(descriptor, cancellationToken);
	}
}