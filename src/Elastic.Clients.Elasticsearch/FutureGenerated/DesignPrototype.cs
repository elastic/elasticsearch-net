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

public abstract class ExperimentalRequestBase<TParameters> : IRequest<TParameters> where TParameters : class, IRequestParameters, new()
{
	[JsonIgnore]
	public string ContentType { get; set; }
}

public interface IRequest<T> : IRequest { }

public abstract class ExperimentalRequestDescriptorBase<TDescriptor, TParameters> : ExperimentalRequestBase<TParameters>, IRequest<TParameters>
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

public class ExampleRequest : ExperimentalRequestBase<ClusterHealthRequestParameters>
{
	public string Name { get; set; }
	public ClusterSubtype Subtype { get; set; }
	public QueryContainer Query { get; set; }
}

// Descriptors instead have an auto-generated one-way JSON converter

[JsonConverter(typeof(ExampleRequestDescriptorConverter))]
public class ExampleRequestDescriptor : ExperimentalRequestDescriptorBase<ExampleRequestDescriptor, ClusterHealthRequestParameters>
{
	//private string _name;
	//private object _subType; // requires some type-checking and casting at serialisation time
	//private object _clusterContainer;

	// This increases field size vs. using a shared common interface but avoids some type checks, casting and an unnecessary marker interface.
	// It supports combination of descriptors and object initialiser syntax quite cleanly (see example).
	// Could be properties but the names would need to not conflict with the descriptor methods
	internal string _name;
	internal ClusterSubtype _subtype;
	internal ClusterSubtypeDescriptor _subtypeDescriptor;
	internal QueryContainer _queryContainer;
	internal QueryContainerDescriptor _queryContainerDescriptor;

	public ExampleRequestDescriptor Name(string name) => Assign(name, (a, v) => a._name = v);

	public ExampleRequestDescriptor Subtype(ClusterSubtype subtype) => Assign(subtype, (a, v) =>
	{
		a._subtype = v;
		a._subtypeDescriptor = null;
	});

	public ExampleRequestDescriptor Subtype(ClusterSubtypeDescriptor descriptor)
		=> Assign(descriptor, (a, v) =>
		{
			_subtypeDescriptor = descriptor;
			_subtype = null;
		});

	public ExampleRequestDescriptor Subtype(Action<ClusterSubtypeDescriptor> configureClusterSubtype)
		=> InvokeAndAssign(configureClusterSubtype, (a, v) =>
		{
			a._subtypeDescriptor = v;
			a._subtype = null;
		});

	public ExampleRequestDescriptor Query(QueryContainer container) => Assign(container, (a, v) =>
	{
		_queryContainer = container;
		_queryContainerDescriptor = null;
	});

	public ExampleRequestDescriptor Query(Action<QueryContainerDescriptor> configureContainer)
		=> InvokeAndAssign(configureContainer, (a, v) =>
		{
			a._queryContainerDescriptor = v;
			a._queryContainer = null;
		});

	public ExampleRequestDescriptor Query(QueryContainerDescriptor descriptor)
		=> Assign(descriptor, (a, v) =>
		{
			a._queryContainerDescriptor = v;
			a._queryContainer = null;
		});

	public static implicit operator ExampleRequest(ExampleRequestDescriptor d) => new ExampleRequest { Name = d._name, Query = d._queryContainer, Subtype = d._subtype };

	public static implicit operator ExampleRequestDescriptor(ExampleRequest r) => new ExampleRequestDescriptor().Name(r.Name).Query(r.Query).Subtype(r.Subtype);
}

public class ClusterSubtype
{
	public string Identifier { get; set; }
}



[JsonConverter(typeof(ClusterSubtypeDescriptorConverter))]
public class ClusterSubtypeDescriptor : ExperimentalDescriptorBase<ClusterSubtypeDescriptor>
{
	private string _identifier;

