using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Webappaspnetmvccodefirst.Models
{
    public class ClientType
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Тип клиента", Order = 5)]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<Client> Clients { get; set; }

    }
}