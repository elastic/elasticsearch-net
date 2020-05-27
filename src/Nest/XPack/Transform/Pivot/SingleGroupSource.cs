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
		/// A script to calculate grouping
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="ISingleGroupSource" />
	public abstract class SingleGroupSourceBase : ISingleGroupSource
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public IScript Script { get; set; }
	}

	/// <inheritdoc cref="ISingleGroupSource" />
	public abstract class SingleGroupSourceDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, ISingleGroupSource
		where TDescriptor : SingleGroupSourceDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ISingleGroupSource
	{
		Field ISingleGroupSource.Field { get; set; }
		IScript ISingleGroupSource.Script { get; set; }

		/// <inheritdoc cref="ISingleGroupSource.Field" />
		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISingleGroupSource.Field" />
		public TDescriptor Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISingleGroupSource.Script" />
		public TDescriptor Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		/// <inheritdoc cref="ISingleGroupSource.Script" />
		public TDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}

	/// <summary>
	/// Builds a collection of <see cref="ISingleGroupSource"/>
	/// </summary>
	public class SingleGroupSourcesDescriptor<T>
		: DescriptorPromiseBase<SingleGroupSourcesDescriptor<T>, IDictionary<string, ISingleGroupSource>>
		where T : class
	{
		public SingleGroupSourcesDescriptor() : base (new Dictionary<string, ISingleGroupSource>()) { }

		/// <inheritdoc cref="ITermsGroupSource" />
		public SingleGroupSourcesDescriptor<T> Terms(string name,
			Func<TermsGroupSourceDescriptor<T>, ITermsGroupSource> selector
		) =>
			Assign(new Tuple<string, ITermsGroupSource>(name, selector?.Invoke(new TermsGroupSourceDescriptor<T>())), (a, v) => a.Add(v.Item1, v.Item2));

		/// <inheritdoc cref="IHistogramGroupSource" />
		public SingleGroupSourcesDescriptor<T> Histogram(string name,
			Func<HistogramGroupSourceDescriptor<T>, IHistogramGroupSource> selector
		) =>
			Assign(new Tuple<string, IHistogramGroupSource>(name, selector?.Invoke(new HistogramGroupSourceDescriptor<T>())), (a, v) => a.Add(v.Item1, v.Item2));

		/// <inheritdoc cref="IDateHistogramGroupSource" />
		public SingleGroupSourcesDescriptor<T> DateHistogram(string name,
			Func<DateHistogramGroupSourceDescriptor<T>, IDateHistogramGroupSource> selector
		) =>
			Assign(new Tuple<string, IDateHistogramGroupSource>(name, selector?.Invoke(new DateHistogramGroupSourceDescriptor<T>())), (a, v) => a.Add(v.Item1, v.Item2));
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

			switch (value)
			{
				case ITermsGroupSource termsGroupSource:
					writer.WritePropertyName("terms");
					Serialize(ref writer, termsGroupSource, formatterResolver);
					break;
				case IDateHistogramGroupSource dateHistogramGroupSource:
					writer.WritePropertyName("date_histogram");
					Serialize(ref writer, dateHistogramGroupSource, formatterResolver);
					break;
				case IHistogramGroupSource histogramGroupSource:
					writer.WritePropertyName("histogram");
					Serialize(ref writer, histogramGroupSource, formatterResolver);
					break;
				default:
					throw new JsonParsingException($"Unknown {nameof(ISingleGroupSource)}: {value.GetType().Name}");
			}

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
			{
				reader.ReadNextBlock();
				return null;
			}

			reader.ReadIsBeginObjectWithVerify();
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
			return groupSource;
		}
	}
}
