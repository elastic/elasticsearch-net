#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Elasticsearch.Net.Utf8Json.Internal.Emit
{
    internal class MetaMember
    {
        public string Name { get; }
        public string MemberName { get; }
        public bool IsProperty => PropertyInfo != null;
        public bool IsField => FieldInfo != null;
        public bool IsWritable { get; }
        public bool IsReadable { get; }
        public Type Type { get; }
        public FieldInfo FieldInfo { get; }
        public PropertyInfo PropertyInfo { get; }
        public PropertyInfo[] InterfacePropertyInfos { get; }
        public MethodInfo ShouldSerializeMethodInfo { get; }
        public MethodInfo ShouldSerializeTypeMethodInfo { get; }
		public object JsonFormatter { get; }

        protected MetaMember(Type type, string name, string memberName, bool isWritable, bool isReadable)
        {
            Name = name;
            MemberName = memberName;
            Type = type;
            IsWritable = isWritable;
            IsReadable = isReadable;
        }

        public MetaMember(FieldInfo info, string name, object jsonFormatter, bool allowPrivate)
        {
            Name = name;
            MemberName = info.Name;
            FieldInfo = info;
            Type = info.FieldType;
            IsReadable = allowPrivate || info.IsPublic;
            IsWritable = allowPrivate || info.IsPublic && !info.IsInitOnly;
            ShouldSerializeMethodInfo = GetShouldSerialize(info);
			ShouldSerializeTypeMethodInfo = info.FieldType.GetShouldSerializeMethod();
			JsonFormatter = jsonFormatter;
		}

        public MetaMember(PropertyInfo info, string name, PropertyInfo[] interfaceInfos, object jsonFormatter, bool allowPrivate)
        {
            Name = name;
            MemberName = info.Name;
            PropertyInfo = info;
			InterfacePropertyInfos = interfaceInfos;
            Type = info.PropertyType;
            IsReadable = info.GetMethod != null && (allowPrivate || info.GetMethod.IsPublic) && !info.GetMethod.IsStatic;
            IsWritable = info.SetMethod != null && (allowPrivate || info.SetMethod.IsPublic) && !info.SetMethod.IsStatic;
            ShouldSerializeMethodInfo = GetShouldSerialize(info);
			ShouldSerializeTypeMethodInfo = info.PropertyType.GetShouldSerializeMethod();
			JsonFormatter = jsonFormatter;
        }

		private static MethodInfo GetShouldSerialize(MemberInfo info)
        {
            var shouldSerialize = $"ShouldSerialize{info.Name}";

            return info.DeclaringType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.Name == shouldSerialize && x.ReturnType == typeof(bool) && x.GetParameters().Length == 0);
        }

		public class AttributeDeclaringType<T> where T : Attribute
		{
			public AttributeDeclaringType(T attribute, Type declaringType)
			{
				Attribute = attribute;
				DeclaringType = declaringType;
			}

			public T Attribute { get; }
			public Type DeclaringType { get; }
		}

        public AttributeDeclaringType<T> GetCustomAttribute<T>(bool inherit) where T : Attribute
		{
			if (IsProperty)
            {
                var attribute = PropertyInfo.GetCustomAttribute<T>(inherit);
				if (attribute != null)
					return new AttributeDeclaringType<T>(attribute, PropertyInfo.DeclaringType);

				if (InterfacePropertyInfos == null)
					return null;

				// check interface properties for respective attribute.
				foreach (var info in InterfacePropertyInfos)
				{
					attribute = info.GetCustomAttribute<T>(inherit);
					if (attribute != null)
						return new AttributeDeclaringType<T>(attribute, info.DeclaringType);
				}
			}
			else if (IsField)
			{
				var attribute = FieldInfo.GetCustomAttribute<T>(inherit);
				if (attribute != null)
					return new AttributeDeclaringType<T>(attribute, FieldInfo.DeclaringType);
			}

			return null;
		}

        public virtual void EmitLoadValue(ILGenerator il)
        {
            if (IsProperty)
				il.EmitCall(PropertyInfo.GetMethod);
			else
				il.Emit(OpCodes.Ldfld, FieldInfo);
		}

        public virtual void EmitStoreValue(ILGenerator il)
        {
            if (IsProperty)
				il.EmitCall(PropertyInfo.SetMethod);
			else
				il.Emit(OpCodes.Stfld, FieldInfo);
		}
    }

    // used for serialize exception...
    internal class StringConstantValueMetaMember : MetaMember
    {
        private readonly string _constant;

        public StringConstantValueMetaMember(string name, string constant)
            : base(typeof(string), name, name, false, true) =>
			_constant = constant;

        public override void EmitLoadValue(ILGenerator il)
        {
            il.Emit(OpCodes.Pop); // pop load instance
            il.Emit(OpCodes.Ldstr, _constant);
        }

        public override void EmitStoreValue(ILGenerator il) => throw new NotSupportedException();
	}

    // used for serialize exception...
    internal class InnerExceptionMetaMember : MetaMember
    {
		private static readonly MethodInfo GetInnerException =
			ExpressionUtility.GetPropertyInfo((Exception ex) => ex.InnerException).GetMethod;
		private static readonly MethodInfo NonGenericSerialize =
			ExpressionUtility.GetMethodInfo<JsonWriter>(writer => JsonSerializer.NonGeneric.Serialize(ref writer, default, default));

		// set after...
        internal ArgumentField ArgWriter;
        internal ArgumentField ArgValue;
        internal ArgumentField ArgResolver;

        public InnerExceptionMetaMember(string name)
            : base(typeof(Exception), name, name, false, true)
        {
        }

        public override void EmitLoadValue(ILGenerator il) => il.Emit(OpCodes.Callvirt, GetInnerException);

		public override void EmitStoreValue(ILGenerator il) => throw new NotSupportedException();

		public void EmitSerializeDirectly(ILGenerator il)
        {
            ArgWriter.EmitLoad();
            ArgValue.EmitLoad();
            il.Emit(OpCodes.Callvirt, GetInnerException);
            ArgResolver.EmitLoad();
            il.EmitCall(NonGenericSerialize);
        }
    }
}
