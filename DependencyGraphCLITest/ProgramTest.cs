namespace DependencyGraphCLITest
{
    using DependencyGraphCLI;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Reflection;

    [TestFixture]
    public class ProgramTest
    {
        // Assembly: X:\DependencyGraph\DependencyGraphCLITest\bin\Debug\DependencyGraphCLITest.dll
        private readonly string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [Test]
        public void AssemblyWithDependencies()
        {
            // Assembly: X:\DependencyGraph\DependencyGraphCLI\bin\Debug\DependencyGraphCLI.exe
            string dllPath = Path.Combine(assemblyDir, "..", "..", "..", "DependencyGraphCLI", "bin", "Debug", "DependencyGraphCLI.exe");
            string json = CaptureStdOut(() => Assert.AreEqual(Program.OK, Program.Main(new[] { "-a", dllPath })));
            Assert.AreEqual(@"{
  ""DependencyGraphCLI.Program, DependencyGraphCLI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"": {
    ""isUsedBy"": [],
    ""dependsOn"": [
      ""DependencyGraphCLI.Options, DependencyGraphCLI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null""
    ]
  },
  ""DependencyGraphCLI.Options, DependencyGraphCLI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"": {
    ""isUsedBy"": [
      ""DependencyGraphCLI.Program, DependencyGraphCLI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null""
    ],
    ""dependsOn"": []
  }
}", json);
        }

        private string CaptureStdOut(Action test)
        {
            using (var stringWriter = new StringWriter())
            {
                var stdOut = Console.Out;
                try
                {
                    Console.SetOut(stringWriter);
                    test();
                }
                finally
                {
                    Console.SetOut(stdOut);
                }
                return stringWriter.ToString();
            }
        }
    }
}
