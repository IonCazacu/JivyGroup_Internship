using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Services.UserServices.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        // public DateTime CreatedDate { get; set; }
        // public DateTime UpdatedDate { get; private set; }

        // public BaseEntity()
        // {
        //     UpdatedDate = DateTime.Now;
        // }
    }
}
