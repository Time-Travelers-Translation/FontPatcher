using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher.InternalContract
{
    public interface IFullWidthCharacterAdditionWorkflow
    {
        void Work(FontData fontData);
    }
}
