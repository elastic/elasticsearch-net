/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elasticsearch.Net.Utf8Json;

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
