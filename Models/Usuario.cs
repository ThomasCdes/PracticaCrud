namespace PracticaCrud.Models
{
    public class Usuario
    {
        public int Id { get; set; }  // Para el campo 'Id' que es de tipo INT
        public string? Nombre { get; set; }  // Para el campo 'Nombre' que es de tipo NVARCHAR
        public string? Email { get; set; }  // Para el campo 'Email' que es de tipo NVARCHAR
        public DateTime FechaRegistro { get; set; }  // Para el campo 'FechaRegistro' que es de tipo DATETIME
    }

}

