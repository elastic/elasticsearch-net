using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Nest;

namespace Nest
{
	/// <summary>
	/// Uses a pre-trained data frame analytics model to infer against the data that is being ingested in the pipeline.
	/// <para />
	/// Available in Elasticsearch 7.6.0+ with at least basic license.
	/// </summary>
	[InterfaceDataContract]
	public interface IInferenceProcessor : IProcessor
	{
		/// <summary>
		/// The ID of the model to load and infer against.
		/// </summary>
		[DataMember(Name = "model_id")]
		string ModelId { get; set; }

		/// <summary>
		/// Field added to incoming documents to contain results objects.
		/// </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// Maps the document field names to the known field names of the model.
		/// </summary>
		[DataMember(Name = "field_mappings")]
		IDictionary<Field, Field> FieldMappings { get; set; }

		/// <summary>
		/// Contains the inference type and its options.
		/// </summary>
		[DataMember(Name = "inference_config")]
		IInferenceConfig InferenceConfig { get; set; }
	}

	/// <inheritdoc cref="IInferenceProcessor" />
	public class InferenceProcessor : ProcessorBase, IInferenceProcessor
	{
		/// <inheritdoc />
		public string ModelId { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		/// <inheritdoc />
		public IDictionary<Field, Field> FieldMappings { get; set; }

		/// <inheritdoc />
		public IInferenceConfig InferenceConfig { get; set; }

		protected override string Name => "inference";
	}

	/// <inheritdoc cref="IInferenceProcessor" />
	public class InferenceProcessorDescriptor<T>
		: ProcessorDescriptorBase<InferenceProcessorDescriptor<T>, IInferenceProcessor>, IInferenceProcessor
		where T : class
	{
		protected override string Name => "inference";

		Field IInferenceProcessor.TargetField { get; set; }
		string IInferenceProcessor.ModelId { get; set; }
		IInferenceConfig IInferenceProcessor.InferenceConfig { get; set; }
		IDictionary<Field, Field> IInferenceProcessor.FieldMappings { get; set; }

		/// <inheritdoc cref="IInferenceProcessor.TargetField" />
		public InferenceProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IInferenceProcessor.TargetField" />
		public InferenceProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IInferenceProcessor.ModelId" />
		public InferenceProcessorDescriptor<T> ModelId(string modelId) =>
			Assign(modelId, (a, v) => a.ModelId = v);

		/// <inheritdoc cref="IInferenceProcessor.ModelId" />
		public InferenceProcessorDescriptor<T> InferenceConfig(Func<InferenceConfigDescriptor<T>, IInferenceConfig> selector) =>
			Assign(selector, (a, v) => a.InferenceConfig = v.InvokeOrDefault(new InferenceConfigDescriptor<T>()));

		/// <inheritdoc cref="IInferenceProcessor.FieldMappings" />
		public InferenceProcessorDescriptor<T> FieldMappings(Func<FluentDictionary<Field, Field>, FluentDictionary<Field, Field>> selector = null) =>
			Assign(selector, (a, v) => a.FieldMappings = v.InvokeOrDefault(new FluentDictionary<Field, Field>()));
	}

	[ReadAs(typeof(InferenceConfig))]
	public interface IInferenceConfig
	{

		[DataMember(Name = "regression")]
		IRegressionInferenceConfig Regression { get; set; }

		[DataMember(Name = "classification")]
		IClassificationInferenceConfig Classification { get; set; }
	}

	public class InferenceConfig
		: IInferenceConfig
	{
		public IRegressionInferenceConfig Regression { get; set; }

		public IClassificationInferenceConfig Classification { get; set; }
	}

	public class InferenceConfigDescriptor<T> : DescriptorBase<InferenceConfigDescriptor<T>, IInferenceConfig>, IInferenceConfig
	{
		IRegressionInferenceConfig IInferenceConfig.Regression { get; set; }
		IClassificationInferenceConfig IInferenceConfig.Classification { get; set; }

		public InferenceConfigDescriptor<T> Regression(Func<RegressionInferenceConfigDescriptor<T>, IRegressionInferenceConfig> selector) =>
			Assign(selector, (a, v) => a.Regression = v.InvokeOrDefault(new RegressionInferenceConfigDescriptor<T>()));

		public InferenceConfigDescriptor<T> Classification(Func<ClassificationInferenceConfigDescriptor<T>, IClassificationInferenceConfig> selector) =>
			Assign(selector, (a, v) => a.Classification = v.InvokeOrDefault(new ClassificationInferenceConfigDescriptor<T>()));
	}

