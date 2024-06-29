namespace Restate.WebApi.Application.Abstractions;

public interface IIdEncoder
{
    string EncodeId(int id);

    int DecodeId(string hashedId);
}
