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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public partial class DataframeAnalysisClassification : DataframeAnalysis, IDataframeAnalysisContainerVariant
	{
		[JsonIgnore]
		string IDataframeAnalysisContainerVariant.DataframeAnalysisContainerVariantName => "classification";
		[JsonInclude]
		[JsonPropertyName("class_assignment_objective")]
		public string? ClassAssignmentObjective { get; set; }

		[JsonInclude]
		[JsonPropertyName("num_top_classes")]
		public int? NumTopClasses { get; set; }
	}

	public sealed partial class DataframeAnalysisClassificationDescriptor<TDocument> : SerializableDescriptorBase<DataframeAnalysisClassificationDescriptor<TDocument>>
	{
		internal DataframeAnalysisClassificationDescriptor(Action<DataframeAnalysisClassificationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DataframeAnalysisClassificationDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessor>? FeatureProcessorsValue { get; set; }

		private double? AlphaValue { get; set; }

		private string? ClassAssignmentObjectiveValue { get; set; }

		private string DependentVariableValue { get; set; }

		private double? DownsampleFactorValue { get; set; }

		private bool? EarlyStoppingEnabledValue { get; set; }

		private double? EtaValue { get; set; }

		private double? EtaGrowthRatePerTreeValue { get; set; }

		private double? FeatureBagFractionValue { get; set; }

		private double? GammaValue { get; set; }

		private double? LambdaValue { get; set; }

		private int? MaxOptimizationRoundsPerHyperparameterValue { get; set; }

		private int? MaxTreesValue { get; set; }

		private int? NumTopClassesValue { get; set; }

		private int? NumTopFeatureImportanceValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? PredictionFieldNameValue { get; set; }

		private double? RandomizeSeedValue { get; set; }

		private int? SoftTreeDepthLimitValue { get; set; }

		private double? SoftTreeDepthToleranceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Percentage? TrainingPercentValue { get; set; }

		public DataframeAnalysisClassificationDescriptor<TDocument> FeatureProcessors(IEnumerable<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessor>? featureProcessors)
		{
			FeatureProcessorsValue = featureProcessors;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> Alpha(double? alpha)
		{
			AlphaValue = alpha;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> ClassAssignmentObjective(string? classAssignmentObjective)
		{
			ClassAssignmentObjectiveValue = classAssignmentObjective;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> DependentVariable(string dependentVariable)
		{
			DependentVariableValue = dependentVariable;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> DownsampleFactor(double? downsampleFactor)
		{
			DownsampleFactorValue = downsampleFactor;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> EarlyStoppingEnabled(bool? earlyStoppingEnabled = true)
		{
			EarlyStoppingEnabledValue = earlyStoppingEnabled;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> Eta(double? eta)
		{
			EtaValue = eta;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> EtaGrowthRatePerTree(double? etaGrowthRatePerTree)
		{
			EtaGrowthRatePerTreeValue = etaGrowthRatePerTree;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> FeatureBagFraction(double? featureBagFraction)
		{
			FeatureBagFractionValue = featureBagFraction;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> Gamma(double? gamma)
		{
			GammaValue = gamma;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> Lambda(double? lambda)
		{
			LambdaValue = lambda;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> MaxOptimizationRoundsPerHyperparameter(int? maxOptimizationRoundsPerHyperparameter)
		{
			MaxOptimizationRoundsPerHyperparameterValue = maxOptimizationRoundsPerHyperparameter;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> MaxTrees(int? maxTrees)
		{
			MaxTreesValue = maxTrees;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> NumTopClasses(int? numTopClasses)
		{
			NumTopClassesValue = numTopClasses;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> NumTopFeatureImportanceValues(int? numTopFeatureImportanceValues)
		{
			NumTopFeatureImportanceValuesValue = numTopFeatureImportanceValues;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> PredictionFieldName(Elastic.Clients.Elasticsearch.Field? predictionFieldName)
		{
			PredictionFieldNameValue = predictionFieldName;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> PredictionFieldName<TValue>(Expression<Func<TDocument, TValue>> predictionFieldName)
		{
			PredictionFieldNameValue = predictionFieldName;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> RandomizeSeed(double? randomizeSeed)
		{
			RandomizeSeedValue = randomizeSeed;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> SoftTreeDepthLimit(int? softTreeDepthLimit)
		{
			SoftTreeDepthLimitValue = softTreeDepthLimit;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> SoftTreeDepthTolerance(double? softTreeDepthTolerance)
		{
			SoftTreeDepthToleranceValue = softTreeDepthTolerance;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor<TDocument> TrainingPercent(Elastic.Clients.Elasticsearch.Percentage? trainingPercent)
		{
			TrainingPercentValue = trainingPercent;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FeatureProcessorsValue is not null)
			{
				writer.WritePropertyName("feature_processors");
				JsonSerializer.Serialize(writer, FeatureProcessorsValue, options);
			}

			if (AlphaValue.HasValue)
			{
				writer.WritePropertyName("alpha");
				writer.WriteNumberValue(AlphaValue.Value);
			}

			if (!string.IsNullOrEmpty(ClassAssignmentObjectiveValue))
			{
				writer.WritePropertyName("class_assignment_objective");
				writer.WriteStringValue(ClassAssignmentObjectiveValue);
			}

			writer.WritePropertyName("dependent_variable");
			writer.WriteStringValue(DependentVariableValue);
			if (DownsampleFactorValue.HasValue)
			{
				writer.WritePropertyName("downsample_factor");
				writer.WriteNumberValue(DownsampleFactorValue.Value);
			}

			if (EarlyStoppingEnabledValue.HasValue)
			{
				writer.WritePropertyName("early_stopping_enabled");
				writer.WriteBooleanValue(EarlyStoppingEnabledValue.Value);
			}

			if (EtaValue.HasValue)
			{
				writer.WritePropertyName("eta");
				writer.WriteNumberValue(EtaValue.Value);
			}

			if (EtaGrowthRatePerTreeValue.HasValue)
			{
				writer.WritePropertyName("eta_growth_rate_per_tree");
				writer.WriteNumberValue(EtaGrowthRatePerTreeValue.Value);
			}

			if (FeatureBagFractionValue.HasValue)
			{
				writer.WritePropertyName("feature_bag_fraction");
				writer.WriteNumberValue(FeatureBagFractionValue.Value);
			}

			if (GammaValue.HasValue)
			{
				writer.WritePropertyName("gamma");
				writer.WriteNumberValue(GammaValue.Value);
			}

			if (LambdaValue.HasValue)
			{
				writer.WritePropertyName("lambda");
				writer.WriteNumberValue(LambdaValue.Value);
			}

			if (MaxOptimizationRoundsPerHyperparameterValue.HasValue)
			{
				writer.WritePropertyName("max_optimization_rounds_per_hyperparameter");
				writer.WriteNumberValue(MaxOptimizationRoundsPerHyperparameterValue.Value);
			}

			if (MaxTreesValue.HasValue)
			{
				writer.WritePropertyName("max_trees");
				writer.WriteNumberValue(MaxTreesValue.Value);
			}

			if (NumTopClassesValue.HasValue)
			{
				writer.WritePropertyName("num_top_classes");
				writer.WriteNumberValue(NumTopClassesValue.Value);
			}

			if (NumTopFeatureImportanceValuesValue.HasValue)
			{
				writer.WritePropertyName("num_top_feature_importance_values");
				writer.WriteNumberValue(NumTopFeatureImportanceValuesValue.Value);
			}

			if (PredictionFieldNameValue is not null)
			{
				writer.WritePropertyName("prediction_field_name");
				JsonSerializer.Serialize(writer, PredictionFieldNameValue, options);
			}

			if (RandomizeSeedValue.HasValue)
			{
				writer.WritePropertyName("randomize_seed");
				writer.WriteNumberValue(RandomizeSeedValue.Value);
			}

			if (SoftTreeDepthLimitValue.HasValue)
			{
				writer.WritePropertyName("soft_tree_depth_limit");
				writer.WriteNumberValue(SoftTreeDepthLimitValue.Value);
			}

			if (SoftTreeDepthToleranceValue.HasValue)
			{
				writer.WritePropertyName("soft_tree_depth_tolerance");
				writer.WriteNumberValue(SoftTreeDepthToleranceValue.Value);
			}

			if (TrainingPercentValue is not null)
			{
				writer.WritePropertyName("training_percent");
				JsonSerializer.Serialize(writer, TrainingPercentValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class DataframeAnalysisClassificationDescriptor : SerializableDescriptorBase<DataframeAnalysisClassificationDescriptor>
	{
		internal DataframeAnalysisClassificationDescriptor(Action<DataframeAnalysisClassificationDescriptor> configure) => configure.Invoke(this);
		public DataframeAnalysisClassificationDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessor>? FeatureProcessorsValue { get; set; }

		private double? AlphaValue { get; set; }

		private string? ClassAssignmentObjectiveValue { get; set; }

		private string DependentVariableValue { get; set; }

		private double? DownsampleFactorValue { get; set; }

		private bool? EarlyStoppingEnabledValue { get; set; }

		private double? EtaValue { get; set; }

		private double? EtaGrowthRatePerTreeValue { get; set; }

		private double? FeatureBagFractionValue { get; set; }

		private double? GammaValue { get; set; }

		private double? LambdaValue { get; set; }

		private int? MaxOptimizationRoundsPerHyperparameterValue { get; set; }

		private int? MaxTreesValue { get; set; }

		private int? NumTopClassesValue { get; set; }

		private int? NumTopFeatureImportanceValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? PredictionFieldNameValue { get; set; }

		private double? RandomizeSeedValue { get; set; }

		private int? SoftTreeDepthLimitValue { get; set; }

		private double? SoftTreeDepthToleranceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Percentage? TrainingPercentValue { get; set; }

		public DataframeAnalysisClassificationDescriptor FeatureProcessors(IEnumerable<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessor>? featureProcessors)
		{
			FeatureProcessorsValue = featureProcessors;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor Alpha(double? alpha)
		{
			AlphaValue = alpha;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor ClassAssignmentObjective(string? classAssignmentObjective)
		{
			ClassAssignmentObjectiveValue = classAssignmentObjective;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor DependentVariable(string dependentVariable)
		{
			DependentVariableValue = dependentVariable;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor DownsampleFactor(double? downsampleFactor)
		{
			DownsampleFactorValue = downsampleFactor;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor EarlyStoppingEnabled(bool? earlyStoppingEnabled = true)
		{
			EarlyStoppingEnabledValue = earlyStoppingEnabled;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor Eta(double? eta)
		{
			EtaValue = eta;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor EtaGrowthRatePerTree(double? etaGrowthRatePerTree)
		{
			EtaGrowthRatePerTreeValue = etaGrowthRatePerTree;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor FeatureBagFraction(double? featureBagFraction)
		{
			FeatureBagFractionValue = featureBagFraction;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor Gamma(double? gamma)
		{
			GammaValue = gamma;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor Lambda(double? lambda)
		{
			LambdaValue = lambda;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor MaxOptimizationRoundsPerHyperparameter(int? maxOptimizationRoundsPerHyperparameter)
		{
			MaxOptimizationRoundsPerHyperparameterValue = maxOptimizationRoundsPerHyperparameter;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor MaxTrees(int? maxTrees)
		{
			MaxTreesValue = maxTrees;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor NumTopClasses(int? numTopClasses)
		{
			NumTopClassesValue = numTopClasses;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor NumTopFeatureImportanceValues(int? numTopFeatureImportanceValues)
		{
			NumTopFeatureImportanceValuesValue = numTopFeatureImportanceValues;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor PredictionFieldName(Elastic.Clients.Elasticsearch.Field? predictionFieldName)
		{
			PredictionFieldNameValue = predictionFieldName;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor PredictionFieldName<TDocument, TValue>(Expression<Func<TDocument, TValue>> predictionFieldName)
		{
			PredictionFieldNameValue = predictionFieldName;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor PredictionFieldName<TDocument>(Expression<Func<TDocument, object>> predictionFieldName)
		{
			PredictionFieldNameValue = predictionFieldName;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor RandomizeSeed(double? randomizeSeed)
		{
			RandomizeSeedValue = randomizeSeed;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor SoftTreeDepthLimit(int? softTreeDepthLimit)
		{
			SoftTreeDepthLimitValue = softTreeDepthLimit;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor SoftTreeDepthTolerance(double? softTreeDepthTolerance)
		{
			SoftTreeDepthToleranceValue = softTreeDepthTolerance;
			return Self;
		}

		public DataframeAnalysisClassificationDescriptor TrainingPercent(Elastic.Clients.Elasticsearch.Percentage? trainingPercent)
		{
			TrainingPercentValue = trainingPercent;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FeatureProcessorsValue is not null)
			{
				writer.WritePropertyName("feature_processors");
				JsonSerializer.Serialize(writer, FeatureProcessorsValue, options);
			}

			if (AlphaValue.HasValue)
			{
				writer.WritePropertyName("alpha");
				writer.WriteNumberValue(AlphaValue.Value);
			}

			if (!string.IsNullOrEmpty(ClassAssignmentObjectiveValue))
			{
				writer.WritePropertyName("class_assignment_objective");
				writer.WriteStringValue(ClassAssignmentObjectiveValue);
			}

			writer.WritePropertyName("dependent_variable");
			writer.WriteStringValue(DependentVariableValue);
			if (DownsampleFactorValue.HasValue)
			{
				writer.WritePropertyName("downsample_factor");
				writer.WriteNumberValue(DownsampleFactorValue.Value);
			}

			if (EarlyStoppingEnabledValue.HasValue)
			{
				writer.WritePropertyName("early_stopping_enabled");
				writer.WriteBooleanValue(EarlyStoppingEnabledValue.Value);
			}

			if (EtaValue.HasValue)
			{
				writer.WritePropertyName("eta");
				writer.WriteNumberValue(EtaValue.Value);
			}

			if (EtaGrowthRatePerTreeValue.HasValue)
			{
				writer.WritePropertyName("eta_growth_rate_per_tree");
				writer.WriteNumberValue(EtaGrowthRatePerTreeValue.Value);
			}

			if (FeatureBagFractionValue.HasValue)
			{
				writer.WritePropertyName("feature_bag_fraction");
				writer.WriteNumberValue(FeatureBagFractionValue.Value);
			}

			if (GammaValue.HasValue)
			{
				writer.WritePropertyName("gamma");
				writer.WriteNumberValue(GammaValue.Value);
			}

			if (LambdaValue.HasValue)
			{
				writer.WritePropertyName("lambda");
				writer.WriteNumberValue(LambdaValue.Value);
			}

			if (MaxOptimizationRoundsPerHyperparameterValue.HasValue)
			{
				writer.WritePropertyName("max_optimization_rounds_per_hyperparameter");
				writer.WriteNumberValue(MaxOptimizationRoundsPerHyperparameterValue.Value);
			}

			if (MaxTreesValue.HasValue)
			{
				writer.WritePropertyName("max_trees");
				writer.WriteNumberValue(MaxTreesValue.Value);
			}

			if (NumTopClassesValue.HasValue)
			{
				writer.WritePropertyName("num_top_classes");
				writer.WriteNumberValue(NumTopClassesValue.Value);
			}

			if (NumTopFeatureImportanceValuesValue.HasValue)
			{
				writer.WritePropertyName("num_top_feature_importance_values");
				writer.WriteNumberValue(NumTopFeatureImportanceValuesValue.Value);
			}

			if (PredictionFieldNameValue is not null)
			{
				writer.WritePropertyName("prediction_field_name");
				JsonSerializer.Serialize(writer, PredictionFieldNameValue, options);
			}

			if (RandomizeSeedValue.HasValue)
			{
				writer.WritePropertyName("randomize_seed");
				writer.WriteNumberValue(RandomizeSeedValue.Value);
			}

			if (SoftTreeDepthLimitValue.HasValue)
			{
				writer.WritePropertyName("soft_tree_depth_limit");
				writer.WriteNumberValue(SoftTreeDepthLimitValue.Value);
			}

			if (SoftTreeDepthToleranceValue.HasValue)
			{
				writer.WritePropertyName("soft_tree_depth_tolerance");
				writer.WriteNumberValue(SoftTreeDepthToleranceValue.Value);
			}

			if (TrainingPercentValue is not null)
			{
				writer.WritePropertyName("training_percent");
				JsonSerializer.Serialize(writer, TrainingPercentValue, options);
			}

			writer.WriteEndObject();
		}
	}
}