using Logic.Business.FontPatcher.InternalContract;
using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher
{
    internal class CharacterWidthAdjustmentWorkflow : ICharacterWidthAdjustmentWorkflow
    {
        private readonly FontPatcherConfiguration _config;

        public CharacterWidthAdjustmentWorkflow(FontPatcherConfiguration config)
        {
            _config = config;
        }

        public void Work(FontData fontData)
        {
            foreach (ushort character in fontData.LargeFont.Glyphs.Keys)
                fontData.LargeFont.Glyphs[character].Width += _config.WidthAdjustment;
        }
    }
}
