using System.ComponentModel.DataAnnotations;

namespace ET.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}