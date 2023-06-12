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

        public UniqueWordProcessing (Dictionary<string, int> UniqueWordsList) 
        { 
            this.UniqueWordsList = UniqueWordsList;
        }

        public void ProcessList(List<string> Words)
        {
            Words.ForEach(w => 
            {
                if (UniqueWordsList.ContainsKey(w))
                    UniqueWordsList[w]++;
                else
                    UniqueWordsList.Add(w, 1);
            });
        }

        public void ProcessWord(string Word)
        {
            if (UniqueWordsList.ContainsKey(Word))
                UniqueWordsList[Word]++;
            else
                UniqueWordsList.Add(Word, 1);
        }

    }





}
