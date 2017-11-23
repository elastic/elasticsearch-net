using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	/**[[custom-serialization]]
     * == Custom Serialization
	 *
	 * Starting with 6.0 NEST ships with a shaded Json.NET dependency. Meaning we merged it into Nest's dll
	 * internalized all Json.NET's types and changed their namespace from `Newtonsoft.Json` to `Nest.Json`.
	 *
	 * NEST has always isolated Json.NET as best as it could but this meant that we had to mandate some things.
	 * For instance NEST heavily relied on the fact that the `ContractConverter` was an instance of `ElasticContractConverter`
	 *
	 * If you wanted to deserialize your `_source` or `_fields` using your own `ContractConverter` you were out of luck.
	 *
	 * So what did we do in 6.x and how does it affect you?
	 *
	 * The `NEST` nuget package from 6.0.0 onwards on its own will use the internal Json.NET serializer and will in affect behave the same
	 * as it did in previous releases.
	 *
	 * If you previously configured a custom Json.NET serializer with custom `JsonSerializerSettings`, `ContractConverter` things
	 * will change a bit, but for the better!
	 *
     *
	 */
	public class GettingStarted
	{
		/**[float]
		 * === Injecting a new serializer
		 *
		 * Starting with NEST 6.x you can inject a serializer that is isolated to only be called
		 * for the (de)serialization of `_source` `_fields` and where ever a user provided value is expected
		 * to be written and returned.
		 *
		 * Internally we call this the `RequestResponseSerializer` and the `SourceSerializer`
		 *
		 * If left unconfigured the internal `RequestResponseSerializer` is the `SourceSerializer` as well.
		 *
		 * Implementing `IElasticsearchSerializer` is technically enough to inject your own `SourceSerialzier`
		 */

		public class VanillaSerializer : IElasticsearchSerializer
		{
			public T Deserialize<T>(Stream stream) => throw new NotImplementedException();

			public object Deserialize(Type type, Stream stream) => throw new NotImplementedException();

			public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken)) =>
				throw new NotImplementedException();

			public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken)) =>
				throw new NotImplementedException();

			public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented) =>
				throw new NotImplementedException();

			public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
				CancellationToken cancellationToken = default(CancellationToken)) =>
				throw new NotImplementedException();
		}

		public void TheNewContract()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings =
				new ConnectionSettings(pool, sourceSerializer: (settings, builtin) => new VanillaSerializer()); // <1> what the Func?
			var client = new ElasticClient(connectionSettings);
		}

		/**
		 * If implementing `IElasticsearchSerializer` is enough why do we need to provide its instance wrapped in a factory `Func`?
		 *
		 * There are various cases where you need to provide a `_source` with `Nest` data type as part of that `_source`.
		 *
		 * An example if you want to use percolation you need to store queries on your document which means you need to have something like
		 * this:
		 */

		public class MyPercolationDocument
		{
			public QueryContainer Query { get; set; }
			public string Category { get; set; }
		}

		/**
		 * Your `SourceSerializer` would not know how to serialize `QueryContainer`. Therefor we ship a separate `NEST.JsonNetSerializer`
		 * package that helps in composing a custom `SourceSerializer` using `Json.NET` that is smart enough to hand back the
		 * (de)serialization of known NEST types back to the builtin `RequestResponseSerializer`
		 */

		public class MyCustomJsonNetSerializer : JsonNetSourceSerializerBase
		{
			public MyCustomJsonNetSerializer(IElasticsearchSerializer builtinSerializer) : base(builtinSerializer) { }

			protected override IEnumerable<JsonConverter> CreateJsonConverters() => Enumerable.Empty<JsonConverter>();

			protected override JsonSerializerSettings CreateJsonSerializerSettings() => new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Include
			};

			protected override IContractResolver CreateContractResolver() => new DefaultContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			};
		}

		public void UsingJsonNetSerializer()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings =
				new ConnectionSettings(pool, sourceSerializer: (settings, builtin) => new MyCustomJsonNetSerializer(builtin)); // <1> what the Func?
			var client = new ElasticClient(connectionSettings);
		}

		/**
		 * Using this `MyCustomJsonNetSerializer` we can (de)serialize using a `NamingStrategy` that snake cases and `JsonSerializerSettings`
		 * that include null properties, without affecting how NEST's own types are serialized.
		 *
		 * Furthermore because this serializer is aware of the builtin serializer we can automatically inject a `JsonConverter` to handle
		 * known NEST types that could appear as part of the source such as the afformentioned `QueryContainer`.
		 *
		 * The final remaining question might be why the reference to `settings` if `MyCustomJsonNetSerializer` does not need it?
		 *
		 * This is to future proof the contract to hopefully isolate the cases of NEST types even further so that e.g a JIL based
		 * source serializer can simply call `QueryContainer.Serialize(stream, settings)`.
		 */
	}
}
