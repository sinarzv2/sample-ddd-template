using System.ComponentModel.DataAnnotations;
using Common.Resources;
using Common.Resources.Messages;

namespace Application.AccountApplication.Dtos
{
    public class RefreshTokenRequestDto
    {
        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DataDictionary), Name = "AccessToken")]
        public string AccessToken { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DataDictionary), Name = "RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
