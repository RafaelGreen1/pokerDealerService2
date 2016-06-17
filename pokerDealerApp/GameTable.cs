using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokerDealerApp
{
    class GameTable
    {
        public GameTable ()
        {
            Id1 = Id2 = Id3 = Id4 = current_id = pot1 = pot2 = pot3 = pot4 = active = firstPlayer = currentFirstPlayer = 0;
            state = "clear";
        }
        public int Id1 { get; set; }
        public int Id2 { get; set; }
        public int Id3 { get; set; }
        public int Id4 { get; set; }
        public int current_id { get; set; }
        public int pot1 { get; set; }
        public int pot2 { get; set; }
        public int pot3 { get; set; }
        public int pot4 { get; set; }
        public int active { get; set; }
        public int firstPlayer { get; set; }
        public int currentFirstPlayer { get; set; }
        public string state { get; set; }
    }
}
