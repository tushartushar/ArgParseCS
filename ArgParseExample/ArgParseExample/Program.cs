using ArgParseCS;
using System;

/*
 * This example deals with three kinds of commands.
 * 1. <program-name> -v
 * 2. <program-name> -i <param1> -o <param2> -t
 * Here, -i and -o both are mandatory arguments with parameters. -t option is optional and doesn't need any argument.
 * 3. <program-name> --help
 * It is desired that one set of command options are not used and mixed with other commands
 * (for example, -i and -v should not be used together in a command).
 *
 * Do not forget to add ArgParseCS library as a reference to this project.
 */
namespace ArgParseExample
{
    class Program
    {
        private ArgParse argParse;
        private readonly string[] args;

        public Program(string[] args)
        {
            DefineParams();
            this.args = args;
        }
        static void Main(string[] args)
        {
            Program program = new Program(args);
            program.Execute();
        }

        private void Execute()
        {
            try
            {
                argParse.Parse(args);
            }
            catch (ArgumentException e)
            {
                argParse.Usage();
                return;
            }
            OptionSet activeOptionSet = argParse.GetActiveOptionSet();
            Console.Out.WriteLine("Active optionset: " + activeOptionSet.Name);
            if (activeOptionSet.Name.Equals("Analysis command"))
            {
                Option option = activeOptionSet.GetOption("-i");
                Console.Out.WriteLine("input: " + option.ParamValue);
                option = activeOptionSet.GetOption("-o");
                Console.Out.WriteLine("output: " + option.ParamValue);
                option = activeOptionSet.GetOption("-t");
                Console.Out.WriteLine("trend: " + (option.IsProvided ? "true" : "false"));
            }
        }

        private void DefineParams()
        {
            argParse = new ArgParse {
                new OptionSet("Version command") {
                    new Option("-v", "--version", "Show version", true, false)
                },
                new OptionSet("Analysis command") {
                    new Option("-i", "--input", "Specify the input folder path", true, true),
                    new Option("-o", "--output", "Specify the output folder path", true, true),
                    new Option("-t", "--trend", "Specify the trend option", false, false),
                },
                new OptionSet("Help command") {
                    new Option("-h", "--help", "Show help options", true, false),
                }
            };

            //argParse = new ArgParse();

            //OptionSet optionSet1 = new OptionSet("Version command");
            //Option optionVersion = new Option("-v", "--version", "Show version", true, false);
            //optionSet1.AddOption(optionVersion);
            //argParse.AddOptionSet(optionSet1);

            //OptionSet optionSet2 = new OptionSet("Analysis command");
            //Option optionInput = new Option("-i", "--input", "Specify the input folder path", true, true);
            //Option optionOutput = new Option("-o", "--output", "Specify the output folder path", true, true);
            //Option optionTrend = new Option("-t", "--trend", "Specify the trend option", false, false);
            //optionSet2.AddOption(optionInput);
            //optionSet2.AddOption(optionOutput);
            //optionSet2.AddOption(optionTrend);
            //argParse.AddOptionSet(optionSet2);

            //OptionSet optionSet3 = new OptionSet("Help command");
            //Option optionHelp = new Option("-h", "--help", "Show help options", true, false);
            //optionSet3.AddOption(optionHelp);
            //argParse.AddOptionSet(optionSet3);
        }
    }
}
