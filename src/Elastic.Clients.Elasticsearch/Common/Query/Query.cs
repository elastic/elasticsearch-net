// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using Elastic.Clients.Elasticsearch.Enrich;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	//public interface IQuery
	//{
	//	/// <summary>
	//	///     Provides a boost to this query to influence its relevance score.
	//	///     For example, a query with a boost of 2 is twice as important as a query with a boost of 1,
	//	///     although the actual boost value that is applied undergoes normalization and internal optimization.
	//	/// </summary>
	//	//float? Boost { get; set; } // Was defined as a double before the code gen work

	//	///// <summary>
	//	/////     Whether the query is conditionless. A conditionless query is not serialized as part of the request
	//	/////     sent to Elasticsearch.
	//	///// </summary>
	//	//[JsonIgnore]
	//	//bool Conditionless { get; }

	//	///// <summary>
	//	/////     Whether the query should be treated as strict. A strict query will throw an exception when serialized
	//	/////     if it is <see cref="Conditionless" />.
	//	///// </summary>
	//	//[JsonIgnore]
	//	//bool IsStrict { get; set; }

	//	///// <summary>
	//	/////     Whether the query should be treated as verbatim. A verbatim query will be serialized as part of the request,
	//	/////     irrespective
	//	/////     of whether it is <see cref="Conditionless" /> or not.
	//	///// </summary>
	//	//[JsonIgnore]
	//	//bool IsVerbatim { get; set; }

	//	/// <summary>
	//	///     Whether the query should be treated as writable. Used when determining how to combine queries.
	//	/// </summary>
	//	//[JsonIgnore]
	//	//bool IsWritable { get; }

	//	/// <summary>
	//	///     The name of the query. Allows you to retrieve for each document what part of the query it matched on.
	//	/// </summary>
	//	//string Name { get; set; }
	//}

	//internal interface IFieldNameQueryDescriptor<T> where T : Descriptor
	//{
	//	T Field(Field field);
	//}

	//public partial class QueryContainerDescriptor
	//{
	//	private void Set<T>(Action<IFieldNameQueryDescriptor<T>> descriptorAction, string variantName)
	//		where T : Descriptor, IFieldNameQueryDescriptor<T>
	//	{
	//		if (ContainsVariant)
	//			throw new Exception("TODO");
	//		ContainedVariantName = variantName;
	//		ContainsVariant = true;
	//		DescriptorType = typeof(T);
	//		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
	//		descriptorAction?.Invoke(descriptor);
	//		Descriptor = descriptor;
	//	}

	//	private void Set<T>(Field field, Action<IFieldNameQueryDescriptor<T>> descriptorAction, string variantName)
	//		where T : Descriptor, IFieldNameQueryDescriptor<T>
	//	{
	//		if (ContainsVariant)
	//			throw new Exception("TODO");
	//		ContainedVariantName = variantName;
	//		ContainsVariant = true;
	//		DescriptorType = typeof(T);
	//		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
	//		descriptor.Field(field);
	//		descriptorAction?.Invoke(descriptor);
	//		Descriptor = descriptor;
	//	}
	//}

	// Leaving descriptor value experiments for future pick up work

	// RESULT OF EXPERIMENTS:
	//|            Method |  Mean [ns] | Error [ns] | StdDev [ns] |  Gen 0 |  Gen 1 | Allocated [B] |
	//|------------------ |-----------:|-----------:|------------:|-------:|-------:|--------------:|
	//|          Existing | 116.408 ns |  2.1955 ns |   2.1563 ns | 0.0942 | 0.0002 |         592 B |
	//|             NewV1 | 113.021 ns |  1.8475 ns |   1.8145 ns | 0.0943 | 0.0004 |         592 B |
	//|             NewV2 | 115.728 ns |  2.2291 ns |   2.0851 ns | 0.0905 | 0.0002 |         568 B |
	//|             NewV3 | 112.813 ns |  2.2198 ns |   3.1118 ns | 0.0867 | 0.0002 |         544 B |
	//|       SetExisting |   1.688 ns |  0.0384 ns |   0.0340 ns |      - |      - |             - |
	//|          SetNewV1 |   2.200 ns |  0.0232 ns |   0.0217 ns |      - |      - |             - |
	//|          SetNewV2 |   2.555 ns |  0.0289 ns |   0.0256 ns |      - |      - |             - |
	//|          SetNewV3 |   1.691 ns |  0.0306 ns |   0.0286 ns |      - |      - |             - |
	//| SerialiseExisting | 461.120 ns |  9.0066 ns |   8.4248 ns | 0.0687 |      - |         432 B |
	//|    SerialiseNewV1 | 472.239 ns |  8.4655 ns |   7.9186 ns | 0.0687 |      - |         432 B |
	//|    SerialiseNewV2 | 476.059 ns |  9.2350 ns |  10.2647 ns | 0.0687 |      - |         432 B |
	//|    SerialiseNewV3 | 483.717 ns |  9.5424 ns |   8.9260 ns | 0.0687 |      - |         432 B |

	// We can save 16 bytes per descriptor property (when those properties themselves support descriptors).
	// We would need to consider cases for types which do not have a descriptor and if we continue with a raw value property directly.
	// This is probably the best choice. Performance for setting and serialising is generally on par. Serialisation for V3 is slower so
	// is a trade off with the allocation reduction. Inlining may help there?

	//public sealed class Existing<TDocument>
	//{
	//	private Elastic.Clients.Elasticsearch.Enrich.Policy? GeoMatchValue { get; set; }
	//	private PolicyDescriptor<TDocument> GeoMatchDescriptor { get; set; }
	//	private Action<PolicyDescriptor<TDocument>> GeoMatchDescriptorAction { get; set; }
	//	private Elastic.Clients.Elasticsearch.Enrich.Policy? MatchValue { get; set; }
	//	private PolicyDescriptor<TDocument> MatchDescriptor { get; set; }
	//	private Action<PolicyDescriptor<TDocument>> MatchDescriptorAction { get; set; }
	//	private Elastic.Clients.Elasticsearch.Enrich.Policy? RangeValue { get; set; }
	//	private PolicyDescriptor<TDocument> RangeDescriptor { get; set; }
	//	private Action<PolicyDescriptor<TDocument>> RangeDescriptorAction { get; set; }
	//}

	//public sealed class NewV1<TDocument>
	//{
	//	private DescriptorValue<Policy?, PolicyDescriptor<TDocument>> GeoMatchValue { get; set; }
	//	private DescriptorValue<Policy?, PolicyDescriptor<TDocument>> MatchValue { get; set; }
	//	private DescriptorValue<Policy?, PolicyDescriptor<TDocument>> RangeValue { get; set; }
	//}

	//public sealed class NewV2<TDocument>
	//{
	//	private DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>> GeoMatchValue { get; set; }
	//	private DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>> MatchValue { get; set; }
	//	private DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>> RangeValue { get; set; }
	//}

	//public sealed partial class EnrichPutPolicyRequestDescriptorV2<TDocument> : RequestDescriptorBase<EnrichPutPolicyRequestDescriptorV2<TDocument>, EnrichPutPolicyRequestParameters>
	//{
	//	internal EnrichPutPolicyRequestDescriptorV2(Action<EnrichPutPolicyRequestDescriptorV2<TDocument>> configure) => configure.Invoke(this);

	//	public EnrichPutPolicyRequestDescriptorV2(Name name) : base(r => r.Required("name", name))
	//	{
	//	}

	//	internal EnrichPutPolicyRequestDescriptorV2()
	//	{
	//	}

	//	internal override ApiUrls ApiUrls => ApiUrlsLookups.EnrichPutPolicy;
	//	protected override HttpMethod HttpMethod => HttpMethod.PUT;
	//	protected override bool SupportsBody => true;

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> Name(Name name)
	//	{
	//		RouteValues.Required("name", name);
	//		return Self;
	//	}

	//	private DescriptorValue<Policy?, PolicyDescriptor<TDocument>> GeoMatchValue { get; set; } = default;
	//	private DescriptorValue<Policy?, PolicyDescriptor<TDocument>> MatchValue { get; set; } = default;
	//	private DescriptorValue<Policy?, PolicyDescriptor<TDocument>> RangeValue { get; set; } = default;

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> GeoMatch(Policy? geoMatch)
	//	{
	//		GeoMatchValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(geoMatch);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> GeoMatch(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		GeoMatchValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> GeoMatch(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		GeoMatchValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> Match(Policy? match)
	//	{
	//		MatchValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(match);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> Match(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		MatchValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> Match(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		MatchValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> Range(Policy? range)
	//	{
	//		RangeValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(range);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> Range(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		RangeValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV2<TDocument> Range(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		RangeValue = DescriptorValue<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	//	{
	//		writer.WriteStartObject();
	//		GeoMatchValue.Serialize("geo_match", writer, options);
	//		MatchValue.Serialize("match", writer, options);
	//		RangeValue.Serialize("range", writer, options);
	//		writer.WriteEndObject();
	//	}
	//}

	//public sealed partial class EnrichPutPolicyRequestDescriptorV3<TDocument> : RequestDescriptorBase<EnrichPutPolicyRequestDescriptorV3<TDocument>, EnrichPutPolicyRequestParameters>
	//{
	//	internal EnrichPutPolicyRequestDescriptorV3(Action<EnrichPutPolicyRequestDescriptorV3<TDocument>> configure) => configure.Invoke(this);

	//	public EnrichPutPolicyRequestDescriptorV3(Name name) : base(r => r.Required("name", name))
	//	{
	//	}

	//	internal EnrichPutPolicyRequestDescriptorV3()
	//	{
	//	}

	//	internal override ApiUrls ApiUrls => ApiUrlsLookups.EnrichPutPolicy;
	//	protected override HttpMethod HttpMethod => HttpMethod.PUT;
	//	protected override bool SupportsBody => true;

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> Name(Name name)
	//	{
	//		RouteValues.Required("name", name);
	//		return Self;
	//	}

	//	private DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>> GeoMatchValue { get; set; } = default;
	//	private DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>> MatchValue { get; set; } = default;
	//	private DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>> RangeValue { get; set; } = default;

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> GeoMatch(Policy? geoMatch)
	//	{
	//		GeoMatchValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(geoMatch);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> GeoMatch(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		GeoMatchValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> GeoMatch(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		GeoMatchValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> Match(Policy? match)
	//	{
	//		MatchValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(match);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> Match(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		MatchValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> Match(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		MatchValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> Range(Policy? range)
	//	{
	//		RangeValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(range);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> Range(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		RangeValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV3<TDocument> Range(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		RangeValue = DescriptorValueV2<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	//	{
	//		writer.WriteStartObject();
	//		GeoMatchValue.Serialize("geo_match", writer, options);
	//		MatchValue.Serialize("geo_match", writer, options);
	//		RangeValue.Serialize("geo_match", writer, options);
	//		writer.WriteEndObject();
	//	}
	//}

	//public sealed partial class EnrichPutPolicyRequestDescriptorV4<TDocument> : RequestDescriptorBase<EnrichPutPolicyRequestDescriptorV4<TDocument>, EnrichPutPolicyRequestParameters>
	//{
	//	internal EnrichPutPolicyRequestDescriptorV4(Action<EnrichPutPolicyRequestDescriptorV4<TDocument>> configure) => configure.Invoke(this);

	//	public EnrichPutPolicyRequestDescriptorV4(Name name) : base(r => r.Required("name", name))
	//	{
	//	}

	//	internal EnrichPutPolicyRequestDescriptorV4()
	//	{
	//	}

	//	internal override ApiUrls ApiUrls => ApiUrlsLookups.EnrichPutPolicy;
	//	protected override HttpMethod HttpMethod => HttpMethod.PUT;
	//	protected override bool SupportsBody => true;

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> Name(Name name)
	//	{
	//		RouteValues.Required("name", name);
	//		return Self;
	//	}

	//	private DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>> GeoMatchValue { get; set; } = default;
	//	private DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>> MatchValue { get; set; } = default;
	//	private DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>> RangeValue { get; set; } = default;

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> GeoMatch(Policy? geoMatch)
	//	{
	//		GeoMatchValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(geoMatch);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> GeoMatch(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		GeoMatchValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> GeoMatch(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		GeoMatchValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> Match(Policy? match)
	//	{
	//		MatchValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(match);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> Match(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		MatchValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> Match(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		MatchValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> Range(Policy? range)
	//	{
	//		RangeValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(range);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> Range(PolicyDescriptor<TDocument> descriptor)
	//	{
	//		RangeValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(descriptor);
	//		return Self;
	//	}

	//	public EnrichPutPolicyRequestDescriptorV4<TDocument> Range(Action<PolicyDescriptor<TDocument>> configure)
	//	{
	//		RangeValue = DescriptorValueV3<Policy?, PolicyDescriptor<TDocument>>.Create(configure);
	//		return Self;
	//	}

	//	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	//	{
	//		writer.WriteStartObject();
	//		GeoMatchValue.Serialize("geo_match", writer, options);
	//		MatchValue.Serialize("geo_match", writer, options);
	//		RangeValue.Serialize("geo_match", writer, options);
	//		writer.WriteEndObject();
	//	}
	//}

	//internal readonly struct DescriptorValue<TObject, TDescriptor>
	//	where TDescriptor : Descriptor
	//{
	//	private readonly TObject? _object;
	//	private readonly TDescriptor? _descriptor;
	//	private readonly Action<TDescriptor>? _descriptorAction;
	//	//private bool _isSet;

	//	private DescriptorValue(TObject value)
	//	{
	//		_descriptor = null;
	//		_descriptorAction = null;
	//		_object = value;
	//	}

	//	private DescriptorValue(TDescriptor value)
	//	{
	//		_descriptor = value;
	//		_descriptorAction = null;
	//		_object = default;
	//	}

	//	private DescriptorValue(Action<TDescriptor> value)
	//	{
	//		_descriptor = null;
	//		_descriptorAction = value;
	//		_object = default;
	//	}

	//	public static DescriptorValue<TObject, TDescriptor> Create(TObject value)
	//		=> value switch
	//			{
	//				null => default,
	//				_ => new(value)
	//			};

	//	public static DescriptorValue<TObject, TDescriptor> Create(TDescriptor descriptor)
	//		=> descriptor switch
	//		{
	//			null => default,
	//			_ => new(descriptor)
	//		};

	//	public static DescriptorValue<TObject, TDescriptor> Create(Action<TDescriptor> action)
	//		=> action switch
	//		{
	//			null => default,
	//			_ => new(action)
	//		};

	//	public void Serialize(string propertyName, Utf8JsonWriter writer, JsonSerializerOptions options)
	//	{
	//		// We could include a ctor taking propertyName so we set it when initialising the property on descriptors?

	//		//if (!_isSet)
	//		//	return;

	//		//writer.WritePropertyName(propertyName);

	//		// Benchmark using a integer flag to know which value is set so we can avoid the null checks which "might" be faster.

	//		if (_object is not null)
	//		{
	//			writer.WritePropertyName(propertyName);
	//			JsonSerializer.Serialize(writer, _object, options);
	//		}
	//		else if (_descriptor is not null)
	//		{
	//			writer.WritePropertyName(propertyName);
	//			JsonSerializer.Serialize(writer, _descriptor, options);
	//		}
	//		else if (_descriptorAction is not null)
	//		{
	//			writer.WritePropertyName(propertyName);
	//			var descriptor = (TDescriptor)Activator.CreateInstance(typeof(TDescriptor), true);
	//			_descriptorAction?.Invoke(descriptor);
	//			JsonSerializer.Serialize(writer, descriptor, options);
	//		}
	//	}
	//}

	//internal readonly struct DescriptorValueV2<TObject, TDescriptor>
	//	where TDescriptor : Descriptor
	//{
	//	private readonly object? _object;
	//	private readonly int _containedValueType;

	//	private DescriptorValueV2(TObject value)
	//	{
	//		_object = value;
	//		_containedValueType = value is null ? 0 : 1;
	//	}

	//	private DescriptorValueV2(TDescriptor value)
	//	{
	//		_object = value;
	//		_containedValueType = value is null ? 0 : 2;
	//	}

	//	private DescriptorValueV2(Action<TDescriptor> value)
	//	{
	//		_object = value;
	//		_containedValueType = value is null ? 0 : 3;
	//	}

	//	public static DescriptorValueV2<TObject, TDescriptor> Create(TObject value)
	//		=> value switch
	//		{
	//			null => default,
	//			_ => new(value)
	//		};

	//	public static DescriptorValueV2<TObject, TDescriptor> Create(TDescriptor descriptor)
	//		=> descriptor switch
	//		{
	//			null => default,
	//			_ => new(descriptor)
	//		};

	//	public static DescriptorValueV2<TObject, TDescriptor> Create(Action<TDescriptor> action)
	//		=> action switch
	//		{
	//			null => default,
	//			_ => new(action)
	//		};

	//	public void Serialize(string propertyName, Utf8JsonWriter writer, JsonSerializerOptions options)
	//	{
	//		// We could include a ctor taking propertyName so we set it when initialising the property on descriptors?

	//		if (_containedValueType == 0)
	//			return;

	//		writer.WritePropertyName(propertyName);

	//		// Benchmark using a integer flag to know which value is set so we can avoid the null checks which "might" be faster.

	//		switch (_containedValueType)
	//		{
	//			case 1:
	//				JsonSerializer.Serialize(writer, (TObject)_object, options);
	//				break;
	//			case 2:
	//				JsonSerializer.Serialize(writer, (TDescriptor)_object, options);
	//				break;
	//			case 3:					
	//				var descriptor = (TDescriptor)Activator.CreateInstance(typeof(TDescriptor), true);
	//				((Action<TDescriptor>)_object)?.Invoke(descriptor);
	//				JsonSerializer.Serialize(writer, descriptor, options);
	//				break;
	//		}
	//	}
	//}

	//internal readonly struct DescriptorValueV3<TObject, TDescriptor>
	//	where TDescriptor : Descriptor
	//{
	//	private readonly object? _object;
	//	// removing the int reduces the footprint but forces the use of type checking to perform operations

	//	private DescriptorValueV3(TObject value) => _object = value;

	//	private DescriptorValueV3(TDescriptor value) => _object = value;

	//	private DescriptorValueV3(Action<TDescriptor> value) => _object = value;

	//	public static DescriptorValueV3<TObject, TDescriptor> Create(TObject value)
	//		=> value switch
	//		{
	//			null => default,
	//			_ => new(value)
	//		};

	//	public static DescriptorValueV3<TObject, TDescriptor> Create(TDescriptor descriptor)
	//		=> descriptor switch
	//		{
	//			null => default,
	//			_ => new(descriptor)
	//		};

	//	public static DescriptorValueV3<TObject, TDescriptor> Create(Action<TDescriptor> action)
	//		=> action switch
	//		{
	//			null => default,
	//			_ => new(action)
	//		};

	//	public void Serialize(string propertyName, Utf8JsonWriter writer, JsonSerializerOptions options)
	//	{
	//		// We could include a ctor taking propertyName so we set it when initialising the property on descriptors?

	//		if (_object is null)
	//			return;

	//		writer.WritePropertyName(propertyName);

	//		// Benchmark using a integer flag to know which value is set so we can avoid the null checks which "might" be faster.

	//		if (_object is TObject value)
	//		{
	//			JsonSerializer.Serialize(writer, value, options);
	//		}
	//		else if (_object is TDescriptor descriptor)
	//		{
	//			JsonSerializer.Serialize(writer, descriptor, options);
	//		}
	//		else
	//		{
	//			var d = (TDescriptor)Activator.CreateInstance(typeof(TDescriptor), true);
	//			((Action<TDescriptor>)_object)?.Invoke(d);
	//			JsonSerializer.Serialize(writer, d, options);
	//		}
	//	}
	//}

	public abstract partial class Query //: IQuery
	{
		//[JsonIgnore]
		//public bool IsWritable => throw new NotImplementedException();

		////protected abstract bool Conditionless { get; }
		//[JsonIgnore]
		//public bool IsStrict { get; set; }

		//[JsonIgnore]
		//public bool IsVerbatim { get; set; }

		//[JsonIgnore]
		//public bool IsWritable => true; //IsVerbatim || !Conditionless;

		//bool IQuery.Conditionless => Conditionless;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(Query a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(Query a) => false;

		//public static QueryBase operator &(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l && r);

		//public static QueryBase operator |(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l || r);

		//public static QueryBase operator !(QueryBase query) => query == null || !query.IsWritable
		//	? null
		//	: new BoolQuery { MustNot = new QueryContainer[] { query } };

		//public static QueryBase operator +(QueryBase query) => query == null || !query.IsWritable
		//	? null
		//	: new BoolQuery { Filter = new QueryContainer[] { query } };

		//private static QueryBase Combine(QueryBase leftQuery, QueryBase rightQuery, Func<QueryContainer, QueryContainer, QueryContainer> combine)
		//{
		//	if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftQuery, rightQuery, out var q))
		//		return q;

		//	IQueryContainer container = combine(leftQuery, rightQuery);
		//	var query = container.Bool;
		//	return new BoolQuery
		//	{
		//		Must = query.Must,
		//		MustNot = query.MustNot,
		//		Should = query.Should,
		//		Filter = query.Filter,
		//	};
		//}

		//private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryBase leftQuery, QueryBase rightQuery,
		//	out QueryBase query)
		//{
		//	query = null;
		//	if (leftQuery == null && rightQuery == null)
		//		return true;

		//	var leftWritable = leftQuery?.IsWritable ?? false;
		//	var rightWritable = rightQuery?.IsWritable ?? false;
		//	if (leftWritable && rightWritable)
		//		return false;
		//	if (!leftWritable && !rightWritable)
		//		return true;

		//	query = leftWritable ? leftQuery : rightQuery;
		//	return true;
		//}

		//public static implicit operator QueryContainer(QueryBase query) =>
		//	query == null ? null : new QueryContainer(query);

		//internal void WrapInContainer(IQueryContainer container) => InternalWrapInContainer(container);

		////container.IsVerbatim = IsVerbatim;
		////container.IsStrict = IsStrict;

		//internal abstract void InternalWrapInContainer(IQueryContainer container);
	}
}
