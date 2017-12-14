using System.Collections.Generic;

namespace Nest
{
	public interface IIsADictionary {}
	public interface IIsADictionary<TKey, TValue> : IDictionary<TKey, TValue>, IIsADictionary {}

}
