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
		public Id(string id) => Value = id;

		public Id(long id) => Value = id;

		public Id(object document) => Document = document;

		internal object Document { get; set; }
		internal object Value { get; set; }

		private string DebugDisplay => Value?.ToString() ?? "Id from instance typeof: " + Document?.GetType().Name;

		public bool Equals(Id other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return Equals(Value, other.Value) && Equals(Document, other.Document);
		}

		public string GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		public static implicit operator Id(string id) => new Id(id);

		public static implicit operator Id(long id) => new Id(id);

		public static implicit operator Id(Guid id) => new Id(id.ToString("D"));

		public static Id From<T>(T document) where T : class => new Id(document);

		internal string GetString(IConnectionSettingsValues nestSettings)
		{
			if (Document != null) Value = nestSettings.Inferrer.Id(Document);

			var s = Value as string;
			return s ?? Value?.ToString();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return obj.GetType() == GetType() && Equals((Id)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Value?.GetHashCode() ?? 0) * 397) ^ (Document?.GetHashCode() ?? 0);
			}
		}

		public static bool operator ==(Id left, Id right) => Equals(left, right);

		public static bool operator !=(Id left, Id right) => !Equals(left, right);
	}
}
