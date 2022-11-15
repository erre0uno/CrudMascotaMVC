using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCMascota.Models
{
    public partial class Historia
    {
        public Historia()
        {
            Visita = new HashSet<Visita>();
        }

        [Display(Name = "Histoia id")]
        public int HistoriaId { get; set; }
        [Display(Name = "Fecha\nCreación")]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Diagnóstico")]
        public string? Diagnostico { get; set; }
        public string? Medicamentos { get; set; }
        [Display(Name = "Mascota id")]
        public int MascotaId { get; set; }

        public virtual Mascota Mascota { get; set; } = null!;
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
