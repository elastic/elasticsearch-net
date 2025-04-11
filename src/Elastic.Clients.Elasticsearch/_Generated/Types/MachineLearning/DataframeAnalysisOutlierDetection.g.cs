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

internal sealed partial class DataframeAnalysisOutlierDetectionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection>
{
	private static readonly System.Text.Json.JsonEncodedText PropComputeFeatureInfluence = System.Text.Json.JsonEncodedText.Encode("compute_feature_influence");
	private static readonly System.Text.Json.JsonEncodedText PropFeatureInfluenceThreshold = System.Text.Json.JsonEncodedText.Encode("feature_influence_threshold");
	private static readonly System.Text.Json.JsonEncodedText PropMethod = System.Text.Json.JsonEncodedText.Encode("method");
	private static readonly System.Text.Json.JsonEncodedText PropNNeighbors = System.Text.Json.JsonEncodedText.Encode("n_neighbors");
	private static readonly System.Text.Json.JsonEncodedText PropOutlierFraction = System.Text.Json.JsonEncodedText.Encode("outlier_fraction");
	private static readonly System.Text.Json.JsonEncodedText PropStandardizationEnabled = System.Text.Json.JsonEncodedText.Encode("standardization_enabled");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propComputeFeatureInfluence = default;
		LocalJsonValue<double?> propFeatureInfluenceThreshold = default;
		LocalJsonValue<string?> propMethod = default;
		LocalJsonValue<int?> propNNeighbors = default;
		LocalJsonValue<double?> propOutlierFraction = default;
		LocalJsonValue<bool?> propStandardizationEnabled = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propComputeFeatureInfluence.TryReadProperty(ref reader, options, PropComputeFeatureInfluence, null))
			{
				continue;
			}

			if (propFeatureInfluenceThreshold.TryReadProperty(ref reader, options, PropFeatureInfluenceThreshold, null))
			{
				continue;
			}

			if (propMethod.TryReadProperty(ref reader, options, PropMethod, null))
			{
				continue;
			}

			if (propNNeighbors.TryReadProperty(ref reader, options, PropNNeighbors, null))
			{
				continue;
			}

			if (propOutlierFraction.TryReadProperty(ref reader, options, PropOutlierFraction, null))
			{
				continue;
			}

			if (propStandardizationEnabled.TryReadProperty(ref reader, options, PropStandardizationEnabled, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ComputeFeatureInfluence = propComputeFeatureInfluence.Value,
			FeatureInfluenceThreshold = propFeatureInfluenceThreshold.Value,
			Method = propMethod.Value,
			NNeighbors = propNNeighbors.Value,
			OutlierFraction = propOutlierFraction.Value,
			StandardizationEnabled = propStandardizationEnabled.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropComputeFeatureInfluence, value.ComputeFeatureInfluence, null, null);
		writer.WriteProperty(options, PropFeatureInfluenceThreshold, value.FeatureInfluenceThreshold, null, null);
		writer.WriteProperty(options, PropMethod, value.Method, null, null);
		writer.WriteProperty(options, PropNNeighbors, value.NNeighbors, null, null);
		writer.WriteProperty(options, PropOutlierFraction, value.OutlierFraction, null, null);
		writer.WriteProperty(options, PropStandardizationEnabled, value.StandardizationEnabled, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionConverter))]
public sealed partial class DataframeAnalysisOutlierDetection
{
#if NET7_0_OR_GREATER
	public DataframeAnalysisOutlierDetection()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DataframeAnalysisOutlierDetection()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeAnalysisOutlierDetection(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Specifies whether the feature influence calculation is enabled.
	/// </para>
	/// </summary>
	public bool? ComputeFeatureInfluence { get; set; }

	/// <summary>
	/// <para>
	/// The minimum outlier score that a document needs to have in order to calculate its feature influence score. Value range: 0-1.
	/// </para>
	/// </summary>
	public double? FeatureInfluenceThreshold { get; set; }

	/// <summary>
	/// <para>
	/// The method that outlier detection uses. Available methods are <c>lof</c>, <c>ldof</c>, <c>distance_kth_nn</c>, <c>distance_knn</c>, and <c>ensemble</c>. The default value is ensemble, which means that outlier detection uses an ensemble of different methods and normalises and combines their individual outlier scores to obtain the overall outlier score.
	/// </para>
	/// </summary>
	public string? Method { get; set; }

	/// <summary>
	/// <para>
	/// Defines the value for how many nearest neighbors each method of outlier detection uses to calculate its outlier score. When the value is not set, different values are used for different ensemble members. This default behavior helps improve the diversity in the ensemble; only override it if you are confident that the value you choose is appropriate for the data set.
	/// </para>
	/// </summary>
	public int? NNeighbors { get; set; }

	/// <summary>
	/// <para>
	/// The proportion of the data set that is assumed to be outlying prior to outlier detection. For example, 0.05 means it is assumed that 5% of values are real outliers and 95% are inliers.
	/// </para>
	/// </summary>
	public double? OutlierFraction { get; set; }

	/// <summary>
	/// <para>
	/// If true, the following operation is performed on the columns before computing outlier scores: <c>(x_i - mean(x_i)) / sd(x_i)</c>.
	/// </para>
	/// </summary>
	public bool? StandardizationEnabled { get; set; }
}

public readonly partial struct DataframeAnalysisOutlierDetectionDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisOutlierDetectionDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisOutlierDetectionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Specifies whether the feature influence calculation is enabled.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor ComputeFeatureInfluence(bool? value = true)
	{
		Instance.ComputeFeatureInfluence = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum outlier score that a document needs to have in order to calculate its feature influence score. Value range: 0-1.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor FeatureInfluenceThreshold(double? value)
	{
		Instance.FeatureInfluenceThreshold = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The method that outlier detection uses. Available methods are <c>lof</c>, <c>ldof</c>, <c>distance_kth_nn</c>, <c>distance_knn</c>, and <c>ensemble</c>. The default value is ensemble, which means that outlier detection uses an ensemble of different methods and normalises and combines their individual outlier scores to obtain the overall outlier score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor Method(string? value)
	{
		Instance.Method = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the value for how many nearest neighbors each method of outlier detection uses to calculate its outlier score. When the value is not set, different values are used for different ensemble members. This default behavior helps improve the diversity in the ensemble; only override it if you are confident that the value you choose is appropriate for the data set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor NNeighbors(int? value)
	{
		Instance.NNeighbors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The proportion of the data set that is assumed to be outlying prior to outlier detection. For example, 0.05 means it is assumed that 5% of values are real outliers and 95% are inliers.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor OutlierFraction(double? value)
	{
		Instance.OutlierFraction = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the following operation is performed on the columns before computing outlier scores: <c>(x_i - mean(x_i)) / sd(x_i)</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor StandardizationEnabled(bool? value = true)
	{
		Instance.StandardizationEnabled = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetectionDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisOutlierDetection(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}