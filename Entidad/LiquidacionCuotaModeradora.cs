using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class LiquidacionCuotaModeradora
    {
        public int numeroLiquidacion;
        public int IDPaciente;
        public string tipoAfilacion;
        public double salarioPaciente;
        public double valorServicion;
        public string nombrePaciente;
        public DateTime fechaLiquidacion;
        public LiquidacionCuotaModeradora(int numeroLiquidacion, int iDPaciente,string nombrePaciente, string tipoAfilacion, 
            double salarioPaciente, double valorServicion, DateTime fechaLiquidacion)
        {
            this.numeroLiquidacion = numeroLiquidacion;
            IDPaciente = iDPaciente;
            this.tipoAfilacion = tipoAfilacion;
            this.salarioPaciente = salarioPaciente;
            this.valorServicion = valorServicion;
            this.fechaLiquidacion = fechaLiquidacion;
            this.nombrePaciente = nombrePaciente;
        }

        public int calcularTarifa()
        {
            double salarioMinimo= 1_000_000;
            int Tarifa = 0;
        
            //Regimen  Contributivo
            if(tipoAfilacion == "Contributivo")
            {
                if (salarioPaciente < 2*salarioMinimo )
                {
                    Tarifa = 15;
                }else if (salarioPaciente >= 2*salarioMinimo && salarioPaciente < 5*salarioMinimo)
                {
                    Tarifa = 20;
                }else if (salarioPaciente >= 5 * salarioMinimo)
                {
                    Tarifa = 25;
                }
            }
            else //Regimen Subsidiado
            {
                Tarifa = 5;
            }

            return Tarifa;
        }

        public double calcularCuota()
        {
            double cuotaModeradora;
            double Tarifa= calcularTarifa();

            cuotaModeradora = valorServicion * Tarifa;

            if(tipoAfilacion == "Contributivo")
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


        public string toString()
        {
            return $"{numeroLiquidacion};{IDPaciente};{nombrePaciente};{tipoAfilacion};" +
                $"{salarioPaciente};{valorServicion};{calcularTarifa()};{calcularCuota()};{fechaLiquidacion}";
        }

    }
}
