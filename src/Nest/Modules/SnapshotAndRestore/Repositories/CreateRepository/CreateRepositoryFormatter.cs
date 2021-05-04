// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest.Utf8Json;

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
