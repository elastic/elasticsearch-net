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

public sealed partial class GetInfluencersRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If true, the results are sorted in descending order.
	/// </para>
	/// </summary>
	public bool? Desc { get => Q<bool?>("desc"); set => Q("desc", value); }

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps earlier than this time.
	/// The default value means it is unset and results are not limited to
	/// specific timestamps.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? End { get => Q<System.DateTimeOffset?>("end"); set => Q("end", value); }

	/// <summary>
	/// <para>
	/// If true, the output excludes interim results. By default, interim results
	/// are included.
	/// </para>
	/// </summary>
	public bool? ExcludeInterim { get => Q<bool?>("exclude_interim"); set => Q("exclude_interim", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of influencers.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Returns influencers with anomaly scores greater than or equal to this
	/// value.
	/// </para>
	/// </summary>
	public double? InfluencerScore { get => Q<double?>("influencer_score"); set => Q("influencer_score", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of influencers to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Specifies the sort field for the requested influencers. By default, the
	/// influencers are sorted by the <c>influencer_score</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Sort { get => Q<Elastic.Clients.Elasticsearch.Field?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps after this time. The default value
	/// means it is unset and results are not limited to specific timestamps.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? Start { get => Q<System.DateTimeOffset?>("start"); set => Q("start", value); }
}

internal sealed partial class GetInfluencersRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropPage = System.Text.Json.JsonEncodedText.Encode("page");

	public override Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.Page?> propPage = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPage.TryReadProperty(ref reader, options, PropPage, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Page = propPage.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPage, value.Page, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get anomaly detection job results for influencers.
/// Influencers are the entities that have contributed to, or are to blame for,
/// the anomalies. Influencer results are available only if an
/// <c>influencer_field_name</c> is specified in the job configuration.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestConverter))]
public sealed partial class GetInfluencersRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetInfluencersRequest(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}
#if NET7_0_OR_GREATER
	public GetInfluencersRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetInfluencersRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningGetInfluencers;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_influencers";

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id JobId { get => P<Elastic.Clients.Elasticsearch.Id>("job_id"); set => PR("job_id", value); }

	/// <summary>
	/// <para>
	/// If true, the results are sorted in descending order.
	/// </para>
	/// </summary>
	public bool? Desc { get => Q<bool?>("desc"); set => Q("desc", value); }

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps earlier than this time.
	/// The default value means it is unset and results are not limited to
	/// specific timestamps.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? End { get => Q<System.DateTimeOffset?>("end"); set => Q("end", value); }

	/// <summary>
	/// <para>
	/// If true, the output excludes interim results. By default, interim results
	/// are included.
	/// </para>
	/// </summary>
	public bool? ExcludeInterim { get => Q<bool?>("exclude_interim"); set => Q("exclude_interim", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of influencers.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Returns influencers with anomaly scores greater than or equal to this
	/// value.
	/// </para>
	/// </summary>
	public double? InfluencerScore { get => Q<double?>("influencer_score"); set => Q("influencer_score", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of influencers to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Specifies the sort field for the requested influencers. By default, the
	/// influencers are sorted by the <c>influencer_score</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Sort { get => Q<Elastic.Clients.Elasticsearch.Field?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps after this time. The default value
	/// means it is unset and results are not limited to specific timestamps.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? Start { get => Q<System.DateTimeOffset?>("start"); set => Q("start", value); }

	/// <summary>
	/// <para>
	/// Configures pagination.
	/// This parameter has the <c>from</c> and <c>size</c> properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Page? Page { get; set; }
}

/// <summary>
/// <para>
/// Get anomaly detection job results for influencers.
/// Influencers are the entities that have contributed to, or are to blame for,
/// the anomalies. Influencer results are available only if an
/// <c>influencer_field_name</c> is specified in the job configuration.
/// </para>
/// </summary>
public readonly partial struct GetInfluencersRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetInfluencersRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest instance)
	{
		Instance = instance;
	}

	public GetInfluencersRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest(jobId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetInfluencersRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest(Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.JobId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the results are sorted in descending order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Desc(bool? value = true)
	{
		Instance.Desc = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps earlier than this time.
	/// The default value means it is unset and results are not limited to
	/// specific timestamps.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor End(System.DateTimeOffset? value)
	{
		Instance.End = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the output excludes interim results. By default, interim results
	/// are included.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor ExcludeInterim(bool? value = true)
	{
		Instance.ExcludeInterim = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Skips the specified number of influencers.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns influencers with anomaly scores greater than or equal to this
	/// value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor InfluencerScore(double? value)
	{
		Instance.InfluencerScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of influencers to obtain.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the sort field for the requested influencers. By default, the
	/// influencers are sorted by the <c>influencer_score</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Sort(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the sort field for the requested influencers. By default, the
	/// influencers are sorted by the <c>influencer_score</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Sort<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps after this time. The default value
	/// means it is unset and results are not limited to specific timestamps.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Start(System.DateTimeOffset? value)
	{
		Instance.Start = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Configures pagination.
	/// This parameter has the <c>from</c> and <c>size</c> properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Page(Elastic.Clients.Elasticsearch.MachineLearning.Page? value)
	{
		Instance.Page = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Configures pagination.
	/// This parameter has the <c>from</c> and <c>size</c> properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Page()
	{
		Instance.Page = Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Configures pagination.
	/// This parameter has the <c>from</c> and <c>size</c> properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Page(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor>? action)
	{
		Instance.Page = Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Get anomaly detection job results for influencers.
/// Influencers are the entities that have contributed to, or are to blame for,
/// the anomalies. Influencer results are available only if an
/// <c>influencer_field_name</c> is specified in the job configuration.
/// </para>
/// </summary>
public readonly partial struct GetInfluencersRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetInfluencersRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest instance)
	{
		Instance = instance;
	}

	public GetInfluencersRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest(jobId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetInfluencersRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest(Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> JobId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.JobId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the results are sorted in descending order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Desc(bool? value = true)
	{
		Instance.Desc = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps earlier than this time.
	/// The default value means it is unset and results are not limited to
	/// specific timestamps.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> End(System.DateTimeOffset? value)
	{
		Instance.End = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the output excludes interim results. By default, interim results
	/// are included.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> ExcludeInterim(bool? value = true)
	{
		Instance.ExcludeInterim = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Skips the specified number of influencers.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns influencers with anomaly scores greater than or equal to this
	/// value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> InfluencerScore(double? value)
	{
		Instance.InfluencerScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of influencers to obtain.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the sort field for the requested influencers. By default, the
	/// influencers are sorted by the <c>influencer_score</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the sort field for the requested influencers. By default, the
	/// influencers are sorted by the <c>influencer_score</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Sort(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns influencers with timestamps after this time. The default value
	/// means it is unset and results are not limited to specific timestamps.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Start(System.DateTimeOffset? value)
	{
		Instance.Start = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Configures pagination.
	/// This parameter has the <c>from</c> and <c>size</c> properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Page(Elastic.Clients.Elasticsearch.MachineLearning.Page? value)
	{
		Instance.Page = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Configures pagination.
	/// This parameter has the <c>from</c> and <c>size</c> properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Page()
	{
		Instance.Page = Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Configures pagination.
	/// This parameter has the <c>from</c> and <c>size</c> properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Page(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor>? action)
	{
		Instance.Page = Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetInfluencersRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}