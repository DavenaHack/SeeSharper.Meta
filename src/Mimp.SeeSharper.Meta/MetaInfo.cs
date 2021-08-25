using Mimp.SeeSharper.Meta.Abstraction;
using Mimp.SeeSharper.Reflection;
using System;
using System.Reflection;

namespace Mimp.SeeSharper.Meta
{
    public class MetaInfo : IMeta
    {


        public MemberInfo Member { get; }

        public object Meta { get; }


        public MetaInfo(MemberInfo member, object meta)
        {
            Member = member ?? throw new ArgumentNullException(nameof(member));
            Meta = meta ?? throw new ArgumentNullException(nameof(meta));
        }


        public static MetaInfo New(MemberInfo member, object meta, Type metaType)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));
            if (meta is null)
                throw new ArgumentNullException(nameof(meta));
            if (metaType is null)
                throw new ArgumentNullException(nameof(metaType));

            if (!metaType.IsInstanceOfType(meta))
                throw new ArgumentException($"{meta} is not a instance of {metaType}", nameof(meta));

            return (MetaInfo)typeof(MetaInfo<>).MakeGenericType(metaType).New(new[] { typeof(MemberInfo), metaType }, new[] { member, meta });
        }

    }


    public class MetaInfo<TMeta> : MetaInfo, IMeta<TMeta> where TMeta : notnull
    {


        public new TMeta Meta => (TMeta)base.Meta;


        public MetaInfo(MemberInfo member, TMeta meta)
            : base(member, meta) { }


    }
}
