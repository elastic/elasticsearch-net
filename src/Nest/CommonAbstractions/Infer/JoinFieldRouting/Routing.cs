using System;
using System.Diagnostics;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(RoutingJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Routing : IEquatable<Routing>, IUrlParameter
	{
		internal object Value { get; set; }
		internal object Document { get; }
		internal Func<object> DocumentGetter { get; }

		internal Routing(Func<object> documentGetter) { DocumentGetter = documentGetter; }
		public Routing(string routing) { Value = routing; }
		public Routing(long routing) { Value = routing; }
		public Routing(object document) { Document = document; }

		public static implicit operator Routing(string routing) => new Routing(routing);
		public static implicit operator Routing(string[] routing) => new Routing(string.Join(",", routing));
		public static implicit operator Routing(long routing) => new Routing(routing);
		public static implicit operator Routing(Guid routing) => new Routing(routing.ToString("D"));

		/// <summary> Use the inferred routing from <paramref name="document"/> </summary>
		public static Routing From<T>(T document) where T : class => new Routing(document);

		private string DebugDisplay => Value?.ToString() ?? "Routing from instance typeof: " + Document?.GetType().Name;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		private string GetString(IConnectionSettingsValues nestSettings)
		{
			if (this.DocumentGetter != null)
			{
				var doc = this.DocumentGetter();
				Value = nestSettings.Inferrer.Routing(doc);
			}
			else if (this.Document != null)
				Value = nestSettings.Inferrer.Routing(this.Document);

			var s = Value as string;
			return s ?? this.Value?.ToString();
		}

		public bool Equals(Routing other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(Value, other.Value) && Equals(Document, other.Document);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == this.GetType() && Equals((Routing)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Value?.GetHashCode() ?? 0) * 397) ^ (Document?.GetHashCode() ?? 0);
			}
		}

		public static bool operator ==(Routing left, Routing right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Routing left, Routing right)
		{
			return !Equals(left, right);
		}
	}
}
