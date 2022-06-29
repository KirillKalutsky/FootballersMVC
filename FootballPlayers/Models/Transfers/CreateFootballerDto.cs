using System;
using System.ComponentModel.DataAnnotations;

namespace FootballPlayers.Models.Transfers
{
    public class CreateFootballerDto
    {
        [Required(ErrorMessage = "Пожалуйста выберите одну из команд")]
        public Guid TeamId { get; set; }

        [Required(ErrorMessage = "Пожалуйста выберите одну из стран")]
        public Country Country { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите имя")]
        [StringLength(20, ErrorMessage = "Имя должно быть длинее {2} и короче {1} букв", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите фамилию")]
        [StringLength(20, ErrorMessage = "Фамилия должна быть длинее {2} и короче {1} букв", MinimumLength = 2)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Пожалуйста выберите пол")]
        public Sex Sex { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите дату рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
        public DateTime BirthDate { get; set; }
    }
}
