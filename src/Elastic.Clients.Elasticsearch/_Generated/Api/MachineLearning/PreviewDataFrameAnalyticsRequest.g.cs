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

public sealed partial class PreviewDataFrameAnalyticsRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PreviewDataFrameAnalyticsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropConfig = System.Text.Json.JsonEncodedText.Encode("config");

	public override Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfig?> propConfig = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propConfig.TryReadProperty(ref reader, options, PropConfig, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Config = propConfig.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropConfig, value.Config, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Preview features used by data frame analytics.
/// Preview the extracted features used by a data frame analytics config.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestConverter))]
public sealed partial class PreviewDataFrameAnalyticsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestParameters>
{
	public PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public PreviewDataFrameAnalyticsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public PreviewDataFrameAnalyticsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningPreviewDataFrameAnalytics;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.preview_data_frame_analytics";

	/// <summary>
	/// <para>
	/// Identifier for the data frame analytics job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get => P<Elastic.Clients.Elasticsearch.Id?>("id"); set => PO("id", value); }

	/// <summary>
	/// <para>
	/// A data frame analytics config as described in create data frame analytics
	/// jobs. Note that <c>id</c> and <c>dest</c> don’t need to be provided in the context of
	/// this API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfig? Config { get; set; }
}

/// <summary>
/// <para>
/// Preview features used by data frame analytics.
/// Preview the extracted features used by a data frame analytics config.
/// </para>
/// </summary>
public readonly partial struct PreviewDataFrameAnalyticsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PreviewDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest instance)
	{
		Instance = instance;
	}

	public PreviewDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(id);
	}

	public PreviewDataFrameAnalyticsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the data frame analytics job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A data frame analytics config as described in create data frame analytics
	/// jobs. Note that <c>id</c> and <c>dest</c> don’t need to be provided in the context of
	/// this API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor Config(Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfig? value)
	{
		Instance.Config = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A data frame analytics config as described in create data frame analytics
	/// jobs. Note that <c>id</c> and <c>dest</c> don’t need to be provided in the context of
	/// this API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor Config(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfigDescriptor> action)
	{
		Instance.Config = Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfigDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A data frame analytics config as described in create data frame analytics
	/// jobs. Note that <c>id</c> and <c>dest</c> don’t need to be provided in the context of
	/// this API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor Config<T>(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfigDescriptor<T>> action)
	{
		Instance.Config = Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfigDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Preview features used by data frame analytics.
/// Preview the extracted features used by a data frame analytics config.
/// </para>
/// </summary>
public readonly partial struct PreviewDataFrameAnalyticsRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PreviewDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest instance)
	{
		Instance = instance;
	}

	public PreviewDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(id);
	}

	public PreviewDataFrameAnalyticsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the data frame analytics job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A data frame analytics config as described in create data frame analytics
	/// jobs. Note that <c>id</c> and <c>dest</c> don’t need to be provided in the context of
	/// this API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> Config(Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfig? value)
	{
		Instance.Config = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A data frame analytics config as described in create data frame analytics
	/// jobs. Note that <c>id</c> and <c>dest</c> don’t need to be provided in the context of
	/// this API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> Config(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfigDescriptor<TDocument>> action)
	{
		Instance.Config = Elastic.Clients.Elasticsearch.MachineLearning.DataframePreviewConfigDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PreviewDataFrameAnalyticsRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}