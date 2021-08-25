using Mimp.SeeSharper.Meta.Abstraction;
using Mimp.SeeSharper.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mimp.SeeSharper.Meta
{
    public class AttributeMetaProvider : IMetaProvider
    {


        public bool Inherit { get; }


        public AttributeMetaProvider(bool inherit)
        {
            Inherit = inherit;
        }

        public AttributeMetaProvider()
            : this(true) { }


        public IEnumerable<IMeta> Provide(MemberInfo member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            return member.GetCustomAttributes(Inherit)
                .Select(a => MetaInfo.New(member, a, typeof(Attribute)));
        }

        public IEnumerable<IMeta> Provide(MemberInfo member, Type metaType)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            if (metaType.IsAttribute())
                return Array.Empty<IMeta>();

            return member.GetCustomAttributes(metaType, Inherit)
                .Select(a => MetaInfo.New(member, a, metaType));
        }


    }
}
