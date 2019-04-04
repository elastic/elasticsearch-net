using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	/// <summary>
	/// A values source for <see cref="ICompositeAggregation" />
	/// </summary>
	[ContractJsonConverter(typeof(CompositeAggregationSourceConverter))]
	public interface ICompositeAggregationSource
	{
		/// <summary>
		/// The field from which to extract value
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// By default documents without a value for a given source are ignored. It is possible to include
		/// them in the response as null by setting this to true
		/// </summary>
		[JsonProperty("missing_bucket")]
		bool? MissingBucket { get; set; }


		/// <summary>
		/// Allows an explicit value to be set when one is missing for the field.
		/// </summary>
		[Obsolete("Will be removed in Elasticsearch 7.x. Use MissingBucket")]
		[JsonProperty("missing")]
		object Missing { get; set; }

		/// <summary>
		/// The name of the source
		/// </summary>
		[JsonIgnore]
		string Name { get; set; }

		/// <summary>
		/// Defines the direction of sorting for each
		/// value source. Defaults to <see cref="SortOrder.Ascending" />
		/// </summary>
		[JsonProperty("order")]
		SortOrder? Order { get; set; }

		/// <summary>
		/// The type of the source
		/// </summary>
		[JsonIgnore]
		string SourceType { get; }
	}

	/// <inheritdoc />
	public abstract class CompositeAggregationSourceBase : ICompositeAggregationSource
	{
		internal CompositeAggregationSourceBase() { }

		protected CompositeAggregationSourceBase(string name) =>
			((ICompositeAggregationSource)this).Name = name;

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? MissingBucket { get; set; }

		/// <inheritdoc />
		[Obsolete("Will be removed in Elasticsearch 7.x. Use MissingBucket")]
		public object Missing { get; set; }

		/// <inheritdoc />
		public SortOrder? Order { get; set; }

		/// <inheritdoc cref="ICompositeAggregationSource.SourceType" />
		protected abstract string SourceType { get; }

		/// <inheritdoc />
		string ICompositeAggregationSource.Name { get; set; }

		string ICompositeAggregationSource.SourceType => SourceType;
	}

	/// <inheritdoc cref="ICompositeAggregationSource" />
	public class CompositeAggregationSourcesDescriptor<T>
		: DescriptorPromiseBase<CompositeAggregationSourcesDescriptor<T>, IList<ICompositeAggregationSource>>
		where T : class
	{
		public CompositeAggregationSourcesDescriptor() : base(new List<ICompositeAggregationSource>()) { }

		/// <inheritdoc cref="ITermsCompositeAggregationSource" />
		public CompositeAggregationSourcesDescriptor<T> Terms(string name,
			Func<TermsCompositeAggregationSourceDescriptor<T>, ITermsCompositeAggregationSource> selector
		) =>
			Assign(selector?.Invoke(new TermsCompositeAggregationSourceDescriptor<T>(name)), (a, v) => a.Add(v));

		/// <inheritdoc cref="IHistogramCompositeAggregationSource" />
		public CompositeAggregationSourcesDescriptor<T> Histogram(string name,
			Func<HistogramCompositeAggregationSourceDescriptor<T>, IHistogramCompositeAggregationSource> selector
		) =>
			Assign(selector?.Invoke(new HistogramCompositeAggregationSourceDescriptor<T>(name)), (a, v) => a.Add(v));

		/// <inheritdoc cref="IDateHistogramCompositeAggregationSource" />
		public CompositeAggregationSourcesDescriptor<T> DateHistogram(string name,
			Func<DateHistogramCompositeAggregationSourceDescriptor<T>, IDateHistogramCompositeAggregationSource> selector
		) =>
			Assign(selector?.Invoke(new DateHistogramCompositeAggregationSourceDescriptor<T>(name)), (a, v) => a.Add(v));
	}

	/// <inheritdoc cref="ICompositeAggregationSource" />
	public abstract class CompositeAggregationSourceDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, ICompositeAggregationSource
		where TDescriptor : CompositeAggregationSourceDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ICompositeAggregationSource
	{
		private readonly string _sourceType;

		protected CompositeAggregationSourceDescriptorBase(string name, string sourceType)
		{
			_sourceType = sourceType;
			Self.Name = name;
		}

		Field ICompositeAggregationSource.Field { get; set; }
		bool? ICompositeAggregationSource.MissingBucket { get; set; }
		[Obsolete("Will be removed in Elasticsearch 7.x. Use MissingBucket")]
		object ICompositeAggregationSource.Missing { get; set; }
		string ICompositeAggregationSource.Name { get; set; }
		SortOrder? ICompositeAggregationSource.Order { get; set; }
		string ICompositeAggregationSource.SourceType => _sourceType;

		/// <inheritdoc cref="ICompositeAggregationSource.Field" />
		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ICompositeAggregationSource.Field" />
		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ICompositeAggregationSource.Order" />
		public TDescriptor Order(SortOrder? order) => Assign(order, (a, v) => a.Order = v);

		/// <inheritdoc cref="ICompositeAggregationSource.MissingBucket" />
		public TDescriptor MissingBucket(bool? includeMissing = true) => Assign(includeMissing, (a, v) => a.MissingBucket = v);

		/// <inheritdoc cref="ICompositeAggregationSource.Missing" />
		[Obsolete("Will be removed in Elasticsearch 7.x. Use MissingBucket")]
		public TDescriptor Missing(object missingObject) => Assign(missingObject, (a, v) => a.Missing = v);
	}

	internal class CompositeAggregationSourceConverter : ReserializeJsonConverter<CompositeAggregationSourceBase, ICompositeAggregationSource>
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			var property = jObject.Properties().Single();
			var name = property.Name;
			var source = (JObject)property.Value;
			var sourceProperty = source.Properties().Single();
			var sourceValue = sourceProperty.Value;
			ICompositeAggregationSource compositeAggregationSource;

			switch (sourceProperty.Name)
			{
				case "terms":
					compositeAggregationSource = sourceValue.ToObject<TermsCompositeAggregationSource>(ElasticContractResolver.Empty);
					break;
				case "date_histogram":
					compositeAggregationSource = sourceValue.ToObject<DateHistogramCompositeAggregationSource>(ElasticContractResolver.Empty);
					break;
				case "histogram":
					compositeAggregationSource = sourceValue.ToObject<HistogramCompositeAggregationSource>(ElasticContractResolver.Empty);
					break;
				default:
					throw new JsonSerializationException($"Unknown {nameof(ICompositeAggregationSource)}: {sourceProperty.Name}");
			}

			compositeAggregationSource.Name = name;
			return compositeAggregationSource;
		}

		protected override void SerializeJson(JsonWriter writer, object value, ICompositeAggregationSource castValue, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(castValue.Name);
			writer.WriteStartObject();
			writer.WritePropertyName(castValue.SourceType);
			Reserialize(writer, value, serializer);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
