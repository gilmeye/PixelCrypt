using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    [Serializable]
    internal class Next
    {
        public string word;
        public int count;

        public Next(string word)
        {
            this.word = word;
            this.count = 1;
        }

        public Next(string word, int count)
        {
            this.word = word;
            this.count = count;
        }

        public void inc()
        {
            this.count++;
        }
    }
}
