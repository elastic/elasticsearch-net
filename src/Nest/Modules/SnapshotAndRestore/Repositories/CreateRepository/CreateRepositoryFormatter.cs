using System;
using Elasticsearch.Net;

namespace Nest
{
	internal class CreateRepositoryFormatter : IJsonFormatter<ICreateRepositoryRequest>
	{
		public ICreateRepositoryRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, ICreateRepositoryRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Repository == null)
			{
				writer.WriteBeginObject();
				writer.WriteEndObject();
				return;
			}

			switch (value.Repository.Type)
			{
				case "s3":
					Serialize<IS3Repository>(ref writer, value.Repository, formatterResolver);
					break;
				case "azure":
					Serialize<IAzureRepository>(ref writer, value.Repository, formatterResolver);
					break;
				case "url":
					Serialize<IReadOnlyUrlRepository>(ref writer, value.Repository, formatterResolver);
					break;
				case "hdfs":
					Serialize<IHdfsRepository>(ref writer, value.Repository, formatterResolver);
					break;
				case "fs":
					Serialize<IFileSystemRepository>(ref writer, value.Repository, formatterResolver);
					break;
				case "source":
					Serialize<ISourceOnlyRepository>(ref writer, value.Repository, formatterResolver);
					break;
				default:
					Serialize<ISnapshotRepository>(ref writer, value.Repository, formatterResolver);
					break;
			}
		}

		private static void Serialize<TRepository>(ref JsonWriter writer, ISnapshotRepository value, IJsonFormatterResolver formatterResolver)
			where TRepository : class, ISnapshotRepository
		{
			var formatter = formatterResolver.GetFormatter<TRepository>();
			formatter.Serialize(ref writer, value as TRepository, formatterResolver);
		}
	}
}
