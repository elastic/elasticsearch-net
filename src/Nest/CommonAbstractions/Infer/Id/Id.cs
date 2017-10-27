using System;
using System.Diagnostics;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IdJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Id : IEquatable<Id>, IUrlParameter
	{
		internal object Value { get; set; }
		internal object Document { get; set; }

		public Id(string id) { Value = id; }
		public Id(long id) { Value = id; }
		public Id(object document) { Document = document; }

		public static implicit operator Id(string id) => new Id(id);
		public static implicit operator Id(long id) => new Id(id);
		public static implicit operator Id(Guid id) => new Id(id.ToString("D"));

		public static Id From<T>(T document) where T : class => new Id(document);

		private string DebugDisplay => Value?.ToString() ?? "Id from instance typeof: " + Document?.GetType().Name;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		private string GetString(IConnectionSettingsValues nestSettings)
		{
			if (this.Document != null)
			{
				Value = nestSettings.Inferrer.Id(this.Document);
			}

			var s = Value as string;
			return s ?? this.Value?.ToString();
		}

		public bool Equals(Id other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(Value, other.Value) && Equals(Document, other.Document);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == this.GetType() && Equals((Id)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Value?.GetHashCode() ?? 0) * 397) ^ (Document?.GetHashCode() ?? 0);
			}
		}

		public static bool operator ==(Id left, Id right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Id left, Id right)
		{
			return !Equals(left, right);
		}
	}
}
