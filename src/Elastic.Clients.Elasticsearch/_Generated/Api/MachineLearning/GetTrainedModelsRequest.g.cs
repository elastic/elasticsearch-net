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

public sealed partial class GetTrainedModelsRequestParameters : Elastic.Transport.RequestParameters
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
	/// Specifies whether the included model definition should be returned as a
	/// JSON map (true) or in a custom compressed format (false).
	/// </para>
	/// </summary>
	public bool? DecompressDefinition { get => Q<bool?>("decompress_definition"); set => Q("decompress_definition", value); }

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of optional fields to include in the response
	/// body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Include? Include { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.Include?>("include"); set => Q("include", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of tags. A trained model can have many tags, or
	/// none. When supplied, only trained models that contain all the supplied
	/// tags are returned.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Tags { get => Q<System.Collections.Generic.ICollection<string>?>("tags"); set => Q("tags", value); }
}

internal sealed partial class GetTrainedModelsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest>
{
	public override Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get trained model configuration info.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestConverter))]
public sealed partial class GetTrainedModelsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestParameters>
{
	public GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.Ids? modelId) : base(r => r.Optional("model_id", modelId))
	{
	}
#if NET7_0_OR_GREATER
	public GetTrainedModelsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetTrainedModelsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningGetTrainedModels;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_trained_models";

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model or a model alias.
	/// </para>
	/// <para>
	/// You can get information for multiple trained models in a single API
	/// request by using a comma-separated list of model IDs or a wildcard
	/// expression.
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
	/// Specifies whether the included model definition should be returned as a
	/// JSON map (true) or in a custom compressed format (false).
	/// </para>
	/// </summary>
	public bool? DecompressDefinition { get => Q<bool?>("decompress_definition"); set => Q("decompress_definition", value); }

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of optional fields to include in the response
	/// body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Include? Include { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.Include?>("include"); set => Q("include", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of tags. A trained model can have many tags, or
	/// none. When supplied, only trained models that contain all the supplied
	/// tags are returned.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Tags { get => Q<System.Collections.Generic.ICollection<string>?>("tags"); set => Q("tags", value); }
}

/// <summary>
/// <para>
/// Get trained model configuration info.
/// </para>
/// </summary>
public readonly partial struct GetTrainedModelsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetTrainedModelsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest instance)
	{
		Instance = instance;
	}

	public GetTrainedModelsRequestDescriptor(Elastic.Clients.Elasticsearch.Ids? modelId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest(modelId);
	}

	public GetTrainedModelsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model or a model alias.
	/// </para>
	/// <para>
	/// You can get information for multiple trained models in a single API
	/// request by using a comma-separated list of model IDs or a wildcard
	/// expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Ids? value)
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
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor AllowNoMatch(bool? value = true)
	{
		Instance.AllowNoMatch = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies whether the included model definition should be returned as a
	/// JSON map (true) or in a custom compressed format (false).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor DecompressDefinition(bool? value = true)
	{
		Instance.DecompressDefinition = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor ExcludeGenerated(bool? value = true)
	{
		Instance.ExcludeGenerated = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma delimited string of optional fields to include in the response
	/// body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor Include(Elastic.Clients.Elasticsearch.MachineLearning.Include? value)
	{
		Instance.Include = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma delimited string of tags. A trained model can have many tags, or
	/// none. When supplied, only trained models that contain all the supplied
	/// tags are returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor Tags(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Tags = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma delimited string of tags. A trained model can have many tags, or
	/// none. When supplied, only trained models that contain all the supplied
	/// tags are returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor Tags(params string[] values)
	{
		Instance.Tags = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetTrainedModelsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}