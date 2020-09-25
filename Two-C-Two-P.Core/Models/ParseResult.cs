using System.Collections.Generic;
using Two_C_Two_P.Core.Entities;

namespace Two_C_Two_P.Core.Models
{
    public class ParseResult
    {
        private readonly List<string> _infos = new List<string>();
        private readonly List<string> _warnings = new List<string>();
        private readonly List<string> _errors = new List<string>();

        public bool IsSucceeded { get; set; }

        public ICollection<Transaction> TransactionData { get; set; } = new List<Transaction>();

        public IList<string> Info => _infos;

        public IList<string> Warnings => _warnings;

        public IList<string> Errors => _errors;
    }
}
