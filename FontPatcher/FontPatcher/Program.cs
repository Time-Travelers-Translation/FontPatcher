using CrossCutting.Core.Contract.DependencyInjection;
using CrossCutting.Core.Contract.EventBrokerage;
using CrossCutting.Core.Contract.Messages;
using FontPatcher;
using Logic.Business.FontPatcher.Contract;

KernelLoader loader = new();
ICoCoKernel kernel = loader.Initialize();

var eventBroker = kernel.Get<IEventBroker>();
eventBroker.Raise(new InitializeApplicationMessage());

var mainLogic = kernel.Get<IFontPatcherWorkflow>();
return mainLogic.Execute();
