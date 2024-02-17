using Application.Utils;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;

namespace Application.Middlewares.Impl;

public class GoogleSheets : IGoogleSheets
{
    public GoogleSheets(IConfiguration configuration)
    {
        var jsonReader = new JsonReader(File.ReadAllText(configuration["ServiceKeys"]!));
        _sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            ApplicationName = "Firesafe Vnex",
            ApiKey = jsonReader.Value("DocsApiKey"),
        });
    }

    private readonly SheetsService _sheetsService;

    public void ReadSheets(string docId)
    {
        throw new NotImplementedException();
    }
}