using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Unit5._6
{
    internal class Program
    {
		public static void Main(string[] args)
		{
			var userInfo = GetUserInfo();

			Console.WriteLine(new string('=', 50));
			Console.WriteLine("ИНФОРМАЦИЯ О ПОЛЬЗОВАТЕЛЕ");

			Console.WriteLine($"Имя: {userInfo.firstName}");
			Console.WriteLine($"Фамилия: {userInfo.lastName}");
			Console.WriteLine($"Возраст: {userInfo.age}");
			Console.WriteLine($"Есть домашние животные: {userInfo.havePet}");
			if (userInfo.havePet)
			{
				foreach (var petName in userInfo.petsNames)
					Console.WriteLine($"\t{petName}");
			}
			Console.WriteLine("Любимые цвета:");
			foreach (var colorName in userInfo.colorsNames)
				Console.WriteLine($"\t{colorName}");

			Console.WriteLine("END.");
			Console.ReadLine();

		}

		static (string firstName, string lastName, double age, bool havePet, string[] petsNames, string[] colorsNames) GetUserInfo()
		{
			(string firstName, string lastName, double age, bool havePet, string[] petsNames, string[] colorsNames) userInfo;


			bool isValidFirstName;
			string firstName;
			do
			{
				Console.Write("1. Введите ваше имя: ");
				firstName = Console.ReadLine();
				isValidFirstName = ValidateString(firstName);
				Console.WriteLine();
			}
			while (!isValidFirstName);

			bool isValidLastName;
			string lastName;
			do
			{
				Console.Write($"{Environment.NewLine}2. Введите вашу фамилию: ");
				lastName = Console.ReadLine();
				isValidLastName = ValidateString(lastName);
				Console.WriteLine();
			}
			while (!isValidLastName);

			float age;
			do
			{
				Console.Write($"{Environment.NewLine}3. Введите ваш возраст: ");
				age = ValidateFloat(Console.ReadLine());
				Console.WriteLine();
			}
			while (age == 0);

			Console.WriteLine("4. У вас есть домашние животные?");
			bool havePets = ValidatePets(Console.ReadLine());
			List<string> petsNames = new List<string>();
			if (havePets)
			{
				bool isValidPetsCount;
				string petsCountStr;
				do
				{
					Console.Write($"{Environment.NewLine}4.1. Сколько у вас питомцев? ");
					petsCountStr = Console.ReadLine();
					isValidPetsCount = ValidateInt(petsCountStr);
					Console.WriteLine();
				}
				while (!isValidPetsCount);

				Console.Write($"{Environment.NewLine}4.2. Введите кличку каждого питомца (имя и нажать Enter)");
				petsNames = GetListOfNames(Convert.ToInt32(petsCountStr));
			}

			bool isValidcolorsCount;
			string colorsCountStr;
			List<string> colorsNames;
			do
			{
				Console.Write($"{Environment.NewLine}5. Сколько у вас любимых цветов? ");
				colorsCountStr = Console.ReadLine();
				isValidcolorsCount = ValidateInt(colorsCountStr);
				Console.WriteLine();
			}
			while (!isValidcolorsCount);

			Console.Write($"{Environment.NewLine}5.1. Введите название каждого цвета (наименование и нажать Enter)");
			colorsNames = GetListOfNames(Convert.ToInt32(colorsCountStr));


			userInfo.firstName = firstName;
			userInfo.lastName = lastName;
			userInfo.age = age;
			userInfo.havePet = havePets;
			userInfo.petsNames = petsNames.ToArray();
			userInfo.colorsNames = colorsNames.ToArray();
			return userInfo;
		}

		static List<string> GetListOfNames(int count)
		{
			List<string> names = new List<string>();

			for (int i = 0; i < count; i++)
			{
				Console.WriteLine(Environment.NewLine);
				names.Add(Console.ReadLine());
			}

			return names;
		}



		// Валидация имён
		static bool ValidateString(string str) => Regex.IsMatch(str, @"^[а-яА-Я]+$");

		// Валидация возраста
		static float ValidateFloat(string number)
		{
			float output = 0;

			if (Regex.Match(number, @"^[0-9,.]*$").Success)
				float.TryParse(number.Replace('.', ','), out output);

			return output > 0 && output < 100 ? output : 0;
		}

		// Валидация количества питомцев и любимых цветов
		static bool ValidateInt(string number) => number.All(char.IsDigit) && Convert.ToInt32(number) > 0;

		// Валидация наличия питомцев
		static bool ValidatePets(string value)
		{
			if (value.ToLower() == "да"
				|| value.ToLower() == "yes"
				|| value.ToLower() == "y"
				|| value == "+")
				return true;
			else
				return false;
		}
	}
}
