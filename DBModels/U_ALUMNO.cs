using System;
using System.Collections.Generic;

namespace UniversidadAPI.DBModels;

public partial class U_ALUMNO
{
    public int ID_ALUMNO { get; set; }

    public string NOMBRE { get; set; } = null!;

    public string APELLIDO { get; set; } = null!;

    public int DNI { get; set; }

    public string? EMAIL { get; set; }

    public virtual ICollection<U_HISTORIA_ACADEMICA> U_HISTORIA_ACADEMICA { get; set; } = new List<U_HISTORIA_ACADEMICA>();
}
