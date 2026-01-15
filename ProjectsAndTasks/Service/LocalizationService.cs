using System.Windows;

namespace ProjectsAndTasks.Service
{
    public class LocalizationService
    {
        public static void SetRussian()
        {
            Change("RussianDictionary");
        }
        public static void SetEnglish()
        {
            Change("EnglishDictionary");
        }
        private static void Change(string dictionaryName)
        {
            var newDict = new ResourceDictionary
            {
                Source = new Uri($"Resources/{dictionaryName}.xaml", UriKind.Relative)
            };
            var dictionaries = Application.Current.Resources.MergedDictionaries;
            var oldDict = dictionaries
                .FirstOrDefault(d => d.Source != null &&
                    (d.Source.OriginalString.Contains("RussianDictionary") ||
                     d.Source.OriginalString.Contains("EnglishDictionary")));
            if (oldDict != null)
                dictionaries.Remove(oldDict);
            dictionaries.Add(newDict);
        }
    }
}
