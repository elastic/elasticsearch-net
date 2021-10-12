// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Experimental;

// Core goals
// - Support serialisation of containers and more complex concepts (tagged unions)
// - Simplify the public API and remove "redundant" interfaces

// Pros
// - Removes interface requirement for descriptor use
// - Simplifies the public API - For example, less Func<T, T> stuff!
// - Avoids leaking implementation details into the public API
// - Can be otimised and tweaked internally without breaking the public contract
// - Types are easier to evolve
// - Provides a cleaner serialisation implementation
// - Containers are easier to support for serialisation
// - Can be code-generated
// - Public API can be considered alpha ready with implemenation evolving
// - Can add descriptors to types in the future without having to add an interface

// Cons
// - More verbose code (although its 100% generated)
// - Sometimes requires more type-checking and casts, although we could optimise that further if profiling shows an issue
// - Might be some cases not yet considered

// Further work
// - Tagged unions
// - In some serialisation cases we may be able to avoid the casting and serialise the `object` directly
// - Compare to using the runtime type overload (performance)

#region Infrastructure

internal static class Fluent
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static TDescriptor Assign<TDescriptor, TValue>(TDescriptor self, TValue value, Action<TDescriptor, TValue> assign)
	where TDescriptor : class
	{
		assign(self, value);
		return self;
	}
}

public interface IRequestParameters { }

public interface IRequest
{
	string ContentType { get; }
}

public abstract class RequestBase<TParameters> : IRequest<TParameters> where TParameters : class, IRequestParameters, new()
{
	[JsonIgnore]
	public string ContentType { get; set; }
}

public interface IRequest<T> : IRequest { }

public abstract class ExperimentalRequestDescriptorBase<TDescriptor, TParameters> : RequestBase<TParameters>, IRequest<TParameters>
	where TDescriptor : ExperimentalRequestDescriptorBase<TDescriptor, TParameters>
	 where TParameters : class, IRequestParameters, new()
{
	private readonly TDescriptor _descriptor;

	protected ExperimentalRequestDescriptorBase() => _descriptor = (TDescriptor)this;

	protected TDescriptor Self => _descriptor;

	protected TDescriptor Assign<TValue>(TValue value, Action<TDescriptor, TValue> assign) => Fluent.Assign(_descriptor, value, assign);

	protected TDescriptor InvokeAndAssign<T>(Action<T> configure, Action<TDescriptor, T> assign) where T : new()
	{
		var d = new T();

		configure(d);

		return Fluent.Assign(_descriptor, d, assign);
	}
}

public abstract class ExperimentalDescriptorBase<TDescriptor>
	where TDescriptor : ExperimentalDescriptorBase<TDescriptor>
{
	private readonly TDescriptor _descriptor;

	protected ExperimentalDescriptorBase() => _descriptor = (TDescriptor)this;

	protected TDescriptor Self => _descriptor;

	protected TDescriptor Assign<TValue>(TValue value, Action<TDescriptor, TValue> assign) => Fluent.Assign(_descriptor, value, assign);

	protected TDescriptor InvokeAndAssign<T>(Action<T> configure, Action<TDescriptor, T> assign) where T : new()
	{
		var d = new T();

		configure(d);

		return Fluent.Assign(_descriptor, d, assign);
	}
}

#endregion

#region Core

public class ClusterHealthRequestParameters : IRequestParameters { }

// We no longer need a common interface between the object representation and the descriptor.
// We no longer need the ConvertAs attribute to support (de)serialisation and can depend on default serialisation.
// Of course, a custom converter can always be registered, for example, for containers (we may be able to have a container base class in that case).
// This design does prevent the use of either an object or descriptor in the object models which may be problematic - Perhaps a simple marker interface is
// useful for that case if we feel it's common enough.
// Alternatively, we could look at a factory method to convert from a descriptor into the object representation, with the added cost of one extra object.
// However, that is likely okay since there are alternatives for high-performance optimisations we can provide.

public class ClusterHealthRequest : RequestBase<ClusterHealthRequestParameters>
{
	public string Name { get; set; }
	public ClusterSubtype Subtype { get; set; }
	public QueryContainer Query { get; set; }
}

public class ClusterSubtype
{
	public string Identifier { get; set; }
}

// Descriptors instead have an auto-generated one-way JSON converter

[JsonConverter(typeof(ClusterHealthRequestDescriptorConverter))]
public class ClusterHealthRequestDescriptor : ExperimentalRequestDescriptorBase<ClusterHealthRequestDescriptor, ClusterHealthRequestParameters>
{
	private string _name;
	private object _subType; // requires some type-checking and casting at serialisation time
	private object _clusterContainer;

