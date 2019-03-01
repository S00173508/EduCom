using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EduCom.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Programme { get; set; }
        public int Year { get; set; }
        public string StudentNumber { get; set; }
        //[ForeignKey("member")]
        ////public int MemberID { get; set; }
        //[Required]
        //public virtual Member Member { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    [Table("Member")]

    public class Member

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column(Order = 1)]
        public int MemberID { get; set; }
        public string MemberName { get; set; }

        //[Column(Order = 2)]
        //[Key, ForeignKey("ApplicationUser")]
        //public int Id { get; set; }
        //[Required]
        //public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Topic> AssociatedTopic { get; set; }


    }

    [Table("Post")]

    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Text { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }



    }

    [Table("Topic")]

    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string TopicName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public virtual ICollection<Member> Members { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
         public ApplicationDbContext()
            : base("EduConnection", throwIfV1Schema: false)
         {
            this.Configuration.LazyLoadingEnabled = false;

         }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Post> Posts { get; set; }




        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}