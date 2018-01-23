using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Represents the union of two types, <typeparamref name="TFirst"/> and <typeparamref name="TSecond"/>. Used
	/// in scenarios where an Elasticsearch API may accept more than one different input data structure.
	/// </summary>
	/// <typeparam name="TFirst">The first type</typeparam>
	/// <typeparam name="TSecond">The second type</typeparam>
	[JsonConverter(typeof(UnionJsonConverter))]
	public class Union<TFirst, TSecond>
	{
		internal readonly TFirst Item1;
		internal readonly TSecond Item2;
		internal readonly int _tag;

		/// <summary>
		/// Creates an new instance of <see cref="Union{TFirst,TSecond}"/> that encapsulates <paramref name="item"/> value
		/// </summary>
		/// <param name="item">The value to encapsulate</param>
		public Union(TFirst item) { Item1 = item; _tag = 0; }

		/// <summary>
		/// Creates an new instance of <see cref="Union{TFirst,TSecond}"/> that encapsulates <paramref name="item"/> value
		/// </summary>
		/// <param name="item">The value to encapsulate</param>
		public Union(TSecond item) { Item2 = item; _tag = 1; }

		/// <summary>
		/// Runs an <see cref="Action{T}"/> delegate against the encapsulated value
		/// </summary>
		/// <param name="first">The delegate to run when this instance encapsulates an instance of <typeparamref name="TFirst"/></param>
		/// <param name="second">The delegate to run when this instance encapsulates an instance of <typeparamref name="TSecond"/></param>
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

		/// <summary>
		/// Runs a <see cref="Func{T,TResult}"/> delegate against the encapsulated value
		/// </summary>
		/// <param name="first">The delegate to run when this instance encapsulates an instance of <typeparamref name="TFirst"/></param>
		/// <param name="second">The delegate to run when this instance encapsulates an instance of <typeparamref name="TSecond"/></param>
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