	// This increases field size vs. using a shared common interface but avoids some type checks, casting and an unnecessary marker interface.
	// It supports combination of descriptors and object initialiser syntax quite cleanly (see example).
	//private ClusterSubtype _subtype;
	//private ClusterSubtypeDescriptor _subtypeDescriptor;

	public ClusterHealthRequestDescriptor Name(string name) => Assign(name, (a, v) => a._name = v);

	public ClusterHealthRequestDescriptor Subtype(ClusterSubtypeDescriptor descriptor) => Assign(descriptor, (a, v) => _subType = descriptor);

	public ClusterHealthRequestDescriptor Subtype(Action<ClusterSubtypeDescriptor> configureClusterSubtype) => InvokeAndAssign(configureClusterSubtype, (a, v) => a._subType = v);

	public ClusterHealthRequestDescriptor Subtype(ClusterSubtype subtype) => Assign(subtype, (a, v) => a._subType = v);

	public ClusterHealthRequestDescriptor Container(QueryContainer container) => Assign(container, (a, v) => _clusterContainer = container);

	public ClusterHealthRequestDescriptor Container(Action<QueryContainerDescriptor> configureContainer) => InvokeAndAssign(configureContainer, (a, v) => a._clusterContainer = v);

	public ClusterHealthRequestDescriptor Container(QueryContainerDescriptor descriptor) => Assign(descriptor, (a, v) => a._clusterContainer = v);

	//public ClusterHealthRequestDescriptor Subtype(ClusterSubtypeDescriptor descriptor) => Assign(descriptor, (a, v) => _subtypeDescriptor = descriptor);

	//public ClusterHealthRequestDescriptor Subtype(Action<ClusterSubtypeDescriptor> configureClusterSubtype) => InvokeAndAssign(configureClusterSubtype, (a, v) => a._subtypeDescriptor = v);

	//public ClusterHealthRequestDescriptor Subtype(ClusterSubtype subtype) => Assign(subtype, (a, v) => a._subtype = v);

	internal bool TryGetName(out string name)
	{
		if (!string.IsNullOrEmpty(_name))
		{
			name = _name;
			return true;
		}

		name = default;
		return false;
	}

	internal bool TryGetSubtypeDescriptor(out ClusterSubtypeDescriptor subtype)
	{
		if (_subType is ClusterSubtypeDescriptor descriptor)
		{
			subtype = descriptor;
			return true;
		}

		subtype = default;
		return false;
	}

	internal bool TryGetSubtype(out ClusterSubtype subtype)
	{
		if (_subType is ClusterSubtype clusterSubtype)
		{
			subtype = clusterSubtype;
			return true;
		}

		subtype = default;
		return false;
	}

	internal bool TryGetQueryContainerDescriptor(out QueryContainerDescriptor container)
	{
		if (_clusterContainer is QueryContainerDescriptor descriptor)
		{
			container = descriptor;
			return true;
		}

		container = default;
		return false;
	}

	internal bool TryGetQueryContainer(out QueryContainerVariant variant)
	{
		if (_subType is QueryContainerVariant clusterSubtype)
		{
			variant = clusterSubtype;
			return true;
		}

		variant = default;
		return false;
	}

	//internal bool TryGetSubtypeDescriptor(out ClusterSubtypeDescriptor subtype)
	//{
	//	if (_subtypeDescriptor is not null)
	//	{
	//		subtype = _subtypeDescriptor;
	//		return true;
	//	}

	//	subtype = default;
	//	return false;
	//}

	//internal bool TryGetSubtype(out ClusterSubtype subtype)
	//{
	//	if (_subtype is not null)
	//	{
	//		subtype = _subtype;
	//		return true;
	//	}

	//	subtype = default;
	//	return false;
	//}
}

[JsonConverter(typeof(ClusterSubtypeDescriptorConverter))]
public class ClusterSubtypeDescriptor : ExperimentalDescriptorBase<ClusterSubtypeDescriptor>
{
	private string _identifier;

	public ClusterSubtypeDescriptor Identifier(string identifier) => Assign(identifier, (a, v) => a._identifier = v);

	internal bool TryGetIdentifier(out string identifier)
	{
		if (!string.IsNullOrEmpty(_identifier))
		{
			identifier = _identifier;
			return true;
		}

		identifier = default;
		return false;
	}
}

