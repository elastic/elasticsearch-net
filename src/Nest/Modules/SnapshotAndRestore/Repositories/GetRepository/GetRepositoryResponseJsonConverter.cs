using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class GetRepositoryResponseFormatter : IJsonFormatter<IGetRepositoryResponse>
	{
		private TRepository GetRepository<TRepository, TSettings>(ArraySegment<byte> settings, IJsonFormatterResolver formatterResolver)
			where TRepository : ISnapshotRepository
			where TSettings : IRepositorySettings
		{
			if (settings == default)
				return (TRepository)typeof(TRepository).CreateInstance();

			var formatter = formatterResolver.GetFormatter<TSettings>();

			var reader = new JsonReader(settings.Array, settings.Offset);
			var resolvedSettings = formatter.Deserialize(ref reader, formatterResolver);

			return (TRepository)typeof(TRepository).CreateInstance(resolvedSettings);
		}

		public void Serialize(ref JsonWriter writer, IGetRepositoryResponse value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IGetRepositoryResponse>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		public IGetRepositoryResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var response = new GetRepositoryResponse();

			var segment = DictionaryResponseFormatterHelpers
				.ReadServerErrorFirst(ref reader, formatterResolver, out var error, out var statusCode);

			response.Error = error;
			response.StatusCode = statusCode;

			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			var count = 0;
			var d = new Dictionary<string, ISnapshotRepository>();

			while (segmentReader.ReadIsInObject(ref count))
			{
				var name = segmentReader.ReadPropertyName();
				if (name == "error" || name == "status")
					continue;

				var snapshotSegment = reader.ReadNextBlockSegment();
				var snapshotSegmentReader = new JsonReader(snapshotSegment.Array, snapshotSegment.Offset);
				var segmentCount = 0;

				string repositoryType = null;
				ArraySegment<byte> settings;

				while (snapshotSegmentReader.ReadIsInObject(ref segmentCount))
				{
					var propertyName = snapshotSegmentReader.ReadPropertyName();
					switch (propertyName)
					{
						case "type":
							repositoryType = snapshotSegmentReader.ReadString();
							break;
						case "settings":
							settings = snapshotSegmentReader.ReadNextBlockSegment();
							break;
					}
				}

				switch (repositoryType)
				{
					case "fs":
						var fs = GetRepository<FileSystemRepository, FileSystemRepositorySettings>(settings, formatterResolver);
						d.Add(name, fs);
						break;
					case "url":
						var url = GetRepository<ReadOnlyUrlRepository, ReadOnlyUrlRepositorySettings>(settings, formatterResolver);
						d.Add(name, url);
						break;
					case "azure":
						var azure = GetRepository<AzureRepository, AzureRepositorySettings>(settings, formatterResolver);
						d.Add(name, azure);
						break;
					case "s3":
						var s3 = GetRepository<S3Repository, S3RepositorySettings>(settings, formatterResolver);
						d.Add(name, s3);
						break;
					case "hdfs":
						var hdfs = GetRepository<HdfsRepository, HdfsRepositorySettings>(settings, formatterResolver);
						d.Add(name, hdfs);
						break;
				}
			}

			response.Repositories = d;
			return response;
		}
	}
}
