using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utf8Json.UniversalCodeGenerator
{
    public interface IResolverRegisterInfo
    {
        string FullName { get; }
        string FormatterName { get; }
    }

    public class ObjectSerializationInfo : IResolverRegisterInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Namespace { get; set; }
        public bool IsClass { get; set; }
        public bool IsStruct { get { return !IsClass; } }
        public MemberSerializationInfo[] ConstructorParameters { get; set; }
        public MemberSerializationInfo[] Members { get; set; }
        public string FormatterName => (Namespace == null ? Name : Namespace + "." + Name) + "Formatter";
        public bool HasConstructor { get; set; }

        public int WriteCount
        {
            get
            {
                return Members.Count(x => x.IsReadable);
            }
        }

        public string GetConstructorString()
        {
            var args = string.Join(", ", ConstructorParameters.Select(x => "__" + x.Name + "__"));
            return $"{FullName}({args})";
        }
    }

    public class MemberSerializationInfo
    {
        public bool IsProperty { get; set; }
        public bool IsField { get; set; }
        public bool IsWritable { get; set; }
        public bool IsReadable { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string MemberName { get; set; }
        public string ShortTypeName { get; set; }

        readonly HashSet<string> primitiveTypes = new HashSet<string>(new string[]
        {
            "short",
            "int",
            "long",
            "ushort",
            "uint",
            "ulong",
            "float",
            "double",
            "bool",
            "byte",
            "sbyte",
            //"char",
            //"global::System.DateTime",
            //"byte[]",
            "string",
        });

        public string GetSerializeMethodString()
        {
            if (primitiveTypes.Contains(Type))
            {
                return $"writer.Write{ShortTypeName.Replace("[]", "s")}(value.{MemberName})";
            }
            else
            {
                return $"formatterResolver.GetFormatterWithVerify<{Type}>().Serialize(ref writer, value.{MemberName}, formatterResolver)";
            }
        }

        public string GetDeserializeMethodString()
        {
            if (primitiveTypes.Contains(Type))
            {
                return $"reader.Read{ShortTypeName.Replace("[]", "s")}()";
            }
            else
            {
                return $"formatterResolver.GetFormatterWithVerify<{Type}>().Deserialize(ref reader, formatterResolver)";
            }
        }
    }
    public class GenericSerializationInfo : IResolverRegisterInfo, IEquatable<GenericSerializationInfo>
    {
        public string FullName { get; set; }

        public string FormatterName { get; set; }

        public bool Equals(GenericSerializationInfo other)
        {
            return FullName.Equals(other.FullName);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }
    }
}