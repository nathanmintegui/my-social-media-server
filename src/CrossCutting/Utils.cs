using System.Text;
using static System.Security.Cryptography.MD5;
using static System.Text.Encoding;

namespace CrossCutting;

public static class Utils
{
    public static string Hash(string input)
    {
        using var md5Hash = Create();
        var data = md5Hash.ComputeHash(UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();

        foreach (var t in data) sBuilder.Append(t.ToString("x2"));

        return sBuilder.ToString();
    }
}