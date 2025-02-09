using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Business.FontPatcher.Contract.Exceptions
{
    [Serializable]
    public class FontPatcherManagementException : Exception
    {
        public FontPatcherManagementException()
        {
        }

        public FontPatcherManagementException(string message) : base(message)
        {
        }

        public FontPatcherManagementException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FontPatcherManagementException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
