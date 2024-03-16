using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame.Core.Models
{
    public class Response
    {
        public int Round { get; set; }
        public List<Player>? players { get; set; }
        public string? Description { get; set; }
        public Error? Error { get; set; }
        public List<FluentValidation.Results.ValidationFailure> ValidationError { get; set; }
    }
    public class Error
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
    }
}
