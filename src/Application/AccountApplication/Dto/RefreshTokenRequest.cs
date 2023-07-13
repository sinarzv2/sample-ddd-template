using Common.Resources;
using Common.Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace Application.AccountApplication.Dto
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DataDictionary), Name = "AccessToken")]
        public string AccessToken { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DataDictionary), Name = "RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
