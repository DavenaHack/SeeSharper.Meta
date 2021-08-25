using System;

namespace Mimp.SeeSharper.Meta.TypeProvider
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
    public class MetaTypeAttribute : Attribute
    {


        public Type Type { get; }


        public MetaTypeAttribute(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }


    }
}
