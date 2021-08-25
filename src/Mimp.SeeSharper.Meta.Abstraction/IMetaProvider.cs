using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mimp.SeeSharper.Meta.Abstraction
{
    public interface IMetaProvider
    {


        public IEnumerable<IMeta> Provide(MemberInfo member);


        public IEnumerable<IMeta> Provide(MemberInfo member, Type metaType);


    }
}
