using System;
using System.Globalization;
using System.IO;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Current directory is '{Environment.CurrentDirectory}'");



        }
        
        static double GetDouble()
        {
            while (true)
            {
                string str = Console.ReadLine();
                double num;
                if (Double.TryParse(str,NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num)) return num;
            }
        }

        // чтение из файла 
        static string ReadFromFile(string fullPath = @"D:\C#\Lab4\text.txt")
        {
            using (FileStream fstream = File.OpenRead(fullPath))
            {
                // преобразуем строку в байты 
                byte[] array = new byte[fstream.Length];
                // считываем данные 
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку 
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                return textFromFile;
            }
        }
    }
}
