// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Text;

namespace Nest
{
	internal class UrlLookup
	{
		private readonly string[] _parts;
		private readonly string _route;
		private readonly string[] _tokenized;
		private readonly int _length;

		public UrlLookup(string route)
		{
			_route = route;
			_tokenized = route.Replace("{", "{@")
				.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

			_parts = _tokenized
				.Where(p => p.StartsWith("@"))
				.Select(p => p.Remove(0, 1))
				.ToArray();

			_length = _route.Length + (_parts.Length * 4);
		}

		public bool Matches(ResolvedRouteValues values)
		{
			for (var i = 0; i < _parts.Length; i++)
			{
				if (!values.ContainsKey(_parts[i]))
					return false;
			}
			return true;
		}

		public string ToUrl(ResolvedRouteValues values)
		{
			if (values.Count == 0 && _tokenized.Length == 1 && _tokenized[0][0] != '@')
				return _tokenized[0];

			var sb = new StringBuilder(_length);
			var i = 0;
			for (var index = 0; index < _tokenized.Length; index++)
			{
				var t = _tokenized[index];
				if (t[0] == '@')
				{
					if (values.TryGetValue(_parts[i], out var v))
					{
						if (string.IsNullOrEmpty(v))
							throw new Exception($"'{_parts[i]}' defined but is empty on url: {_route}");

						sb.Append(Uri.EscapeDataString(v));
					}
					else throw new Exception($"No value provided for '{_parts[i]}' on url: {_route}");

					i++;
				}
				else sb.Append(t);
			}
			return sb.ToString();
		}
	}
}
