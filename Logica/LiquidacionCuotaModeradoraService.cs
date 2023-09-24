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
        LiquidacionCuotaModeradoraRepository Repositorio = null;
        private List<LiquidacionCuotaModeradora> lista = null;
        public LiquidacionCuotaModeradoraService()
        {
            Repositorio = new LiquidacionCuotaModeradoraRepository();
            lista = Repositorio.ConsultarTodas();
        }
        public string Guardar(LiquidacionCuotaModeradora Liquidacion)
        {
            if (Liquidacion == null)
            {
                return "No se puede guardar una liquidacion nula o sin informacion";
            }
            else
            {
                var mensaje = Repositorio.Guardar(Liquidacion);
                lista = Repositorio.ConsultarTodas();
                return mensaje;
            }

        }
        public List<LiquidacionCuotaModeradora> ConsultarTodas()
        {
            return lista;
        }
        public string EliminarLiquidacion(LiquidacionCuotaModeradora liquidacionEliminada)
        {
            if (liquidacionEliminada is null)
            {
                return "LA PERSONA BUSCADA NO EXISTE";
            }
            else
            {
                lista.Remove(BuscarId(liquidacionEliminada.NumeroLiquidacion));
                var mensaje = Repositorio.ActualizarLista(lista);
                return mensaje;
            }
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
            List<LiquidacionCuotaModeradora> listaFiltrada = new List<LiquidacionCuotaModeradora>();
            bool listaVacia = true;

            foreach (var Liquidacion in lista)
            {
                if (Liquidacion.TipoAfiliacion == Afiliacion)
                {
                    listaFiltrada.Add(Liquidacion);
                    listaVacia = false;
                }
            }

            if (listaVacia == true)
            {
                listaFiltrada = null;
            }

            return listaFiltrada;
        }
        public List<LiquidacionCuotaModeradora> FiltroPorNombre(String Nombre)
        {
            List<LiquidacionCuotaModeradora> listaFiltrada = new List<LiquidacionCuotaModeradora>();
            bool listaVacia = true;

            foreach (var Liquidacion in lista)
            {
                if (Liquidacion.NombrePaciente == Nombre)
                {
                    listaFiltrada.Add(Liquidacion);
                    listaVacia=false;
                }
            }

            if (listaVacia == true)
            {
                listaFiltrada = null;
            }
            return listaFiltrada;
        }
    }
}
