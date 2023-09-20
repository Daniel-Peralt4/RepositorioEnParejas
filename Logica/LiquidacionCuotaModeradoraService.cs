using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LiquidacionCuotaModeradoraService
    {
        public LiquidacionCuotaModeradoraRepository Repositorio = null;
        public List<LiquidacionCuotaModeradora> lista = null;

        public LiquidacionCuotaModeradoraService()
        {
            Repositorio = new LiquidacionCuotaModeradoraRepository();
            lista = Repositorio.ConsultarTodas();
        }

        public void Guardar(LiquidacionCuotaModeradora Liquidacion)
        {
            Repositorio.Guardar(Liquidacion);
            Console.WriteLine($"Informacion sobre {Liquidacion.numeroLiquidacion} guardada");
        }

        public void Eliminar(LiquidacionCuotaModeradora liquidacionEliminada)
        {
            if (liquidacionEliminada is null)
            {
                Console.WriteLine("LA PERSONA BUSCADA NO EXISTE");

            }
            else
            {
                lista.Remove(buscarId(liquidacionEliminada.numeroLiquidacion));
                Repositorio.Guardar(lista);
            }
        }

        public LiquidacionCuotaModeradora buscarId(int ID)
        {
            foreach (var LiquidacionBuscada in lista) { 
                
                if(LiquidacionBuscada.numeroLiquidacion == ID)
                {
                    return LiquidacionBuscada;
                }

            }

            return null;
        }

    }
}
