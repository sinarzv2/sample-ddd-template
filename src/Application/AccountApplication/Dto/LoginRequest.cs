using Common.Resources;
using Common.Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace Application.AccountApplication.Dto
{
    public class LoginRequest
    {
        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "MaxLength")]
        [Display(ResourceType = typeof(DataDictionary), Name = "Username")]

        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "MaxLength")]
        [Display(ResourceType = typeof(DataDictionary), Name = "Password")]
        public string Password { get; set; }
    }
}
