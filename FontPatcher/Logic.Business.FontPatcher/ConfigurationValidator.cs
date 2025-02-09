using Logic.Business.FontPatcher.InternalContract;

namespace Logic.Business.FontPatcher
{
    internal class ConfigurationValidator : IConfigurationValidator
    {
        public void Validate(FontPatcherConfiguration config)
        {
            if (config.ShowHelp)
                return;

            ValidateInputPath(config);
        }

        private void ValidateInputPath(FontPatcherConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config.InputFile))
                throw new InvalidOperationException("No file path given. Specify one by using the -i argument.");

            if (!File.Exists(config.InputFile))
                throw new InvalidOperationException($"Path '{config.InputFile}' does not exist.");
        }
    }
}
