using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RotinaEstudo.Models
{
    [Table("tarefas_estudo")]
    public class TarefaEstudo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A matéria é obrigatória")]
        [StringLength(100, ErrorMessage = "A matéria não pode ter mais de 100 caracteres")]
        [Column("materia")]
        public string Materia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tema é obrigatório")]
        [StringLength(200, ErrorMessage = "O tema não pode ter mais de 200 caracteres")]
        [Column("tema")]
        public string Tema { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tempo de estudo é obrigatório")]
        [Range(1, 480, ErrorMessage = "O tempo de estudo deve ser entre 1 e 480 minutos")]
        [Column("tempo_estudo_minutos")]
        public int TempoEstudoMinutos { get; set; }

        [Required(ErrorMessage = "A prioridade é obrigatória")]
        [Column("prioridade")]
        public string Prioridade { get; set; } = "Média";

        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Column("concluida")]
        public bool Concluida { get; set; } = false;

        [Column("data_conclusao")]
        public DateTime? DataConclusao { get; set; }
    }

    public enum PrioridadeEstudo
    {
        Baixa,
        Media,
        Alta
    }


}