using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// This is a custom dictionary that helps in the creation of remote cluster configuration
	/// you can pass to the Put Cluster Settings API
	/// </summary>
	public class RemoteClusterConfiguration : IsADictionaryBase<string, object>
	{
		private readonly Dictionary<string, object> _remoteDictionary =
			new Dictionary<string, object>();

		public RemoteClusterConfiguration() =>
			BackingDictionary["cluster"] = new Dictionary<string, object>()
			{
				{ "remote", _remoteDictionary }
			};

		public void Add(string name, params Uri[] seeds) =>
			Add(name, seeds.Select(u => $"{u.Host}:{u.Port}").ToArray());

		public void Add(string name, params string[] seeds) =>
			_remoteDictionary[name] = new Dictionary<string, object>()
			{
				{ "seeds", seeds }
			};

		public static Dictionary<string, object> operator +(RemoteClusterConfiguration left, IDictionary<string, object> right) => Combine(left, right);
		public static Dictionary<string, object> operator +(IDictionary<string, object> left, RemoteClusterConfiguration right) => Combine(left, right);

		private static Dictionary<string, object> Combine(IDictionary<string, object> left, IDictionary<string, object> right)
		{
			if (left == null && right == null) return null;
			if (left == null) return new Dictionary<string, object>(right);
			if (right == null) return new Dictionary<string, object>(left);

			foreach (var kv in right) left[kv.Key] = kv.Value;
			return left;
		}
	}
}
