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
            Console.WriteLine($"Informacion sobre {Liquidacion.NumeroLiquidacion} guardada");
        }
        public List<LiquidacionCuotaModeradora> Consultar()
        {
            return lista;
        }
        public string Eliminar(LiquidacionCuotaModeradora liquidacionEliminada)
        {
            if (liquidacionEliminada is null)
            {
                Console.WriteLine("LA PERSONA BUSCADA NO EXISTE");
            }
            else
            {
                lista.Remove(BuscarId(liquidacionEliminada.NumeroLiquidacion));
            }
            var mensaje = Repositorio.Guardar(lista);
            return mensaje;
        }
        public LiquidacionCuotaModeradora BuscarId(int ID)
        {
            foreach (var LiquidacionBuscada in lista) { 
                
                if(LiquidacionBuscada.NumeroLiquidacion == ID)
                {
                    return LiquidacionBuscada;
                }
            }
            return null;
        }
        public List<LiquidacionCuotaModeradora> FiltroPorAfiliacion(String Afiliacion)
        {
            List<LiquidacionCuotaModeradora> listaFiltrada = null;

            foreach (var Liquidacion in lista)
            {
                if (Liquidacion.TipoAfilacion == Afiliacion)
                {
                    listaFiltrada.Add(Liquidacion);
                }
            }
            return listaFiltrada;
        }
        public List<LiquidacionCuotaModeradora> FiltroPorNombre(String Nombre)
        {
            List<LiquidacionCuotaModeradora> listaFiltrada = null;

            foreach (var Liquidacion in lista)
            {
                if (Liquidacion.NombrePaciente == Nombre)
                {
                    listaFiltrada.Add(Liquidacion);
                }
            }
            return listaFiltrada;
        }
    }
}
