﻿#nullable disable

namespace Models.Entities
{
    public partial class PriceSubscriber
    {
        public int Id { get; set; }
        public string BrandSlug { get; set; }
        public string PhoneSlug { get; set; }
        public string Email { get; set; }
    }
}
