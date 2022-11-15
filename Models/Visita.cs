using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCMascota.Models
{
    public partial class Visita
    {
        [Display(Name = "Visita id")]
        public int VisitaId { get; set; }
        [Display(Name = "Fecha\nVisita")]
        public DateTime FechaVisita { get; set; }
        [Display(Name = "Temperatura")]
        public float Temperatura { get; set; }
        [Display(Name = "Peso")]
        public float Peso { get; set; }

        [Display(Name = "Frecuencia\nCardiaca ")]
        public int FrecuenciaCardiaca { get; set; }
        [Display(Name = "Frecuencia\nRespiratoria")]
        public int FrecuenciaRespiratoria { get; set; }
        [Display(Name = "Estado\nAnimo")]
        public string EstadoAnimo { get; set; } = null!;
        public string? Recomendacion { get; set; }
        public int MedicoId { get; set; }

        [Display(Name = "Mascota Id")]
        public int HistoriaId { get; set; }

        public virtual Historia? Historia { get; set; }
        public virtual Medico Medico { get; set; } = null!;
    }
}
