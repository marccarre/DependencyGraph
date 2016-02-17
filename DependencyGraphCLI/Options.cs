namespace DependencyGraphCLI
{
    using CommandLine;
    using CommandLine.Text;
    using System;

    class Options
    {
        [OptionArray('a', "assemblies", Required = true, DefaultValue = new string[] { }, HelpText = "Comma separated paths to assembly files to process.")]
        public string[] Files { get; set; }

        private const int NumSpaces = 2;

        [HelpOption]
        public string Usage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("DependencyGraphCLI", "1.0"),
                Copyright = new CopyrightInfo("Marc Carré", 2016),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            var errors = help.RenderParsingErrorsText(this, NumSpaces);
            if (!string.IsNullOrEmpty(errors))
            {
                help.AddPreOptionsLine(string.Concat(Environment.NewLine, "[ERROR(S)]:"));
                help.AddPreOptionsLine(errors);
            }

            help.AddPreOptionsLine("License: Apache License, Version 2.0");
            help.AddPreOptionsLine("Usage: DependencyGraphCLI -a/--assemblies [FILE]...");
            help.AddOptions(this);
            return help;
        }
    }
}

