// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;


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

		/// <inheritdoc cref="IGeoTileGridCompositeAggregationSource" />
		public CompositeAggregationSourcesDescriptor<T> GeoTileGrid(string name,
			Func<GeoTileGridCompositeAggregationSourceDescriptor<T>, IGeoTileGridCompositeAggregationSource> selector
		) =>
			Assign(selector?.Invoke(new GeoTileGridCompositeAggregationSourceDescriptor<T>(name)), (a, v) => a.Add(v));
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
		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ICompositeAggregationSource.Field" />
		public TDescriptor Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ICompositeAggregationSource.Order" />
		public TDescriptor Order(SortOrder? order) => Assign(order, (a, v) => a.Order = v);

		/// <inheritdoc cref="ICompositeAggregationSource.MissingBucket" />
		public TDescriptor MissingBucket(bool? includeMissing = true) => Assign(includeMissing, (a, v) => a.MissingBucket = v);
	}

	internal class CompositeAggregationSourceFormatter : IJsonFormatter<ICompositeAggregationSource>
	{
		public void Serialize(ref JsonWriter writer, ICompositeAggregationSource value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginObject();
			writer.WritePropertyName(value.Name);
			writer.WriteBeginObject();
			writer.WritePropertyName(value.SourceType);

			switch (value)
			{
				case ITermsCompositeAggregationSource termsCompositeAggregationSource:
					Serialize(ref writer, termsCompositeAggregationSource, formatterResolver);
					break;
				case IDateHistogramCompositeAggregationSource dateHistogramCompositeAggregationSource:
					Serialize(ref writer, dateHistogramCompositeAggregationSource, formatterResolver);
					break;
				case IHistogramCompositeAggregationSource histogramCompositeAggregationSource:
					Serialize(ref writer, histogramCompositeAggregationSource, formatterResolver);
					break;
				case IGeoTileGridCompositeAggregationSource geoTileGridCompositeAggregationSource:
					Serialize(ref writer, geoTileGridCompositeAggregationSource, formatterResolver);
					break;
				default:
					Serialize(ref writer, value, formatterResolver);
					break;
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		private static void Serialize<TCompositeAggregationSource>(ref JsonWriter writer, TCompositeAggregationSource value,
			IJsonFormatterResolver formatterResolver
		) where TCompositeAggregationSource : ICompositeAggregationSource
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<TCompositeAggregationSource>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		private static readonly AutomataDictionary AggregationSource = new AutomataDictionary
		{
			{ "terms", 0 },
			{ "date_histogram", 1 },
			{ "histogram", 2 },
			{ "geotile_grid", 3 },
		};

		public ICompositeAggregationSource Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			reader.ReadIsBeginObjectWithVerify();
			var name = reader.ReadPropertyName();

			reader.ReadIsBeginObjectWithVerify(); // into source

			var sourcePropertyName = reader.ReadPropertyNameSegmentRaw();

			ICompositeAggregationSource compositeAggregationSource = null;

			if (AggregationSource.TryGetValue(sourcePropertyName, out var value))
			{
				switch (value)
				{
					case 0:
						compositeAggregationSource = formatterResolver.GetFormatter<TermsCompositeAggregationSource>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						compositeAggregationSource = formatterResolver.GetFormatter<DateHistogramCompositeAggregationSource>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 2:
						compositeAggregationSource = formatterResolver.GetFormatter<HistogramCompositeAggregationSource>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 3:
						compositeAggregationSource = formatterResolver.GetFormatter<GeoTileGridCompositeAggregationSource>()
							.Deserialize(ref reader, formatterResolver);
						break;
				}
			}
			else
				throw new Exception($"Unknown {nameof(ICompositeAggregationSource)}: {sourcePropertyName.Utf8String()}");

			reader.ReadIsEndObjectWithVerify();
			reader.ReadIsEndObjectWithVerify();

			compositeAggregationSource.Name = name;
			return compositeAggregationSource;
		}
	}
}
