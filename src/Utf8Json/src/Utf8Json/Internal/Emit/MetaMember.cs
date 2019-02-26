using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Runtime.Serialization;

namespace Utf8Json.Internal.Emit
{
    internal class MetaMember
    {
        public string Name { get; private set; }
        public string MemberName { get; private set; }

        public bool IsProperty { get { return PropertyInfo != null; } }
        public bool IsField { get { return FieldInfo != null; } }
        public bool IsWritable { get; private set; }
        public bool IsReadable { get; private set; }
        public Type Type { get; private set; }
        public FieldInfo FieldInfo { get; private set; }
        public PropertyInfo PropertyInfo { get; private set; }
        public PropertyInfo[] InterfacePropertyInfos { get; private set; }
        public MethodInfo ShouldSerializeMethodInfo { get; private set; }
        public MethodInfo ShouldSerializeTypeMethodInfo { get; private set; }

        MethodInfo getMethod;
        MethodInfo setMethod;

        protected MetaMember(Type type, string name, string memberName, bool isWritable, bool isReadable)
        {
            this.Name = name;
            this.MemberName = memberName;
            this.Type = type;
            this.IsWritable = isWritable;
            this.IsReadable = isReadable;
        }

        public MetaMember(FieldInfo info, string name, bool allowPrivate)
        {
            this.Name = name;
            this.MemberName = info.Name;
            this.FieldInfo = info;
            this.Type = info.FieldType;
            this.IsReadable = allowPrivate || info.IsPublic;
            this.IsWritable = allowPrivate || (info.IsPublic && !info.IsInitOnly);
            this.ShouldSerializeMethodInfo = GetShouldSerialize(info);
			this.ShouldSerializeTypeMethodInfo = info.FieldType.GetTypeInfo().GetShouldSerializeMethod();
		}

        public MetaMember(PropertyInfo info, string name, PropertyInfo[] interfaceInfos, bool allowPrivate)
        {
            this.getMethod = info.GetGetMethod(true);
            this.setMethod = info.GetSetMethod(true);

            this.Name = name;
            this.MemberName = info.Name;
            this.PropertyInfo = info;
			this.InterfacePropertyInfos = interfaceInfos;
            this.Type = info.PropertyType;
            this.IsReadable = (getMethod != null) && (allowPrivate || getMethod.IsPublic) && !getMethod.IsStatic;
            this.IsWritable = (setMethod != null) && (allowPrivate || setMethod.IsPublic) && !setMethod.IsStatic;
            this.ShouldSerializeMethodInfo = GetShouldSerialize(info);
			this.ShouldSerializeTypeMethodInfo = info.PropertyType.GetTypeInfo().GetShouldSerializeMethod();
        }

        static MethodInfo GetShouldSerialize(MemberInfo info)
        {
            var shouldSerialize = "ShouldSerialize" + info.Name;

            // public only
            return info.DeclaringType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(x => x.Name == shouldSerialize && x.ReturnType == typeof(bool) && x.GetParameters().Length == 0);
        }

		public class AttributeDeclaringType<T> where T : Attribute
		{
			public AttributeDeclaringType(T attribute, Type declaringType)
			{
				Attribute = attribute;
				DeclaringType = declaringType;
			}

			public T Attribute { get; private set; }
			public Type DeclaringType { get; private set; }
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

				return null;
			}
            else if (FieldInfo != null)
            {
                var attribute = FieldInfo.GetCustomAttribute<T>(inherit);
                if (attribute != null)
					return new AttributeDeclaringType<T>(attribute, FieldInfo.DeclaringType);

				return null;
			}
            else
            {
                return null;
            }
        }

        public virtual void EmitLoadValue(ILGenerator il)
        {
            if (IsProperty)
            {
                il.EmitCall(getMethod);
            }
            else
            {
                il.Emit(OpCodes.Ldfld, FieldInfo);
            }
        }

        public virtual void EmitStoreValue(ILGenerator il)
        {
            if (IsProperty)
            {
                il.EmitCall(setMethod);
            }
            else
            {
                il.Emit(OpCodes.Stfld, FieldInfo);
            }
        }
    }

    // used for serialize exception...
    internal class StringConstantValueMetaMember : MetaMember
    {
        readonly string constant;

        public StringConstantValueMetaMember(string name, string constant)
            : base(typeof(String), name, name, false, true)
        {
            this.constant = constant;
        }

        public override void EmitLoadValue(ILGenerator il)
        {
            il.Emit(OpCodes.Pop); // pop load instance
            il.Emit(OpCodes.Ldstr, constant);
        }

        public override void EmitStoreValue(ILGenerator il)
        {
            throw new NotSupportedException();
        }
    }

    // used for serialize exception...
    internal class InnerExceptionMetaMember : MetaMember
    {
        static readonly MethodInfo getInnerException = ExpressionUtility.GetPropertyInfo((Exception ex) => ex.InnerException).GetGetMethod();
        static readonly MethodInfo nongenericSerialize = ExpressionUtility.GetMethodInfo<JsonWriter>(writer => JsonSerializer.NonGeneric.Serialize(ref writer, default(object), default(IJsonFormatterResolver)));

        // set after...
        internal ArgumentField argWriter;
        internal ArgumentField argValue;
        internal ArgumentField argResolver;

        public InnerExceptionMetaMember(string name)
            : base(typeof(Exception), name, name, false, true)
        {
        }

        public override void EmitLoadValue(ILGenerator il)
        {
            il.Emit(OpCodes.Callvirt, getInnerException);
        }

        public override void EmitStoreValue(ILGenerator il)
        {
            throw new NotSupportedException();
        }

        public void EmitSerializeDirectly(ILGenerator il)
        {
            // JsonSerializer.NonGeneric.Serialize(ref writer, value.InnerException, formatterResolver);
            argWriter.EmitLoad();
            argValue.EmitLoad();
            il.Emit(OpCodes.Callvirt, getInnerException);
            argResolver.EmitLoad();
            il.EmitCall(nongenericSerialize);
        }
    }
}
