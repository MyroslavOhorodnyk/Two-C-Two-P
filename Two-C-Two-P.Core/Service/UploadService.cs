using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Two_C_Two_P.Core.Interfaces;
using Two_C_Two_P.Core.Interfaces.FileReaders;
using Two_C_Two_P.Core.Interfaces.Services;
using Two_C_Two_P.Core.Models;

namespace Two_C_Two_P.Core.Service
{
    public class UploadService : IUploadService
    {
        private readonly IMapper _mapper;
        public IUnitOfWork _unitOfWork { get; set; }

        public UploadService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TResult> UploadAsync<TResult>(byte[] bytes, IFileReader fileReader)
        {
            var result = new ParseResult();

            using (Stream stream = new MemoryStream(bytes))
            {
                result = fileReader.ReadTransactions(stream);
            }

            if (result.IsSucceeded)
            {
                var existingTransactions = await _unitOfWork.TransactionRepository.ListAllAsync();

                foreach (var t in result.TransactionData)
                {
                    if (existingTransactions.Any(x => string.Equals(x.Id, t.Id)))
                    {
                        result.IsSucceeded = false;
                        result.Errors.Add($"Transaction with Id: {t.Id} is already stored.");
                    }
                }

                if (result.IsSucceeded)
                {
                    await _unitOfWork.TransactionRepository.AddRangeAsync(result.TransactionData.ToArray());
                    await _unitOfWork.CommitAsync();
                }
            }

            return _mapper.Map<TResult>(result);
        }
    }
}
