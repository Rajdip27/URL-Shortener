using System.Numerics;

namespace UrlShortener.Infrastructure.Utilities;

public class Base62Encoder
{
    /// <summary>
    /// The alphabet
    /// </summary>
    private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    // Base62 encoding is a way to represent binary data in a text format using 62 characters (0-9, a-z, A-Z).
    public string Encode(Guid guid)
    {
        var bytes = guid.ToByteArray();
        var big = new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray()); // ensure positive
        return EncodeBigInteger(big);
    }
    /// <summary>
    /// Encodes the big integer.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    private string EncodeBigInteger(BigInteger value)
    {
        var s = new System.Text.StringBuilder();
        while (value > 0)
        {
            var rem = (int)(value % 62);
            s.Insert(0, Alphabet[rem]);
            value /= 62;
        }
        var result = s.ToString();
        if (string.IsNullOrEmpty(result)) return "0";
        return result.Substring(0, Math.Min(8, result.Length)); // keep short
    }
}
