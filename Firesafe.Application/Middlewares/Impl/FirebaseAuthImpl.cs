using System.Collections;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using JsonReader = Application.Utils.JsonReader;

namespace Application.Middlewares.Impl;

public class FirebaseAuthImpl : IFirebaseAuth
{
    private readonly FirebaseAuthClient _client;

    public FirebaseAuthImpl(IConfiguration configuration)
    {
        var jsonReader = new JsonReader(File.ReadAllText(configuration["ServiceKeys"]!));

        var config = new FirebaseAuthConfig
        {
            ApiKey = jsonReader.Value("FirebaseAuthClient", "apiKey"),
            AuthDomain = jsonReader.Value("FirebaseAuthClient", "authDomain"),
            Providers =
            [
                new GoogleProvider().AddScopes("email"),
                new EmailProvider()
            ]
        };
        _client = new FirebaseAuthClient(config);
    }

    public async Task<string?> SignInWithEmailAndPassword(string email, string password)
    {
        var userCredential = await _client.SignInWithEmailAndPasswordAsync(email, password);
        return userCredential?.User.Credential.IdToken;
    }
}