public class Client
{
	private static readonly Transport Transport = new();

	public void Send(ClusterHealthRequest request) => DoRequest(request);

	public void Send(Action<ClusterHealthRequestDescriptor> configureClusterHealthRequest)
	{
		var descriptor = new ClusterHealthRequestDescriptor();
		configureClusterHealthRequest.Invoke(descriptor);
		DoRequest(descriptor);
	}

	public void Send(ClusterHealthRequestDescriptor requestDescriptor) => DoRequest(requestDescriptor);

	private void DoRequest<T>(T request) where T : IRequest => Transport.Send(request);
}

public class Transport
{
	private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

	public void Send<T>(T data) where T : IRequest
	{
		var json = JsonSerializer.Serialize(data, _options);
	}
}

// The converters can be generated

public class ClusterHealthRequestDescriptorConverter : JsonConverter<ClusterHealthRequestDescriptor>
{
	// Descriptors will only ever need to be serialised.
	public override ClusterHealthRequestDescriptor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, ClusterHealthRequestDescriptor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		// For simple properties this is quite straight-forward
		if (value.TryGetName(out var name))
		{
			writer.WritePropertyName("name");
			writer.WriteStringValue(name);
		}

		// When we support complex types which themselves can be defined using a descriptor, we may support setting them
		// both with the descriptor directly, or with the object initialiser type.
		if (value.TryGetSubtypeDescriptor(out var subtypeDescriptor))
		{
			writer.WritePropertyName("subtype");
			JsonSerializer.Serialize(writer, subtypeDescriptor, options);
		}
		else if (value.TryGetSubtype(out var subtype))
		{
			writer.WritePropertyName("subtype");
			JsonSerializer.Serialize(writer, subtype, options);
		}

		if (value.TryGetQueryContainerDescriptor(out var queryContainerDescriptor))
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, queryContainerDescriptor, options);
		}
		else if (value.TryGetQueryContainer(out var queryContainer))
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, queryContainer, options);
		}

		writer.WriteEndObject();
	}
}

public class ClusterSubtypeDescriptorConverter : JsonConverter<ClusterSubtypeDescriptor>
{
	// Descriptors will only ever need to be serialised.
	public override ClusterSubtypeDescriptor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, ClusterSubtypeDescriptor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		if (value.TryGetIdentifier(out var identifier))
		{
			writer.WritePropertyName("identifier");
			writer.WriteStringValue(identifier);
		}

		writer.WriteEndObject();
	}
}

#endregion

#region ContainterPrototypes

public abstract class QueryContainerVariant
{
	[JsonIgnore]
	internal abstract string VariantName { get; }
}

public class BoolQuery : QueryContainerVariant
{
	internal override string VariantName => "bool";

	public string Tag { get; set; }
}

public class BoostingQuery : QueryContainerVariant
{
	internal override string VariantName => "boosting";

	public int Boost { get; set; }
}

[JsonConverter(typeof(QueryContainerConverter))]
public class QueryContainer
{
	private readonly QueryContainerVariant _variant;

	public QueryContainer(QueryContainerVariant variant)
	{
		if (variant == null)
			return;

		_variant = variant;
	}

	internal bool TryGetBoolQuery(out BoolQuery variantOne)
	{
		if (_variant is BoolQuery variant)
		{
			variantOne = variant;
			return true;
		}

		variantOne = default;
		return false;
	}

	internal bool TryGetBoostingQuery(out BoostingQuery variantTwo)
	{
		if (_variant is BoostingQuery variant)
		{
			variantTwo = variant;
			return true;
		}

		variantTwo = default;
		return false;
	}

	internal string VariantName => _variant is not null ? _variant.VariantName : string.Empty;

	internal bool HasVariant => !string.IsNullOrEmpty(_variant.VariantName) && _variant is not null;
}

[JsonConverter(typeof(QueryContainerDescriptorConverter))]
public class QueryContainerDescriptor : ExperimentalDescriptorBase<QueryContainerDescriptor>
{
	private QueryContainer container;
	private object _containerDescriptor;

	public QueryContainerDescriptor Bool(BoolQuery variant) => Assign(variant, (a, v) => a.container = new QueryContainer(v));

	public QueryContainerDescriptor Boosting(BoostingQuery variant) => Assign(variant, (a, v) => a.container = new QueryContainer(v));

	public QueryContainerDescriptor Bool(Action<BoolQueryDescriptor> configureVariant) => InvokeAndAssign(configureVariant, (a, v) => a._containerDescriptor = v);

