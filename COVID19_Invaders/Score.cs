using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19_Invaders
{
    [Serializable]
   public  class Score
    {
        public  int score { get; set; }

        public Score()
        {
            this.score = 0;
        }

        public void increment()
        {
            this.score++;
        }
    }
}
