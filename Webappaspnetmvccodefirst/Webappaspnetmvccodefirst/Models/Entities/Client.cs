using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Webappaspnetmvccodefirst.Attributes;
using Webappaspnetmvccodefirst.Extentions;


namespace Webappaspnetmvccodefirst.Models
{
    public class Client
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        [Required]
        [Display(Name = "Имя клиента", Order = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        [Required]
        [Display(Name = "Фамилия клиента", Order = 10)]
        public string Surname { get; set; }

        /// <summary>
        /// Возраст клиента
        /// </summary>
        [Required]
        [Display(Name = "Возраст клиента", Order = 20)]
        public int Age { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        
        [Display(Name = "Дата рождения", Order = 30)]
        public DateTime? Birthday { get; set; }

        [ScaffoldColumn(false)]
        public Gender Gender { get; set; }


        [Display(Name = "Пол", Order = 40)]
        [UIHint("DropDownList")]
        [TargetProperty("Gender")]
        [NotMapped]
        public IEnumerable<SelectListItem> GenderDictionary
        {
            get
            {
                var dictionary = new List<SelectListItem>();

                foreach (Gender type in Enum.GetValues(typeof(Gender)))
                {
                    dictionary.Add(new SelectListItem
                    {
                        Value = ((int)type).ToString(),
                        Text = type.GetDisplayValue(),
                        Selected = type == Gender
                    });
                }

                return dictionary;
            }
        }

        /// <summary>
        /// Пол
        /// </summary>
        /// [Required]
        [ScaffoldColumn(false)]
        public int ClientTypeId { get; set; }

        [ScaffoldColumn(false)]
        public virtual ClientType ClientTypes { get; set; }

        [Display(Name = "Лицо", Order = 50)]
        [UIHint("DropDownList")]
        [TargetProperty("ClientTypeId")]
        [NotMapped]
        public IEnumerable<SelectListItem> ClientTypeSelect
        {
            get
            {
                var db = new GosuslugiContext();
                var query = db.ClientTypes;

                if (query != null)
                {
                    var selection = new List<SelectListItem>();
                    selection.AddRange(query.OrderBy(d => d.Name).ToSelectList(c => c.Id, c => c.Name, c => c.Id == ClientTypeId));
                    return selection;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }




        /// <summary>
        /// Связь с таблицей Order
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual ICollection<Order> Orders { get; set; }
        
        [ScaffoldColumn(false)]
        public virtual Document Documents { get; set; }


        [Display(Name = "Паспорт", Order = 60)]
        [NotMapped]
        public HttpPostedFileBase DocumentFile { get; set; }




        [ScaffoldColumn(false)]
        public List<int> OrderIds { get; set; }

        [Display(Name = "Услуги", Order = 70)]
        [UIHint("MultipleDropDownList")]
        [TargetProperty("OrderIds")]
        [NotMapped]
        public IEnumerable<SelectListItem> OrderDictionary
        {
            get
            {
                var db = new GosuslugiContext();
                var query = db.Orders;

                if (query != null)
                {
                    var Ids = query.Where(s => s.Clients.Any(ss => ss.Id == Id)).Select(s => s.Id).ToList();
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.ToSelectList(c => c.Id, c => $"{c.Procedure} {c.Datetime}", c => Ids.Contains(c.Id)));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }
    }
}