	public ClusterSubtypeDescriptor Identifier(string identifier) => Assign(identifier, (a, v) => a._identifier = v);

	// Alternative approach moving the checking into the type
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

	public static implicit operator ClusterSubtype(ClusterSubtypeDescriptor d) => new ClusterSubtype { Identifier = d._identifier };

	public static implicit operator ClusterSubtypeDescriptor(ClusterSubtype r) => new ClusterSubtypeDescriptor().Identifier(r.Identifier);
}

public class Client
{
	private static readonly Transport Transport = new();

	public void SomeEndpoint(ExampleRequest request) => DoRequest(request);

	/// <summary>
	/// Send a request to an example endpoint.
	/// </summary>
	/// <param name="configureRequest">An <see cref="Action{T}"/> used to configure the <see cref="ExampleRequestDescriptor"/>.</param>
	public void SomeEndpoint(Action<ExampleRequestDescriptor> configureRequest)
	{
		var descriptor = new ExampleRequestDescriptor();
		configureRequest.Invoke(descriptor);
		DoRequest(descriptor);
	}

	public void SomeEndpoint(ExampleRequestDescriptor requestDescriptor) => DoRequest(requestDescriptor);

	public void CombinedEndpoint(CombinedRequest request) => DoRequest(request);

	public void CombinedEndpoint(Action<CombinedRequest> configureRequest)
	{
		var descriptor = new CombinedRequest();
		configureRequest.Invoke(descriptor);
		DoRequest(descriptor);
	}

	private void DoRequest<T>(T request) where T : IRequest => Transport.Send(request);
}

public class Transport
{
	private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

	public void Send<T>(T data) where T : IRequest
	{
		var json = JsonSerializer.Serialize(data, _options);
		Console.WriteLine(json);
	}
}

// The converters can be generated

public class ExampleRequestDescriptorConverter : JsonConverter<ExampleRequestDescriptor>
{
	// Descriptors will only ever need to be serialised.
	public override ExampleRequestDescriptor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, ExampleRequestDescriptor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		if (value._name is not null)
		{
			writer.WritePropertyName("name");
			writer.WriteStringValue(value._name);
		}

		// When we support complex types which themselves can be defined using a descriptor, we may support setting them
		// both with the descriptor directly, or with the object initialiser type.
		if (value._subtypeDescriptor is not null)
		{
			writer.WritePropertyName("subtype");
			JsonSerializer.Serialize(writer, value._subtypeDescriptor, options);
		}
		else if (value._subtype is not null)
		{
			writer.WritePropertyName("subtype");
			JsonSerializer.Serialize(writer, value._subtype, options);
		}

		if (value._queryContainerDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, value._queryContainerDescriptor, options);
		}
		else if (value._queryContainer is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, value._queryContainer, options);
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

//public abstract class QueryContainerVariant
//{
//	[JsonIgnore]
//	internal abstract string VariantName { get; }

//	public QueryContainer WrapInContainer() => new(this);
//}


public abstract class ContainerVariantBase
{
	[JsonIgnore]
	internal abstract string VariantName { get; }
}

public abstract class ContainerVariantBase<TContainer> : ContainerVariantBase where TContainer : ContainerBase
{
	public TContainer WrapInContainer() => (TContainer)Activator.CreateInstance(typeof(TContainer), this);
}

public abstract class ContainerVariantBase<TContainer, TContainer2> : ContainerVariantBase where TContainer : ContainerBase where TContainer2 : ContainerBase
{
	public TContainer WrapInContainer() => (TContainer)Activator.CreateInstance(typeof(TContainer), this);
	//public TContainer2 WrapInContainer() => (TContainer2)Activator.CreateInstance(typeof(TContainer2), this);
}


public abstract class QueryBase : ContainerVariantBase<QueryContainer> { }

public class BoolQuery : QueryBase
{
	internal override string VariantName => "bool";

	public string Tag { get; set; }
}

