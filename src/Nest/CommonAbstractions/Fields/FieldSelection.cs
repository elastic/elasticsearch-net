using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public interface IFieldSelection<out T>
	{
		K[] FieldValues<K>(string path);

		K FieldValue<K>(string path);

		K[] FieldValues<TBindTo, K>(Expression<Func<TBindTo, object>> objectPath)
			where TBindTo : class;

		K FieldValue<TBindTo, K>(Expression<Func<TBindTo, object>> objectPath)
			where TBindTo : class;

		IDictionary<string, object> FieldValuesDictionary { get; set; }
	}

	public class FieldSelection<T> : IFieldSelection<T>
	{
		private IFieldSelection<T> Self => this;

		private ElasticInferrer Infer { get; set; }

		IDictionary<string, object> IFieldSelection<T>.FieldValuesDictionary { get; set; }

		public FieldSelection(ElasticInferrer inferrer, IDictionary<string, object> valuesDictionary = null)
		{
			this.Infer = inferrer;
			this.Self.FieldValuesDictionary = valuesDictionary;
		}

		public K[] FieldValues<K>(string path)
		{
			return this.FieldArray<K[]>(path);
		}

		public K FieldValue<K>(string path) => FieldValues<K>(path).FirstOrDefault();


		public K[] FieldValues<TBindTo, K>(Expression<Func<TBindTo, object>> objectPath)
			where TBindTo : class
		{
			var path = this.Infer.Field(objectPath);
			return this.FieldArray<K[]>(path);
		}

		public K FieldValue<TBindTo, K>(Expression<Func<TBindTo, object>> objectPath) 
			where TBindTo : class => FieldValues<TBindTo, K>(objectPath).FirstOrDefault();

		public K[] FieldValues<K>(Expression<Func<T, K>> objectPath)
		{
			var path = this.Infer.Field(objectPath);
			return this.FieldArray<K[]>(path);
		}

		public K FieldValue<K>(Expression<Func<T, K>> objectPath) => FieldValues<K>(objectPath).FirstOrDefault();

		private K FieldArray<K>(string path)
		{
			object o;
			if (this.Self.FieldValuesDictionary != null && this.Self.FieldValuesDictionary.TryGetValue(path, out o))
			{
				var t = typeof(K);
				if (o is JArray && t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IEnumerable)))
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
