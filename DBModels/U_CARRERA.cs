using System;
using System.Collections.Generic;

namespace UniversidadAPI.DBModels;

public partial class U_CARRERA
{
    public int ID_CARRERA { get; set; }

    public string NOMBRE_CARRERA { get; set; } = null!;

    public byte DURACION { get; set; }

    public string TITULO { get; set; } = null!;

    public string NIVEL { get; set; } = null!;

    public virtual ICollection<U_HISTORIA_ACADEMICA> U_HISTORIA_ACADEMICA { get; set; } = new List<U_HISTORIA_ACADEMICA>();

    public virtual ICollection<U_MATERIA> U_MATERIA { get; set; } = new List<U_MATERIA>();
}
