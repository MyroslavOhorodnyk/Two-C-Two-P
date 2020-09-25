using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Two_C_Two_P.Core.Interfaces;
using Two_C_Two_P.Core.Interfaces.Services;

namespace Two_C_Two_P.Core.Service
{
    public class TransactionService : ITransactionService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TResult>> GetAsync<TResult>()
        {
            var transactions = await _unitOfWork.TransactionRepository.ListAllAsync();

            return _mapper.Map<IEnumerable<TResult>>(transactions);
        }

        public Task<TResult> GetAsync<TResult>(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
