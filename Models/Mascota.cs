using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCMascota.Models
{
    public partial class Mascota
    {
        public Mascota()
        {
            Historia = new HashSet<Historia>();
        }

        [Display(Name = "Mascota id")]
        public int MascotaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Especie { get; set; } = null!;
        public string? Raza { get; set; }
        [Display(Name = "Medico id")]
        public int MedicoId { get; set; }
        [Display(Name = "Dueño id")]
        public int DuenoId { get; set; }

        public virtual Dueno Dueno { get; set; } = null!;
        public virtual Medico Medico { get; set; } = null!;
        public virtual ICollection<Historia> Historia { get; set; }
    }
}
