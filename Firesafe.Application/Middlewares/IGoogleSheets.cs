using Google.Apis.Sheets.v4.Data;

namespace Application.Middlewares;

public interface IGoogleSheets
{
    public ValueRange ReadSheets(string sheetId, string range);
}