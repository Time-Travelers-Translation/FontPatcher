using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Business.FontPatcher.InternalContract
{
    public interface IConfigurationValidator
    {
        void Validate(FontPatcherConfiguration config);
    }
}
