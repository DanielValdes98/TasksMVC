namespace TareasMVC.Entidades
{
    public class Paso
    {
        public Guid Id { get; set; } // Id con numeros para guardar millones de registros (9792-0837-9029-0830)
        public int TareaId { get; set; } // Llave foranea que apunta a la tabla Tarea
        public Tarea Tarea { get; set; } // Propiedad de navegacion: Carga la data relacionada de las tablas Tarea y Paso (algo similar a un JOIN). Navega hacia una entidad relacionada de una manera sencilla (Relacion de uno a uno,  a un paso le corresponde una unica tarea)
        public string Descripcion { get; set; }
        public bool Realizado { get; set; }
        public int Orden { get; set; }
    }
}
