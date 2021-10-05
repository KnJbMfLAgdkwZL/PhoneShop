﻿using Models.Interfaces;

namespace Models.Entities.RemoteApi
{
    public partial class Brand: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}