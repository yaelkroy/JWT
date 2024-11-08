using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public class Program
{
    public static void Main()
    {
        Console.WriteLine(GenerateToken());
    }

    private static string GenerateToken()
    {
        const string clientID = "bfb9842f-99b1-4900-82fa-11fbae311937";
        const string secret = "c8291a4e-8789-4222-b690-dbbd4f8af98a";
        const string secretValue = "euI7uFgXHtHytBUOqbcIptFdPhef2GYGG/p1VtUMkJY=";
        const string username = "demedet";

        var tokenHandler = new JwtSecurityTokenHandler();

        //secret value
        var key = Encoding.ASCII.GetBytes(secretValue);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("sub",username)
                ,new Claim("aud","tableau")
                ,new Claim("jti",DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt"))
                ,new Claim("iss",clientID)
                ,new Claim("scp","tableau:views:embed")
                ,new Claim("scp","tableau:views:embed_authoring")
                ,new Claim("scp","tableau:views:metrics")
                ,new Claim("scp"," ")
            }),
            Expires = DateTime.UtcNow.AddMinutes(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        //client id
        token.Header.Add("iss", clientID);

        //secret id
        token.Header.Add("kid", secret);

        return tokenHandler.WriteToken(token);

    }
}