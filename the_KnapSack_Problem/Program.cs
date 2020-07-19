using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace the_KnapSack_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            //входной параметр,для которого мы ищем ближайшую сумму
            decimal inputNumber = 0;

            //выходной параметр, ближайшая сумма к заданному числу
            decimal outputSum = 0;

            //путь к файлу с числами
            string path = @"nums.txt";

            //число строк в массиве
            int stringsInFile = 0;
            //строковый массив с числами из файла
            string[] readText = new string[stringsInFile];
            

            //читаем массив чисел из файла
            if (File.Exists(path))
            {
                readText = File.ReadAllLines(path);
            }
            else
            Console.WriteLine("Исходный файл с числами не найден." +"\n"+ "Поместите его в папку bin/Debug/netcoreapp3.1");

            //вычисляем его длину
            foreach (string str in readText)
            stringsInFile++;
            
            //создаем массив чисел inputDecimals, конвертируем в него строковый массив readText, он нужен для сортировки и работы
            decimal[] inputDecimals = new decimal[stringsInFile];

            //создаем массив чисел inputDecimalsClone, в нем порядок чисел не меняем,он нужен для поиска индексов ответов в конце
            decimal[] inputDecimalsClone = new decimal[stringsInFile];

            // используем formatter, чтобы при конвертации из строки в дробное число комп не ругался на "," или "."
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

            //преобразуем числа из строкового типа readText в числа decimal массивов inputDecimals и inputDecimalsClone
            for (int k = 0; k < stringsInFile - 1; k++)
            {
                if (readText[k] != null)
                {
                    inputDecimalsClone[k] = inputDecimals[k] = decimal.Parse(readText[k], formatter);
                }
            }

            //создаем лист с ответами 
            List<decimal> result = new List<decimal>();
            
            //здесь юзер вводит входной параметр(число)
            Console.WriteLine("Введите заданное число");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out inputNumber))
                inputNumber = Convert.ToInt32(input);
            else
                Console.WriteLine("Введите корректное число");

            //вводим краткое обозначение, чтобы постоянно не писать "inputDecimals.Length"
            int AL = inputDecimals.Length;
            //обнуляем все элементы, которые больше заданной суммы, экономим время многоуважаемого компьютера
            for (int i = 0; i < AL; i++)
            if (inputDecimals[i] >= inputNumber) inputDecimals[i] = 0;

            //сортировка пузырьком

            decimal temp;
            for (int i = 0; i < AL - 1; i++)
            {
                for (int j = i + 1; j < AL; j++)
                {
                    if (inputDecimals[i] > inputDecimals[j])
                    {
                        temp = inputDecimals[i];
                        inputDecimals[i] = inputDecimals[j];
                        inputDecimals[j] = temp;
                    }
                }
            }

            //лист для сортировки, в нем будет половина чисел из исходного массива, по величине наибольшие
            List<decimal> secondHalf = new List<decimal>();

            for (int i = AL / 2; i < AL; i++)
            {
                if (inputDecimals[i] > 0)
                {
                    secondHalf.Add(inputDecimals[i]);
                }
                
            }

            //лист для сортировки, в нем будет половина чисел из исходного массива, по величине наименьшие
            List<decimal> firstHalf = new List<decimal>();

            for (int i = 0; i < AL / 2; i++)
            {
                if (inputDecimals[i] > 0)
                {
                    firstHalf.Add(inputDecimals[i]);
                }
            }
              
            //от верха списка больших чисел спускаемся вниз
            for (int k = secondHalf.Count - 1; k >= 0; k--)
            {
                if (secondHalf[k] + outputSum <= inputNumber)
                {
                    outputSum += secondHalf[k];
                    result.Add(secondHalf[k]);
                }
            }
            
            //от низа списка малых чисел поднимаемся вверх
            for (int k = 0; k <= firstHalf.Count - 1; k++)
            {
                if (firstHalf[k] + outputSum <= inputNumber)
                {
                    outputSum += firstHalf[k];
                    result.Add(firstHalf[k]);
                }
            }

            //выведем на экран значения, для удобства проверки работы программы
            Console.WriteLine("Искомые значения:");

            for (int i = 0; i < result.Count ; i++)
            {
                Console.Write(result[i]+";  ");
            }
            Console.Write("в сумме " + outputSum);
            Console.WriteLine("\n"+"\n" +"Искомые индексы:");

            //проверим исходный массив и наши ответы на совпадение, получим индексы наших ответов

            for (int d = 0; d < inputDecimalsClone.Length; d++)
            {
                for (int v = 0; v < result.Count; v++)
                {
                    if (inputDecimalsClone[d] == result[v])
                    {
                        Console.Write(d+"; ");
                    }
                }
            }
            Console.WriteLine("\n"+ "\n" + "Если программа не вывела варианты, их нет");
            Console.ReadKey();
        }
    }
}
