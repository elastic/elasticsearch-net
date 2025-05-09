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

public sealed partial class GetDatafeedsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="number">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no datafeeds that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the <c>_all</c> string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// The default value is <c>true</c>, which returns an empty <c>datafeeds</c> array
	/// when there are no matches and the subset of results when there are
	/// partial matches. If this parameter is <c>false</c>, the request returns a
	/// <c>404</c> status code when there are no matches or only partial matches.
	/// </para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }
}

internal sealed partial class GetDatafeedsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest>
{
	public override Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get datafeeds configuration info.
/// You can get information for multiple datafeeds in a single API request by
/// using a comma-separated list of datafeeds or a wildcard expression. You can
/// get information for all datafeeds by using <c>_all</c>, by specifying <c>*</c> as the
/// <c>&lt;feed_id></c>, or by omitting the <c>&lt;feed_id></c>.
/// This API returns a maximum of 10,000 datafeeds.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestConverter))]
public sealed partial class GetDatafeedsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestParameters>
{
	public GetDatafeedsRequest(Elastic.Clients.Elasticsearch.Ids? datafeedId) : base(r => r.Optional("datafeed_id", datafeedId))
	{
	}
#if NET7_0_OR_GREATER
	public GetDatafeedsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetDatafeedsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetDatafeedsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningGetDatafeeds;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_datafeeds";

	/// <summary>
	/// <para>
	/// Identifier for the datafeed. It can be a datafeed identifier or a
	/// wildcard expression. If you do not specify one of these options, the API
	/// returns information about all datafeeds.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ids? DatafeedId { get => P<Elastic.Clients.Elasticsearch.Ids?>("datafeed_id"); set => PO("datafeed_id", value); }

	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="number">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no datafeeds that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the <c>_all</c> string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// The default value is <c>true</c>, which returns an empty <c>datafeeds</c> array
	/// when there are no matches and the subset of results when there are
	/// partial matches. If this parameter is <c>false</c>, the request returns a
	/// <c>404</c> status code when there are no matches or only partial matches.
	/// </para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }
}

/// <summary>
/// <para>
/// Get datafeeds configuration info.
/// You can get information for multiple datafeeds in a single API request by
/// using a comma-separated list of datafeeds or a wildcard expression. You can
/// get information for all datafeeds by using <c>_all</c>, by specifying <c>*</c> as the
/// <c>&lt;feed_id></c>, or by omitting the <c>&lt;feed_id></c>.
/// This API returns a maximum of 10,000 datafeeds.
/// </para>
/// </summary>
public readonly partial struct GetDatafeedsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetDatafeedsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest instance)
	{
		Instance = instance;
	}

	public GetDatafeedsRequestDescriptor(Elastic.Clients.Elasticsearch.Ids? datafeedId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest(datafeedId);
	}

	public GetDatafeedsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest(Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the datafeed. It can be a datafeed identifier or a
	/// wildcard expression. If you do not specify one of these options, the API
	/// returns information about all datafeeds.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor DatafeedId(Elastic.Clients.Elasticsearch.Ids? value)
	{
		Instance.DatafeedId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="number">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no datafeeds that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the <c>_all</c> string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// The default value is <c>true</c>, which returns an empty <c>datafeeds</c> array
	/// when there are no matches and the subset of results when there are
	/// partial matches. If this parameter is <c>false</c>, the request returns a
	/// <c>404</c> status code when there are no matches or only partial matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor AllowNoMatch(bool? value = true)
	{
		Instance.AllowNoMatch = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor ExcludeGenerated(bool? value = true)
	{
		Instance.ExcludeGenerated = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetDatafeedsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}