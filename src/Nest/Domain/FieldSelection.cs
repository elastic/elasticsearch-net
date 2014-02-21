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
		/// <summary>
		/// As of elasticsearch fields are always returned as an array. except for internal metadata values such as routing.
		/// </summary>
		/// <typeparam name="K">The type to return the value as, remember that if your field is a string K should be string[]</typeparam>
		K FieldValue<K>(string path);

	}

	public class FieldSelection<T> : IFieldSelection<T>
	{
		private ElasticInferrer Infer { get; set; }
		public FieldSelection(IConnectionSettingsValues settings)
		{
			this.Infer = new ElasticInferrer(settings);
		}


		internal FieldSelection(ElasticInferrer inferrer, IDictionary<string, object> values)
		{
			this.Infer = inferrer;
			this.FieldValues = values;
		}
		
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<string, object> FieldValues { get; set; }

		/// <summary>
		/// As of elasticsearch fields are always returned as an array. except for internal metadata values such as routing.
		/// </summary>
		public K[] FieldValue<K>(Expression<Func<T, K>> objectPath)
		{
			var path = this.Infer.PropertyPath(objectPath);
			return this.FieldValue<K[]>(path);
		}

		/// <summary>
		/// As of elasticsearch fields are always returned as an array. except for internal metadata values such as routing.
		/// </summary>
		public K[] FieldValue<K>(Expression<Func<T, object>> objectPath)
		{
			var path = this.Infer.PropertyPath(objectPath);
			return this.FieldValue<K[]>(path);
		}

		/// <summary>
		/// As of elasticsearch fields are always returned as an array. except for internal metadata values such as routing.
		/// </summary>
		/// <typeparam name="K">The type to return the value as, remember that if your field is a string K should be string[]</typeparam>
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
