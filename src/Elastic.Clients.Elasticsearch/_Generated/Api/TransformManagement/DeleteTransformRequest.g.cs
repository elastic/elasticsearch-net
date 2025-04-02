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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

public sealed partial class DeleteTransformRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If this value is true, the destination index is deleted together with the transform. If false, the destination
	/// index will not be deleted
	/// </para>
	/// </summary>
	public bool? DeleteDestIndex { get => Q<bool?>("delete_dest_index"); set => Q("delete_dest_index", value); }

	/// <summary>
	/// <para>
	/// If this value is false, the transform must be stopped before it can be deleted. If true, the transform is
	/// deleted regardless of its current state.
	/// </para>
	/// </summary>
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class DeleteTransformRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest>
{
	public override Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delete a transform.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestConverter))]
public sealed partial class DeleteTransformRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteTransformRequest(Elastic.Clients.Elasticsearch.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}
#if NET7_0_OR_GREATER
	public DeleteTransformRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DeleteTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.TransformManagementDeleteTransform;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "transform.delete_transform";

	/// <summary>
	/// <para>
	/// Identifier for the transform.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id TransformId { get => P<Elastic.Clients.Elasticsearch.Id>("transform_id"); set => PR("transform_id", value); }

	/// <summary>
	/// <para>
	/// If this value is true, the destination index is deleted together with the transform. If false, the destination
	/// index will not be deleted
	/// </para>
	/// </summary>
	public bool? DeleteDestIndex { get => Q<bool?>("delete_dest_index"); set => Q("delete_dest_index", value); }

	/// <summary>
	/// <para>
	/// If this value is false, the transform must be stopped before it can be deleted. If true, the transform is
	/// deleted regardless of its current state.
	/// </para>
	/// </summary>
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Delete a transform.
/// </para>
/// </summary>
public readonly partial struct DeleteTransformRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteTransformRequestDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest instance)
	{
		Instance = instance;
	}

	public DeleteTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Id transformId)
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest(transformId);
	}

	[System.Obsolete("TODO")]
	public DeleteTransformRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest instance) => new Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest(Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor TransformId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.TransformId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If this value is true, the destination index is deleted together with the transform. If false, the destination
	/// index will not be deleted
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor DeleteDestIndex(bool? value = true)
	{
		Instance.DeleteDestIndex = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If this value is false, the transform must be stopped before it can be deleted. If true, the transform is
	/// deleted regardless of its current state.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor Force(bool? value = true)
	{
		Instance.Force = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor(new Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.DeleteTransformRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}