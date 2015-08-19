using System;

namespace Tests.Framework.Integration
{
	public class ApiUsage
	{
		private readonly object _lock = new object();
		private bool _called = false;
		private LazyResponses _responses = null;

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage)
		{
			if (_called) return _responses;
			lock (_lock)
			{
				if (_called) return _responses;
				this._responses = clientUsage();
				_called = true;
			}
			return _responses;
		}
	}
}