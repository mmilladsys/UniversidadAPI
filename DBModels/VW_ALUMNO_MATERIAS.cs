using System;
using System.Collections.Generic;

namespace UniversidadAPI.DBModels;

public partial class VW_ALUMNO_MATERIAS
{
    public string NOMBRE { get; set; } = null!;

    public string APELLIDO { get; set; } = null!;

    public string NOMBRE_CARRERA { get; set; } = null!;

    public byte DURACION { get; set; }

    public decimal? CANTIDAD_MATERIAS { get; set; }

    public decimal? MATERIAS_APROBADAS { get; set; }
}
