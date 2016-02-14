using RedesII_TII.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RedesII_TII.Model
{
    public class DencryptingModel
    {

        public string publicKeyID     { get; set; }
        public string privateKeyID    { get; set; }

        public PublicKey publicKey    { get; set; }
        public PrivateKey privateKey  { get; set; }

        public DencryptingModel ()
        {
            publicKeyID     = ConfigurationManager.AppSettings["publicKey"];
            privateKeyID    = ConfigurationManager.AppSettings["privateKey"];

            publicKey       = new PublicKey();
            privateKey      = new PrivateKey();
        }

        /// <summary>
        /// this method is supposed to work encrypting and managing this process.
        /// </summary>
        /// <param name="filePath"></param>
        public void DencryptFile(string filePath)
        {

            string fileWrittenPath  = Path.GetDirectoryName(filePath);
            string fileName         = Path.GetFileName(filePath);
            fileName                = Guid.NewGuid().ToString() + "_" +fileName;
            fileWrittenPath         = Path.Combine( fileWrittenPath, fileName );

            using ( BinaryReader fileReader = new BinaryReader(File.Open(filePath,FileMode.Open)) )
            using ( BinaryWriter fileWriter = new BinaryWriter(File.Open(fileWrittenPath, FileMode.CreateNew)) )
            {
                long length = new FileInfo(filePath).Length, count = 0;

                bool div = (length) % 2 == 0;

                if( !div )
                {
                    // Alert user about even number of bytes
                    return;
                }
                
                while(count < length)
                {
                    ushort byteRead = fileReader.ReadUInt16();
                    ushort exponencial =  Util.GetExponencial((ushort)byteRead, this.publicKey.d, this.publicKey.n);

                    if(exponencial > 255)
                    {
                        // Alert user that it can not happen
                        return;
                    }

                    fileWriter.Write((byte)exponencial);
                    count+=2;
                }
            }
        }
        
        public void ProcessKey(string filePath)
        {
            using( StreamReader reader = new StreamReader(filePath) )
            {
                
                string line         = reader.ReadLine();
                while(line != null)
                {
                    string[] lineSplit = line.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                    if (lineSplit.Length == 2 && lineSplit[0].Trim().Equals(publicKeyID, StringComparison.OrdinalIgnoreCase))
                    {
                        string informationStr = ExtractInformation(lineSplit[1].Trim());
                        AddPublicInformation(informationStr);
                    }
                    else if (lineSplit.Length == 2 && lineSplit[0].Trim().Equals(privateKeyID, StringComparison.OrdinalIgnoreCase))
                    {
                        string informationStr = ExtractInformation(lineSplit[1].Trim());
                        AddPrivateInformation(informationStr);
                    }
                    line = reader.ReadLine();
                }
                
            }
        }

        private string ExtractInformation( string keyStr )
        {
            int indexOpen   = keyStr.IndexOf("(", StringComparison.OrdinalIgnoreCase);
            int indexClose  = keyStr.IndexOf(")", StringComparison.OrdinalIgnoreCase);

            string temp     = keyStr.Substring(indexOpen+1, indexClose-1);

            return temp;

        }

        private void AddPublicInformation(string p)
        {
            string[] twoOInfos = p.Split(new[]{ ',' },StringSplitOptions.RemoveEmptyEntries);
            if(twoOInfos.Length == 2)
            {
                ushort n;
                ushort.TryParse(twoOInfos[0].Trim(), out n);
                int d;
                int.TryParse(twoOInfos[1].Trim(), out d);

                this.publicKey.n = n;
                this.publicKey.d = d;

            }
        }

        private void AddPrivateInformation(string p)
        {
            string[] twoOInfos = p.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (twoOInfos.Length == 2)
            {
                ushort n;
                ushort.TryParse(twoOInfos[0].Trim(), out n);
                int e;
                int.TryParse(twoOInfos[1].Trim(), out e);

                this.privateKey.n = n;
                this.privateKey.e = e;
            }
        }
    }
}
