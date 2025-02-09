using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher.InternalContract
{
    public interface ICharacterRemapWorkflow
    {
        void Work(FontData fontData);
    }
}
