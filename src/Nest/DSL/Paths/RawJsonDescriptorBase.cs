using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Nest
{
	/// <summary>
	/// Provides all descriptors with an option to inject raw json
	/// DO NOT USE, this is an attempt to make json.net take a IDictionary with extra properties 
	/// However the GetEnumerator it depends on is not performant nor does it use any registered 
	/// JSON.net property resolvers.
	/// </summary>
	public class RawJsonDescriptorBase<TPathDescriptor> : IDictionary<string, object>
		where TPathDescriptor : RawJsonDescriptorBase<TPathDescriptor>
	{
		//[JsonConverter(typeof(RawJsonConverter))]
		private static readonly ConcurrentDictionary<Type, List<Func<object, KeyValuePair<string, object>>>>
 			PropertyAccessors = new ConcurrentDictionary<Type, List<Func<object, KeyValuePair<string, object>>>>();
		public TPathDescriptor RawJson(IDictionary<string, object> jsonObject)
		{
			foreach (var kv in jsonObject)
				this._dictionary[kv.Key] = kv.Value;
			return (TPathDescriptor) this;
		}

		private readonly IDictionary<string, object> _dictionary = new Dictionary<string, object>();

		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			List<Func<object, KeyValuePair<string, object>>> propertyAccessors;
			if (!PropertyAccessors.TryGetValue(this.GetType(), out propertyAccessors))
			{
				var properties = from p in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					let a = p.GetCustomAttributes(typeof (JsonPropertyAttribute), true).FirstOrDefault() as JsonPropertyAttribute
					where a != null
					select new Func<object, KeyValuePair<string, object>>(
						(o) => new KeyValuePair<string, object>(
							a.PropertyName ?? p.Name,
							p.GetValue(o, null)
							)
						);
				propertyAccessors = properties.ToList();
				PropertyAccessors.TryAdd(this.GetType(), propertyAccessors);
			}

			return propertyAccessors.Select(pa=>pa(this))
				.Where(kv=>kv.Value != null)
				.Concat(this._dictionary).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			this._dictionary.Add(item);
		}

		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			this._dictionary.Clear();
		}

		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return this._dictionary.Contains(item);
		}

		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			this._dictionary.CopyTo(array, arrayIndex);
		}

		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return this._dictionary.Remove(item);
		}

		int ICollection<KeyValuePair<string, object>>.Count
		{
			get { return this._dictionary.Count;  }
		}

		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get { return this._dictionary.IsReadOnly; }
		}

		bool IDictionary<string, object>.ContainsKey(string key)
		{
			return this._dictionary.ContainsKey(key);
		}

		void IDictionary<string, object>.Add(string key, object value)
		{
			this._dictionary.Add(key, value);
		}

		bool IDictionary<string, object>.Remove(string key)
		{
			return this._dictionary.Remove(key);
		}

		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		object IDictionary<string, object>.this[string key]
		{
			get { return this._dictionary[key];  }
			set { this._dictionary[key] = value; }
		}

		ICollection<string> IDictionary<string, object>.Keys
		{
			get { return this._dictionary.Keys; }
		}

		ICollection<object> IDictionary<string, object>.Values
		{
			get { return this._dictionary.Values; }
		}
	}
}