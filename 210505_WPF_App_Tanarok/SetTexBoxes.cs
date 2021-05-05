using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Controls;

namespace _210505_WPF_App_Tanarok
{
    public class SetTexBoxes
    {
        public static IEnumerable<TextBox> get_required_textboxes_by_tagname(StackPanel panel, string tagname)
        {
            return panel.Children.OfType<TextBox>().Where(x => x.Tag.ToString() == tagname);
        }

        public static List<TextBoxInput> set_textBoxInputs(StackPanel panel, string tagname)
        {
            var TextBoxInputs = new List<TextBoxInput>();
            IEnumerable<TextBox> textboxes = get_required_textboxes_by_tagname(panel, tagname);

            foreach (var item in textboxes)
            {
                var inputItem = new TextBoxInput(item.DataContext.ToString(), item);
                TextBoxInputs.Add(inputItem);
            }

            return TextBoxInputs;
        }

        public static int get_column_character_maximum_length(string TableName, string ColumnName)
        {
            using (var context = new TanarokDBEntities())
            {
                return context.Database.SqlQuery<int>(
                    "SELECT character_maximum_length FROM information_schema.columns WHERE table_name = @table AND COLUMN_NAME = @column",
                    new SqlParameter("table", TableName), new SqlParameter("column", ColumnName))
                    .FirstOrDefault();
            };
        }

        public static void set_maximum_length_of_inputs(List<TextBoxInput> TextBoxInputs, string TableName)
        {
            TextBoxInputs.ForEach(item =>
            {
                item.UIElement.MaxLength = get_column_character_maximum_length(TableName, item.DbAttribute);
            });
        }

    };
}
