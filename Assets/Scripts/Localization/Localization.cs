using System;

namespace PixelFire.Localization
{
    public static class Localization
    {
        /*
         * Этот массив содержит весь текст, который нужно переводить.
         * Первая строка содержит обозначения языков.
         * Первый столбец - ключи для получения нужного текста.
        */
        private static string[,] _table =
        {
        {"", "ru", "en"},
        {"lang", "Язык", "Language"},
        {"time", "Время: ", "Time: "}
        };

        public static Language Language { get; private set; } = Language.ru;
        public static event Action OnLanguageChanged;

        public static void Initialize()
        {
            /*
             * Здесь может быть логика получения таблицы из файла.
             * Вызывать данный метод в точке входа.
             * При необходимости, передавать нужные параметры.
             */
        }

        public static string Get(string key)
        {
            int wordIndex = 0;
            for (int i = 1; i < _table.GetLength(1); i++)
            {
                if (key == _table[i, 0])
                {
                    wordIndex = i;
                    break;
                }
            }

            if (wordIndex == 0)
                throw new ArgumentException($"Key \"{key}\" not found!");

            return _table[wordIndex, (int)Language];
        }

        public static void ChangeLanguage(Language lang)
        {
            Language = lang;
            OnLanguageChanged?.Invoke();
        }

        public static void NextLanguage()
        {
            int i = (int)Language + 1;
            if (i > 2) // Размер enum'а.
                i = 1;
            ChangeLanguage((Language)i);
        }
    }

    public enum Language
    {
        ru = 1,
        en,
    }
}
