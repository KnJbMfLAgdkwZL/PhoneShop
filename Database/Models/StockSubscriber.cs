﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class StockSubscriber
    {
        public int Id { get; set; }
        public string BrandSlug { get; set; }
        public string PhoneSlug { get; set; }
        public string Email { get; set; }
    }
}