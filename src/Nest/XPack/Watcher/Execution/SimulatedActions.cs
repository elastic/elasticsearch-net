// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(SimulatedActionsFormatter))]
	public class SimulatedActions
	{
		private SimulatedActions() { }

		public IEnumerable<string> Actions { get; private set; }

		public static SimulatedActions All => new SimulatedActions { UseAll = true };
		public bool UseAll { get; private set; }

		public static SimulatedActions Some(params string[] actions) => new SimulatedActions { Actions = actions };

		public static SimulatedActions Some(IEnumerable<string> actions) => new SimulatedActions { Actions = actions };
	}

	internal class SimulatedActionsFormatter : IJsonFormatter<SimulatedActions>
	{
		public SimulatedActions Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
				return SimulatedActions.All;

			var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
			return SimulatedActions.Some(formatter.Deserialize(ref reader, formatterResolver));
		}

		public void Serialize(ref JsonWriter writer, SimulatedActions value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) return;

			if (value.UseAll) writer.WriteString("_all");
			else
			{
				var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
				formatter.Serialize(ref writer, value.Actions, formatterResolver);
			}
		}
	}
}
