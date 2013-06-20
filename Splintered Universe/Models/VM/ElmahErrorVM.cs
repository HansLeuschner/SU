using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Cumis.Models.VM
{
    public class ElmahErrorVM
    {
        [Required]
        public Guid Id { get; set; }
        public string Application { get; set;}
        public string Host { get; set;}
        public string Type { get; set;}
        public string Source { get; set;}
        public string Message { get; set;}
        public string User { get; set;}
        public int StatusCode { get; set;}
        public DateTime TimeUTC { get; set;}
        public int Sequence { get; set;}
    }
}

