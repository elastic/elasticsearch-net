using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Stores model information along with the results.
	/// It provides a more detailed view into anomaly detection.
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ModelPlotConfig>))]
	public interface IModelPlotConfig : IModelPlotConfigEnabled
	{
		/// <summary>
		/// Limits data collection to this list of partition or by field values.
		/// If terms are not specified, no filtering is applied.
		/// </summary>
		/// <remarks>
		/// This is experimental. Only the specified terms can be viewed when using the Single Metric Viewer.
		/// </remarks>
		[JsonProperty("terms")]
		Fields Terms { get; set; }
	}

	/// <inheritdoc />
	public class ModelPlotConfig : IModelPlotConfig
	{
		/// <inheritdoc />
		public bool? Enabled { get; set; }
		/// <inheritdoc />
		public Fields Terms { get; set; }
	}

	/// <inheritdoc />
	public class ModelPlotConfigDescriptor<T> : DescriptorBase<ModelPlotConfigDescriptor<T>, IModelPlotConfig>, IModelPlotConfig where T : class
	{
		bool? IModelPlotConfigEnabled.Enabled { get; set; }
		Fields IModelPlotConfig.Terms { get; set; }

		/// <inheritdoc />
		public ModelPlotConfigDescriptor<T> Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);

		/// <inheritdoc />
		public ModelPlotConfigDescriptor<T> Terms(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Terms = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc />
		public ModelPlotConfigDescriptor<T> Terms(Fields fields) => Assign(a => a.Terms = fields);
	}

	/// <summary>
	/// Stores model information along with the results.
	/// It provides a more detailed view into anomaly detection.
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ModelPlotConfigEnabled>))]
	public interface IModelPlotConfigEnabled
	{
		/// <summary>
		/// Enables calculation and storage of the model bounds for each entity that is being analyzed.
		/// By default, this is not enabled.
		/// </summary>
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }
	}

	/// <inheritdoc />
	public class ModelPlotConfigEnabled : IModelPlotConfigEnabled
	{
		/// <inheritdoc />
		public bool? Enabled { get; set; }
	}

	/// <inheritdoc />
	public class ModelPlotConfigEnabledDescriptor<T> : DescriptorBase<ModelPlotConfigEnabledDescriptor<T>, IModelPlotConfigEnabled>, IModelPlotConfigEnabled where T : class
	{
		bool? IModelPlotConfigEnabled.Enabled { get; set; }

		/// <inheritdoc />
		public ModelPlotConfigEnabledDescriptor<T> Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);
	}
}
