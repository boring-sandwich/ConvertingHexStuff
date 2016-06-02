using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertingStuff
{
    public class HexToDecimal
    {
        const decimal c_MB = 1048576;
        char[] characters;
        public bool CheckInputHEX(string hexInput)
        {
            if (hexInput.Length > 0)
                return true;
            else
                return false;
        }
        public bool CheckInputHex02(string hexInput)
        {
            hexInput = hexInput.ToLower().Trim();

            characters = hexInput.ToCharArray();
            foreach (var character in characters)
            {
                if (!character.Equals('a') && !character.Equals('b') &&
                    !character.Equals('c') && !character.Equals('d') &&
                    !character.Equals('e') && !character.Equals('f') &&
                    !char.IsNumber(character))
                    return false;
            }
            return true;
        }
        public bool CheckInputPercentage(int percentage)
        {
            if (percentage <= 0 || percentage >= 101)
                return false;
            else
                return true;
        }
        public void HexMethod(string hexInput, out decimal hexOutput)
        {
            double dblPower = 0;
            double currentDigit = 0;

            hexInput = hexInput.ToLower().Trim();
            characters = hexInput.ToCharArray();

            for (int i = hexInput.Length - 1; i >= 0; i--)
            {
                double dblStore;
                if (characters[i].Equals('a'))
                {
                    dblStore = 10;
                }
                else if (characters[i].Equals('b'))
                {
                    dblStore = 11;
                }
                else if (characters[i].Equals('c'))
                {
                    dblStore = 12;
                }
                else if (characters[i].Equals('d'))
                {
                    dblStore = 13;
                }
                else if (characters[i].Equals('e'))
                {
                    dblStore = 14;
                }
                else if (characters[i].Equals('f'))
                {
                    dblStore = 15;
                }
                else
                {
                    dblStore = double.Parse(characters[i].ToString());
                }
                currentDigit += dblStore * Math.Pow(16, dblPower);
                dblPower++;
            }
            hexOutput = (decimal)currentDigit;
        }
        public decimal HexSum(decimal hddCapacity, decimal hddCurrentAvailable, int percentage)
        {
            //find out what 1% of the hard drive is and then multiply by the percentage
            decimal hddCurrentUsed = hddCapacity - hddCurrentAvailable;
            hddCapacity = percentage * (hddCapacity / 100);
            hddCapacity = (hddCapacity - hddCurrentUsed) / c_MB; //minus the space already used and return the decimal remainder
            Math.Floor(hddCapacity);
            if (hddCapacity < 0)
            {
                hddCapacity = 0;
                return hddCapacity;
            }
            return hddCapacity;
        }

    }
}
