using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using Logic;


namespace DeDuper
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                DirectoryWalker walker = new DirectoryWalker(options.Directory);
                walker.ProcessDirectoryTree();
            }
        }
    }

    class Options
    {
        [Option('d', "Directory", Required = true,
            HelpText = "Directory to start (top level)")]
        public string Directory { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
        
    }
}
