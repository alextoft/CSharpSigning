using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        string javaKeystorePath = @"C:\Java\example.jks";
        string pfxFilePath = @"C:\test.pfx";
        string password = "password";
        string keytoolPath = @"C:\Java\bin\keytool.exe";

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = keytoolPath;
        startInfo.Arguments = $@"-importkeystore -srckeystore ""{javaKeystorePath}"" -srcstoretype JKS -destkeystore ""{pfxFilePath}"" -deststoretype PKCS12 -srcstorepass ""{password}"" -deststorepass ""{password}""";
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine(output);
    }
}
