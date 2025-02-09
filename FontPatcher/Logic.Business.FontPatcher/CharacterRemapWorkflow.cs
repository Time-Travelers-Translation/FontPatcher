using Logic.Business.FontPatcher.InternalContract;
using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher
{
    internal class CharacterRemapWorkflow : ICharacterRemapWorkflow
    {
        private readonly ICharacterProvider _characterProvider;

        public CharacterRemapWorkflow(ICharacterProvider characterProvider)
        {
            _characterProvider = characterProvider;
        }

        public void Work(FontData fontData)
        {
            foreach (char origCharacter in _characterProvider.GetAll())
            {
                if (!_characterProvider.TryGet(origCharacter, out char newCharacter))
                    continue;

                if (!fontData.LargeFont.Glyphs.TryGetValue(origCharacter, out GlyphData? glyphData))
                    continue;

                fontData.LargeFont.Glyphs.Remove(glyphData.CodePoint);
                fontData.LargeFont.Glyphs[newCharacter] = glyphData;

                glyphData.CodePoint = newCharacter;
            }
        }
    }
}
