using Mimp.SeeSharper.Meta.Abstraction;
using Mimp.SeeSharper.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mimp.SeeSharper.Meta
{
    public abstract class MetaTypeMetaProvider : IMetaProvider
    {


        public IMetaProvider MetaProvider { get; }


        public MetaTypeMetaProvider(IMetaProvider metaProvider)
        {
            MetaProvider = metaProvider ?? throw new ArgumentNullException(nameof(metaProvider));
        }


        public IEnumerable<IMeta> Provide(MemberInfo member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            return Provide(member, MetaProvider.Provide);
        }

        public IEnumerable<IMeta> Provide(MemberInfo member, Type metaType)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            return Provide(member, metaMember => MetaProvider.Provide(metaMember, metaType));
        }


        private IEnumerable<IMeta> Provide(MemberInfo member, Func<MemberInfo, IEnumerable<IMeta>> provide)
        {
            var types = GetMetaTypes(member is Type t ? t : member.ReflectedType!);
            if (types is not null)
                foreach (var metaType in types)
                {
                    var metaMember = GetMetaMembers(member, metaType);
                    if (metaMember is not null)
                        foreach (var m in provide(metaMember))
                            yield return m;
                }
        }


        protected abstract IEnumerable<Type> GetMetaTypes(Type type);


        protected virtual MemberInfo? GetMetaMembers(MemberInfo member, Type metaType)
        {
            try
            {
                return member switch
                {
                    Type _ => metaType,
                    PropertyInfo prop => metaType.GetProperty(prop.Name, prop.HasPublicGet(), prop.HasPublicSet(), prop.IsStatic(), true),
                    FieldInfo field => metaType.GetField(field.Name, field.IsPublic, field.IsStatic, true),
                    ConstructorInfo ctor => metaType.GetSingleConstructor(ctor.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic, ctor.GetParameterTypes()),
                    MethodInfo meth => metaType.GetMethod(meth.Name, meth.IsPublic, meth.IsStatic, true, meth.GetGenericArguments(), meth.GetParameterTypes()),
                    EventInfo evnt => metaType.GetEvent(evnt.Name),
                    _ => null,
                };
            }
            catch
            {
                return null;
            }
        }


    }
}
