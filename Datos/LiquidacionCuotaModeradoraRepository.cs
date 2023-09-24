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
            var liquidaciones = new List<LiquidacionCuotaModeradora>();

            if (!File.Exists(FileName))
            {
                return liquidaciones;
            }
            else
            {
                var lector = new StreamReader(FileName);

                while (!lector.EndOfStream)
                {
                    liquidaciones.Add(Mapeo(lector.ReadLine()));
                }

                lector.Close();
                return liquidaciones;
            }
        }
        public string ActualizarLista(List<LiquidacionCuotaModeradora> lista)
        {
            var escritor = new StreamWriter(FileName);

            foreach (var item in lista)
            {
                escritor.WriteLine(item.ToString());
            }
            escritor.Close();
            return "Lista Actualizada";
        }
        private LiquidacionCuotaModeradora Mapeo(string linea)
        {
            int Año, Mes, Dia;

            var liquidacion = new LiquidacionCuotaModeradora();
            liquidacion.NumeroLiquidacion = int.Parse(linea.Split(';')[0]);
            liquidacion.IDPaciente = int.Parse(linea.Split(';')[1]);
            liquidacion.NombrePaciente = linea.Split(';')[2];
            liquidacion.TipoAfiliacion = linea.Split(';')[3];
            liquidacion.SalarioPaciente = double.Parse(linea.Split(';')[4]);
            liquidacion.ValorServicio = double.Parse(linea.Split(';')[5]);

            Año = int.Parse(linea.Split(';')[6]);
            Mes = int.Parse(linea.Split(';')[7]);
            Dia = int.Parse(linea.Split(';')[8]);

            liquidacion.FechaLiquidacion = new DateTime(Año, Mes, Dia);

            return liquidacion;
        }
    }
}
