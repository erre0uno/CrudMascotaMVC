using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MVCMascota.Models
{
    public partial class Dueno
    {
        public Dueno()
        {
            Mascota = new HashSet<Mascota>();
        }

        [Display(Name = "Dueño id")]
        public int DuenoId { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = null!;
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public virtual ICollection<Mascota> Mascota { get; set; }
    }
}
