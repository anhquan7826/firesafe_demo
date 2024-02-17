using Application.Utils;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Middlewares.Impl;

public class GoogleDocs : IGoogleDocs
{
    private readonly ILogger<GoogleDocs> _logger;
    private readonly DocsService _docsService;

    public GoogleDocs(IConfiguration configuration, ILogger<GoogleDocs> logger)
    {
        _logger = logger;
        _docsService = new DocsService(new BaseClientService.Initializer
        {
            ApplicationName = "Firesafe Vnex",
            HttpClientInitializer = GoogleCredential.FromJson(File.ReadAllText("test.json"))
        });
    }

    public void ReadDocs(string docId)
    {
        var d = _docsService.Documents.Get(docId).Execute();
        _logger.LogInformation(JsonConvert.SerializeObject(d));
        // d.Body.Content[1].Paragraph.Elements[1].InlineObjectElement.InlineObjectId;
        _logger.LogInformation(d.InlineObjects["kix.2n4pktnreif6"].InlineObjectProperties.EmbeddedObject.ImageProperties.ContentUri);
    }
}