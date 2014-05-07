using Newtonsoft.Json;

namespace Nest.DSL.Suggest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzinessSuggestDescriptor<T> : IFuzzySuggestDescriptor<T> where T : class 
	{
		[JsonProperty(PropertyName = "fuzziness")]
		internal object _Fuzziness { get; private set; }

		public FuzzinessSuggestDescriptor()
		{
			this._Fuzziness = new object();
		}

		public FuzzinessSuggestDescriptor<T> Fuzziness(string fuzziness)
		{
			this._Fuzziness = fuzziness;
			return this;
		}

		public FuzzinessSuggestDescriptor<T> Fuzziness(int fuzziness)
		{
			this._Fuzziness = fuzziness;
			return this;
		}

		public FuzzinessSuggestDescriptor<T> Fuzziness(double fuzziness)
		{
			this._Fuzziness = fuzziness;
			return this;
		}
	}
}
