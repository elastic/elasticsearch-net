// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	public class HasPrivilegesResponse : ResponseBase
	{
		[DataMember(Name = "application")]
		[JsonFormatter(typeof(ApplicationsPrivilegesFormatter))]
		public IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>> Applications { get; internal set; } =
			EmptyReadOnly<string, IReadOnlyCollection<ResourcePrivileges>>.Dictionary;

		[DataMember(Name = "cluster")]
		public IReadOnlyDictionary<string, bool> Clusters { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;

		[DataMember(Name = "has_all_requested")]
		public bool HasAllRequested { get; internal set; }

		[DataMember(Name = "index")]
		[JsonFormatter(typeof(IndicesPrivilegesFormatter))]
		public IReadOnlyCollection<ResourcePrivileges> Indices { get; internal set; } = EmptyReadOnly<ResourcePrivileges>.Collection;

		[DataMember(Name = "username")]
		public string Username { get; internal set; }
	}

	public class ResourcePrivileges
	{
		public IReadOnlyDictionary<string, bool> Privileges { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;
		public string Resource { get; internal set; }
	}

	internal class IndicesPrivilegesFormatter : IJsonFormatter<IReadOnlyCollection<ResourcePrivileges>>
	{
		public IReadOnlyCollection<ResourcePrivileges> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			ReadResourcePrivileges(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, IReadOnlyCollection<ResourcePrivileges> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var count = 0;
			var formatter = formatterResolver.GetFormatter<IReadOnlyDictionary<string, bool>>();
			foreach (var privilege in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName(privilege.Resource);
				formatter.Serialize(ref writer, privilege.Privileges, formatterResolver);

				count++;
			}
			writer.WriteEndObject();
		}

		internal static IReadOnlyCollection<ResourcePrivileges> ReadResourcePrivileges(ref JsonReader reader, IJsonFormatterResolver formatterResolver
		)
		{
			if (reader.ReadIsNull())
				return null;

			var privileges = new List<ResourcePrivileges>();
			var count = 0;
			var formatter = formatterResolver.GetFormatter<IReadOnlyDictionary<string, bool>>();

			while (reader.ReadIsInObject(ref count))
			{
				var resource = reader.ReadPropertyName();
				var resourcePrivileges = formatter.Deserialize(ref reader, formatterResolver);
				privileges.Add(new ResourcePrivileges { Resource = resource, Privileges = resourcePrivileges });
			}

			return privileges;
		}
	}

	internal class ApplicationsPrivilegesFormatter : IJsonFormatter<IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>>>
	{
		private static readonly IndicesPrivilegesFormatter Formatter = new IndicesPrivilegesFormatter();

		public IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>> Deserialize(ref JsonReader reader,
			IJsonFormatterResolver formatterResolver
		)
		{
			if (reader.ReadIsNull())
				return null;

			var apps = new Dictionary<string, IReadOnlyCollection<ResourcePrivileges>>();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var app = reader.ReadPropertyName();
				var privileges = Formatter.Deserialize(ref reader, formatterResolver);
				apps.Add(app, privileges);
			}

			return apps;
		}

		public void Serialize(ref JsonWriter writer, IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>> value,
			IJsonFormatterResolver formatterResolver
		)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var count = 0;
			foreach (var privilege in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName(privilege.Key);
				Formatter.Serialize(ref writer, privilege.Value, formatterResolver);
				count++;
			}
			writer.WriteEndObject();
		}
	}
}
