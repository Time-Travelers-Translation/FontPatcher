using Logic.Business.FontPatcher.InternalContract;
using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher
{
    internal class FuriganaRemovalWorkflow : IFuriganaRemovalWorkflow
    {
        public void Work(FontData fontData)
        {
            fontData.SmallFont.Glyphs.Clear();
            fontData.SmallFont.FallbackCharacter = 0;
            fontData.SmallFont.MaxHeight = 0;
        }
    }
}