public class BoostingQuery : QueryBase
{
	internal override string VariantName => "boosting";

	public int Boost { get; set; }
}

public static class Query
{
	public static QueryContainer Bool(Action<BoolQueryDescriptor> configure) => new QueryContainerDescriptor().Bool(configure).ToQueryContainer();
}

public abstract class ContainerBase
{
	internal ContainerVariantBase Variant { get; }

	public ContainerBase(ContainerVariantBase variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
}


public abstract class ContainerAndVariantBase<TVariantContainer> : ContainerVariantBase<TVariantContainer> where TVariantContainer : ContainerBase
{
	internal ContainerVariantBase Variant { get; }

	public ContainerAndVariantBase(ContainerVariantBase variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
}

public class AnotherContainer : ContainerAndVariantBase<QueryContainer>
{
	public AnotherContainer(ContainerVariantBase variant) : base(variant)
	{
	}

	internal override string VariantName => "Something";
}

[JsonConverter(typeof(QueryContainerConverter))]
public class QueryContainer : ContainerBase
{
	public QueryContainer(QueryBase variant) : base(variant)
	{
	}

	// Similar to Sylvain's support for accessing the query from inside a container.
	// Decide if this is useful to expose?
	// Could also provide a more direct method which throws when the variant type is wrong.
	public bool TryGetBoolQuery(out BoolQuery boolQuery)
	{
		if (Variant is BoolQuery query)
		{
			boolQuery = query;
			return true;
		}

		boolQuery = default;
		return false;
	}

	private string _variantName;

	//internal string VariantName => Variant is not null ? Variant.VariantName : string.Empty;

	internal string VariantName
	{
		get
		{
			if (!string.IsNullOrEmpty(_variantName))
				return _variantName;

			_variantName = Variant.VariantName;

			return _variantName;
		}
	}

	//internal QueryContainerVariant Variant => base.Variant;

	internal bool HasVariant => !string.IsNullOrEmpty(Variant.VariantName) && Variant is not null;
}

public class VariantDescriptorBase<T> : ExperimentalDescriptorBase<T> where T : ExperimentalDescriptorBase<T>
{
	internal string VariantName { get; private set; }

	protected void SetVariantName(string name) => VariantName = name;
}

[JsonConverter(typeof(QueryContainerDescriptorConverter))]
public class QueryContainerDescriptor : VariantDescriptorBase<QueryContainerDescriptor>
{
	private QueryContainer _container;

	// We could have fields for each descriptor here instead, although this type only ever holds one instance
	private IQueryVariantDescriptor _containerDescriptor;

	public QueryContainerDescriptor Bool(BoolQuery variant) => Assign(variant, (a, v) => { a._container = new QueryContainer(v); SetVariantName("bool"); });

	public QueryContainerDescriptor Boosting(BoostingQuery variant) => Assign(variant, (a, v) => a._container = new QueryContainer(v));

	public QueryContainerDescriptor Bool(Action<BoolQueryDescriptor> configureVariant) => InvokeAndAssign(configureVariant, (a, v) => a._containerDescriptor = v);

	public QueryContainerDescriptor Boosting(Action<BoostingQueryDescriptor> configureVariant) => InvokeAndAssign(configureVariant, (a, v) => a._containerDescriptor = v);

	internal QueryContainer ToQueryContainer()
	{
		if (_container is not null)
			return _container;

		switch (_containerDescriptor)
		{
			case BoolQueryDescriptor boolQuery:
				return new QueryContainer(boolQuery.ToBoolQuery());
			case BoostingQueryDescriptor boostingQuery:
				return new QueryContainer(boostingQuery.ToBoostingQuery());
		}

		return null;
	}


	internal bool TryGetContainer(out QueryContainer variantDescriptor)
	{
		if (_container is not null)
		{
			variantDescriptor = _container;
			return true;
		}

		variantDescriptor = default;
		return false;
	}

