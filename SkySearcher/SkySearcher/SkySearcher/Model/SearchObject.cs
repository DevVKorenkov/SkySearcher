using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SkySearcher.Model
{
    class SearchObject
    {
        int i = 0;

        public delegate void ProgressBarDelegate(int count);
        public event ProgressBarDelegate ProgressBarCountEvent;
        public delegate void SearchErrorDelegate(string errorMsg);
        public event SearchErrorDelegate SearchErrorEvent;

        /// <summary>
        /// Метод поиска ПК в AD
        /// </summary>
        /// <param name="pcName"></param>
        /// <returns></returns>
        public List<DirectoryEntry> SearchPc(List<string> pcName)
        {
            pcName.RemoveAll(new Predicate<object>(DepEmpty));

            string msg = string.Empty;

            DirectoryEntry entry = new DirectoryEntry();

            // Коллекция свойств из AD по введенным ПК
            List<DirectoryEntry> temp = new List<DirectoryEntry>();

            // Цикл получения свойств по каждому отдельному ПК
            foreach (string t in pcName)
            {
                DirectorySearcher search = new DirectorySearcher(entry, $"(&(ObjectClass=Computer)(Name={t}))");

                // Результат поиска ПК
                SearchResult result = search.FindOne();

                if (result != null)
                    temp.Add(result.GetDirectoryEntry());
                else
                {
                    msg = msg + $"{t}; ";

                    continue;
                }
            }

            if (!string.IsNullOrEmpty(msg))
                SearchErrorEvent?.Invoke(msg);

            entry.Close();

            return temp;
        }

        /// <summary>
        /// Метод поиска ПК в AD
        /// </summary>
        /// <param name="excels"></param>
        /// <returns></returns>
        public List<DirectoryEntry> SearchPc(List<ExcelData> excels)
        {
            excels.RemoveAll(new Predicate<object>(DepEmpty));

            string msg = string.Empty;

            DirectoryEntry entry = new DirectoryEntry();

            List<DirectoryEntry> temp = new List<DirectoryEntry>();

            foreach (ExcelData t in excels)
            {
                DirectorySearcher search = new DirectorySearcher(entry, $"(&(ObjectClass=Computer)(Name={t.PcName}))");

                SearchResult result = search.FindOne();

                if (result != null)
                    temp.Add(result.GetDirectoryEntry());
                else
                {
                    msg = msg + $"{t.PcName}; ";

                    continue;
                }
            }

            if (!string.IsNullOrEmpty(msg))
                SearchErrorEvent?.Invoke(msg);

            entry.Close();

            return temp;
        }

        /// <summary>
        /// Медо записи даных в AD
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="entries"></param>
        public async Task InputSomePcInv(ObservableCollection<AttributeValueObject> attributes, List<DirectoryEntry> entries, List<PcSaveErrors> pcSaveErrors)
        {
            foreach (var t in attributes)
            {
                DirectoryEntry temp = new DirectoryEntry();

                // Выбор каждого отдельного ПК
                await Task.Run(() => temp = entries.FirstOrDefault(x => x.Name.Contains(t.AttributePcValue)));

                string tempSelectedDepInv = t.SelectDepInv;

                if (temp != null)
                {
                    if(int.TryParse(t.InputInv, out _))
                    {
                        t.InputInv = CheckSelectedDep(tempSelectedDepInv, t.InputInv);
                    }
                    else
                    {
                        pcSaveErrors.Add(new PcSaveErrors(DateTime.Now.ToString("dd.MM.yyyy hh:mm"), t.AttributePcValue, $"{t.InputInv} Не должен содержать символы."));
                        continue;
                    }

                    try
                    {
                        SearchResultCollection tempList = CheckForDouble(t.InputInv);

                        if (string.IsNullOrEmpty(tempSelectedDepInv))
                            //throw new NullException("Не выбрана система учета");
                            pcSaveErrors.Add(new PcSaveErrors(DateTime.Now.ToString("dd.MM.yyyy hh:mm"), t.AttributePcValue, "Не выбрана система учета"));
                        else if (string.IsNullOrEmpty(t.InputInv))
                            //throw new NullException("Не заполнен инвентерный номер");
                            pcSaveErrors.Add(new PcSaveErrors(DateTime.Now.ToString("dd.MM.yyyy hh:mm"), t.AttributePcValue, "Не заполнен инвентерный номер"));
                        else if (tempList.Count > 0)
                        {
                            string msg = string.Empty;

                            foreach (SearchResult i in tempList)
                            {
                                msg = msg + $"{i.GetDirectoryEntry().Name.Remove(0, 3)}\n";
                            }

                            pcSaveErrors.Add(new PcSaveErrors(DateTime.Now.ToString("dd.MM.yyyy hh:mm"), t.AttributePcValue, $"Дублирование инвентарных номеров.\nИнвентарный номер {t.InputInv} применен к:\n{msg}"));
                        }
                        else
                        {
                            await InputInv(temp, t.InputInv);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        private SearchResultCollection CheckForDouble(string inputInv)
        {
            DirectoryEntry entry = new DirectoryEntry();

            DirectorySearcher search = new DirectorySearcher(entry, $"(&(ObjectClass=Computer)(extensionAttribute7={inputInv}))");

            return search.FindAll();
        }

        private string CheckSelectedDep(string tempSelectedDepInv, string tempInv)
        {
            switch (tempSelectedDepInv.ToLower())
            {
                case "дл транс":
                    tempSelectedDepInv = "ДТ";

                    if (tempInv.ToLower().StartsWith(tempSelectedDepInv.ToLower()))
                        tempInv = tempInv.Remove(0, tempSelectedDepInv.Length);

                    else if (tempInv.StartsWith("600"))
                        tempInv = tempInv.Remove(0, 3);

                    break;
                case "колл центр":
                    tempSelectedDepInv = "КЦ";

                    if (tempInv.ToLower().StartsWith(tempSelectedDepInv.ToLower()))
                        tempInv = tempInv.Remove(0, tempSelectedDepInv.Length);

                    else if (tempInv.ToLower().StartsWith("296cc"))
                        tempInv = tempInv.Remove(0, 5);

                    else if (tempInv.ToLower().StartsWith("296сс"))
                        tempInv = tempInv.Remove(0, 5);

                    else if (tempInv.StartsWith("296"))
                    {
                        tempInv = tempInv.Remove(0, 3);
                        tempSelectedDepInv = string.Empty;
                    }

                    break;

                case "грузоперевозки":
                    tempSelectedDepInv = "296";

                    if (tempInv.ToLower().StartsWith(tempSelectedDepInv))
                        tempInv = tempInv.Remove(0, tempSelectedDepInv.Length);

                    tempSelectedDepInv = string.Empty;

                    break;
                default:
                    tempSelectedDepInv = null;
                    break;
            }

            return tempSelectedDepInv + tempInv;
        }

        /// <summary>
        /// Метод записи инвентарного номера в AD
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="input"></param>
        public async Task InputInv(DirectoryEntry entry, string input)
        {
            try
            {
                
                // Присваивание значения определенному атрибуту в AD
                entry.Properties["extensionAttribute7"].Value = $"{input}";
                // Сохранение данных в AD
                await Task.Run(() => entry.CommitChanges());

                ProgressBarCountEvent?.Invoke(i += 1);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Метод присваивания данных экзкмпляру класса AttributeValueObject для вывода в форму и изменения данных
        /// </summary>
        /// <param name="entrie"></param>
        /// <param name="attribute"></param>
        /// <param name="depNum"></param>
        /// <param name="invNum"></param>
        /// <returns></returns>
        public AttributeValueObject AddAttributeValue(DirectoryEntry entrie, AttributeValueObject attribute, string depNum, string invNum)
        {
            attribute = new AttributeValueObject();

            // Условие заполнения инвертарного номера для отображения
            if (string.IsNullOrWhiteSpace(entrie.Properties["extensionAttribute7"].Value?.ToString()))
            {
                attribute.AttributeDescValue = "Не заполнено";
            }
            else
            {
                attribute.AttributeDescValue = entrie.Properties["extensionAttribute7"].Value.ToString();
            }

            // Присваивание значения сойств для вывода в форму
            attribute.AttributePcName = entrie.Properties["cn"].PropertyName;
            attribute.AttributePcValue = entrie.Properties["cn"].Value.ToString();
            attribute.AttributeDescName = entrie.Properties["extensionAttribute7"].PropertyName;
            attribute.SelectDepInv = depNum;
            attribute.InputInv = invNum;

            return attribute;
        }

        private bool DepEmpty(object item)
        {
            bool isEmpty = false;

            if (item is string tempStringItem)
            {
                isEmpty = string.IsNullOrWhiteSpace(tempStringItem);
            }
            if (item is ExcelData tempExceleItem)
            {
                isEmpty = string.IsNullOrWhiteSpace(tempExceleItem.PcName);
            }

            return isEmpty;
        }

        private bool CheckSymbols(string inputInv)
        {
            bool output = true;

            foreach(var t in inputInv)
            {
                if(char.IsDigit(t))
                {
                    output = false;
                    break;
                }
            }

            return output;
        }
    }
}
