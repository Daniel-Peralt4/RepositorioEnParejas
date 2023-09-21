using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class LiquidacionCuotaModeradora
    {
        public LiquidacionCuotaModeradora() { }
        public LiquidacionCuotaModeradora(int numeroLiquidacion, int iDPaciente, string nombrePaciente, string tipoAfilacion,
            double salarioPaciente, double valorServicio, DateTime fechaLiquidacion)
        {
            NumeroLiquidacion = numeroLiquidacion;
            IDPaciente = iDPaciente;
            NombrePaciente = nombrePaciente;
            TipoAfilacion = tipoAfilacion;
            SalarioPaciente = salarioPaciente;
            ValorServicio = valorServicio;
            FechaLiquidacion = fechaLiquidacion;
        }
        public int NumeroLiquidacion { get; set; }
        public int IDPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public string TipoAfilacion { get; set; }
        public double SalarioPaciente { get; set; }
        public double ValorServicio { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public int CalcularTarifa()
        {
            double salarioMinimo= 1_000_000;
            int Tarifa = 0;
        
            if(TipoAfilacion == "Contributivo")
            {
                if (SalarioPaciente < 2*salarioMinimo )
                {
                    Tarifa = 15;
                }else if ((SalarioPaciente >= 2*salarioMinimo) && (SalarioPaciente < 5*salarioMinimo))
                {
                    Tarifa = 20;
                }else if (SalarioPaciente >= 5 * salarioMinimo)
                {
                    Tarifa = 25;
                }
            }
            else if(TipoAfilacion == "Subsidiado")
            {
                Tarifa = 5;
            }

            return Tarifa;
        }

        public double CalcularCuota()
        {
            double cuotaModeradora;
            double Tarifa= CalcularTarifa();

            cuotaModeradora = ValorServicio * Tarifa;

            if(TipoAfilacion == "Contributivo")
            {
                if(cuotaModeradora > 250_000)
                {
                    if (Tarifa ==15 && cuotaModeradora> 250_000)
                    {
                        cuotaModeradora = 250_000;
                    }else if (Tarifa == 20 && cuotaModeradora > 900_000)
                    {
                        cuotaModeradora = 900_000;
                    }else if(Tarifa == 15 && cuotaModeradora > 1_500_000)
                    {
                        cuotaModeradora = 1_500_000;
                    }
                }
            }else
            {
                if (Tarifa==5 && cuotaModeradora>200_000)
                {
                    cuotaModeradora = 200_000;
                }
            }

            return cuotaModeradora;
        }
        public override string ToString()
        {
            return $"{NumeroLiquidacion};{IDPaciente};{NombrePaciente};{TipoAfilacion};" +
                $"{SalarioPaciente};{ValorServicio};{CalcularTarifa()};{CalcularCuota()};{FechaLiquidacion}";
        }
    }
}
