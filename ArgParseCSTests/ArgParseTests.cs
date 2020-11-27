using System;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using ArgParseCS;

namespace ArgParseCSTests
{
    [TestFixture]
    public class ArgParseTests
    {
        [Test]
        public void ArgParse_OneOptionSet_Add_Success()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            OptionSet optionSet = new OptionSet("Single parameter");
            optionSet.AddOption(option);
            argParse.AddOptionSet(optionSet);

            Assert.AreEqual("Usage:\nSingle parameter:\n\t-a\t--add Value\t\tAdd an option\n", argParse.Usage());
        }

        [Test]
        public void ArgParse_OneOptionSetTwoOptions_Add_Success()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            Option option2 = new Option("-d", "--delete", "Delete an option", false, true);
            OptionSet optionSet = new OptionSet("Two parameters");
            optionSet.AddOption(option);
            optionSet.AddOption(option2);
            argParse.AddOptionSet(optionSet);

            Assert.AreEqual("Usage:\nTwo parameters:\n\t-a\t--add Value\t\tAdd an option" +
                "\n\t-d\t--delete Value\t\t[Optional] Delete an option\n", argParse.Usage());
        }


        [Test]
        public void ArgParse_TwoOptionSetTwoOptions_Add_Success()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            Option option2 = new Option("-d", "--delete", "Delete an option", false, true);
            OptionSet optionSet = new OptionSet("Two parameters");
            optionSet.AddOption(option);
            optionSet.AddOption(option2);
            argParse.AddOptionSet(optionSet);
            OptionSet optionSet2 = new OptionSet("One parameter");
            Option option3 = new Option("-v", "--version", "Version of the application", true, false);
            optionSet2.AddOption(option3);
            argParse.AddOptionSet(optionSet2);

            Assert.AreEqual("Usage:\nTwo parameters:\n\t-a\t--add Value\t\t" +
                "Add an option\n\t-d\t--delete Value\t\t[Optional] Delete an option\n" +
                "One parameter:\n\t-v\t--version\t\tVersion of the application\n", argParse.Usage());
        }

        [Test]
        public void ArgParse_Parse_Success()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            Option option2 = new Option("-d", "--delete", "Delete an option", false, true);
            OptionSet optionSet = new OptionSet("Two parameters");
            optionSet.AddOption(option);
            optionSet.AddOption(option2);
            argParse.AddOptionSet(optionSet);
            OptionSet optionSet2 = new OptionSet("One parameter");
            Option option3 = new Option("-v", "--version", "Version of the application", true, false);
            optionSet2.AddOption(option3);
            argParse.AddOptionSet(optionSet2);

            string[] args = { "-a", "anOption" };
            argParse.Parse(args);
            OptionSet activeOptionSet = argParse.GetActiveOptionSet();
            if (activeOptionSet != null)
                Assert.AreEqual("Two parameters", activeOptionSet.Name);
            else
                Assert.Fail();
        }

        [Test]
        public void ArgParse_Parse_withQuotes_Success()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            Option option2 = new Option("-d", "--delete", "Delete an option", false, true);
            OptionSet optionSet = new OptionSet("Two parameters");
            optionSet.AddOption(option);
            optionSet.AddOption(option2);
            argParse.AddOptionSet(optionSet);
            OptionSet optionSet2 = new OptionSet("One parameter");
            Option option3 = new Option("-v", "--version", "Version of the application", true, false);
            optionSet2.AddOption(option3);
            argParse.AddOptionSet(optionSet2);

            string[] args = { "-a", "'anOption'" };
            argParse.Parse(args);
            OptionSet activeOptionSet = argParse.GetActiveOptionSet();
            if (activeOptionSet != null)
            {
                Option o = activeOptionSet.GetOption("-a");
                if (o != null)
                    Assert.AreEqual("anOption", o.ParamValue);
            }
            else
                Assert.Fail();
        }

        [Test]
        public void ArgParse_Parse_twooptions_Success()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            Option option2 = new Option("-d", "--delete", "Delete an option", false, true);
            OptionSet optionSet = new OptionSet("Two parameters");
            optionSet.AddOption(option);
            optionSet.AddOption(option2);
            argParse.AddOptionSet(optionSet);
            OptionSet optionSet2 = new OptionSet("One parameter");
            Option option3 = new Option("-v", "--version", "Version of the application", true, false);
            optionSet2.AddOption(option3);
            argParse.AddOptionSet(optionSet2);

            string[] args = { "-a", "anOption", "--delete", "anotherOPtion" };
            argParse.Parse(args);
            OptionSet activeOptionSet = argParse.GetActiveOptionSet();
            if (activeOptionSet != null)
                Assert.AreEqual("Two parameters", activeOptionSet.Name);
            else
                Assert.Fail();
        }

        [Test]
        public void ArgParse_Parse_onearg_Success()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            Option option2 = new Option("-d", "--delete", "Delete an option", false, true);
            OptionSet optionSet = new OptionSet("Two parameters");
            optionSet.AddOption(option);
            optionSet.AddOption(option2);
            argParse.AddOptionSet(optionSet);
            OptionSet optionSet2 = new OptionSet("One parameter");
            Option option3 = new Option("-v", "--version", "Version of the application", true, false);
            optionSet2.AddOption(option3);
            argParse.AddOptionSet(optionSet2);

            string[] args2 = { "-v" };
            argParse.Parse(args2);
            OptionSet activeOptionSet = argParse.GetActiveOptionSet();
            if (activeOptionSet != null)
                Assert.AreEqual("One parameter", activeOptionSet.Name);
            else
                Assert.Fail();
        }

        [Test]
        public void ArgParse_Parse_onearg_Fail()
        {
            ArgParse argParse = new ArgParse();
            Option option = new Option("-a", "--add", "Add an option", true, true);
            Option option2 = new Option("-d", "--delete", "Delete an option", false, true);
            OptionSet optionSet = new OptionSet("Two parameters");
            optionSet.AddOption(option);
            optionSet.AddOption(option2);
            argParse.AddOptionSet(optionSet);
            OptionSet optionSet2 = new OptionSet("One parameter");
            Option option3 = new Option("-v", "--version", "Version of the application", true, false);
            optionSet2.AddOption(option3);
            argParse.AddOptionSet(optionSet2);

            string[] args2 = { "-d", "anOption" };
            Assert.Throws<ArgumentException>(() => argParse.Parse(args2));
            
        }
    }
}
