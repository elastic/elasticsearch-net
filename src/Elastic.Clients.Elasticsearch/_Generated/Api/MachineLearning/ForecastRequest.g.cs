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
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class ForecastRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class ForecastRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDuration = System.Text.Json.JsonEncodedText.Encode("duration");
	private static readonly System.Text.Json.JsonEncodedText PropExpiresIn = System.Text.Json.JsonEncodedText.Encode("expires_in");
	private static readonly System.Text.Json.JsonEncodedText PropMaxModelMemory = System.Text.Json.JsonEncodedText.Encode("max_model_memory");

	public override Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propDuration = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propExpiresIn = default;
		LocalJsonValue<string?> propMaxModelMemory = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDuration.TryReadProperty(ref reader, options, PropDuration, null))
			{
				continue;
			}

			if (propExpiresIn.TryReadProperty(ref reader, options, PropExpiresIn, null))
			{
				continue;
			}

			if (propMaxModelMemory.TryReadProperty(ref reader, options, PropMaxModelMemory, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Duration = propDuration.Value,
			ExpiresIn = propExpiresIn.Value,
			MaxModelMemory = propMaxModelMemory.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDuration, value.Duration, null, null);
		writer.WriteProperty(options, PropExpiresIn, value.ExpiresIn, null, null);
		writer.WriteProperty(options, PropMaxModelMemory, value.MaxModelMemory, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Predict future behavior of a time series.
/// </para>
/// <para>
/// Forecasts are not supported for jobs that perform population analysis; an
/// error occurs if you try to create a forecast for a job that has an
/// <c>over_field_name</c> in its configuration. Forcasts predict future behavior
/// based on historical data.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestConverter))]
public sealed partial class ForecastRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ForecastRequest(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}
#if NET7_0_OR_GREATER
	public ForecastRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ForecastRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningForecast;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.forecast";

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job. The job must be open when you
	/// create a forecast; otherwise, an error occurs.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id JobId { get => P<Elastic.Clients.Elasticsearch.Id>("job_id"); set => PR("job_id", value); }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>duration</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Duration { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>expires_in</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? ExpiresIn { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>max_model_memory</c> query parameter.
	/// </para>
	/// </summary>
	public string? MaxModelMemory { get; set; }
}

/// <summary>
/// <para>
/// Predict future behavior of a time series.
/// </para>
/// <para>
/// Forecasts are not supported for jobs that perform population analysis; an
/// error occurs if you try to create a forecast for a job that has an
/// <c>over_field_name</c> in its configuration. Forcasts predict future behavior
/// based on historical data.
/// </para>
/// </summary>
public readonly partial struct ForecastRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ForecastRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest instance)
	{
		Instance = instance;
	}

	public ForecastRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest(jobId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public ForecastRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest(Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job. The job must be open when you
	/// create a forecast; otherwise, an error occurs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.JobId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>duration</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor Duration(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Duration = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>expires_in</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor ExpiresIn(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.ExpiresIn = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>max_model_memory</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor MaxModelMemory(string? value)
	{
		Instance.MaxModelMemory = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ForecastRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}