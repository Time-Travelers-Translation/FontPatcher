using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher.InternalContract
{
    public interface ICharacterWidthAdjustmentWorkflow
    {
        void Work(FontData fontData);
    }
}
