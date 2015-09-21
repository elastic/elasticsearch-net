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
		public string Value { get; set; }

		public Id(string id) { Value = id; }
		public Id(long id) { Value = id.ToString(); }

		public static implicit operator Id(string id) => new Id(id);
		public static implicit operator Id(long id) => new Id(id);

		public string GetString(IConnectionConfigurationValues settings) => ((IUrlParameter)Ids.Single(this)).GetString(settings);
	}

	public class Ids : IUrlParameter
	{
		internal IEnumerable<Id> _ids;
		internal object _document;

		internal Ids(Id id) { _ids = new List<Id> { id }; }
		internal Ids(object document) { _document = document; }

		public static Ids Single(Id id) => new Ids(id);
		public static Ids Single<T>(T document) where T : class => new Ids(document);
		public static Ids Many(IEnumerable<Id> ids) => new Ids(ids);
		public static Ids Many(params Id[] ids) => new Ids(ids);

		public string GetString(IConnectionConfigurationValues settings)
		{
			if (_document != null)
			{
				var nestSettings = settings as IConnectionSettingsValues;
				if (nestSettings == null)
					throw new Exception("Tried to pass ids on querysting but it could not be resolved because no nest settings are available");
				var infer = new ElasticInferrer(nestSettings);
				return infer.Id(_document);
			}
			return string.Join(",", _ids);
		}
	}
}