	internal bool TryGetBoolQueryDescriptor(out BoolQueryDescriptor variantDescriptor)
	{
		if (VariantName == "bool" && _containerDescriptor is BoolQueryDescriptor containerVariant)
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

public abstract class VariantDescriptor<T> : ExperimentalDescriptorBase<T> where T : ExperimentalDescriptorBase<T>
{
	[JsonIgnore]
	internal abstract string VariantName { get; }
}

public interface IQueryVariantDescriptor { }

[JsonConverter(typeof(BoolQueryDescriptorConverter))]
public class BoolQueryDescriptor : VariantDescriptor<BoolQueryDescriptor>, IQueryVariantDescriptor
{
	private string _tag;

	public BoolQueryDescriptor Tag(string tag) => Assign(tag, (a, v) => a._tag = v);

	internal bool TryGetTag(out string stringValue)
	{
		if (!string.IsNullOrEmpty(_tag))
		{
			stringValue = _tag;
			return true;
		}

		stringValue = default;
		return false;
	}

	internal BoolQuery ToBoolQuery()
	{
		var query = new BoolQuery();

		if (TryGetTag(out var tag))
			query.Tag = tag;

		return query;
	}

	internal override string VariantName => "bool";
}

[JsonConverter(typeof(BoostingQueryDescriptorConverter))]
public class BoostingQueryDescriptor : VariantDescriptor<BoostingQueryDescriptor>, IQueryVariantDescriptor
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

	internal BoostingQuery ToBoostingQuery()
	{
		var query = new BoostingQuery();

		if (TryGetBoost(out var boost))
			query.Boost = boost;

		return query;
	}

	internal override string VariantName => "boosting";
}

// If we retain interfaces, we may need to make variants conform as those can be used elsewhere...

//public interface IAnotherQuery
//{
//	public string Thing { get; set; }
//}

////[JsonConverter(typeof(BoostingQueryDescriptorConverter))]
//public class AnotherQueryDescriptor : VariantDescriptor<BoostingQueryDescriptor>, IAnotherQuery
//{
//	string IAnotherQuery.Thing { get; set; }

//	public BoostingQueryDescriptor Thing(string thing) => Assign(thing, (a, v) => a.Thing = v);

//	internal override string VariantName => "another";
//}

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

		switch (value.Variant)
		{
			case BoolQuery boolQuery:
				JsonSerializer.Serialize(writer, boolQuery, options);
				break;
			case BoostingQuery boostingQuery:
				JsonSerializer.Serialize(writer, boostingQuery, options);
				break;
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

		if (value.TryGetTag(out var tag))
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

#region CombinedTypeExperiment

public class CombinedRequest : ExperimentalRequestBase<ClusterHealthRequestParameters>
{
	public string Name { get; set; }
	public ComplexType Thing { get; set; }

	public CombinedRequest WithName(string name) { Name = name; return this; }

	public CombinedRequest WithName(int name) { Name = name.ToString("D"); return this; }

	public CombinedRequest WithThing(ComplexType thing) { Thing = thing; return this; }

	public CombinedRequest WithThing(Action<ComplexType> configureThing)
	{
		var d = new ComplexType();
		configureThing.Invoke(d);
		Thing = d;
		return this;
	}
}

public class ComplexType
{
	public int? Id { get; set; }

	public string Title { get; set; }

	public ComplexType WithId(int id) { Id = id; return this; }

	public ComplexType WithTitle(string title) { Title = title; return this; }
}

#endregion

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
// - BREAKING: Migration path for those using Func<T,T> descriptor methods and storing parial queries could be painful and potentially very breaking
// - More verbose code (although its 100% generated)
// - Sometimes requires more type-checking and casts, although we could optimise that further if profiling shows an issue
// - Might be some cases not yet considered

// Further work
// - Tagged unions
// - In some serialisation cases we may be able to avoid the casting and serialise the `object` directly
// - Compare to using the runtime type overload (performance)
