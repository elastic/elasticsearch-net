// Copyright (c) 2012 Rizal Almashoor
// Licensed under the MIT license.
// http://gist.github.com/2818038#file_license.txt

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	internal static class TaskHelper
	{
		// Inspired by http://blogs.msdn.com/b/pfxteam/archive/2010/11/21/10094564.aspx

		public static Task Then(this Task task, Action<Task> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<AsyncVoid>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						next(previousTask);
						tcs.TrySetResult(default(AsyncVoid));
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		public static Task Then(this Task task, Func<Task, Task> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<AsyncVoid>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						next(previousTask).ContinueWith(nextTask =>
						{
							if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
							else if (nextTask.IsCanceled) tcs.TrySetCanceled();
							else tcs.TrySetResult(default(AsyncVoid));
						});
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		public static Task<TNextResult> Then<TNextResult>(this Task task, Func<Task, TNextResult> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TNextResult>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						tcs.TrySetResult(next(previousTask));
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		public static Task<TNextResult> Then<TNextResult>(this Task task, Func<Task, Task<TNextResult>> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TNextResult>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						next(previousTask).ContinueWith(nextTask =>
						{
							if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
							else if (nextTask.IsCanceled) tcs.TrySetCanceled();
							else
							{
								try
								{
									tcs.TrySetResult(nextTask.Result);
								}
								catch (Exception ex)
								{
									tcs.TrySetException(ex);
								}
							}
						});
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		public static Task Then<TResult>(this Task<TResult> task, Action<Task<TResult>> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<AsyncVoid>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						next(previousTask);
						tcs.TrySetResult(default(AsyncVoid));
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		public static Task Then<TResult>(this Task<TResult> task, Func<Task<TResult>, Task> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<AsyncVoid>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						next(previousTask).ContinueWith(nextTask =>
						{
							if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
							else if (nextTask.IsCanceled) tcs.TrySetCanceled();
							else tcs.TrySetResult(default(AsyncVoid));
						});
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		public static Task<TNextResult> Then<TResult, TNextResult>(this Task<TResult> task, Func<Task<TResult>, TNextResult> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TNextResult>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						tcs.TrySetResult(next(previousTask));
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		public static Task<TNextResult> Then<TResult, TNextResult>(this Task<TResult> task, Func<Task<TResult>, Task<TNextResult>> next)
		{
			if (task == null) throw new ArgumentNullException("task");
			if (next == null) throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TNextResult>();

			task.ContinueWith(previousTask =>
			{
				if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
				else if (previousTask.IsCanceled) tcs.TrySetCanceled();
				else
				{
					try
					{
						next(previousTask).ContinueWith(nextTask =>
						{
							if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
							else if (nextTask.IsCanceled) tcs.TrySetCanceled();
							else
							{
								try
								{
									tcs.TrySetResult(nextTask.Result);
								}
								catch (Exception ex)
								{
									tcs.TrySetException(ex);
								}
							}
						});
					}
					catch (Exception ex)
					{
						tcs.TrySetException(ex);
					}
				}
			});

			return tcs.Task;
		}

		/// <summary>
		/// Analogous to the finally block in a try/finally
		/// </summary>
		public static void Finally(this Task task, Action<Exception> exceptionHandler, Action finalAction = null)
		{
			task.ContinueWith(t =>
			{
				if (finalAction != null) finalAction();

				if (t.IsCanceled || !t.IsFaulted || exceptionHandler == null) return;
				var innerException = t.Exception.Flatten().InnerExceptions.FirstOrDefault();
				exceptionHandler(innerException ?? t.Exception);
			});
		}
	}

	public struct AsyncVoid
	{
		// Based on Brad Wilson's idea, to simulate a non-generic TaskCompletionSource
		// http://bradwilson.typepad.com/blog/2012/04/tpl-and-servers-pt4.html
	}
}
