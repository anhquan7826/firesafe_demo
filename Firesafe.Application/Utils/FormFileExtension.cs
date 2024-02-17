using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Application.Utils;

public static class FormFileExtension
{
    public static string FileExtension(this IFormFile file)
    {
        var pattern = @"\.([^.]+)$";
        var regex = new Regex(pattern);
        var match = regex.Match(file.FileName);
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    public static string ReadAllText(this IFormFile file)
    {
        using var memStream = new MemoryStream();
        file.CopyTo(memStream);
        memStream.Position = 0;
        using var reader = new StreamReader(memStream);
        return reader.ReadToEnd();
    }
    
    public static async Task<string> ReadAllTextAsync(this IFormFile file)
    {
        using var memStream = new MemoryStream();
        await file.CopyToAsync(memStream);
        memStream.Position = 0;
        using var reader = new StreamReader(memStream);
        return await reader.ReadToEndAsync();
    }
}