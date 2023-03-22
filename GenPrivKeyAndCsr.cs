using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MeshCertificateExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a new RSA key pair with a key size of 2048 bits
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

            // Create a new certificate signing request with the only subject field being CN=example.com
            CertificateRequest csr = new CertificateRequest("CN=example.com", rsa, HashAlgorithmName.SHA256);

            // Generate the CSR and convert it to PEM format
            byte[] csrBytes = csr.CreateSigningRequest();
            string csrPem = "-----BEGIN CERTIFICATE REQUEST-----\n" + Convert.ToBase64String(csrBytes) + "\n-----END CERTIFICATE REQUEST-----";

            // Save the CSR to the Windows user temp directory as meshcsr.pem
            string csrFilePath = Path.Combine(Path.GetTempPath(), "meshcsr.pem");
            File.WriteAllText(csrFilePath, csrPem, Encoding.ASCII);

            // Save the private key to the Windows user temp directory as meshprivkey.pem without encryption
            string privateKeyPem = rsa.ToXmlString(true);
            string privateKeyFilePath = Path.Combine(Path.GetTempPath(), "meshprivkey.pem");
            File.WriteAllText(privateKeyFilePath, privateKeyPem, Encoding.ASCII);

            Console.WriteLine("CSR and private key generated and saved to the following files:");
            Console.WriteLine(csrFilePath);
            Console.WriteLine(privateKeyFilePath);
        }
    }
}
