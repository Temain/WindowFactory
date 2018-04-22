using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WindowFactory.Web.Models
{
    // Модели, возвращенные действиями MeController.
    public class GetViewModel
    {
        public string Hometown { get; set; }

        public string LastName { get; set; }
    }
}