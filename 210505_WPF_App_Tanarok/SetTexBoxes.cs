using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _210505_WPF_App_Tanarok
{


    /*
    
    0) Az adattábla attribútumainak megfelelően a TextBoxok létrehozásakor feltétlenül szükséges beállítani a következő propertyket:
           1. DataContext="tábla.oszlopnév"
           2. Tag="req" (Lehet bármi, lényeg, hogy később ezt adjuk meg paraméterként!)
       Az adott TextBox elemek egy StackPanelen kell hogy elhelyezkedjenek. 

    1) get_required_textBoxes_by_tagname
       A metódus egy IEnumerable<TextBox> típusú objetummal tér vissza, amibe összegyűjti a beviteli paraméterek szerinti StackPanelen elhelyezett TextBoxokat, az előre beállított Tag proppertyk alapján. (pl. Tag="req")

    2) set_textBox_inputs
       A metódus a korábban definiált TextBoxInput Class adatstruktúrája alapján az első lépésben begyűjtött TextBoxokat egy objetumba gyűjti össze, 2 fontos propertyt beállítva.
           1. A DbAttribute property a TextBox előre definiált DataContext értéke lesz, ami nem más, mint a beviteli mező adatbázisbeli attribútumának megfelelője. ( tábla oszlop név = input mező DataContext)
           2. A UIElement property maga TextBox maga a TextBox, mint elem.
       Visszatérési értéke egy <TextBoxInput> típusú lista.

    3) get_column_character_maximum_length
       A metódus egy SqlQuery alapján kéri le a paramterként megadott tábla, és oszlopnevek alapján ezek előre definiált adattípus hosszát. Visszatérési értéke egy szám. 
    
    4) set_maximum_length_of_inputs
       Paraméterként a mintaként szolgáló adattábla neve kerül átadásra, illetve a 2. pontban elkészült <TextBoxInput> típusú lista. A listán végigiterálva az elemek DbAttribute tulajdonságai alapján beállítja
       a TextBoxok megengedett maximális karakterhosszát a MaxLength propertyvel.
               
     */


    public class SetTexBoxes
    {

        public static IEnumerable<TextBox> get_required_textBoxes_by_tagname(string tagname, StackPanel panel )
        {
           return panel.Children.OfType<TextBox>().Where(x => x.Tag.ToString() == tagname);
        }

        public static List<TextBoxInput> set_textBox_inputs(string tagname, StackPanel panel)
        {
            var TextBoxInputs = new List<TextBoxInput>();
            IEnumerable<TextBox> textboxes = get_required_textBoxes_by_tagname(tagname, panel);

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
