using System.Threading.Tasks;
using Two_C_Two_P.Core.Interfaces.FileReaders;

namespace Two_C_Two_P.Core.Interfaces.Services
{
    public interface IUploadService
    {
        Task<TResult> UploadAsync<TResult>(byte[] bytes, IFileReader fileReader);
    }
}
