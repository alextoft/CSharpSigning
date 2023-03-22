using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

class Program
{
    static void Main(string[] args)
    {
        // Get the user's temp directory
        string tempDir = Path.GetTempPath();

        // Load the keystore
        var ks = new X509Store(@"C:\path\to\keystore.jks", "keystore_password");
        ks.Open(OpenFlags.ReadOnly);

        // Get the certificate and private key
        var cert = ks.Certificates.Find(X509FindType.FindByAlias, "mesh_client", true)[0];
        var rsa = (RSACryptoServiceProvider)cert.PrivateKey;

        // Convert certificate to PEM format and save to file in temp directory
        string certPem = "-----BEGIN CERTIFICATE-----\n" +
            Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks) +
            "\n-----END CERTIFICATE-----\n";
        File.WriteAllText(Path.Combine(tempDir, "oldmeshcert.pem"), certPem);

        // Convert private key to RSA PEM format and save to file in temp directory
        RsaPrivateCrtKeyParameters privateKeyParams = DotNetUtilities.GetRsaPrivateKey(rsa);
        var stringWriter = new StringWriter();
        var pemWriter = new PemWriter(stringWriter);
        pemWriter.WriteObject(privateKeyParams);
        pemWriter.Writer.Flush();
        File.WriteAllText(Path.Combine(tempDir, "oldmeshprivatekey.pem"), stringWriter.ToString());

        Console.WriteLine($"Private key and certificate extracted and saved to files in {tempDir}.");
    }
}
