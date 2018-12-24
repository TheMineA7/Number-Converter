using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Number_Converter____Roman_to_Regular
{
    public partial class numberConverter : Form
    {
        public numberConverter()
        {
            InitializeComponent();
            PrivateFontCollection customFonts = new PrivateFontCollection();
            customFonts.AddFontFile(Application.StartupPath + "\\Roman Numerals.ttf");

            romanNumberBox.Font = new Font(customFonts.Families[0], 16, FontStyle.Regular);
            regulaNumberBox.Font = new Font(customFonts.Families[0], 16, FontStyle.Regular);
            outputLabel.Font = new Font(customFonts.Families[0], 16, FontStyle.Regular);
        }

        //Key filtering for the roman number textbox
        private void romanNumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*List<string> test = new List<string>();
            test.Add("A");
            string[] test2 = new string[4] {"a", "b", "c", "d"};*/
            string[] allowedKeysArray = new string[21] { "I, Shift", "V, Shift", "X, Shift", "L, Shift", "C, Shift", "D, Shift", "M, Shift", "I", "V", "X", "L", "C", "D", "M", "A, Control", "C, Control", "X, Control", "Back", "Delete", "Right", "Left" };
            string[] keyDataArray = new string[14] { "I, Shift", "V, Shift", "X, Shift", "L, Shift", "C, Shift", "D, Shift", "M, Shift", "I", "V", "X", "L", "C", "D", "M" };
            string[] romanNumbersArray = new string[14] { "I", "V", "X", "L", "C", "D", "M", "i", "v", "x", "l", "c", "d", "m" };
           
            string numberBoxText = romanNumberBox.Text;
            string enteredKey = e.KeyData.ToString();

            if (allowedKeysArray.Contains(enteredKey) == true)
            {
                //Checks if the entered value is a roman number
                //Also makes sure you are not going over the limit of 3 occurences for each roman number
                if (keyDataArray.Contains(enteredKey) == true)
                {
                    string keyPressed = romanNumbersArray[Array.IndexOf(keyDataArray, enteredKey)];
                    int letterCount = 0;

                    for (int i = 0; i < romanNumberBox.TextLength; i++)
                    {
                        if (numberBoxText.Substring(i, 1) == keyPressed)
                            letterCount++;
                    }
                    if (letterCount >= 3)
                        e.SuppressKeyPress = true;
                    else
                        e.SuppressKeyPress = false;
                }
                else
                    e.SuppressKeyPress = false;
            }
            else
                e.SuppressKeyPress = true;
        }

        //Prevents invalid input in the roman number textbox
        private void romanNumberBox_TextChanged(object sender, EventArgs e)
        {
            romanNumberBox.Text = ToRoman(ToRegular(romanNumberBox.Text));
        }

        //Key filtering for the regular number textbox
        private void regulaNumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            var eKeyData = e.KeyData;
            var eKeyValue = e.KeyValue;
            e.SuppressKeyPress = true;

            if ((eKeyValue >= 48 && eKeyValue <= 57) || (eKeyValue >= 96 && eKeyValue <= 105)
                || eKeyData == (Keys.Control | Keys.A) || eKeyData == (Keys.Control | Keys.C) || eKeyData == (Keys.Control | Keys.X)
                || eKeyValue == 8 || eKeyValue == 46 || eKeyValue == 37 || eKeyValue == 39)
            {
                //Checks if the key pressed is a number
                //Also makes sure user isn't going over the limit of 2999999
                if ((eKeyValue >= 48 && eKeyValue <= 57) || (eKeyValue >= 96 && eKeyValue <= 105))
                {
                    if (regulaNumberBox.Text.Length >= 6 && Convert.ToInt32(regulaNumberBox.Text) > 299999)
                        e.SuppressKeyPress = true;
                    else
                        e.SuppressKeyPress = false;
                }
                else
                    e.SuppressKeyPress = false;
            }
        }

        //Event handler for converting from roman numbers to regular numbers
        private void convertToRegular_Click(object sender, EventArgs e)
        {
            string romanNumber = romanNumberBox.Text;
            int regularNumber = ToRegular(romanNumber);
            outputLabel.Text = regularNumber.ToString();
        }

        //Method for converting from roman numbers to regular numbers
        private int ToRegular(string romanNumber)
        {
            int[] regularNumbersArray = new int[14] { 1, 5, 10, 50, 100, 500, 1000, 1000, 5000, 10000, 50000, 100000, 500000, 1000000 };
            string[] romanNumbersArray = new string[14] { "I", "V", "X", "L", "C", "D", "M", "i", "v", "x", "l", "c", "d", "m" };

            int value = 0;
            int nextValue = 0;
            int romanNumberLength = romanNumber.Length;
            int regularNumber = 0;

            for (int i = 0; i < romanNumberLength; i++)
            {
                value = regularNumbersArray[Array.IndexOf(romanNumbersArray, romanNumber.Substring(i, 1))];

                if (i < romanNumberLength - 1)
                {
                    char nextCharacter = romanNumber[i + 1];
                    nextValue = regularNumbersArray[Array.IndexOf(romanNumbersArray, romanNumber.Substring(i + 1, 1))]; ;
                }

                if (nextValue <= value)
                    regularNumber = regularNumber + value;
                else
                    regularNumber = regularNumber - value;
            }
            return regularNumber;
        }

        //Event handler for converting from regular numbers to roman numbers
        private void convertToRoman_Click(object sender, EventArgs e)
        {
            int regularNumber = Convert.ToInt32(regulaNumberBox.Text);
            string romanNumber = ToRoman(regularNumber);
            outputLabel.Text = romanNumber;
        }

        //Method for converting from regular numbers to roman numbers
        private string ToRoman(int regularNumber)
        {
            int[] regularNumbersArray = new int[25] {1000000, 900000, 500000, 400000, 100000, 90000, 50000, 40000, 10000, 9000, 5000, 4000, 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5 , 4, 1 };
            string[] romanNumbersArray = new string[25] { "m", "cm", "d", "cd", "c", "xc", "l", "xl", "x", "ix", "v", "iv", "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            string romanNumber = "";
            int remainder = regularNumber;
            int quotient = 0;

            for(int i = 0; i < regularNumbersArray.Length; i++)
            {
                quotient = remainder / regularNumbersArray[i];
                remainder = remainder % regularNumbersArray[i];

                for (int j = 0; j < quotient; j++)
                    romanNumber = romanNumber + romanNumbersArray[i];
            }
            return romanNumber;
        }
    }
}
