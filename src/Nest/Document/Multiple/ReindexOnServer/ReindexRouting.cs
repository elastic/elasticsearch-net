// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ReindexRoutingFormatter))]
	public class ReindexRouting
	{
		public static ReindexRouting Discard = new ReindexRouting("discard", true);
		public static ReindexRouting Keep = new ReindexRouting("keep", true);
		private readonly string _newRoutingValue;

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
