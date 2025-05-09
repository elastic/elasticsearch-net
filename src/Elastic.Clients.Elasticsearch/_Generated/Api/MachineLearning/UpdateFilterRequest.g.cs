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

public sealed partial class UpdateFilterRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class UpdateFilterRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropAddItems = System.Text.Json.JsonEncodedText.Encode("add_items");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropRemoveItems = System.Text.Json.JsonEncodedText.Encode("remove_items");

	public override Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propAddItems = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propRemoveItems = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAddItems.TryReadProperty(ref reader, options, PropAddItems, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propRemoveItems.TryReadProperty(ref reader, options, PropRemoveItems, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AddItems = propAddItems.Value,
			Description = propDescription.Value,
			RemoveItems = propRemoveItems.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAddItems, value.AddItems, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropRemoveItems, value.RemoveItems, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Update a filter.
/// Updates the description of a filter, adds items, or removes items from the list.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestConverter))]
public sealed partial class UpdateFilterRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateFilterRequest(Elastic.Clients.Elasticsearch.Id filterId) : base(r => r.Required("filter_id", filterId))
	{
	}
#if NET7_0_OR_GREATER
	public UpdateFilterRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UpdateFilterRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningUpdateFilter;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.update_filter";

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a filter.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id FilterId { get => P<Elastic.Clients.Elasticsearch.Id>("filter_id"); set => PR("filter_id", value); }

	/// <summary>
	/// <para>
	/// The items to add to the filter.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? AddItems { get; set; }

	/// <summary>
	/// <para>
	/// A description for the filter.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The items to remove from the filter.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? RemoveItems { get; set; }
}

/// <summary>
/// <para>
/// Update a filter.
/// Updates the description of a filter, adds items, or removes items from the list.
/// </para>
/// </summary>
public readonly partial struct UpdateFilterRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateFilterRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest instance)
	{
		Instance = instance;
	}

	public UpdateFilterRequestDescriptor(Elastic.Clients.Elasticsearch.Id filterId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest(filterId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public UpdateFilterRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest(Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor FilterId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.FilterId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The items to add to the filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor AddItems(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.AddItems = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The items to add to the filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor AddItems(params string[] values)
	{
		Instance.AddItems = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A description for the filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The items to remove from the filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor RemoveItems(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.RemoveItems = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The items to remove from the filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor RemoveItems(params string[] values)
	{
		Instance.RemoveItems = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateFilterRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}