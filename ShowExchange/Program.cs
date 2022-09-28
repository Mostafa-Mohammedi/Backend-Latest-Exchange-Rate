using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using RestSharp;
using ViewModels;

while (true)
{
    Console.WriteLine("\nPlease enter a currency : \n");

    var input = Console.ReadLine();

    int currency_Code_Name = 3;
    
    while (input.Length != currency_Code_Name)
    {
        Console.WriteLine("\n invalid currency \n");
        input = Console.ReadLine();
    }

    var client = new RestClient($"https://localhost:7030/CurrencyApi/GetCurrency?baseName={input}");
    client.Timeout = -1;
    var request = new RestRequest(Method.GET);

    IRestResponse response = client.Execute(request);

    UpdateViewModel update = JsonConvert.DeserializeObject<UpdateViewModel>(response.Content);

    Console.WriteLine("\n ----------Results--------");

    Console.WriteLine(" \n Currency " + update.Base);
    
    Console.WriteLine("\n Timespan: " + update.Timestamp);
    Console.WriteLine("\n Date " + update.DateUpdate);


    Console.WriteLine("Press Enter to check for a specific currency or spacebar to check all currency: ");
    var userChoiceInput = Console.ReadKey();



    if(userChoiceInput.Key == ConsoleKey.Enter)
    {
        Console.WriteLine("Press short code currency name 3 digit: ");
        var currencyUserSearch = Console.ReadLine();

        foreach (var item in update.RatesUpdates)
        {
            if (item.Currency == currencyUserSearch.ToUpper())
            {
                Console.WriteLine($"{item.Currency} : {item.Amount}");
            }
        }

    }

    else if (userChoiceInput.Key == ConsoleKey.Spacebar)
    {
        foreach (var item in update.RatesUpdates)
        {
            Console.WriteLine($"{item.Currency} : {item.Amount}");   
        }
    }

    Console.WriteLine("\n Do you wanna stopp press enter: or press space to continue");
    var ReadKeyValue = Console.ReadKey();
    if (ReadKeyValue.Key == ConsoleKey.Enter)
    {
        break;
    }
    else if(ReadKeyValue.Key == ConsoleKey.Spacebar)
    {
        Console.Clear();
    }
    else
    {
        Console.WriteLine(" \n Invalid input please \n");
    }
}




