using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Tests.Framework
{
	/// <summary>
	/// Provides support for asynchronous lazy initialization. This type is fully threadsafe.
	/// </summary>
	/// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
	public class AsyncLazy<T>
	{
		/// <summary>
		/// The underlying lazy task.
		/// </summary>
		private readonly Lazy<Task<T>> instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="factory">The delegate that is invoked on a background thread to produce the value when it is needed.</param>
		public AsyncLazy(Func<T> factory) => instance = new Lazy<Task<T>>(() => Task.Run(factory));

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="factory">The asynchronous delegate that is invoked on a background thread to produce the value when it is needed.</param>
		public AsyncLazy(Func<Task<T>> factory) => instance = new Lazy<Task<T>>(() => Task.Run(factory));

		/// <summary>
		/// Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy&lt;T&gt;"/> to be await'ed.
		/// </summary>
		public TaskAwaiter<T> GetAwaiter() => instance.Value.GetAwaiter();

		/// <summary>
		/// Starts the asynchronous initialization, if it has not already started.
		/// </summary>
		public void Start()
		{
			var unused = instance.Value;
		}
	}
}
