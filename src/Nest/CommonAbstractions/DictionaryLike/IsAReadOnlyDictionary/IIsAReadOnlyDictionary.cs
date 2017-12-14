using System.Collections.Generic;

namespace Nest
{
	public interface IIsAReadOnlyDictionary {}
	public interface IIsAReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IIsAReadOnlyDictionary {}

}
