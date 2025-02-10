using Logic.Business.FontPatcher.Contract;
using Logic.Business.FontPatcher.InternalContract;
using Logic.Domain.Level5Management.Contract.Font;
using Logic.Domain.Level5Management.Contract.DataClasses.Font;

namespace Logic.Business.FontPatcher
{
    internal class FontPatcherWorkflow : IFontPatcherWorkflow
    {
        private readonly FontPatcherConfiguration _config;
        private readonly IConfigurationValidator _configValidator;
        private readonly IFontParser _fontParser;
        private readonly IFontComposer _fontComposer;
        private readonly ICharacterRemapWorkflow _characterRemapWorkflow;
        private readonly IFullWidthCharacterAdditionWorkflow _fullWidthCharacterWorkflow;
        private readonly ICharacterWidthAdjustmentWorkflow _characterWidthAdjustmentWorkflow;
        private readonly IFuriganaRemovalWorkflow _furiganaRemovalWorkflow;

        public FontPatcherWorkflow(FontPatcherConfiguration config, IConfigurationValidator configValidator,
            IFontParser fontParser, IFontComposer fontComposer,
            ICharacterRemapWorkflow characterRemapWorkflow, IFullWidthCharacterAdditionWorkflow fullWidthCharacterWorkflow,
            ICharacterWidthAdjustmentWorkflow characterWidthAdjustmentWorkflow, IFuriganaRemovalWorkflow furiganaRemovalWorkflow)
        {
            _config = config;
            _configValidator = configValidator;
            _fontParser = fontParser;
            _fontComposer = fontComposer;
            _characterRemapWorkflow = characterRemapWorkflow;
            _fullWidthCharacterWorkflow = fullWidthCharacterWorkflow;
            _characterWidthAdjustmentWorkflow = characterWidthAdjustmentWorkflow;
            _furiganaRemovalWorkflow = furiganaRemovalWorkflow;
        }

        public int Execute()
        {
            if (_config.ShowHelp || Environment.GetCommandLineArgs().Length <= 1)
            {
                PrintHelp();
                return 0;
            }

            if (!IsValidConfig())
            {
                PrintHelp();
                return 0;
            }

            ApplyPatches();

            return 0;
        }

        private void ApplyPatches()
        {
            FontImageData? fontImageData = LoadFont();
            if (fontImageData == null)
                return;

            // Remove furigana subset to reduce total line height and file size
            _furiganaRemovalWorkflow.Work(fontImageData.Font);

            // Remap configured special characters (eg. ä, ö, ü) to SJIS compatible placeholders
            _characterRemapWorkflow.Work(fontImageData.Font);

            // Copy ASCII characters to SJIS full-width equivalents
            // HINT: The game only properly works with the SJIS range >0x8000
            _fullWidthCharacterWorkflow.Work(fontImageData.Font);

            // Adds additional width to every character to adjust global kerning
            _characterWidthAdjustmentWorkflow.Work(fontImageData.Font);

            SaveFont(fontImageData);
        }

        private FontImageData? LoadFont()
        {
            Stream inputStream = File.OpenRead(_config.InputFile!);

            FontImageData? fontImageData = _fontParser.Parse(inputStream);
            if (fontImageData == null)
                return null;

            inputStream.Close();

            return fontImageData;
        }

        private void SaveFont(FontImageData fontImageData)
        {
            string outputPath = string.IsNullOrWhiteSpace(_config.OutputFile) ? _config.InputFile! : _config.OutputFile;
            using Stream outputStream = File.Create(outputPath);

            _fontComposer.Compose(fontImageData, outputStream);
        }

        private bool IsValidConfig()
        {
            try
            {
                _configValidator.Validate(_config);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Input parameters are incorrect: {GetInnermostException(e).Message}");
                Console.WriteLine();

                return false;
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine("Following commands exist:");
            Console.WriteLine("  -h, --help\t\tShows this help message.");
            Console.WriteLine("  -i, --input\tThe file path to the font to patch.");
            Console.WriteLine("  -o, --output\tThe file path to write the patched font to.");
        }

        private Exception GetInnermostException(Exception e)
        {
            while (e.InnerException != null)
                e = e.InnerException;

            return e;
        }
    }
}
