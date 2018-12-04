using System.Collections.Generic;
using Utf8Json;

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
