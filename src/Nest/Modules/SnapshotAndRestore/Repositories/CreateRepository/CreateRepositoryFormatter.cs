using System;
using Utf8Json;

namespace Nest
{
	internal class CreateRepositoryFormatter : IJsonFormatter<ICreateRepositoryRequest>
	{
		public ICreateRepositoryRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, ICreateRepositoryRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var formatter = formatterResolver.GetFormatter<ISnapshotRepository>();
			formatter.Serialize(ref writer, value.Repository, formatterResolver);
		}
	}
}
