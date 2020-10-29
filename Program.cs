using System;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = ReadInput();

            string result=Calculate(text);

            WriteOutput(result);
        }

        static string GetPath(string path= "input.txt")
        {
            string filePath = Environment.CurrentDirectory;
            filePath = @filePath.Substring(0, filePath.IndexOf("bin"))+ path;
            return filePath;
        }

        static void WriteInFile(string path,string text="")
        {
            if(text=="")text = Console.ReadLine();

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        //чтение из файла
        static string ReadInput()
        {
            string textFromFile;
            string filePath = GetPath();


            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
            }

            while (true)
            {
                using (FileStream fStream = File.OpenRead(filePath))
                {
                    // преобразуем строку в байты 
                    byte[] array = new byte[fStream.Length];
                    // считываем данные 
                    fStream.Read(array, 0, array.Length);
                    // декодируем байты в строку 
                    textFromFile = System.Text.Encoding.Default.GetString(array);
                    //textFromFile = textFromFile.Trim();
                }
                if (!IsInputCorrect(textFromFile))
                {
                    Console.WriteLine("Данные некорректны или файл пуст!");
                    Console.WriteLine("Введите новые данные:");
                    WriteInFile(filePath);
                }
                else return textFromFile;
            }
        }

        static bool IsInputCorrect(string input)
        {
            int count = 0;
            if (input == "") return false;
            else foreach(char ch in input)
                    if (ch == ' ' || ch == '*' || ch == '/' || ch == '+' || ch == '-' || ch==','||ch=='.'|| char.IsDigit(ch))count++;
            if (count == input.Length) return true;
            else return false;
        }

        static string Calculate(string input)
        {
            bool check = true;
            string[] arrayStr = input.Split(new char[] { '*', '/', '+', '-' });

            if (arrayStr.Length > 2) check=false;

            arrayStr[0] = arrayStr[0].Trim();
            arrayStr[1] = arrayStr[1].Trim();

            double num1;
            double num2;

            if (!double.TryParse(arrayStr[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num1)) check = false;
            if (!double.TryParse(arrayStr[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num2)) check = false;

            char sign = GetSign(input);
            if (sign == '/' && num2 == 0) check = false;

            if (check)
            {
                switch(sign)
                {
                    case '*':
                        return (num1 * num2).ToString();
                    case '-':
                        return (num1 - num2).ToString();
                    case '+':
                        return (num1 + num2).ToString();
                    case '/':
                        return (num1 / num2).ToString();
                    default:
                        return "Недопустимая операция!";
                }
            }
            else return "Недопустимая операция!";

        }
        static char GetSign(string str)
        {
            char[] array = { '*', '/', '+', '-' };
            foreach(char letter in str)
            {
                foreach (char ch in array)
                    if (letter == ch) return ch;
            }
            return '0';
        }

        static void WriteOutput(string text)
        {
            string path = GetPath("output.txt");
            WriteInFile(path, text);
        }
    }
}
