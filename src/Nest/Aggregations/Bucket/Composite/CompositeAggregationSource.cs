using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	/// <summary>
	/// A values source for <see cref="ICompositeAggregation"/>
	/// </summary>
	[ContractJsonConverter(typeof(CompositeAggregationSourceConverter))]
	public interface ICompositeAggregationSource
	{
		/// <summary>
		/// The name of the source
		/// </summary>
		[JsonIgnore]
		string Name { get; set; }

		/// <summary>
		/// The type of the source
		/// </summary>
		[JsonIgnore]
		string SourceType { get; }

		/// <summary>
		/// The field from which to extract value
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// Defines the direction of sorting for each
		/// value source. Defaults to <see cref="SortOrder.Ascending"/>
		/// </summary>
		[JsonProperty("order")]
		SortOrder? Order { get; set; }
	}

	/// <inheritdoc />
	public abstract class CompositeAggregationSourceBase : ICompositeAggregationSource
	{
		/// <inheritdoc />
		string ICompositeAggregationSource.Name { get; set; }
		string ICompositeAggregationSource.SourceType => SourceType;

		/// <inheritdoc cref="ICompositeAggregationSource.SourceType"/>
		protected abstract string SourceType { get;  }

		internal CompositeAggregationSourceBase() { }

		protected CompositeAggregationSourceBase(string name) =>
			((ICompositeAggregationSource)this).Name = name;

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public SortOrder? Order { get; set; }
	}

	/// <inheritdoc cref="ICompositeAggregationSource"/>
	public class CompositeAggregationSourcesDescriptor<T> :
		DescriptorPromiseBase<CompositeAggregationSourcesDescriptor<T>, IList<ICompositeAggregationSource>>
		where T : class
	{
		public CompositeAggregationSourcesDescriptor() : base(new List<ICompositeAggregationSource>()) {}

		/// <inheritdoc cref="ITermsCompositeAggregationSource"/>
		public CompositeAggregationSourcesDescriptor<T> Terms(string name, Func<TermsCompositeAggregationSourceDescriptor<T>, ITermsCompositeAggregationSource> selector) =>
			Assign(a => a.Add(selector?.Invoke(new TermsCompositeAggregationSourceDescriptor<T>(name))));

		/// <inheritdoc cref="IHistogramCompositeAggregationSource"/>
		public CompositeAggregationSourcesDescriptor<T> Histogram(string name, Func<HistogramCompositeAggregationSourceDescriptor<T>, IHistogramCompositeAggregationSource> selector) =>
			Assign(a => a.Add(selector?.Invoke(new HistogramCompositeAggregationSourceDescriptor<T>(name))));

		/// <inheritdoc cref="IDateHistogramCompositeAggregationSource"/>
		public CompositeAggregationSourcesDescriptor<T> DateHistogram(string name, Func<DateHistogramCompositeAggregationSourceDescriptor<T>, IDateHistogramCompositeAggregationSource> selector) =>
			Assign(a => a.Add(selector?.Invoke(new DateHistogramCompositeAggregationSourceDescriptor<T>(name))));
	}

	/// <inheritdoc cref="ICompositeAggregationSource"/>
	public abstract class CompositeAggregationSourceDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, ICompositeAggregationSource
		where TDescriptor : CompositeAggregationSourceDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ICompositeAggregationSource
	{
		private readonly string _sourceType;

		string ICompositeAggregationSource.Name { get; set; }
		string ICompositeAggregationSource.SourceType => _sourceType;
		Field ICompositeAggregationSource.Field { get; set; }
		SortOrder? ICompositeAggregationSource.Order { get; set; }

		protected CompositeAggregationSourceDescriptorBase(string name, string sourceType)
		{
			_sourceType = sourceType;
			Self.Name = name;
		}

		/// <inheritdoc cref="ICompositeAggregationSource.Field"/>
		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="ICompositeAggregationSource.Field"/>
		public TDescriptor Field(Expression<Func<T,object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="ICompositeAggregationSource.Order"/>
		public TDescriptor Order(SortOrder? order) => Assign(a => a.Order = order);
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
					base.Reserialize(writer, value, serializer);
				writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
