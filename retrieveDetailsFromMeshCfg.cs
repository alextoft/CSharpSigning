using System;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\MESH-APP-HOME\meshclient.cfg";

        try
        {
            // Load the XML file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            // Get the KeyStorePath value
            XmlNode keyStorePathNode = xmlDoc.SelectSingleNode("/MESHConfig/KeyStorePath");
            string keyStorePath = keyStorePathNode.InnerText;

            // Get the KeyStorePassword value
            XmlNode keyStorePasswordNode = xmlDoc.SelectSingleNode("/MESHConfig/KeyStorePassword");
            string keyStorePassword = keyStorePasswordNode.InnerText;

            // Print the values to the console
            Console.WriteLine("KeyStorePath: " + keyStorePath);
            Console.WriteLine("KeyStorePassword: " + keyStorePassword);

            // Do something with the values...
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}
