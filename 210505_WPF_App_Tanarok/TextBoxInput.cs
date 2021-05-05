using System.Windows.Controls;

namespace _210505_WPF_App_Tanarok
{
    public class TextBoxInput
    {
        public string DbAttribute { get; set; }
        public TextBox UIElement { get; set; }
        public TextBoxInput(string dbAttribute, TextBox textbox)
        {
            this.DbAttribute = dbAttribute;
            this.UIElement = textbox;
        }
    };
}
