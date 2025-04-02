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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class ModifyDataStreamRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class ModifyDataStreamRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropActions = System.Text.Json.JsonEncodedText.Encode("actions");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction>> propActions = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propActions.TryReadProperty(ref reader, options, PropActions, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Actions = propActions.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropActions, value.Actions, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Update data streams.
/// Performs one or more data stream modification actions in a single atomic operation.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestConverter))]
public sealed partial class ModifyDataStreamRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ModifyDataStreamRequest(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction> actions)
	{
		Actions = actions;
	}
#if NET7_0_OR_GREATER
	public ModifyDataStreamRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The request contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ModifyDataStreamRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ModifyDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementModifyDataStream;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.modify_data_stream";

	/// <summary>
	/// <para>
	/// Actions to perform.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction> Actions { get; set; }
}

/// <summary>
/// <para>
/// Update data streams.
/// Performs one or more data stream modification actions in a single atomic operation.
/// </para>
/// </summary>
public readonly partial struct ModifyDataStreamRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ModifyDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest instance)
	{
		Instance = instance;
	}

	public ModifyDataStreamRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest(Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Actions to perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor Actions(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction> value)
	{
		Instance.Actions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Actions to perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor Actions()
	{
		Instance.Actions = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndexModifyDataStreamAction.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Actions to perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor Actions(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndexModifyDataStreamAction>? action)
	{
		Instance.Actions = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndexModifyDataStreamAction.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Actions to perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor Actions(params Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction[] values)
	{
		Instance.Actions = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Actions to perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor Actions(params System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamActionDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamAction>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.IndexManagement.IndexModifyDataStreamActionDescriptor.Build(action));
		}

		Instance.Actions = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ModifyDataStreamRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}