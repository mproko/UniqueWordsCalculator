using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UniqueWordsCalculator
{
    internal class Program
    {
        private const int DEFAULT_BUFFER_SIZE = 1024;
        private const FileOptions DEFAULTOPTIONS = FileOptions.Asynchronous | FileOptions.SequentialScan;

        static void Main(string[] args)
        {   
            
            var FileList = new List<string>();
            var Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            FileList.Add(Location + "\\" + @"example.txt");
            FileList.Add(Location + "\\" + @"example2.txt");

            FileStream stream;
            Dictionary<string, int> UniqueWordsList = new Dictionary<string, int>();
            UniqueWordProcessing WordProcessing = new UniqueWordProcessing(UniqueWordsList);
            FileUniqueWordReader WordReader = new FileUniqueWordReader(WordProcessing, DEFAULT_BUFFER_SIZE);

            foreach (var FileName in FileList)
            {
                using (stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read, DEFAULT_BUFFER_SIZE, DEFAULTOPTIONS))
                using (WordReader.FileManager = new StreamReader(stream))
                    WordReader.ParseFile();
            }

            foreach (var UniqueWord in UniqueWordsList)
                Console.WriteLine(UniqueWord.Key + ", " + UniqueWord.Value);

            Console.ReadLine();
        }
    }
}




