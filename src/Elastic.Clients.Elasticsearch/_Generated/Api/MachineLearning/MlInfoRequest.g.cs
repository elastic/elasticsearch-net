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

public sealed partial class MlInfoRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class MlInfoRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest>
{
	public override Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get machine learning information.
/// Get defaults and limits used by machine learning.
/// This endpoint is designed to be used by a user interface that needs to fully
/// understand machine learning configurations where some options are not
/// specified, meaning that the defaults should be used. This endpoint may be
/// used to find out what those defaults are. It also provides information about
/// the maximum size of machine learning jobs that could run in the current
/// cluster configuration.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestConverter))]
public sealed partial class MlInfoRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestParameters>
{
#if NET7_0_OR_GREATER
	public MlInfoRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public MlInfoRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MlInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningInfo;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.info";
}

/// <summary>
/// <para>
/// Get machine learning information.
/// Get defaults and limits used by machine learning.
/// This endpoint is designed to be used by a user interface that needs to fully
/// understand machine learning configurations where some options are not
/// specified, meaning that the defaults should be used. This endpoint may be
/// used to find out what those defaults are. It also provides information about
/// the maximum size of machine learning jobs that could run in the current
/// cluster configuration.
/// </para>
/// </summary>
public readonly partial struct MlInfoRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MlInfoRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest instance)
	{
		Instance = instance;
	}

	public MlInfoRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest(Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor descriptor) => descriptor.Instance;

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.MlInfoRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}