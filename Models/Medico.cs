using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCMascota.Models
{
    public partial class Medico
    {
        public Medico()
        {
            Mascota = new HashSet<Mascota>();
            Visita = new HashSet<Visita>();
        }

        [Display(Name = "Medico id")]
        public int MedicoId { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = null!;
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = null!;
        public string Tarjeta { get; set; } = null!;

        public virtual ICollection<Mascota> Mascota { get; set; }
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
