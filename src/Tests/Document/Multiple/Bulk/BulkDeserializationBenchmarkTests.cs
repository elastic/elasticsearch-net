using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;
using System.Text;
using Tests.Framework.Benchmarks;

#if !DOTNETCORE
using System.Buffers;
#endif

namespace Tests.Document.Multiple.Bulk
{
	[BenchmarkConfig]
	public class BulkDeserializationBenchmarkTests
	{
		private static readonly IElasticClient Client = TestClient.GetInMemoryClient();
		private byte[] _tinyResponse;
		private byte[] _mediumResponse;
		private byte[] _largeResponse;
		private byte[] _hugeResponse;
		private JsonSerializer _jsonSerializer;

#pragma warning disable 618
		[Setup]
#pragma warning restore 618
		public void Setup()
		{
			var serializer = Client.RequestResponseSerializer;
			_tinyResponse = serializer.SerializeToBytes(ReturnBulkResponse(1));
			_mediumResponse = serializer.SerializeToBytes(ReturnBulkResponse(100));
			_largeResponse = serializer.SerializeToBytes(ReturnBulkResponse(1000));
			_hugeResponse = serializer.SerializeToBytes(ReturnBulkResponse(100000));

			_jsonSerializer = new JsonSerializer();
		}

		[Benchmark(Description = "deserialize 1 item in bulk response")]
		public BulkResponse TinyResponse()
		{
			using (var ms = new MemoryStream(_tinyResponse))
				return Client.RequestResponseSerializer.Deserialize<BulkResponse>(ms);
		}

		[Benchmark(Description = "deserialize 100 items in bulk response")]
		public BulkResponse MediumResponse()
		{
			using (var ms = new MemoryStream(_mediumResponse))
				return Client.RequestResponseSerializer.Deserialize<BulkResponse>(ms);
		}

		[Benchmark(Description = "deserialize 1,000 items in bulk response")]
		public BulkResponse LargeResponse()
		{
			using (var ms = new MemoryStream(_largeResponse))
				return Client.RequestResponseSerializer.Deserialize<BulkResponse>(ms);
		}

		[Benchmark(Description = "deserialize 100,000 items in bulk response")]
		public BulkResponse HugeResponse()
		{
			using (var ms = new MemoryStream(_hugeResponse))
				return Client.RequestResponseSerializer.Deserialize<BulkResponse>(ms);
		}

		[Benchmark(Description = "deserialize 100,000 items in bulk response")]
		public BulkResponse HugeResponseWithStream()
		{
			using (var ms = new JsonTextReader(new StreamReader(new MemoryStream(_hugeResponse))))
				return _jsonSerializer.Deserialize<BulkResponse>(ms);
		}

		[Benchmark(Description = "deserialize 100,000 items in bulk string response")]
		public BulkResponse HugeResponseWithString()
		{
			using (var reader = new JsonTextReader(new StringReader(Encoding.UTF8.GetString(_hugeResponse))))
				return _jsonSerializer.Deserialize<BulkResponse>(reader);
		}

#if !DOTNETCORE
		[Benchmark(Description = "deserialize 100,000 items in bulk string response with array pool")]
		public BulkResponse HugeResponseWithStringAndArrayPool()
		{
			using (var reader = new JsonTextReader(new StringReader(Encoding.UTF8.GetString(_hugeResponse))))
			{
				reader.ArrayPool = JsonArrayPool.Instance;
				return _jsonSerializer.Deserialize<BulkResponse>(reader);
			}
		}
#endif

		[Benchmark(Description = "Baseline", Baseline = true)]
		public BulkResponse Baseline()
		{
			using (var reader = new JsonTextReader(new StreamReader(new MemoryStream(_hugeResponse))))
			{
				while (reader.Read())
				{
				}

				return new BulkResponse();
			}
		}

		private static object BulkItemResponse() => new
		{
			index = new
			{
				_index = "nest-52cfd7aa",
				_type = "project",
				_id = "Kuhn LLC",
				_version = 1,
				_shards = new
				{
					total = 2,
					successful = 1,
					failed = 0
				},
				created = true,
				status = 201
			}
		};

		private static object ReturnBulkResponse(int numberOfItems)
		{
			return new
			{
				took = 276,
				errors = false,
				items = Enumerable.Range(0, numberOfItems)
					.Select(i => BulkItemResponse())
					.ToArray()
			};
		}

#if !DOTNETCORE
		public class JsonArrayPool : IArrayPool<char>
		{
			public static readonly JsonArrayPool Instance = new JsonArrayPool();

			public char[] Rent(int minimumLength)
			{
				// get char array from System.Buffers shared pool
				return ArrayPool<char>.Shared.Rent(minimumLength);
			}

			public void Return(char[] array)
			{
				// return char array to System.Buffers shared pool
				ArrayPool<char>.Shared.Return(array);
			}
		}
#endif
	}
}
