using CrossCutting.Core.Contract.Bootstrapping;
using CrossCutting.Core.Contract.Configuration;
using CrossCutting.Core.Contract.DependencyInjection;
using CrossCutting.Core.Contract.DependencyInjection.DataClasses;
using CrossCutting.Core.Contract.EventBrokerage;
using Logic.Business.FontPatcher.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Business.FontPatcher.InternalContract;

namespace Logic.Business.FontPatcher
{
    public class FontPatcherActivator : IComponentActivator
    {
        public void Activating()
        {
        }

        public void Activated()
        {
        }

        public void Deactivating()
        {
        }

        public void Deactivated()
        {
        }

        public void Register(ICoCoKernel kernel)
        {
            kernel.Register<IFontPatcherWorkflow, FontPatcherWorkflow>(ActivationScope.Unique);
            kernel.Register<ICharacterRemapWorkflow, CharacterRemapWorkflow>(ActivationScope.Unique);
            kernel.Register<IFullWidthCharacterAdditionWorkflow, FullWidthCharacterAdditionWorkflow>(ActivationScope.Unique);
            kernel.Register<ICharacterWidthAdjustmentWorkflow, CharacterWidthAdjustmentWorkflow>(ActivationScope.Unique);
            kernel.Register<IFuriganaRemovalWorkflow, FuriganaRemovalWorkflow>(ActivationScope.Dependency);

            kernel.Register<ICharacterProvider, CharacterProvider>(ActivationScope.Unique);

            kernel.Register<IConfigurationValidator, ConfigurationValidator>(ActivationScope.Unique);

            kernel.RegisterConfiguration<FontPatcherConfiguration>();
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
        }

        public void Configure(IConfigurator configurator)
        {
        }
    }
}