	[ReadAs(typeof(RegressionInferenceConfig))]
	public interface IRegressionInferenceConfig
	{
		/// <summary>
		/// Specifies the field to which the inference prediction is written. Defaults to <c>predicted_value</c>.
		/// </summary>
		[DataMember(Name = "results_field")]
		Field ResultsField { get; set; }
	}

	public class RegressionInferenceConfig : IRegressionInferenceConfig
	{
		/// <summary>
		/// Specifies the field to which the inference prediction is written. Defaults to <c>predicted_value</c>.
		/// </summary>
		public Field ResultsField { get; set; }
	}

	public class RegressionInferenceConfigDescriptor<T>
		: DescriptorBase<RegressionInferenceConfigDescriptor<T>, IRegressionInferenceConfig>, IRegressionInferenceConfig
	{
		Field IRegressionInferenceConfig.ResultsField { get; set; }

		/// <inheritdoc cref="IRegressionInferenceConfig.ResultsField" />
		public RegressionInferenceConfigDescriptor<T> ResultsField(Field field) => Assign(field, (a, v) => a.ResultsField = v);

		/// <inheritdoc cref="IRegressionInferenceConfig.ResultsField" />
		public RegressionInferenceConfigDescriptor<T> ResultsField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.ResultsField = v);
	}

	[ReadAs(typeof(ClassificationInferenceConfig))]
	public interface IClassificationInferenceConfig
	{
		/// <summary>
		/// Specifies the field to which the inference prediction is written. Defaults to <c>predicted_value</c>.
		/// </summary>
		[DataMember(Name = "results_field")]
		Field ResultsField { get; set; }

		/// <summary>
		/// Specifies the number of top class predictions to return. Defaults to <c>0</c>.
		/// </summary>
		[DataMember(Name = "num_top_classes")]
		int? NumTopClasses { get; set; }

		/// <summary>
		/// Specifies the field to which the top classes are written. Defaults to <c>top_classes</c>.
		/// </summary>
		[DataMember(Name = "top_classes_results_field")]
		Field TopClassesResultsField { get; set; }
	}

	public class ClassificationInferenceConfig : IClassificationInferenceConfig
	{
		/// <summary>
		/// Specifies the field to which the inference prediction is written. Defaults to <c>predicted_value</c>.
		/// </summary>
		public Field ResultsField { get; set; }

		/// <summary>
		/// Specifies the number of top class predictions to return. Defaults to <c>0</c>.
		/// </summary>
		public int? NumTopClasses { get; set; }

		/// <summary>
		/// Specifies the field to which the top classes are written. Defaults to <c>top_classes</c>.
		/// </summary>
		public Field TopClassesResultsField { get; set; }
	}

	public class ClassificationInferenceConfigDescriptor<T> : DescriptorBase<ClassificationInferenceConfigDescriptor<T>, IClassificationInferenceConfig>, IClassificationInferenceConfig
	{
		Field IClassificationInferenceConfig.ResultsField { get; set; }
		int? IClassificationInferenceConfig.NumTopClasses { get; set; }
		Field IClassificationInferenceConfig.TopClassesResultsField { get; set; }

		/// <inheritdoc cref="IClassificationInferenceConfig.ResultsField" />
		public ClassificationInferenceConfigDescriptor<T> ResultsField(Field field) => Assign(field, (a, v) => a.ResultsField = v);

		/// <inheritdoc cref="IClassificationInferenceConfig.ResultsField" />
		public ClassificationInferenceConfigDescriptor<T> ResultsField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.ResultsField = v);

		/// <inheritdoc cref="IClassificationInferenceConfig.NumTopClasses" />
		public ClassificationInferenceConfigDescriptor<T> NumTopClasses(int? numTopClasses) => Assign(numTopClasses, (a, v) => a.NumTopClasses = v);

		/// <inheritdoc cref="IClassificationInferenceConfig.TopClassesResultsField" />
		public ClassificationInferenceConfigDescriptor<T> TopClassesResultsField(Field field) => Assign(field, (a, v) => a.TopClassesResultsField = v);

		/// <inheritdoc cref="IClassificationInferenceConfig.TopClassesResultsField" />
		public ClassificationInferenceConfigDescriptor<T> TopClassesResultsField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TopClassesResultsField = v);
	}
}
