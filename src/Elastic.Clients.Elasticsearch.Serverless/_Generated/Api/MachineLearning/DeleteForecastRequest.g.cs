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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class DeleteForecastRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies whether an error occurs when there are no forecasts. In
	/// particular, if this parameter is set to <c>false</c> and there are no
	/// forecasts associated with the job, attempts to delete all forecasts
	/// return an error.
	/// </para>
	/// </summary>
	public bool? AllowNoForecasts { get => Q<bool?>("allow_no_forecasts"); set => Q("allow_no_forecasts", value); }

	/// <summary>
	/// <para>
	/// Specifies the period of time to wait for the completion of the delete
	/// operation. When this period of time elapses, the API fails and returns an
	/// error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Delete forecasts from a job.
/// By default, forecasts are retained for 14 days. You can specify a
/// different retention period with the <c>expires_in</c> parameter in the forecast
/// jobs API. The delete forecast API enables you to delete one or more
/// forecasts before they expire.
/// </para>
/// </summary>
public sealed partial class DeleteForecastRequest : PlainRequest<DeleteForecastRequestParameters>
{
	public DeleteForecastRequest(Elastic.Clients.Elasticsearch.Serverless.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	public DeleteForecastRequest(Elastic.Clients.Elasticsearch.Serverless.Id jobId, Elastic.Clients.Elasticsearch.Serverless.Id? forecastId) : base(r => r.Required("job_id", jobId).Optional("forecast_id", forecastId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteForecast;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_forecast";

	/// <summary>
	/// <para>
	/// Specifies whether an error occurs when there are no forecasts. In
	/// particular, if this parameter is set to <c>false</c> and there are no
	/// forecasts associated with the job, attempts to delete all forecasts
	/// return an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoForecasts { get => Q<bool?>("allow_no_forecasts"); set => Q("allow_no_forecasts", value); }

	/// <summary>
	/// <para>
	/// Specifies the period of time to wait for the completion of the delete
	/// operation. When this period of time elapses, the API fails and returns an
	/// error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Delete forecasts from a job.
/// By default, forecasts are retained for 14 days. You can specify a
/// different retention period with the <c>expires_in</c> parameter in the forecast
/// jobs API. The delete forecast API enables you to delete one or more
/// forecasts before they expire.
/// </para>
/// </summary>
public sealed partial class DeleteForecastRequestDescriptor : RequestDescriptor<DeleteForecastRequestDescriptor, DeleteForecastRequestParameters>
{
	internal DeleteForecastRequestDescriptor(Action<DeleteForecastRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteForecastRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id jobId, Elastic.Clients.Elasticsearch.Serverless.Id? forecastId) : base(r => r.Required("job_id", jobId).Optional("forecast_id", forecastId))
	{
	}

	public DeleteForecastRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteForecast;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_forecast";

	public DeleteForecastRequestDescriptor AllowNoForecasts(bool? allowNoForecasts = true) => Qs("allow_no_forecasts", allowNoForecasts);
	public DeleteForecastRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public DeleteForecastRequestDescriptor ForecastId(Elastic.Clients.Elasticsearch.Serverless.Id? forecastId)
	{
		RouteValues.Optional("forecast_id", forecastId);
		return Self;
	}

	public DeleteForecastRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Serverless.Id jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}