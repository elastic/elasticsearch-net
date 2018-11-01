using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Elasticsearch.Net;
using static Tests.Framework.Integration.ClientMethod;

namespace Tests.Framework.Integration
{
	/// <summary>
	///     Holds unique values for the the two DSL's and the exposed sync and async methods we expose
	///     <see cref="ClientMethod" />
	/// </summary>
	public class CallUniqueValues : Dictionary<ClientMethod, string>
	{
		private readonly string _prefix;
		private string UniqueValue => $"{_prefix}-{ViewName}-{Guid.NewGuid().ToString("N").Substring(0, 8)}";
		public string FixedForAllCallsValue { get; }

		private IDictionary<ClientMethod, ConcurrentDictionary<string, object>> ExtendedValues { get; }
			= new Dictionary<ClientMethod, ConcurrentDictionary<string, object>>();

		public ClientMethod CurrentView { get; set; } = Fluent;
		public string ViewName => CurrentView.GetStringValue().ToLowerInvariant();

		public ClientMethod[] Views { get; } = { Fluent, FluentAsync, Initializer, InitializerAsync };

		public string Value => this[CurrentView];

		public T ExtendedValue<T>(string key) where T : class => ExtendedValues[CurrentView][key] as T;

		public void ExtendedValue<T>(string key, T value) where T : class => ExtendedValues[CurrentView][key] = value;

		public T ExtendedValue<T>(string key, Func<T> value) where T : class =>
			ExtendedValues[CurrentView].GetOrAdd(key, value) as T;

		public CallUniqueValues(string prefix = "nest")
		{
			_prefix = prefix;
			FixedForAllCallsValue = UniqueValue;
			SetupClientMethod(Fluent);
			SetupClientMethod(FluentAsync);
			SetupClientMethod(Initializer);
			SetupClientMethod(InitializerAsync);
			CurrentView = Fluent;
		}

		private void SetupClientMethod(ClientMethod method)
		{
			CurrentView = method;
			Add(method, UniqueValue);
			ExtendedValues.Add(method, new ConcurrentDictionary<string, object>());
		}
	}
}
