using SpringBlog.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
namespace SpringBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public int? ParentId { get; set; }

        public int PostId { get; set; }
        
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime? CreationTime { get; set; }

        [Required]
        public DateTime? ModificationTime { get; set; }

        public CommentState State { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }

        //bir yorum birden cok posta mı yapılır. tek post a yapılır.
        public virtual Post Post { get; set; }
        //bir yorumun birden cok üst yorumu olur mu olmaz
        public virtual Comment Parent { get; set; }

        //bu yorumun altındaki yorumlar
        public virtual ICollection<Comment> Children { get; set; }
    }
}