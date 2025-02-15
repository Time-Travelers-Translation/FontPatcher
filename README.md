# FontPatcher
This tool patches the fonts found in Time Travelers according to certain requirements we decided on during the development of the patch.

The adjustments made are:
- Global width adjustments
- Remapping of special characters (eg. diacritics, symbols)
- Remove furigana
- Adding full-width Latin characters, based on half-width Latin characters

## Configuration

The character mapping and width adjustment are externally configurable through the `config.json`, explained below:

|Category|Key|Description|
|--|--|--|
|`Logic.Business.FontPatcher`|`PatchMapPath`|The file path, relative to FontPatcher, that contains a mapping of font characters.|
|`Logic.Business.FontPatcher`|`WidthAdjustment`|The globally applied adjustment of character widths. Can be negative.|

## Global Width Adjustments

Characters in the font "Roboto Condensed", we used, had too much space between each other.<br>
Therefore, we applied an adjustment of -1 to the width of every character supported by the font, to bring them closer together.<br>
This didn't just grant us more space to put text and be more expressive, it also just looked better.

|![adjust_widths_2](https://github.com/user-attachments/assets/9ab473bf-2f79-49ac-a578-63fd90a82cd2)|![adjust_widths_1](https://github.com/user-attachments/assets/6b029dc8-f9a9-4998-9388-98f8dd015f3f)|
|:--:|:--:|
|Not Adjusted|Adjusted|

## Special Character Remapping

Some characters, like umlauts (eg ö, é, É) and symbols (eg ×), are not supported by the text encoding, Shift-JIS, the game natively uses.<br>
To circumvent its limitations we use placeholder characters, that were never used in our translated text, and assign them a different glyph.

For example, the umlaut ö is represented by the japanese symbol あ.<br>
Such placeholders are properly set during our text injection pipeline.

|![remap_chars_2](https://github.com/user-attachments/assets/75ed2a60-eded-4150-b0c1-39998678d5a2)|![remap_chars_1](https://github.com/user-attachments/assets/cfc93506-f482-4335-b58a-1d30197288fe)|
|:--:|:--:|
|Not Adjusted|Adjusted|

## Remove Furigana

Furigana is the common name for Hiragana characters written above Kanji, so people unfamiliar with the proper pronounciation of those Kanji can actually pronounce them. With the correct pronounciation, the meaning of a Kanji, or set of Kanji also becomes less ambiguous.

However, in english text we do not need Furigana. It also takes away some space for every line of text, which decreases the usable space.<br>
Therefore, we removed Furigana entirely from the font, which also leads internal line height calculations to give more space to display text.

|![remove_furigana_2](https://github.com/user-attachments/assets/ed31e27e-6506-44e0-a772-5b5f27052590)|![remove_furigana_1](https://github.com/user-attachments/assets/79c25742-7cb5-4a67-adf4-64ca7e39e6d9)|
|:--:|:--:|
|Not Adjusted|Adjusted|

## Full-Width Latin

The game's internal systems work the most reliably on full-width characters. Every character with character code <0x80 should therefore be replaced by its full-width equivalent.

However, full-width characters are normally also visually wider. We wanted the glyphs of the half-width characters used by their full-width equivalents to tie all the benefits of the engine together.

One of those benefits was proper text-wrapping around image objects.<br>
As you can see, two lines are completely misplaced under the image, at the most left of the screen. This text-wrapping only works properly for full-width characters.

|![sjis_remap_2](https://github.com/user-attachments/assets/22fb52bd-590f-42c9-b61b-00022e7d92c5)|![sjis_remap_1](https://github.com/user-attachments/assets/ead82cce-ce2c-48b3-a7b3-85678229fd07)|
|:--:|:--:|
|Not Adjusted|Adjusted|
