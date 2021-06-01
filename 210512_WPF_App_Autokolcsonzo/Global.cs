using System.Collections.Generic;
using System.Windows.Controls;

namespace _210512_WPF_App_Autokolcsonzo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    class Global
    {
        public static bool isInputsEmpty(List<TextBox> Inputs)
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
    }
}
