using CrossCutting.Core.Contract.Aspects;
using Logic.Business.FontPatcher.Contract.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Business.FontPatcher.Contract
{
    [MapException(typeof(FontPatcherManagementException))]
    public interface IFontPatcherWorkflow
    {
        int Execute();
    }
}
