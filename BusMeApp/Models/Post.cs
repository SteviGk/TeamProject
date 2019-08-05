using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Foolproof;

namespace BusMeApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(500)]
        [Required]
        public string  Text { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateSent { get; set; }


        [ForeignKey("FromUser")]
        [Required]
        public string FromUserId { get; set; }

        public virtual ApplicationUser FromUser { get; set; }

        [ForeignKey("ToUser")]
        [Required]
        [NotEqualTo("FromUserId")]
        public string ToUserId { get; set; }

        public virtual ApplicationUser ToUser { get; set; }
    }
}