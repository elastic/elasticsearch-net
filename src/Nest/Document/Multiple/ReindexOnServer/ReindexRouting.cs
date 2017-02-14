using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReindexRoutingJsonConverter))]
	public class ReindexRouting
	{
		private readonly string _newRoutingValue;
		public static ReindexRouting Keep = new ReindexRouting("keep", true);
		public static ReindexRouting Discard = new ReindexRouting("discard", true);

		/// <summary>
		/// Use ReindexRouting.Keep or ReindexRouting.Discard if you want to sent "keep" or "discard", this
		/// constructor always sends newRoutingValue prefixed with '='
		/// </summary>
		public ReindexRouting(string newRoutingValue) : this(newRoutingValue, false) { }

		private ReindexRouting(string newRoutingValue, bool noPrefix)
		{
			var routing = newRoutingValue.TrimStart('=');
			var prefix = noPrefix ? "" : "=";
			_newRoutingValue = $"{prefix}{routing}";
		}

		public static implicit operator ReindexRouting(string routing) => new ReindexRouting(routing);

		public override string ToString() => _newRoutingValue;
	}
}
