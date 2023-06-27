using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueWordsCalculator
{
    public interface IUniqueWordProcessingy
    {
        void ProcessList(List<string> Words);
        void ProcessWord(string Word);
    }

    public class UniqueWordProcessing: IUniqueWordProcessingy
    {
        Dictionary<string, int> UniqueWordsList;
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        public UniqueWordProcessing (Dictionary<string, int> UniqueWordsList) 
        { 
            this.UniqueWordsList = UniqueWordsList;
        }

        public void ProcessList(List<string> Words)
        {
            int ovalue;
            cacheLock.EnterReadLock();
            try
            {
                Words.ForEach(w => 
                {
                    if (UniqueWordsList.TryGetValue(w, out ovalue))
                        UniqueWordsList[w]++;
                    else
                        UniqueWordsList.Add(w, 1);
                });
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public void ProcessWord(string Word)
        {
            int ovalue;
            cacheLock.EnterReadLock();
            try
            {
                if (UniqueWordsList.TryGetValue(Word, out ovalue))
                    UniqueWordsList[Word]++;
                else
                    UniqueWordsList.Add(Word, 1);
            }
            finally
            {
                cacheLock.ExitReadLock();
            }

        }

    }





}
