using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

StreamReader filein1 = new StreamReader("/home/ob3r0n/Desktop/lab8.1.12");
string text1 = filein1.ReadToEnd();
Console.WriteLine(text1);
filein1.Close();
string pattern = @"[\d]{4}[.][\d]{2}[.][\d]{2}[:][\d]{2}[:][\d]{2}[:][\d]{2}";

MatchCollection matches = Regex.Matches(text1, pattern, RegexOptions.Multiline);
Console.WriteLine("Matches: " + matches.Count);
var dates = new List<string>();

foreach (Match match in matches)
{
    string patt2 = @"(\d+)[.](\d+)[.](\d+)[:](\d+)[:](\d+)[:](\d+)";
    Match m = Regex.Match(match.Value, patt2);
    if (m.Success)
    {
        var year = int.Parse(m.Groups[1].Value);
        if (year < 1900 || year > 2099)
            continue;
        var month = int.Parse(m.Groups[2].Value);
        if (month < 1 || month > 12)
            continue;
        var day = int.Parse(m.Groups[3].Value);
        if (day < 1 || day > 31)
            continue;

        var hour = int.Parse(m.Groups[4].Value);
        if (hour < 00 || hour > 24)
            continue;
        var min = int.Parse(m.Groups[5].Value);
        if (min < 00 || min > 60)
            continue;
        var sec = int.Parse(m.Groups[6].Value);
        if (sec < 00 || sec > 60)
            continue;

        Console.WriteLine($"Normal date: {year}.{month}.{day} {hour}:{min}:{sec}");
        dates.Add($"{year}.{month}.{day} {hour}:{min}:{sec}");
    }
}

Console.WriteLine("1. Нічого не змінювати");
Console.WriteLine("2. Видалити дату.");
Console.WriteLine("3. Замінити дату.");
int.TryParse(Console.ReadLine(), out var input);
switch (input)
{
    case 1:
    {
        break;
    }
    case 2:
    {
        Console.WriteLine($"Розмір масиву: {dates.Count}");
        Console.WriteLine("Виберіть, який індекс видалити: ");
        int.TryParse(Console.ReadLine(), out var toRemove);
        dates.RemoveAt(toRemove);
        break;
    }
    case 3:
    {
        Console.WriteLine($"Розмір масиву: {dates.Count}");
        Console.WriteLine("Виберіть, який індекс змінити: ");
        int.TryParse(Console.ReadLine(), out var toChange);
        Console.WriteLine("Введіть нову дату: ");
        dates[toChange] = Console.ReadLine();
        break;
    }
}
var fileout = new StreamWriter(new FileStream("/home/ob3r0n/Desktop/lab8.1.12.Out",
    FileMode.Create,
    FileAccess.Write));
foreach(string date in dates) {
    fileout.WriteLine(date);
}
fileout.Close();

StreamReader filein2 = new StreamReader("/home/ob3r0n/Desktop/lab8.2.12");
string text2 = filein2.ReadToEnd();
Console.WriteLine($"Original text: {text2}");
filein2.Close();
string pattern2ForDelete = @"\s?\b[re]\w+|\s?\b[not]\w+|\s?\b[be]\w+";
string text2WithRemovedWords = Regex.Replace(text2, pattern2ForDelete, "");
Console.WriteLine($"Text with removed words: {text2WithRemovedWords}");
string pattern2ForEdit = @"\sне";
string text2Edited = Regex.Replace(text2WithRemovedWords, pattern2ForEdit, " not");
Console.WriteLine($"Text edited: {text2Edited}");
var fileout2 = new StreamWriter(new FileStream("/home/ob3r0n/Desktop/lab8.2.12.Out",
    FileMode.Create,
    FileAccess.Write));
fileout2.WriteLine(text2Edited);
fileout2.Close();

StreamReader filein31 = new StreamReader("/home/ob3r0n/Desktop/lab8.3.12.1");
string text31 = filein31.ReadToEnd();
StreamReader filein32 = new StreamReader("/home/ob3r0n/Desktop/lab8.3.12.2");
string text32 = filein32.ReadToEnd();
string text32Copy = text32;
Console.WriteLine(text31);
filein1.Close();
string pattern3 = @"\w+";
MatchCollection matches3 = Regex.Matches(text31, pattern3, RegexOptions.Multiline);
foreach (Match match in matches3)
{
    text32Copy = Regex.Replace(text32Copy, $@"\s?{match.Value}", "");
}
Console.WriteLine($"Stock text1: {text31}");
Console.WriteLine($"Stock text2: {text32}");
Console.WriteLine($"Edited text2: {text32Copy}");
var fileout3 = new StreamWriter(new FileStream("/home/ob3r0n/Desktop/lab3.12.Out",
    FileMode.Create,
    FileAccess.Write));
fileout3.WriteLine(text32Copy);
fileout3.Close();

Console.WriteLine("Введіть n: ");
int.TryParse(Console.ReadLine(), out var n);
var numbers = new List<double>();
for (var i = 0; i < n; i++)
{
    numbers.Add(int.Parse(Console.ReadLine()));
}
var fileout4 = new BinaryWriter(new FileStream("/home/ob3r0n/Desktop/lab8.4.12In",
    FileMode.Create,
    FileAccess.Write));
foreach(double number in numbers) {
    fileout4.Write(number);
}
fileout4.Close();
BinaryReader filein4 = new BinaryReader(new FileStream("/home/ob3r0n/Desktop/lab8.4.12In", FileMode.Open));
var readNumbers = new List<double>();
double result = 0;
for (var i = 0; i < numbers.Count; i++)
{
    readNumbers.Add(filein4.ReadDouble());
    if (i % 2 == 0) result += readNumbers.Last();
}
result /= Math.Ceiling(numbers.Count/2.0);
Console.WriteLine($"Result: {result}");