﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicLibrary;

namespace BotLibrary
{
    public class Counter : Bot
    {
        public Counter(int str)
            : base(str)
        {
        }
        
        public override int Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters, Shoes shoes)
        {
            double count = 2 * this.CounterWork(shoes);


            if (count <= -20) return 0;
            else if (count >= 20) return 2;
            else return base.Answer(hand, dealerHand, gamesters, shoes);
        }

        public double CounterWork(Shoes shoes)
        {
            double count = 0;
            for (int i = shoes.Current; i >= 0; i--)
            {
                int value = shoes.Queue[i].Value;
                if (value == 2 || value == 7) count += 0.5;
                else if (value == 3 || value == 4 || value == 6) count += 1;
                else if (value == 5) count += 1.5;
                else if (value == 9) count -= 0.5;
                else count -= 1;
            }
            return count;
        }
    }
}
