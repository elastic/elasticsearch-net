using System;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (RangeQueryJsonConverter))]
	public interface IRangeQuery : IFieldNameQuery { }

}