	public QueryContainerDescriptor Boosting(Action<BoostingQueryDescriptor> configureVariant) => InvokeAndAssign(configureVariant, (a, v) => a._containerDescriptor = v);

	internal bool TryGetContainer(out QueryContainer variantDescriptor)
	{
		if (container is not null)
		{
			variantDescriptor = container;
			return true;
		}

		variantDescriptor = default;
		return false;
	}

	internal bool TryGetBoolQueryDescriptor(out BoolQueryDescriptor variantDescriptor)
	{
		if (_containerDescriptor is BoolQueryDescriptor containerVariant)
		{
			variantDescriptor = containerVariant;
			return true;
		}

		variantDescriptor = default;
		return false;
	}

	internal bool TryGetBoostingQueryDescriptor(out BoostingQueryDescriptor variantDescriptor)
	{
		if (_containerDescriptor is BoostingQueryDescriptor containerVariant)
		{
			variantDescriptor = containerVariant;
			return true;
		}

		variantDescriptor = default;
		return false;
	}

	//internal bool HasVariant => !string.IsNullOrEmpty(_variant.VariantName) && _variant is not null;
}

[JsonConverter(typeof(BoolQueryDescriptorConverter))]
public class BoolQueryDescriptor : ExperimentalDescriptorBase<BoolQueryDescriptor>
{
	private string _tag;

	public BoolQueryDescriptor Tag(string tag) => Assign(tag, (a, v) => a._tag = v);

	internal bool GetTag(out string stringValue)
	{
		if (!string.IsNullOrEmpty(_tag))
		{
			stringValue = _tag;
			return true;
		}

		stringValue = default;
		return false;
	}

	internal string VariantName => "bool";
}

[JsonConverter(typeof(BoostingQueryDescriptorConverter))]
public class BoostingQueryDescriptor : ExperimentalDescriptorBase<BoostingQueryDescriptor>
{
	private int? _boost;

	public BoostingQueryDescriptor BoostAmount(int intValue) => Assign(intValue, (a, v) => a._boost = v);

	internal bool TryGetBoost(out int intValue)
	{
		if (_boost.HasValue)
		{
			intValue = _boost.Value;
			return true;
		}

		intValue = default;
		return false;
	}

	internal string VariantName => "boosting";
}

public class QueryContainerConverter : JsonConverter<QueryContainer>
{
	public override QueryContainer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, QueryContainer value, JsonSerializerOptions options)
	{
		if (value is null || !value.HasVariant)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartObject();

		writer.WritePropertyName(value.VariantName);

		if (value.TryGetBoolQuery(out var variantOne))
		{
			JsonSerializer.Serialize(writer, variantOne, options);
		}
		else if (value.TryGetBoostingQuery(out var variantTwo))
		{
			JsonSerializer.Serialize(writer, variantTwo, options);
		}

		writer.WriteEndObject();
	}
}

public class QueryContainerDescriptorConverter : JsonConverter<QueryContainerDescriptor>
{
	// Descriptors will only ever need to be serialised.
	public override QueryContainerDescriptor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, QueryContainerDescriptor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		if (value.TryGetContainer(out var container))
		{
			writer.WritePropertyName(container.VariantName);
			JsonSerializer.Serialize(writer, container, options);
		}
		else if (value.TryGetBoolQueryDescriptor(out var variantOneDescriptor))
		{
			writer.WritePropertyName(variantOneDescriptor.VariantName);
			JsonSerializer.Serialize(writer, variantOneDescriptor, options);
		}
		else if (value.TryGetBoostingQueryDescriptor(out var variantTwoDescriptor))
		{
			writer.WritePropertyName(variantTwoDescriptor.VariantName);
			JsonSerializer.Serialize(writer, variantTwoDescriptor, options);
		}

		writer.WriteEndObject();
	}
}

public class BoolQueryDescriptorConverter : JsonConverter<BoolQueryDescriptor>
{
	// Descriptors will only ever need to be serialised.
	public override BoolQueryDescriptor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, BoolQueryDescriptor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		if (value.GetTag(out var tag))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(tag);
		}

		writer.WriteEndObject();
	}
}

public class BoostingQueryDescriptorConverter : JsonConverter<BoostingQueryDescriptor>
{
	// Descriptors will only ever need to be serialised.
	public override BoostingQueryDescriptor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, BoostingQueryDescriptor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		if (value.TryGetBoost(out var boost))
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(boost);
		}

		writer.WriteEndObject();
	}
}

#endregion
