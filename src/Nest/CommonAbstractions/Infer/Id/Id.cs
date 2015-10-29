using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IdJsonConverter))]
	public class Id : IUrlParameter
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

		public string GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		internal string GetString(IConnectionSettingsValues nestSettings)
		{
			if (this.Document != null)
			{
				return nestSettings.Inferrer.Id(this.Document);
			}

			var s = Value as string;
			return s ?? this.Value?.ToString();
		}
	}

	//TODO do we need this? discus with @gmarz
	public class Ids : IUrlParameter
	{
		internal IEnumerable<Id> _ids;

		internal Ids(Id id) { _ids = new List<Id> { id }; }
		internal Ids(IEnumerable<Id> ids) { _ids = ids; }

		public static Ids Single(Id id) => new Ids(id);
		public static Ids Single<T>(T document) where T : class => new Ids(Id.From(document));
		public static Ids Many(IEnumerable<Id> ids) => new Ids(ids);
		public static Ids Many(params Id[] ids) => new Ids(ids);

		public string GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass ids on querystring but it could not be resolved because no nest settings are available");

			var ids = _ids
				.Select(i=>i.GetString(nestSettings))
				.ToList();
			if (ids.Any(id => id.IsNullOrEmpty())) throw new ArgumentException("One or more ids were null or empty", "ids");
			return string.Join(",", ids);
		}
	}
}
