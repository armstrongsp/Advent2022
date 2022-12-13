using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2022
{
    public enum Day13_ReturnState
    {
        Good = 1,
        Neutral = 2,
        Bad = 3
    }

    public class Day13_Data
    {
        public Day13_Data(string input)
        {
            number = -1;
            empty = false;
            if (input == "")
            {
                empty = true;
                return;
            }

            //it's just a number
            int temp;
            if (int.TryParse(input, out temp)) { number = temp; return; }

            //it's more than just a number
            values = new List<Day13_Data>();
            int depth = 0;
            int pos = 0;
            while (pos < input.Length)
            {
                if (depth == 0 && input[pos] == ',')
                {
                    values.Add(new Day13_Data(input.Substring(0, pos)));
                    input = input.Substring(pos + 1, input.Length - (pos + 1));
                    pos = -1;
                }
                else if (input[pos] == '[') { depth++; }
                else if (input[pos] == ']') { depth--; }

                pos++;
            }
            if (input.Length > 0 && input.StartsWith('[')) values.Add(new Day13_Data(input.Substring(1, input.Length - 2)));
            else if (input.Length > 0) values.Add(new Day13_Data(input));
        }

        public bool empty { get; set; }
        public int number { get; set; }
        public List<Day13_Data> values { get; set; }
    }
}
