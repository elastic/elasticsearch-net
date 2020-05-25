// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// Simplifies the creation of remote cluster configuration, can be combined with a dictionary using the overloaded + operator
	/// </summary>
	public class RemoteClusterConfiguration : IsADictionaryBase<string, object>
	{
		private readonly Dictionary<string, object> _remoteDictionary =
			new Dictionary<string, object>();

		public RemoteClusterConfiguration() =>
			BackingDictionary["cluster"] = new Dictionary<string, object>
			{
				{ "remote", _remoteDictionary }
			};

		/// <summary>
		/// Adds seeds for the remote cluster specified by name
		/// </summary>
		public void Add(string name, params Uri[] seeds) =>
			Add(name, seeds.Select(u => $"{u.Host}:{u.Port}").ToArray());

		/// <summary>
		/// Adds seeds for the remote cluster specified by name
		/// </summary>
		public void Add(string name, params string[] seeds) =>
			Add(name, new Dictionary<string, object>
			{
				{ "seeds", seeds }
			});

		/// <summary>
		/// Adds settings for the remote cluster specified by name
		/// </summary>
		public void Add(string name, Dictionary<string, object> remoteSettings) =>
			_remoteDictionary[name] = remoteSettings;

		public static Dictionary<string, object> operator +(RemoteClusterConfiguration left, IDictionary<string, object> right) => Combine(left, right);
		public static Dictionary<string, object> operator +(IDictionary<string, object> left, RemoteClusterConfiguration right) => Combine(left, right);

		private static Dictionary<string, object> Combine(IDictionary<string, object> left, IDictionary<string, object> right)
		{
			if (left == null && right == null) return null;
			if (left == null) return new Dictionary<string, object>(right);
			if (right == null) return new Dictionary<string, object>(left);

			foreach (var kv in right) left[kv.Key] = kv.Value;
			return new Dictionary<string, object>(left);
		}
	}
}
