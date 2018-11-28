using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A values source for <see cref="ICompositeAggregation" />
	/// </summary>
	[JsonFormatter(typeof(CompositeAggregationSourceFormatter))]
	public interface ICompositeAggregationSource
	{
		/// <summary>
		/// The field from which to extract value
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// By default documents without a value for a given source are ignored. It is possible to include
		/// them in the response as null by setting this to true
		/// </summary>
		[DataMember(Name = "missing_bucket")]
		bool? MissingBucket { get; set; }

		/// <summary>
		/// The name of the source
		/// </summary>
		[IgnoreDataMember]
		string Name { get; set; }

		/// <summary>
		/// Defines the direction of sorting for each
		/// value source. Defaults to <see cref="SortOrder.Ascending" />
		/// </summary>
		[DataMember(Name = "order")]
		SortOrder? Order { get; set; }

		/// <summary>
		/// The type of the source
		/// </summary>
		[IgnoreDataMember]
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
			Assign(a => a.Add(selector?.Invoke(new TermsCompositeAggregationSourceDescriptor<T>(name))));

		/// <inheritdoc cref="IHistogramCompositeAggregationSource" />
		public CompositeAggregationSourcesDescriptor<T> Histogram(string name,
			Func<HistogramCompositeAggregationSourceDescriptor<T>, IHistogramCompositeAggregationSource> selector
		) =>
			Assign(a => a.Add(selector?.Invoke(new HistogramCompositeAggregationSourceDescriptor<T>(name))));

		/// <inheritdoc cref="IDateHistogramCompositeAggregationSource" />
		public CompositeAggregationSourcesDescriptor<T> DateHistogram(string name,
			Func<DateHistogramCompositeAggregationSourceDescriptor<T>, IDateHistogramCompositeAggregationSource> selector
		) =>
			Assign(a => a.Add(selector?.Invoke(new DateHistogramCompositeAggregationSourceDescriptor<T>(name))));
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

		string ICompositeAggregationSource.Name { get; set; }
		SortOrder? ICompositeAggregationSource.Order { get; set; }
		string ICompositeAggregationSource.SourceType => _sourceType;

		/// <inheritdoc cref="ICompositeAggregationSource.Field" />
		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="ICompositeAggregationSource.Field" />
		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="ICompositeAggregationSource.Order" />
		public TDescriptor Order(SortOrder? order) => Assign(a => a.Order = order);

		/// <inheritdoc cref="ICompositeAggregationSource.MissingBucket" />
		public TDescriptor MissingBucket(bool? includeMissing = true) => Assign(a => a.MissingBucket = includeMissing);
	}

	internal class CompositeAggregationSourceFormatter : IJsonFormatter<ICompositeAggregationSource>
	{
		public void Serialize(ref JsonWriter writer, ICompositeAggregationSource value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginObject();
			writer.WritePropertyName(value.Name);
			writer.WriteBeginObject();
			writer.WritePropertyName(value.SourceType);


			// TODO: Reserialize rest of aggregation source
			// Reserialize(writer, value, serializer);

			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		public ICompositeAggregationSource Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			reader.ReadNext();
			var name = reader.ReadPropertyName();

			reader.ReadNext(); // into source

			var sourcePropertyName = reader.ReadPropertyName();

			ICompositeAggregationSource compositeAggregationSource;

			switch (sourcePropertyName)
			{
				case "terms":
					compositeAggregationSource = formatterResolver.GetFormatter<TermsCompositeAggregationSource>()
						.Deserialize(ref reader, formatterResolver);
					break;
				case "date_histogram":
					compositeAggregationSource = formatterResolver.GetFormatter<DateHistogramCompositeAggregationSource>()
						.Deserialize(ref reader, formatterResolver);
					break;
				case "histogram":
					compositeAggregationSource = formatterResolver.GetFormatter<HistogramCompositeAggregationSource>()
						.Deserialize(ref reader, formatterResolver);
					break;
				default:
					throw new Exception($"Unknown {nameof(ICompositeAggregationSource)}: {sourcePropertyName}");
			}

			compositeAggregationSource.Name = name;
			return compositeAggregationSource;
		}
	}
}
