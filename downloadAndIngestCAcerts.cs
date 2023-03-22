using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

class Program {
    static void Main(string[] args) {
        string url = "http://example.com/list.txt";
        string keystorePath = "C:\\Java\\keystore.jks";
        string keytoolPath = "C:\\Java\\bin\\keytool.exe";
        string password = "keystore_password";

        // Get the Windows user temp directory
        string tempDir = Path.GetTempPath();

        // Download the list of CA certificates from the URL
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(url);
        StreamReader reader = new StreamReader(stream);
        string line;
        while ((line = reader.ReadLine()) != null) {
            // Download each certificate and add it to the keystore
            string certUrl = line.Trim();
            byte[] certBytes = client.DownloadData(certUrl);
            X509Certificate2 cert = new X509Certificate2(certBytes);
            string cn = cert.GetNameInfo(X509NameType.SimpleName, false);
            string certPath = Path.Combine(tempDir, $"{cn}.pem");
            File.WriteAllBytes(certPath, certBytes);
            string arguments = string.Format("-importcert -trustcacerts -alias \"{0}\" -keystore \"{1}\" -storepass {2} -file \"{3}\"", cn, keystorePath, password, certPath);
            System.Diagnostics.Process.Start(keytoolPath, arguments).WaitForExit();
            File.Delete(certPath);
        }
    }
}
