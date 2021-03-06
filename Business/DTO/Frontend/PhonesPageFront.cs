using Application.DTO.Frontend.Forms;
using Database.Models;
using System.Collections.Generic;

namespace Application.DTO.Frontend
{
    public class PhonesPageFront
    {
        public int TotalPhones { set; get; } = 0;
        public int TotalPages { set; get; } = 0;
        public int PageSize { set; get; } = 0;
        public int Page { set; get; } = 0;
        public List<Phone> Phones { set; get; } = new List<Phone>();
        public PhonesFilterForm FilterForm { set; get; } = new PhonesFilterForm();
    }
}