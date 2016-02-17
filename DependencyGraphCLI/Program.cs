namespace DependencyGraphCLI
{
    using CommandLine;
    using DependencyGraph;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class Program
    {
        public const int OK = 0;
        public const int Error = 1;

        public static int Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                if (!ValidateFiles(options))
                    return Error;

                var assemblies = options.Files.Select(f => Assembly.LoadFrom(f));
                var reflector = new Reflector(assemblies.First());
                reflector.GetDependencyGraph().ExportAsJson(Console.Out);
                return OK;
            }
            else
            {
                return Error;
            }
        }

        private static bool ValidateFiles(Options options)
        {
            if ((options.Files == null) || (options.Files.Count() == 0))
            {
                Console.WriteLine("[ERROR] Please provide at least one assembly file to process.");
                return false;
            }

            foreach (string file in options.Files)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine("[ERROR] Assembly file {0} cannot be found.", file);
                    return false;
                }
            }
            return true;
        }
    }
}
