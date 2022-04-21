// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	public sealed partial class ScriptDescriptor : DescriptorBase<ScriptDescriptor>
	{
		internal ScriptDescriptor(Action<ScriptDescriptor> configure) => configure.Invoke(this);

		internal InlineScriptDescriptor InlineScriptDescriptor { get; private set; }

		internal StoredScriptId StoredScriptId { get; private set; }

		/// <summary>
		/// A script that has been stored in Elasticsearch with the specified <paramref name="id"/>.
		/// </summary>
		public ScriptDescriptor Id(string id) => Assign(id, (a, v) => a.StoredScriptId = new StoredScriptId(v));

		/// <summary>
		/// An inline script to execute.
		/// </summary>
		public ScriptDescriptor Source(string script) => Assign(script, (a, v) => a.InlineScriptDescriptor = new InlineScriptDescriptor(v));

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (InlineScriptDescriptor is not null)
			{
				JsonSerializer.Serialize(writer, InlineScriptDescriptor, options);
				return;
			}

			if (StoredScriptId is not null)
			{
				JsonSerializer.Serialize(writer, StoredScriptId, options);
				return;
			}
		}
	}
}
