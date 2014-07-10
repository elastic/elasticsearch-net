using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest.Domain
{
	public interface IFieldSelection<out T>
	{
		/// <summary>
		/// As of elasticsearch fields are always returned as an array. except for internal metadata values such as routing.
		/// </summary>
		/// <typeparam name="K">The type to return the value as, remember that if your field is a string K should be string[]</typeparam>
		K FieldValues<K>(string path);
		
		K[] FieldValues<TBindTo, K>(Expression<Func<TBindTo, object>> objectPath)
			where TBindTo : class;

		IDictionary<string, object> FieldValuesDictionary { get; set; }
	}

	public class FieldSelection<T> : IFieldSelection<T>
	{
		private ElasticInferrer Infer { get; set; }
		public FieldSelection(IConnectionSettingsValues settings, IDictionary<string, object> valuesDictionary = null)
		{
			this.Infer = settings.Inferrer;
			((IFieldSelection<T>)this).FieldValuesDictionary = valuesDictionary;
		}

		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, object> IFieldSelection<T>.FieldValuesDictionary { get; set; }

		/// <summary>
		/// As of elasticsearch fields are always returned as an array. except for internal metadata values such as routing.
		/// </summary>
		/// <typeparam name="K">The type to return the value as, remember that if your field is a string K should be string[]</typeparam>
		public K FieldValues<K>(string path)
		{
			return this.FieldArray<K>(path);
		}

		/// <summary>
		/// As of elasticsearch fields are always returned as an array. 
		/// except for internal metadata values such as routing.
		/// </summary>
		public K[] FieldValues<TBindTo, K>(Expression<Func<TBindTo, object>> objectPath)
			where TBindTo : class
		{
			var path = this.Infer.PropertyPath(objectPath);
			return this.FieldArray<K[]>(path);
		}

		/// <summary>
		/// As of elasticsearch fields are always returned as an array. 
		/// except for internal metadata values such as routing.
		/// </summary>
		public K[] FieldValues<K>(Expression<Func<T, K>> objectPath)
		{
			var path = this.Infer.PropertyPath(objectPath);
			return this.FieldArray<K[]>(path);
		}

		/// <summary>
		/// As of elasticsearch fields are always returned as an array. except for internal metadata values such as routing.
		/// </summary>
		/// <typeparam name="K">The type to return the value as, remember that if your field is a string K should be string[]</typeparam>
		private K FieldArray<K>(string path)
		{
			object o;
			if (((IFieldSelection<T>)this).FieldValuesDictionary.TryGetValue(path, out o))
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
