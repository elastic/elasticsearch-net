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

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class BucketKsAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropAlternative = System.Text.Json.JsonEncodedText.Encode("alternative");
	private static readonly System.Text.Json.JsonEncodedText PropBucketsPath = System.Text.Json.JsonEncodedText.Encode("buckets_path");
	private static readonly System.Text.Json.JsonEncodedText PropFractions = System.Text.Json.JsonEncodedText.Encode("fractions");
	private static readonly System.Text.Json.JsonEncodedText PropSamplingMethod = System.Text.Json.JsonEncodedText.Encode("sampling_method");

	public override Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propAlternative = default;
		LocalJsonValue<object?> propBucketsPath = default;
		LocalJsonValue<System.Collections.Generic.ICollection<double>?> propFractions = default;
		LocalJsonValue<string?> propSamplingMethod = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAlternative.TryReadProperty(ref reader, options, PropAlternative, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propBucketsPath.TryReadProperty(ref reader, options, PropBucketsPath, null))
			{
				continue;
			}

			if (propFractions.TryReadProperty(ref reader, options, PropFractions, static System.Collections.Generic.ICollection<double>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<double>(o, null)))
			{
				continue;
			}

			if (propSamplingMethod.TryReadProperty(ref reader, options, PropSamplingMethod, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Alternative = propAlternative.Value,
			BucketsPath = propBucketsPath.Value,
			Fractions = propFractions.Value,
			SamplingMethod = propSamplingMethod.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAlternative, value.Alternative, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropBucketsPath, value.BucketsPath, null, null);
		writer.WriteProperty(options, PropFractions, value.Fractions, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<double>? v) => w.WriteCollectionValue<double>(o, v, null));
		writer.WriteProperty(options, PropSamplingMethod, value.SamplingMethod, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// A sibling pipeline aggregation which executes a two sample Kolmogorov–Smirnov test (referred
/// to as a "K-S test" from now on) against a provided distribution, and the distribution implied
/// by the documents counts in the configured sibling aggregation. Specifically, for some metric,
/// assuming that the percentile intervals of the metric are known beforehand or have been computed
/// by an aggregation, then one would use range aggregation for the sibling to compute the p-value
/// of the distribution difference between the metric and the restriction of that metric to a subset
/// of the documents. A natural use case is if the sibling aggregation range aggregation nested in a
/// terms aggregation, in which case one compares the overall distribution of metric to its restriction
/// to each term.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationConverter))]
public sealed partial class BucketKsAggregation
{
#if NET7_0_OR_GREATER
	public BucketKsAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public BucketKsAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal BucketKsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A list of string values indicating which K-S test alternative to calculate. The valid values
	/// are: "greater", "less", "two_sided". This parameter is key for determining the K-S statistic used
	/// when calculating the K-S test. Default value is all possible alternative hypotheses.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Alternative { get; set; }

	/// <summary>
	/// <para>
	/// Path to the buckets that contain one set of values to correlate.
	/// </para>
	/// </summary>
	public object? BucketsPath { get; set; }

	/// <summary>
	/// <para>
	/// A list of doubles indicating the distribution of the samples with which to compare to the <c>buckets_path</c> results.
	/// In typical usage this is the overall proportion of documents in each bucket, which is compared with the actual
	/// document proportions in each bucket from the sibling aggregation counts. The default is to assume that overall
	/// documents are uniformly distributed on these buckets, which they would be if one used equal percentiles of a
	/// metric to define the bucket end points.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<double>? Fractions { get; set; }

	/// <summary>
	/// <para>
	/// Indicates the sampling methodology when calculating the K-S test. Note, this is sampling of the returned values.
	/// This determines the cumulative distribution function (CDF) points used comparing the two samples. Default is
	/// <c>upper_tail</c>, which emphasizes the upper end of the CDF points. Valid options are: <c>upper_tail</c>, <c>uniform</c>,
	/// and <c>lower_tail</c>.
	/// </para>
	/// </summary>
	public string? SamplingMethod { get; set; }
}

/// <summary>
/// <para>
/// A sibling pipeline aggregation which executes a two sample Kolmogorov–Smirnov test (referred
/// to as a "K-S test" from now on) against a provided distribution, and the distribution implied
/// by the documents counts in the configured sibling aggregation. Specifically, for some metric,
/// assuming that the percentile intervals of the metric are known beforehand or have been computed
/// by an aggregation, then one would use range aggregation for the sibling to compute the p-value
/// of the distribution difference between the metric and the restriction of that metric to a subset
/// of the documents. A natural use case is if the sibling aggregation range aggregation nested in a
/// terms aggregation, in which case one compares the overall distribution of metric to its restriction
/// to each term.
/// </para>
/// </summary>
public readonly partial struct BucketKsAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public BucketKsAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public BucketKsAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation(Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A list of string values indicating which K-S test alternative to calculate. The valid values
	/// are: "greater", "less", "two_sided". This parameter is key for determining the K-S statistic used
	/// when calculating the K-S test. Default value is all possible alternative hypotheses.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor Alternative(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Alternative = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of string values indicating which K-S test alternative to calculate. The valid values
	/// are: "greater", "less", "two_sided". This parameter is key for determining the K-S statistic used
	/// when calculating the K-S test. Default value is all possible alternative hypotheses.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor Alternative(params string[] values)
	{
		Instance.Alternative = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Path to the buckets that contain one set of values to correlate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor BucketsPath(object? value)
	{
		Instance.BucketsPath = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of doubles indicating the distribution of the samples with which to compare to the <c>buckets_path</c> results.
	/// In typical usage this is the overall proportion of documents in each bucket, which is compared with the actual
	/// document proportions in each bucket from the sibling aggregation counts. The default is to assume that overall
	/// documents are uniformly distributed on these buckets, which they would be if one used equal percentiles of a
	/// metric to define the bucket end points.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor Fractions(System.Collections.Generic.ICollection<double>? value)
	{
		Instance.Fractions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of doubles indicating the distribution of the samples with which to compare to the <c>buckets_path</c> results.
	/// In typical usage this is the overall proportion of documents in each bucket, which is compared with the actual
	/// document proportions in each bucket from the sibling aggregation counts. The default is to assume that overall
	/// documents are uniformly distributed on these buckets, which they would be if one used equal percentiles of a
	/// metric to define the bucket end points.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor Fractions(params double[] values)
	{
		Instance.Fractions = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates the sampling methodology when calculating the K-S test. Note, this is sampling of the returned values.
	/// This determines the cumulative distribution function (CDF) points used comparing the two samples. Default is
	/// <c>upper_tail</c>, which emphasizes the upper end of the CDF points. Valid options are: <c>upper_tail</c>, <c>uniform</c>,
	/// and <c>lower_tail</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor SamplingMethod(string? value)
	{
		Instance.SamplingMethod = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.BucketKsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}