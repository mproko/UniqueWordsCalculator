using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniqueWordsCalculator
{
    internal class Program
    {
        private const int DEFAULT_BUFFER_SIZE = 1024;
        private const int DEFAULT_TASK_NUMBER = 10;
        private const FileOptions DEFAULTOPTIONS = FileOptions.Asynchronous | FileOptions.SequentialScan;
        private static IList<Task> _TaskList;

        public static async Task Main(string[] args)
        {

            _TaskList = new List<Task>();
            var FileList = new List<string>();
            FileList.Add(@"C:\projects\UniqueWordsCalculator\example.txt");

            FileStream stream;
            Dictionary<string, int> UniqueWordsList = new Dictionary<string, int>();
            UniqueWordProcessing WordProcessing = new UniqueWordProcessing(UniqueWordsList);
            FileUniqueWordReader WordReader;

            foreach (var FileName in FileList)
            {
                if (_TaskList.Count(p => !p.IsCompleted) >= DEFAULT_TASK_NUMBER)
                {
                    await Task.WhenAny(_TaskList.Where(p => !p.IsCompleted));

                    //Thread.Sleep(250);
                }

                try
                {
                    stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read, DEFAULT_BUFFER_SIZE, DEFAULTOPTIONS);
                    WordReader = new FileUniqueWordReader(WordProcessing);
                    WordReader.FileManager = new StreamReader(stream);
                    WordReader.Buffer_Size = DEFAULT_BUFFER_SIZE;
                    Task ReadTask = WordReader.ParseFileAsync();
                    _TaskList.Add(ReadTask);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to Load: " + FileName);
                }
              
            }

            await Task.WhenAll(_TaskList);

            foreach (var UniqueWord in UniqueWordsList)
                Console.WriteLine(UniqueWord.Value + ": " + UniqueWord.Key);

        }
    }
}




