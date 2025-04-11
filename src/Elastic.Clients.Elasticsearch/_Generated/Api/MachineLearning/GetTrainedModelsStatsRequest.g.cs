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

public sealed partial class GetTrainedModelsStatsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no models that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the _all string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// If true, it returns an empty array when there are no matches and the
	/// subset of results when there are partial matches.
	/// </para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

internal sealed partial class GetTrainedModelsStatsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest>
{
	public override Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get trained models usage info.
/// You can get usage information for multiple trained
/// models in a single API request by using a comma-separated list of model IDs or a wildcard expression.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestConverter))]
public sealed partial class GetTrainedModelsStatsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestParameters>
{
	public GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.Ids? modelId) : base(r => r.Optional("model_id", modelId))
	{
	}
#if NET7_0_OR_GREATER
	public GetTrainedModelsStatsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetTrainedModelsStatsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningGetTrainedModelsStats;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_trained_models_stats";

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model or a model alias. It can be a
	/// comma-separated list or a wildcard expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ids? ModelId { get => P<Elastic.Clients.Elasticsearch.Ids?>("model_id"); set => PO("model_id", value); }

	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no models that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the _all string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// If true, it returns an empty array when there are no matches and the
	/// subset of results when there are partial matches.
	/// </para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get trained models usage info.
/// You can get usage information for multiple trained
/// models in a single API request by using a comma-separated list of model IDs or a wildcard expression.
/// </para>
/// </summary>
public readonly partial struct GetTrainedModelsStatsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetTrainedModelsStatsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest instance)
	{
		Instance = instance;
	}

	public GetTrainedModelsStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Ids? modelId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest(modelId);
	}

	public GetTrainedModelsStatsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model or a model alias. It can be a
	/// comma-separated list or a wildcard expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Ids? value)
	{
		Instance.ModelId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no models that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the _all string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// If true, it returns an empty array when there are no matches and the
	/// subset of results when there are partial matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor AllowNoMatch(bool? value = true)
	{
		Instance.AllowNoMatch = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsStatsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}