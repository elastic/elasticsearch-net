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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Elasticsearch.Net.Utf8Json.Internal.Emit
{
	internal class MetaMethodInfoComparer : IEqualityComparer<MethodInfo>
	{
		public static readonly MetaMethodInfoComparer Default = new MetaMethodInfoComparer();

		public bool Equals(MethodInfo x, MethodInfo y)
		{
			if (x == null || y == null)
				return false;

			return x.Name == y.Name && x.DeclaringType == y.DeclaringType;
		}

		public int GetHashCode(MethodInfo obj) => obj.GetHashCode();
	}

	internal class MetaType
    {
        public Type Type { get; }
        public bool IsClass { get; }
        public bool IsStruct => !IsClass;
		public bool IsConcreteClass { get; }
        public ConstructorInfo BestMatchConstructor { get; internal set; }
        public MetaMember[] ConstructorParameters { get; internal set; }
        public MetaMember[] Members { get; internal set; }

		private static TAttribute GetCustomAttribute<TAttribute>(PropertyInfo propertyInfo, bool inherit, List<PropertyInfo> interfaceProperties)
			where TAttribute : Attribute
		{
			var attribute = propertyInfo.GetCustomAttribute<TAttribute>(inherit);
			if (attribute != null)
				return attribute;

			if (interfaceProperties == null || interfaceProperties.Count == 0)
				return null;

			var interfaceProperty = interfaceProperties.FirstOrDefault();
			return interfaceProperty != null ? interfaceProperty.GetCustomAttribute<TAttribute>(inherit) : null;
		}

        public MetaType(Type type, Func<string, string> nameMutator, Func<MemberInfo, JsonProperty> propertyMapper, bool allowPrivate)
        {
			var isClass = type.IsClass || type.IsInterface || type.IsAbstract;
            var dataContractPresent = type.GetCustomAttribute<DataContractAttribute>(true) != null ||
                                      type.GetCustomAttribute<InterfaceDataContractAttribute>(true) != null;

            Type = type;

            var stringMembers = new Dictionary<string, MetaMember>();
			{
				var interfaceMaps = type.IsClass
					? type.GetInterfaces().Select(type.GetInterfaceMap).ToArray()
					: null;

                foreach (var item in type.GetAllProperties())
                {
                    if (item.GetIndexParameters().Length > 0) continue; // skip indexer

                    // get interface properties this property implements
					List<PropertyInfo> interfaceProps = null;
					if (interfaceMaps != null)
					{
						var accessor = item.GetMethod ?? item.SetMethod;

						for (var i = 0; i < interfaceMaps.Length; i++)
						{
							var interfaceMap = interfaceMaps[i];
							if (interfaceMap.TargetMethods.Contains(accessor, MetaMethodInfoComparer.Default))
							{
								interfaceProps ??= new List<PropertyInfo>();

								var propertyName = item.Name.StartsWith(interfaceMap.InterfaceType.FullName + ".")
									? item.Name.Substring(interfaceMap.InterfaceType.FullName.Length + 1)
									: item.Name;

								var info = interfaceMap.InterfaceType.GetProperty(propertyName);
								if (info != null)
									interfaceProps.Add(info);
							}
						}
					}

					if (GetCustomAttribute<IgnoreDataMemberAttribute>(item, true, interfaceProps) != null)
						continue;

                    var dm = GetCustomAttribute<DataMemberAttribute>(item, true, interfaceProps);

					if (dataContractPresent && dm == null)
						continue;

					var name = dm?.Name ?? nameMutator(item.Name);
					var allowPrivateMember = allowPrivate;

					object jsonFormatter = null;

					var property = propertyMapper?.Invoke(item);
					if (property != null)
					{
						if (property.Ignore)
							continue;

						if (!string.IsNullOrEmpty(property.Name))
							name = property.Name;

						if (property.AllowPrivate.HasValue)
							allowPrivateMember = property.AllowPrivate.Value;

						if (property.JsonFormatter != null)
							jsonFormatter = property.JsonFormatter;
					}

					var props = interfaceProps?.ToArray();

                    var member = new MetaMember(item, name, props, jsonFormatter, allowPrivateMember || dm != null);
                    if (!member.IsReadable && !member.IsWritable) continue;

                    if (!stringMembers.ContainsKey(member.Name))
						stringMembers.Add(member.Name, member);
				}
                foreach (var item in type.GetAllFields())
                {
                    if (item.GetCustomAttribute<IgnoreDataMemberAttribute>(true) != null) continue;
                    if (item.GetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>(true) != null) continue;
                    if (item.IsStatic) continue;
                    if (item.Name.StartsWith("<")) continue; // compiler generated field(anonymous type, etc...)

                    var dm = item.GetCustomAttribute<DataMemberAttribute>(true);
					if (dataContractPresent && dm == null) continue;
                    var name = (dm != null && dm.Name != null) ? dm.Name : nameMutator(item.Name);
					var allowPrivateMember = allowPrivate;
					object jsonFormatter = null;

					var field = propertyMapper?.Invoke(item);
					if (field != null)
					{
						if (field.Ignore)
							continue;

						if (!string.IsNullOrEmpty(field.Name))
							name = field.Name;

						if (field.AllowPrivate.HasValue)
							allowPrivateMember = field.AllowPrivate.Value;

						if (field.JsonFormatter != null)
							jsonFormatter = field.JsonFormatter;
					}

					var member = new MetaMember(item, name, jsonFormatter, allowPrivateMember || dm != null);
                    if (!member.IsReadable && !member.IsWritable) continue;

                    if (!stringMembers.ContainsKey(member.Name))
						stringMembers.Add(member.Name, member);
				}
            }

            // GetConstructor
            var ctor = type.GetDeclaredConstructors()
                .SingleOrDefault(x => x.GetCustomAttribute<SerializationConstructorAttribute>(false) != null);
            var constructorParameters = new List<MetaMember>();
            {
                IEnumerator<ConstructorInfo> ctorEnumerator = null;
                if (ctor == null)
                {
                    // descending.
                    ctorEnumerator = type.GetDeclaredConstructors()
						.Where(x => x.IsPublic)
						.OrderByDescending(x => x.GetParameters().Length).GetEnumerator();
                    if (ctorEnumerator.MoveNext())
						ctor = ctorEnumerator.Current;
				}

                if (ctor != null)
                {
                    var constructorLookupDictionary = stringMembers.ToLookup(x => x.Key, x => x, StringComparer.OrdinalIgnoreCase);
                    do
                    {
                        constructorParameters.Clear();
                        var ctorParamIndex = 0;
                        foreach (var item in ctor.GetParameters())
                        {
							var hasKey = constructorLookupDictionary[item.Name];
                            var len = hasKey.Count();
                            if (len != 0)
                            {
                                if (len != 1)
								{
									if (ctorEnumerator != null)
                                    {
                                        ctor = null;
                                        continue;
                                    }

									throw new InvalidOperationException("duplicate matched constructor parameter name:" + type.FullName + " parameterName:" + item.Name + " paramterType:" + item.ParameterType.Name);
								}

                                var paramMember = hasKey.First().Value;
                                if (item.ParameterType == paramMember.Type && paramMember.IsReadable)
									constructorParameters.Add(paramMember);
								else
                                {
                                    ctor = null;
                                    continue;
                                }
                            }
                            else
                            {
                                ctor = null;
                                continue;
                            }
                            ctorParamIndex++;
                        }
                    } while (TryGetNextConstructor(ctorEnumerator, ref ctor));
                }
            }

            IsClass = isClass;
            IsConcreteClass = isClass && !(type.IsAbstract || type.IsInterface);
            BestMatchConstructor = ctor;
            ConstructorParameters = constructorParameters.ToArray();
            Members = stringMembers.Values.ToArray();
        }

		private static bool TryGetNextConstructor(IEnumerator<ConstructorInfo> ctorEnumerator, ref ConstructorInfo ctor)
        {
            if (ctorEnumerator == null || ctor != null)
				return false;

			if (ctorEnumerator.MoveNext())
            {
                ctor = ctorEnumerator.Current;
                return true;
            }

			ctor = null;
			return false;
		}
    }
}
