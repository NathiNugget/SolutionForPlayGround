// See https://aka.ms/new-console-template for more information
using ExampleExam;
using System.Net.Security;

Console.WriteLine("Hello, World!");
PlayGroundsDB db = new PlayGroundsDB();
foreach (var item in db.GetAll())
{
    Console.WriteLine(item);
}
