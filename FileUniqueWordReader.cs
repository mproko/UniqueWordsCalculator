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

        private IUniqueWordProcessingy _WordProcessor;
        private StreamReader _FileManager;

        public StreamReader FileManager { set { _FileManager = value; } }

        public FileUniqueWordReader(IUniqueWordProcessingy WordProcessor, int Default_Buffer_Size) {
            _WordProcessor = WordProcessor;
            _Default_Buffer_Size = Default_Buffer_Size;
        }

        public int ParseFile()
        {
            int Count = 0;
            using (_FileManager)
            {
                char[] Buffer = new char[_Default_Buffer_Size];

                int NumRead;
                string TempWord = "";
                while ((NumRead = _FileManager.ReadBlock(Buffer, 0, Buffer.Length)) > 0)
                {
                    if (NumRead < Buffer.Length) { Array.Resize<char>(ref Buffer, NumRead); }
                    foreach (char c in Buffer)
                    {
                        if (Char.IsWhiteSpace(c) || !Char.IsAsciiLetterOrDigit(c))
                        {
                            if (TempWord.Length > 0)
                            {
                                TempWord = TempWord.Trim();
                                _WordProcessor.ProcessWord(TempWord);
                                Count++;
                                TempWord = "";
                            }    
                        }
                        else
                            TempWord = TempWord + c;
                    }
                }
                TempWord = TempWord.Trim();
                if (TempWord.Length > 0)
                {
                    _WordProcessor.ProcessWord(TempWord);
                    Count++;
                }
                _FileManager.Close();
            }
            return Count;
        }
    }
}
