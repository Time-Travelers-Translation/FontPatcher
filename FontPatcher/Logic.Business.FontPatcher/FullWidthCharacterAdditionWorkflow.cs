using Logic.Business.FontPatcher.InternalContract;
using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher
{
    internal class FullWidthCharacterAdditionWorkflow : IFullWidthCharacterAdditionWorkflow
    {
        public void Work(FontData fontData)
        {
            var addedChars = new List<GlyphData>();

            foreach (ushort character in fontData.LargeFont.Glyphs.Keys)
            {
                GlyphData glyph = fontData.LargeFont.Glyphs[character];
                int newCodePoint = character switch
                {
                    ' ' => 0x3000,
                    >= '!' and <= '~' => character + 0xFEE0,
                    _ => character
                };

                addedChars.Add(CloneGlyphData(glyph, (ushort)newCodePoint));
            }

            foreach (GlyphData addedChar in addedChars)
                fontData.LargeFont.Glyphs[addedChar.CodePoint] = addedChar;
        }

        private GlyphData CloneGlyphData(GlyphData glyph, ushort newCodePoint)
        {
            return new GlyphData
            {
                CodePoint = newCodePoint,
                Width = glyph.Width,
                Location = new GlyphLocationData
                {
                    X = glyph.Location.X,
                    Y = glyph.Location.Y,
                    Index = glyph.Location.Index
                },
                Description = new GlyphDescriptionData
                {
                    X = glyph.Description.X,
                    Y = glyph.Description.Y,
                    Width = glyph.Description.Width,
                    Height = glyph.Description.Height
                }
            };
        }
    }
}
