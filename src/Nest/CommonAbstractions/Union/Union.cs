using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(UnionJsonConverter))]
	public class Union<TFirst, TSecond>
	{
		internal readonly TFirst Item1;
		internal readonly TSecond Item2;
		internal readonly int _tag;

		public Union(TFirst item) { Item1 = item; _tag = 0; }
		public Union(TSecond item) { Item2 = item; _tag = 1; }

		public void Match(Action<TFirst> first, Action<TSecond> second)
		{
			switch (_tag)
			{
				case 0:
					first(Item1);
					break;
				case 1:
					second(Item2);
					break;
				default: throw new Exception($"Unrecognized tag value: {_tag}");
			}
		}
		public T Match<T>(Func<TFirst, T> first, Func<TSecond, T> second)
		{
			switch (_tag)
			{
				case 0: return first(Item1);
				case 1: return second(Item2);
				default: throw new Exception($"Unrecognized tag value: {_tag}");
			}
		}

		public static implicit operator Union<TFirst, TSecond>(TFirst first) => new Union<TFirst, TSecond>(first);
		public static implicit operator Union<TFirst, TSecond>(TSecond second) => new Union<TFirst, TSecond>(second);
	}
}
