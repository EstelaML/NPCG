using Postgrest.Models;

namespace preguntaods.Entities
{
    public partial class Reto : BaseModel, IEntity
    {
        public Reto() { }
        public Reto(int id, int dificultad, int oDSFk, ODS oDS)
        {
            this.Id = id;
            this.Dificultad = dificultad;
            this.Ods_tratada = oDSFk;
            this.Ods = oDS;
        }
 
    }
}