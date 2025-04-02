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

namespace Elastic.Clients.Elasticsearch.Inference;

public sealed partial class StreamCompletionRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class StreamCompletionRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropInput = System.Text.Json.JsonEncodedText.Encode("input");
	private static readonly System.Text.Json.JsonEncodedText PropTaskSettings = System.Text.Json.JsonEncodedText.Encode("task_settings");

	public override Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propInput = default;
		LocalJsonValue<object?> propTaskSettings = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propInput.TryReadProperty(ref reader, options, PropInput, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propTaskSettings.TryReadProperty(ref reader, options, PropTaskSettings, null))
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
		return new Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Input = propInput.Value,
			TaskSettings = propTaskSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropInput, value.Input, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropTaskSettings, value.TaskSettings, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Perform streaming inference.
/// Get real-time responses for completion tasks by delivering answers incrementally, reducing response times during computation.
/// This API works only with the completion task type.
/// </para>
/// <para>
/// IMPORTANT: The inference APIs enable you to use certain services, such as built-in machine learning models (ELSER, E5), models uploaded through Eland, Cohere, OpenAI, Azure, Google AI Studio, Google Vertex AI, Anthropic, Watsonx.ai, or Hugging Face. For built-in models and models uploaded through Eland, the inference APIs offer an alternative way to use and manage trained models. However, if you do not plan to use the inference APIs to use these models or if you want to use non-NLP models, use the machine learning trained model APIs.
/// </para>
/// <para>
/// This API requires the <c>monitor_inference</c> cluster privilege (the built-in <c>inference_admin</c> and <c>inference_user</c> roles grant this privilege). You must use a client that supports streaming.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestConverter))]
public sealed partial class StreamCompletionRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StreamCompletionRequest(Elastic.Clients.Elasticsearch.Id inferenceId) : base(r => r.Required("inference_id", inferenceId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StreamCompletionRequest(Elastic.Clients.Elasticsearch.Id inferenceId, System.Collections.Generic.ICollection<string> input) : base(r => r.Required("inference_id", inferenceId))
	{
		Input = input;
	}
#if NET7_0_OR_GREATER
	public StreamCompletionRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal StreamCompletionRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.InferenceStreamCompletion;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "inference.stream_completion";

	/// <summary>
	/// <para>
	/// The unique identifier for the inference endpoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id InferenceId { get => P<Elastic.Clients.Elasticsearch.Id>("inference_id"); set => PR("inference_id", value); }

	/// <summary>
	/// <para>
	/// The text on which you want to perform the inference task.
	/// It can be a single string or an array.
	/// </para>
	/// <para>
	/// NOTE: Inference endpoints for the completion task type currently only support a single string as input.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> Input { get; set; }

	/// <summary>
	/// <para>
	/// Optional task settings
	/// </para>
	/// </summary>
	public object? TaskSettings { get; set; }
}

/// <summary>
/// <para>
/// Perform streaming inference.
/// Get real-time responses for completion tasks by delivering answers incrementally, reducing response times during computation.
/// This API works only with the completion task type.
/// </para>
/// <para>
/// IMPORTANT: The inference APIs enable you to use certain services, such as built-in machine learning models (ELSER, E5), models uploaded through Eland, Cohere, OpenAI, Azure, Google AI Studio, Google Vertex AI, Anthropic, Watsonx.ai, or Hugging Face. For built-in models and models uploaded through Eland, the inference APIs offer an alternative way to use and manage trained models. However, if you do not plan to use the inference APIs to use these models or if you want to use non-NLP models, use the machine learning trained model APIs.
/// </para>
/// <para>
/// This API requires the <c>monitor_inference</c> cluster privilege (the built-in <c>inference_admin</c> and <c>inference_user</c> roles grant this privilege). You must use a client that supports streaming.
/// </para>
/// </summary>
public readonly partial struct StreamCompletionRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StreamCompletionRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest instance)
	{
		Instance = instance;
	}

	public StreamCompletionRequestDescriptor(Elastic.Clients.Elasticsearch.Id inferenceId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest(inferenceId);
#pragma warning restore CS0618
	}

	[System.Obsolete("TODO")]
	public StreamCompletionRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest instance) => new Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest(Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier for the inference endpoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor InferenceId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.InferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The text on which you want to perform the inference task.
	/// It can be a single string or an array.
	/// </para>
	/// <para>
	/// NOTE: Inference endpoints for the completion task type currently only support a single string as input.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor Input(System.Collections.Generic.ICollection<string> value)
	{
		Instance.Input = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The text on which you want to perform the inference task.
	/// It can be a single string or an array.
	/// </para>
	/// <para>
	/// NOTE: Inference endpoints for the completion task type currently only support a single string as input.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor Input()
	{
		Instance.Input = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The text on which you want to perform the inference task.
	/// It can be a single string or an array.
	/// </para>
	/// <para>
	/// NOTE: Inference endpoints for the completion task type currently only support a single string as input.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor Input(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.Input = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The text on which you want to perform the inference task.
	/// It can be a single string or an array.
	/// </para>
	/// <para>
	/// NOTE: Inference endpoints for the completion task type currently only support a single string as input.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor Input(params string[] values)
	{
		Instance.Input = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional task settings
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor TaskSettings(object? value)
	{
		Instance.TaskSettings = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest Build(System.Action<Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor(new Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.StreamCompletionRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}