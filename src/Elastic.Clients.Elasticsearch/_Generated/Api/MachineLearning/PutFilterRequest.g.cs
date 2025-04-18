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

public sealed partial class PutFilterRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutFilterRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropItems = System.Text.Json.JsonEncodedText.Encode("items");

	public override Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propItems = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propItems.TryReadProperty(ref reader, options, PropItems, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			Items = propItems.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropItems, value.Items, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create a filter.
/// A filter contains a list of strings. It can be used by one or more anomaly detection jobs.
/// Specifically, filters are referenced in the <c>custom_rules</c> property of detector configuration objects.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestConverter))]
public sealed partial class PutFilterRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutFilterRequest(Elastic.Clients.Elasticsearch.Id filterId) : base(r => r.Required("filter_id", filterId))
	{
	}
#if NET7_0_OR_GREATER
	public PutFilterRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutFilterRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningPutFilter;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.put_filter";

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
	/// A description of the filter.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The items of the filter. A wildcard <c>*</c> can be used at the beginning or the end of an item.
	/// Up to 10000 items are allowed in each filter.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Items { get; set; }
}

/// <summary>
/// <para>
/// Create a filter.
/// A filter contains a list of strings. It can be used by one or more anomaly detection jobs.
/// Specifically, filters are referenced in the <c>custom_rules</c> property of detector configuration objects.
/// </para>
/// </summary>
public readonly partial struct PutFilterRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutFilterRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest instance)
	{
		Instance = instance;
	}

	public PutFilterRequestDescriptor(Elastic.Clients.Elasticsearch.Id filterId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest(filterId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutFilterRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest(Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor FilterId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.FilterId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A description of the filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The items of the filter. A wildcard <c>*</c> can be used at the beginning or the end of an item.
	/// Up to 10000 items are allowed in each filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor Items(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Items = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The items of the filter. A wildcard <c>*</c> can be used at the beginning or the end of an item.
	/// Up to 10000 items are allowed in each filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor Items(params string[] values)
	{
		Instance.Items = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutFilterRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}