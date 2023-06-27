using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueWordsCalculator
{
    public class FileUniqueWordReader
    {
        private static int _Default_Buffer_Size = 1024;

        private static IUniqueWordProcessingy _WordProcessor;
        private static StreamReader _FileManager;

        public int Buffer_Size { get; set; } = _Default_Buffer_Size;

        public StreamReader FileManager { set { _FileManager = value; } }

        public FileUniqueWordReader(IUniqueWordProcessingy WordProcessor) {
            _WordProcessor = WordProcessor;
        }

        public Task<bool> ParseFileAsync()
        {
            char[] Buffer = new char[_Default_Buffer_Size];

            int NumRead;
            string TempWord = "";
            using (_FileManager)
            {
                while ((NumRead = _FileManager.ReadBlock(Buffer, 0, Buffer.Length)) > 0)
                {
                    for (int i = 0; i < NumRead; i++)
                    {
                        if (Char.IsWhiteSpace(Buffer[i]) || !Char.IsAsciiLetterOrDigit(Buffer[i]))
                        {
                            if (TempWord.Length > 0)
                            {
                                TempWord = TempWord.Trim();
                                _WordProcessor.ProcessWord(TempWord);
                                TempWord = "";
                            }
                        }
                        else
                            TempWord += Buffer[i];
                    }
                }
                TempWord = TempWord.Trim();
                if (TempWord.Length > 0)
                    _WordProcessor.ProcessWord(TempWord);
            }
            return Task.FromResult(true);
        }
    }
}
