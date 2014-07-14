using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Profiling.Indexing.Serializer
{
	public class SsTextNestSerializer : NestSerializer
	{
		public SsTextNestSerializer(IConnectionSettingsValues settings)
			: base(settings)
		{
		}

		//public override T Deserialize<T>(Str)
		//{
		//	JsConfig.EmitCamelCaseNames = true;
		//	return Encoding.UTF8.GetString(bytes).FromJson<T>();
		//}

		//public override byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		//{
		//	if (formatting == SerializationFormatting.None)
		//		return base.Serialize(data, formatting);
		//	JsConfig.EmitCamelCaseNames = true;
		//	return Encoding.UTF8.GetBytes(data.ToJson());
		//}
	}
}
