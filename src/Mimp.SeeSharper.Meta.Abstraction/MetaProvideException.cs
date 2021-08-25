using System;
using System.Runtime.Serialization;

namespace Mimp.SeeSharper.Meta.Abstraction
{
    /// <summary>
    /// Throws if <see cref="IMetaProvider"/> failed to provide <see cref="IMeta"/>s.
    /// </summary>
    [Serializable]
    public class MetaProvideException : Exception
    {


        public MetaProvideException() { }

        public MetaProvideException(string? message)
            : base(message) { }

        public MetaProvideException(string? message, Exception? inner)
            : base(message, inner) { }


        protected MetaProvideException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }


    }
}
