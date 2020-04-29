using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;

namespace Nest
{
	/// <summary>
	/// A single grouping for a transform
	/// </summary>
	[JsonFormatter(typeof(SingleGroupSourceFormatter))]
	public interface ISingleGroupSource
	{
		/// <summary>
		/// The field from on which to group
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// The name of the group by
		/// </summary>
		[IgnoreDataMember]
		string Name { get; set; }

		/// <summary>
		/// The type of the group by
		/// </summary>
		[IgnoreDataMember]
		string Type { get; }
	}

	/// <inheritdoc cref="ISingleGroupSource" />
	public abstract class SingleGroupSourceBase : ISingleGroupSource
	{
		protected SingleGroupSourceBase(string name) => ((ISingleGroupSource)this).Name = name;

		/// <inheritdoc cref="ISingleGroupSource.Type" />
		protected abstract string Type { get; }

		/// <inheritdoc />
		public Field Field { get; set; }

		string ISingleGroupSource.Name { get; set; }

		string ISingleGroupSource.Type => Type;
	}

	/// <inheritdoc cref="ISingleGroupSource" />
	public abstract class SingleGroupSourceDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, ISingleGroupSource
		where TDescriptor : SingleGroupSourceDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ISingleGroupSource
	{
		private readonly string _type;

		protected SingleGroupSourceDescriptorBase(string name, string type)
		{
			_type = type;
			Self.Name = name;
		}

		Field ISingleGroupSource.Field { get; set; }
		string ISingleGroupSource.Name { get; set; }
		string ISingleGroupSource.Type => _type;

		/// <inheritdoc cref="ISingleGroupSource.Field" />
		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISingleGroupSource.Field" />
		public TDescriptor Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

	}

	/// <summary>
	/// Builds a collection of <see cref="ISingleGroupSource"/>
	/// </summary>
	public class SingleGroupSourcesDescriptor<T>
		: DescriptorPromiseBase<SingleGroupSourcesDescriptor<T>, IList<ISingleGroupSource>>
		where T : class
	{
		public SingleGroupSourcesDescriptor() : base (new List<ISingleGroupSource>()) { }

		/// <inheritdoc cref="ITermsGroupSource" />
		public SingleGroupSourcesDescriptor<T> Terms(string name,
			Func<TermsGroupSourceDescriptor<T>, ITermsGroupSource> selector
		) =>
			Assign(selector?.Invoke(new TermsGroupSourceDescriptor<T>(name)), (a, v) => a.Add(v));

		/// <inheritdoc cref="IHistogramGroupSource" />
		public SingleGroupSourcesDescriptor<T> Histogram(string name,
			Func<HistogramGroupSourceDescriptor<T>, IHistogramGroupSource> selector
		) =>
			Assign(selector?.Invoke(new HistogramGroupSourceDescriptor<T>(name)), (a, v) => a.Add(v));

		/// <inheritdoc cref="IDateHistogramGroupSource" />
		public SingleGroupSourcesDescriptor<T> DateHistogram(string name,
			Func<DateHistogramGroupSourceDescriptor<T>, IDateHistogramGroupSource> selector
		) =>
			Assign(selector?.Invoke(new DateHistogramGroupSourceDescriptor<T>(name)), (a, v) => a.Add(v));
	}

	internal class SingleGroupSourceFormatter : IJsonFormatter<ISingleGroupSource>
	{
		private static readonly AutomataDictionary GroupSource = new AutomataDictionary
		{
			{ "terms", 0 },
			{ "date_histogram", 1 },
			{ "histogram", 2 },
		};

		public void Serialize(ref JsonWriter writer, ISingleGroupSource value, IJsonFormatterResolver formatterResolver)
		{
			if (value is null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Name);
			writer.WriteBeginObject();
			writer.WritePropertyName(value.Type);

			switch (value)
			{
				case ITermsGroupSource termsGroupSource:
					Serialize(ref writer, termsGroupSource, formatterResolver);
					break;
				case IDateHistogramGroupSource dateHistogramGroupSource:
					Serialize(ref writer, dateHistogramGroupSource, formatterResolver);
					break;
				case IHistogramGroupSource histogramGroupSource:
					Serialize(ref writer, histogramGroupSource, formatterResolver);
					break;
				default:
					throw new JsonParsingException($"Unknown {nameof(ISingleGroupSource)}: {value.GetType().Name}");
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		private static void Serialize<TGroupSource>(ref JsonWriter writer, TGroupSource value,
			IJsonFormatterResolver formatterResolver
		) where TGroupSource : ISingleGroupSource
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<TGroupSource>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		public ISingleGroupSource Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			reader.ReadIsBeginObjectWithVerify();
			var name = reader.ReadPropertyName();
			reader.ReadIsBeginObjectWithVerify(); // into source

			var sourcePropertyName = reader.ReadPropertyNameSegmentRaw();

			ISingleGroupSource groupSource = null;

			if (GroupSource.TryGetValue(sourcePropertyName, out var value))
			{
				switch (value)
				{
					case 0:
						groupSource = formatterResolver.GetFormatter<TermsGroupSource>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						groupSource = formatterResolver.GetFormatter<DateHistogramGroupSource>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 2:
						groupSource = formatterResolver.GetFormatter<HistogramGroupSource>()
							.Deserialize(ref reader, formatterResolver);
						break;
				}
			}
			else
				throw new JsonParsingException($"Unknown {nameof(ISingleGroupSource)}: {sourcePropertyName.Utf8String()}");

			reader.ReadIsEndObjectWithVerify();
			reader.ReadIsEndObjectWithVerify();

			groupSource.Name = name;
			return groupSource;
		}
	}
}
