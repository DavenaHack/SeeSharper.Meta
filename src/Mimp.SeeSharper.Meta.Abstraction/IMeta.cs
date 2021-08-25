using System.Reflection;

namespace Mimp.SeeSharper.Meta.Abstraction
{
    public interface IMeta
    {


        public MemberInfo Member { get; }


        public object Meta { get; }


    }


    public interface IMeta<T> : IMeta where T : notnull
    {


        public new T Meta { get; }


    }
}