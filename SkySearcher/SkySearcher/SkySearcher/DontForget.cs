using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearcher
{
    class DontForget
    {
        /// Получение всех свойств класса AD

        //var context = new DirectoryContext(DirectoryContextType.Forest, "dellin.local");

        //var shema = ActiveDirectorySchema.GetSchema(context);

        //var compClass = shema.FindClass("Computer");

        //    foreach(ActiveDirectorySchemaProperty t in compClass.GetAllProperties())
        //    {
        //        File.AppendAllText("Prop.txt", $"{t.Name}\n");
        //    }

        /// Вариант записи значения атрибута AD

        //DirectoryEntry entry = new DirectoryEntry();

        //DirectorySearcher search = new DirectorySearcher(entry, $"(&(ObjectClass=Computer)(Name={pcName}))");

        //SearchResult result = search.FindOne();

        //DirectoryEntry temp = result.GetDirectoryEntry();

        //    try
        //    {
        //        temp.Properties["description"].Value = $"{input}";
        //        temp.CommitChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }

        /// Вариант получения и сохранения имен свойств и их значения

        //foreach (PropertyValueCollection t in result.GetDirectoryEntry().Properties)
        //    {
        //        File.AppendAllText("test.txt", $"{t.PropertyName} - {t.Value}\n");
        //    }

        //class NameConverter : IValueConverter
        //{
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        if ((string)value == "cn")
        //        {
        //            return value = "Имя ПК";
        //        }
        //        else if ((string)value == "description")
        //        {
        //            return value = "Описание";
        //        }
        //        else
        //        {
        //            return value;
        //        }
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        return DependencyProperty.UnsetValue;
        //    }
        //}
    }
}
