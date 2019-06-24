using System;
using System.Collections.Generic;

namespace Elasticsearch.Net.Diagnostics 
{
	public abstract class TypedDiagnosticObserverBase<TOnNext> : IObserver<KeyValuePair<string, object>>
	{
		private readonly Action<(string EventName, TOnNext EventData)> _onNext;
		private readonly Action<Exception> _onError;
		private readonly Action _onCompleted;

		protected TypedDiagnosticObserverBase(
			Action<(string EventName, TOnNext RequestData)> onNext,
			Action<Exception> onError = null,
			Action onCompleted = null
		)
		{
			_onNext= onNext;
			_onError = onError;
			_onCompleted = onCompleted;
		}
		
		void IObserver<KeyValuePair<string, object>>.OnCompleted() => _onCompleted?.Invoke();

		void IObserver<KeyValuePair<string, object>>.OnError(Exception error) => _onError?.Invoke(error);

		void IObserver<KeyValuePair<string, object>>.OnNext(KeyValuePair<string, object> value)
		{
			if (value.Value is TOnNext next) _onNext?.Invoke((EventName: value.Key, EventData: next));
			else if (value.Value == null) _onNext?.Invoke((EventName: value.Key, EventData: default));
			
			else throw new Exception($"{value.Key} received unexpected type {value.Value.GetType()}");

		} 
	}
	public abstract class TypedDiagnosticObserverBase<TOnNextStart, TOnNextEnd> : IObserver<KeyValuePair<string, object>>
	{
		private readonly Action<(string EventName, TOnNextStart EventData)> _onNextStart;
		private readonly Action<(string EventName, TOnNextEnd EventData)> _onNextEnd;
		private readonly Action<Exception> _onError;
		private readonly Action _onCompleted;

		protected TypedDiagnosticObserverBase(
			Action<(string EventName, TOnNextStart RequestData)> onNextStart,
			Action<(string EventName, TOnNextEnd RequestData)> onNextEnd,
			Action<Exception> onError = null,
			Action onCompleted = null
		)
		{
			_onNextStart = onNextStart;
			_onNextEnd = onNextEnd;
			_onError = onError;
			_onCompleted = onCompleted;
		}
		
		void IObserver<KeyValuePair<string, object>>.OnCompleted() => _onCompleted?.Invoke();

		void IObserver<KeyValuePair<string, object>>.OnError(Exception error) => _onError?.Invoke(error);

		void IObserver<KeyValuePair<string, object>>.OnNext(KeyValuePair<string, object> value)
		{
			if (value.Value is TOnNextStart nextStart) _onNextStart?.Invoke((EventName: value.Key, EventData: nextStart));
			else if (value.Key.EndsWith(".Start") && value.Value is null) _onNextStart?.Invoke((EventName: value.Key, EventData: default));
			
			else if (value.Value is TOnNextEnd nextEnd) _onNextEnd?.Invoke((EventName: value.Key, EventData: nextEnd));
			else if (value.Key.EndsWith(".Stop") && value.Value is null) _onNextEnd?.Invoke((EventName: value.Key, EventData: default));
			
			else throw new Exception($"{value.Key} received unexpected type {value.Value.GetType()}");

		} 
	}
}