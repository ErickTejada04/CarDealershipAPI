﻿using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class ImageEntity
    {
        [Key]
        public string ImageID { get; set; }
        public string CarID { get; set; }
        public string ImageURL { get; set; }
        public int Main { get; set; }
    }
}
