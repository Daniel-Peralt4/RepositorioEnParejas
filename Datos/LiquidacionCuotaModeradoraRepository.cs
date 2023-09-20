using Entidad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class LiquidacionCuotaModeradoraRepository
    {
        public string FileName = "Liquidaciones.txt";
        public string Guardar(LiquidacionCuotaModeradora liquidacion)
        {
            var escritor = new StreamWriter(FileName, true);
            escritor.WriteLine(liquidacion.ToString());
            escritor.Close();
            return "Liquidación guardada con éxito";
        }
        public List<LiquidacionCuotaModeradora> ConsultarTodas()
        {
            var liquidaciones = new List<LiquidacionCuotaModeradora>;
            return liquidaciones;
        }

        //Eliminar solicitando un numero
        public void EliminarLiquidacion()
        {

        }
        public void ConsultaPorTipoAfiliacion()
        {

        }
        public void ConsultaPorNombre()
        {

        }
        public void Modificar()
        {

        }
    }
}
