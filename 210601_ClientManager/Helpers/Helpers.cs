using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _210601_ClientManager.Helpers
{
    class Helpers
    {
        public static bool areInputsEmpty(List<TextBox> Inputs)
        {
            var leave = false;
            Inputs.ForEach(input => {
                if (input.Text.Length == 0)
                {
                    leave = true;
                };
            });

            return leave;
        }


        public static bool isInputNumber(string textToInt)
        {

            if (int.TryParse(textToInt, out int number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool areInputsNumeric(List<TextBox> Inputs)
        {

            var isNumeric = true;

            foreach (var item in Inputs)
            {
                var isNumber = isInputNumber(item.Text);
                if (!isNumber)
                {
                    isNumeric = false;
                    break;
                }
                else
                {
                    isNumeric = true;
                }
            }

            return isNumeric;
        }


        public static string setWindowTitle(bool add, string text)
        {
            if (add)
            {
                return $"Add new {text}!";
            }
            else
            {
                return $"Edit {text}!";
            }
        }

        public static string setAcceptBtnText(bool add, string text)
        {
            if (add)
            {
                return $"Add new {text}!";
            }
            else
            {
                return "Save changes!";
            }
        }
    }
}
