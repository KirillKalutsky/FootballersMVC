using System.ComponentModel.DataAnnotations;

namespace FootballPlayers.Models.Transfers
{
    public class CreateTeamDto
    {
        [Required(ErrorMessage = "Пожалуйста введите название команды")]
        [StringLength(30, ErrorMessage = "Название команды должно быть длинее {2} и короче {1} букв", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
