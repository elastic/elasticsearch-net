using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest.Domain
{
	public interface IFieldSelection<out T>
	{
		
	}

	public class FieldSelection<T> : IFieldSelection<T>
	{
		private ElasticInferrer Infer { get; set; }
		public FieldSelection(IConnectionSettings settings)
		{
			this.Infer = new ElasticInferrer(settings);
		}


		internal FieldSelection(IDictionary<string, object> values)
		{
			this.FieldValues = values;
		}
		
		//TODO REMOVE ?
		public T Document { get; set; }

		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<string, object> FieldValues { get; set; }

		public K FieldValue<K>(Expression<Func<T, K>> objectPath)
		{
			var path = this.Infer.PropertyPath(objectPath);
			return this.FieldValue<K>(path);
		}
		public K FieldValue<K>(Expression<Func<T, object>> objectPath)
		{
			var path = this.Infer.PropertyPath(objectPath);
			return this.FieldValue<K>(path);
		}

		public K FieldValue<K>(string path)
		{
			object o;
			if (FieldValues.TryGetValue(path, out o))
			{
				var t = typeof(K);
				if (o is JArray && t.GetInterfaces().Contains(typeof(IEnumerable)))
				{
					var array = (JArray)o;
					return array.ToObject<K>();
				}
				return (K)Convert.ChangeType(o, typeof(K));
			}
			return default(K);
		}
	}
}
