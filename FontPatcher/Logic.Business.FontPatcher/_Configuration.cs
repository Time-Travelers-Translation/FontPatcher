using CrossCutting.Core.Contract.Configuration.DataClasses;

namespace Logic.Business.FontPatcher
{
    public class FontPatcherConfiguration
    {
        [ConfigMap("CommandLine", new[] { "h", "help" })]
        public virtual bool ShowHelp { get; set; } = false;

        [ConfigMap("CommandLine", new[] { "i", "input" })]
        public virtual string? InputFile { get; set; }

        [ConfigMap("CommandLine", new[] { "o", "output" })]
        public virtual string? OutputFile { get; set; }

        [ConfigMap("Logic.Business.FontPatcher", "PatchMapPath")]
        public virtual string PatchMapPath { get; set; }

        [ConfigMap("Logic.Business.FontPatcher", "WidthAdjustment")]
        public virtual int WidthAdjustment { get; set; }
    }
}