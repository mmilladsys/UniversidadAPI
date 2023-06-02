using System;
using System.Collections.Generic;

namespace UniversidadAPI.DBModels;

public partial class U_HISTORIA_ACADEMICA
{
    public int ID_HISTORIA { get; set; }

    public int? ID_ALUMNO { get; set; }

    public int? ID_MATERIA { get; set; }

    public int? ID_CARRERA { get; set; }

    public string ESTADO { get; set; } = null!;

    public byte NOTA { get; set; }

    public DateTime FECHA_INICIO { get; set; }

    public DateTime FECHA_FIN { get; set; }

    public virtual U_ALUMNO? ALUMNO { get; set; }

    public virtual U_CARRERA? CARRERA { get; set; }

    public virtual U_MATERIA? MATERIA { get; set; }
}
