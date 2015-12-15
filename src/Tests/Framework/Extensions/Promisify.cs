using Nest;

namespace Tests.Framework
{
	public static class Promisify
	{
		public static PromiseValue<T> Promise<T>(T o) where T : class => new PromiseValue<T>(o);

		public class PromiseValue<T> : IPromise<T> where T : class
		{
			private readonly T _o;

			public PromiseValue(T o)
			{
				_o = o;
			}

			T IPromise<T>.Value => _o;
		}
	}
}