using Donde.Augmentor.Core.Domain.Interfaces;
using System;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class Log : IDondeModel
    {
        public Guid Id { get; set; }
        public string Application { get; set; }
        public string LoggedDateUtc { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
    }
}
