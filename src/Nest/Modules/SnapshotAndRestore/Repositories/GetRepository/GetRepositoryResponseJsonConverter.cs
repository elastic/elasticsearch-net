using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class GetRepositoryResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new GetRepositoryResponse();
			var repositories = JObject.Load(reader).Properties()
				.Where(p => p.Name != "error" && p.Name != "status")
				.ToDictionary(p => p.Name, p => p.Value);
			if (!repositories.HasAny())
				return response;
			response.Repositories = new Dictionary<string, ISnapshotRepository>();
			foreach (var kv in repositories)
			{
				var repository = JObject.FromObject(kv.Value);
				var type = repository.Properties().Where(p => p.Name == "type").SingleOrDefault();
				if (type == null) continue;
				var typeName = type.Value.ToString();
				var settings = GetSetingsJObject(repository);
				if (typeName == "fs")
				{
					var fs = GetRepository<FileSystemRepository, FileSystemRepositorySettings>(settings);
					response.Repositories.Add(kv.Key, fs);
				}
				else if (typeName == "url")
				{
					var url = GetRepository<ReadOnlyUrlRepository, ReadOnlyUrlRepositorySettings>(settings);
					response.Repositories.Add(kv.Key, url);
				}
				else if (typeName == "azure")
				{
					var azure = GetRepository<AzureRepository, AzureRepositorySettings>(settings);
					response.Repositories.Add(kv.Key, azure);
				}
				else if (typeName == "s3")
				{
					var s3 = GetRepository<S3Repository, S3RepositorySettings>(settings);
					response.Repositories.Add(kv.Key, s3);
				}
				else if (typeName == "hdfs")
				{
					var hdfs = GetRepository<HdfsRepository, HdfsRepositorySettings>(settings);
					response.Repositories.Add(kv.Key, hdfs);
				}
			}
			return response;
		}

		private TRepository GetRepository<TRepository, TSettings>(JObject settings)
			where TRepository : ISnapshotRepository
			where TSettings : IRepositorySettings
		{
			if (settings == null)
				return (TRepository)typeof(TRepository).CreateInstance();
			return (TRepository)typeof(TRepository).CreateInstance(settings.ToObject<TSettings>());
		}

		private JObject GetSetingsJObject(JObject repository)
		{
			var settings = JObject.FromObject(repository).Properties()
				.Where(p => p.Name == "settings")
				.SingleOrDefault();
			if (settings == null) return null;
			return JObject.FromObject(settings.Value);
		}
	}
}

