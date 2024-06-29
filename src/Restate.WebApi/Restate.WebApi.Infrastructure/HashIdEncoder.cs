using Microsoft.Extensions.Configuration;
using Restate.WebApi.Application.Abstractions;
using Sqids;

namespace Restate.WebApi.Infrastructure;

internal class HashIdEncoder : IIdEncoder
{
    private readonly SqidsEncoder<int> _sqids;

    public HashIdEncoder(IConfiguration configuration)
    {
        var alphabet = configuration.GetValue<string>("HashIds:Alphabet")
            ?? throw new Exception("Configuration for {HashIds:Alphabeth} is missing");

        _sqids = new SqidsEncoder<int>(new()
        {
            Alphabet = alphabet,
            MinLength = 8,
        });
    }

    public string EncodeId(int id) => _sqids.Encode(id);

    public int DecodeId(string hashedId) => _sqids.Decode(hashedId).Single();
}
