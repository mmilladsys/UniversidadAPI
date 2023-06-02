using System;
using System.Collections.Generic;

namespace UniversidadAPI.DBModels;

public partial class U_MATERIA
{
    public int ID_MATERIA { get; set; }

    public string? NOMBRE_MATERIA { get; set; }

    public int? ID_CARRERA { get; set; }

    public virtual U_CARRERA? CARRERA { get; set; }

    public virtual ICollection<U_HISTORIA_ACADEMICA> U_HISTORIA_ACADEMICA { get; set; } = new List<U_HISTORIA_ACADEMICA>();
}
