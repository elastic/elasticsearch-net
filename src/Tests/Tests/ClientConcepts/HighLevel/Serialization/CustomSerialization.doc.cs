using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Tests.Framework;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	/**[[custom-serialization]]
	 * === Custom Serialization
	 *
	 * NEST 6.x ships with a _shaded_ Json.NET dependency, meaning that all of Json.NET's types are
	 * internalized and IL merged into the NEST assembly, and their namespace has been changed
	 * from `Newtonsoft.Json` to `Nest.Json`.
	 *
	 * Why would we do this, you may ask? Well, NEST has always isolated its dependency on Json.NET as best as it could,
	 * but this meant that we had to mandate how certain things were in the client. For instance,
	 * NEST heavily relied on the fact that the `IContractResolver` used by the configured serializer was
	 * an instance of `ElasticContractResolver`, so if you wanted to deserialize your `_source` or `_fields`
	 * using your own resolver, you were out of luck. In addition, continued improvements to NEST's serialization pipeline
	 * was stymied by this dependency and as client authors, we wanted to unhinder ourselves from this in order to explore the myriad
	 * of exciting developments happening in the .NET ecosystem around performance with the likes of `Span<T>`,
	 * `ArrayPool<T>` and a more compact UTF-8 string representation.
	 *
	 * So what did we do in 6.x and how does it affect you?
	 *
	 * The `NEST` nuget package from 6.0.0 onwards will use the internal Json.NET serializer and will in effect, behave the same
	 * as it did in previous releases. If you previously relied on custom Json.NET serialization and configuration with custom
	 * `JsonSerializerSettings` and `JsonConverter` types for example, things have changed a bit. The following section explains
	 * how to continue working with these with NEST 6.x.
	 */
	public class GettingStarted
	{
		/**[float]
		 * ==== Injecting a new serializer
		 *
		 * Starting with NEST 6.x you can inject a serializer that is isolated to only be called
		 * for the (de)serialization of `_source`, `_fields`, or wherever a user provided value is expected
		 * to be written and returned.
		 *
		 * Within NEST, we refer to this serializer as the `SourceSerializer`.
		 *
		 * Another serializer also exists within NEST known as the `RequestResponseSerializer`. This serializer is internal
		 * and is responsible for serializing the request and response types that are part of NEST.
		 *
		 * If `SourceSerializer` is left unconfigured, the internal `RequestResponseSerializer` is the `SourceSerializer` as well.
		 *
		 * Implementing `IElasticsearchSerializer` is technically enough to inject your own `SourceSerializer`
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

		/**
		 * Hooking up the serializer is performed in the `ConnectionSettings` constructor
		 */
		public void TheNewContract()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(
				pool,
				sourceSerializer: (builtin, settings) => new VanillaSerializer()); // <1> what the Func?
			var client = new ElasticClient(connectionSettings);
		}

		/**
		 * If implementing `IElasticsearchSerializer` is enough, why do we need to provide an instance wrapped in a factory `Func`?
		 *
		 * There are various cases where you might have a POCO type that contains a NEST type as one of its properties. For example,
		 * consider if you want to use percolation; you need to store Elasticsearch queries as part of the `_source` of your document,
		 * which means you need to have a POCO that looks something like this
		 */
		public class MyPercolationDocument
		{
			public QueryContainer Query { get; set; }
			public string Category { get; set; }
		}
		/**
		 * A custom serializer would not know how to serialize `QueryContainer` or other NEST types that could appear as part of
		 * the `_source` of a document, therefore a custom serializer needs to have a way to delegate serialization of NEST types
		 * back to NEST's built-in serializer.
		 */

		/**
		 * ==== JsonNetSerializer
		 *
		 * We ship a separate {nuget}/NEST.JsonNetSerializer[NEST.JsonNetSerializer] package that helps in composing a custom `SourceSerializer`
		 * using `Json.NET`, that is smart enough to delegate the serialization of known NEST types back to the built-in
		 * `RequestResponseSerializer`. This package is also useful if you want to control how your documents and values are stored
		 * and retrieved from Elasticsearch using `Json.NET`, without interfering with the way NEST uses `Json.NET` internally.
		 *
		 * The easiest way to hook this custom source serializer up is as follows
		 */
		public void DefaultJsonNetSerializer()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings =
				new ConnectionSettings(pool, sourceSerializer: JsonNetSerializer.Default);
			var client = new ElasticClient(connectionSettings);
		}
		/**
		 * `JsonNetSerializer.Default` is just syntactic sugar for passing a delegate like
		 */

		public void DefaultJsonNetSerializerUnsugared()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(
				pool,
				sourceSerializer: (builtin, settings) => new JsonNetSerializer(builtin, settings));
			var client = new ElasticClient(connectionSettings);
		}
		/**
		 * `JsonNetSerializer`'s constructor takes several methods that allow you to control the `JsonSerializerSettings` and modify
		 * the contract resolver from `Json.NET`.
		 */

		public void DefaultJsonNetSerializerFactoryMethods()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings =
				new ConnectionSettings(pool, sourceSerializer: (builtin, settings) => new JsonNetSerializer(
					builtin, settings,
					() => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include },
					resolver => resolver.NamingStrategy = new SnakeCaseNamingStrategy()
				));
			var client = new ElasticClient(connectionSettings);
		}

		/**
		 * ==== Derived serializers
		 *
		 * If you'd like to be more explicit, you can also derive from `ConnectionSettingsAwareSerializerBase`
		 * and override the `CreateJsonSerializerSettings` and `ModifyContractResolver` methods
		 */
		public class MyFirstCustomJsonNetSerializer : ConnectionSettingsAwareSerializerBase
		{
			public MyFirstCustomJsonNetSerializer(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
				: base(builtinSerializer, connectionSettings) { }

			protected override JsonSerializerSettings CreateJsonSerializerSettings() =>
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Include
				};

			protected override void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) =>
				resolver.NamingStrategy = new SnakeCaseNamingStrategy();
		}
		/**
		 * Using `MyCustomJsonNetSerializer`, we can serialize using
		 *
		 * - a Json.NET `NamingStrategy` that snake cases property names
		 * - `JsonSerializerSettings` that includes `null` properties
		 *
		 * without affecting how NEST's own types are serialized. Furthermore, because this serializer is aware of
		 * the built-in serializer, we can automatically inject a `JsonConverter` to handle
		 * known NEST types that could appear as part of the source, such as the aformentioned `QueryContainer`.
		 *
		 * Let's demonstrate with an example document type
		 */
		public class MyDocument
		{
			public int Id { get; set; }

			public string Name { get; set; }

			public string FilePath { get; set; }

			public int OwnerId { get; set; }

			public IEnumerable<MySubDocument> SubDocuments { get; set; }
		}

		public class MySubDocument
		{
			public string Name { get; set; }
		}

		/**
		 * Hooking up the serializer and using it is as follows
		 */
		[U] public void UsingJsonNetSerializer()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(
				pool,
				connection: new InMemoryConnection(), // <1> an _in-memory_ connection is used here for example purposes. In your production application, you would use an `IConnection` implementation that actually sends a request.
				sourceSerializer: (builtin, settings) => new MyFirstCustomJsonNetSerializer(builtin, settings))
				.DefaultIndex("my-index");

			//hide
			connectionSettings.DisableDirectStreaming();

			var client = new ElasticClient(connectionSettings);

			/** Now, if we index an instance of our document type */
			var document = new MyDocument
			{
				Id = 1,
				Name = "My first document",
				OwnerId = 2
			};

			var ndexResponse = client.IndexDocument(document);

			/** it serializes to */
			//json
			var expected = new
			{
				id = 1,
				name = "My first document",
				file_path = (string) null,
				owner_id = 2,
				sub_documents = (object) null
			};
			/**
			 * which adheres to the conventions of our configured `MyCustomJsonNetSerializer` serializer.
			 */

			// hide
			Expect(expected, preserveNullInExpected: true).FromRequest(ndexResponse);
		}

		/** ==== Serializing Type Information
		 * Here's another example that implements a custom contract resolver. The custom contract resolver
		 * will include the type name within the serialized JSON for the document, which can be useful when
		 * returning covariant document types within a collection.
		 */
		public class MySecondCustomContractResolver : ConnectionSettingsAwareContractResolver
		{
			public MySecondCustomContractResolver(IConnectionSettingsValues connectionSettings)
				: base(connectionSettings) { }

			protected override JsonContract CreateContract(Type objectType)
			{
				var contract = base.CreateContract(objectType);
				if (contract is JsonContainerContract containerContract)
				{
					if (containerContract.ItemTypeNameHandling == null)
						containerContract.ItemTypeNameHandling = TypeNameHandling.None;
				}

				return contract;
			}
		}

		public class MySecondCustomJsonNetSerializer : ConnectionSettingsAwareSerializerBase
		{
			public MySecondCustomJsonNetSerializer(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
				: base(builtinSerializer, connectionSettings) { }

			protected override JsonSerializerSettings CreateJsonSerializerSettings() =>
				new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.All,
					NullValueHandling = NullValueHandling.Ignore,
					TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
				};

			protected override ConnectionSettingsAwareContractResolver CreateContractResolver() =>
				new MySecondCustomContractResolver(ConnectionSettings); // <1> override the contract resolver
		}

		/**
		 * Now, hooking up this serializer
         */
		[U] public void MySecondJsonNetSerializer()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(
					pool,
					connection: new InMemoryConnection(),
					sourceSerializer: (builtin, settings) => new MySecondCustomJsonNetSerializer(builtin, settings))
				.DefaultIndex("my-index");

			//hide
			connectionSettings.DisableDirectStreaming();

			var client = new ElasticClient(connectionSettings);

			/** and indexing an instance of our document type */
			var document = new MyDocument
			{
				Id = 1,
				Name = "My first document",
				OwnerId = 2,
				SubDocuments = new []
				{
					new MySubDocument { Name = "my first sub document" },
					new MySubDocument { Name = "my second sub document" },
				}
			};

			var ndexResponse = client.IndexDocument(document);

			/** serializes to */
			//json
			var expected = new JObject
			{
				{ "$type", "Tests.ClientConcepts.HighLevel.Serialization.GettingStarted+MyDocument, Tests" },
				{ "id", 1 },
				{ "name", "My first document" },
				{ "ownerId", 2 },
				{ "subDocuments", new JArray
					{
						new JObject { { "name", "my first sub document" } },
						new JObject { { "name", "my second sub document" } },
					}
				}
			};
			/**
			 * the type information is serialized for the outer `MyDocument` instance, but not for each
			 * `MySubDocument` instance in the `SubDocuments` collection.
			 *
			 * When implementing a custom contract resolver derived from `ConnectionSettingsAwareContractResolver`,
			 * be careful not to change the behaviour of the resolver for NEST types; doing so will result in
			 * unexpected behaviour.
			 *
			 * [WARNING]
			 * --
			 * Per the https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_TypeNameHandling.htm[Json.NET documentation on TypeNameHandling],
			 * it should be used with caution when your application deserializes JSON from an external source.
			 * --
			 */

			// hide
			Expect(expected).FromRequest(ndexResponse);
		}
	}
}
