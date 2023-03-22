using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string pfxFilePath = @"C:\test.pfx";
        string password = "password";
        string certificateAlias = "signing_cert";
        string dataFilePath = @"C:\signme.txt";
        string signedDataFilePath = @"C:\signed_data.txt";

        // Load the certificate and private key from the PFX file
        X509Certificate2 certificate = new X509Certificate2(pfxFilePath, password);
        RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)certificate.PrivateKey;

        // Read the data to sign
        byte[] dataToSign = File.ReadAllBytes(dataFilePath);

        // Sign the data
        byte[] signature = privateKey.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        // Write the signed data to a file
        File.WriteAllBytes(signedDataFilePath, signature);
    }
}
