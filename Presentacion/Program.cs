using Logica;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{

    internal class Program
    {
        static void Main(string[] args)
        {
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();

            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("*****MENU PRINCIPAL*****");
                Console.WriteLine("-");
                Console.WriteLine("1. AGREGAR LIQUIDACION");
                Console.WriteLine("2. ELIMINAR LIQUIDACION");
                Console.WriteLine("3. CONSULTAR TODAS LAS LIQUIDACIONES");
                Console.WriteLine("4. CONSULTAR POR NOMBRE DEL PACIENTE");
                Console.WriteLine("5. CONSULTAR POR AFILIACION");
                Console.WriteLine("6. SALIR DEL PROGRAMA");
                //Console.WriteLine("3. MODIFICAR LIQUIDACION DE LA LISTA");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Guardar();
                        break;
                    case 2:
                        EliminarLiquidacion();
                        break;
                    case 3:
                        ConsultarTodas();
                        break;
                    case 4:
                        FiltroPorNombre();
                        break;
                    case 5:
                        FiltroPorAfiliacion();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opcion no válida, vuelva a intentarlo");
                        break;
                }
            } while (opcion != 6);
        }
        private static void Guardar()
        {
            Console.Clear();
            Console.Write("Ingrese el numero de liquidacion: ");
            int NumeroLiquidacion = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la identicicacion del paciente: ");
            int IdPaciente = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el nombre del paciente: ");
            string NombrePaciente = Console.ReadLine();

            Console.Write("Ingrese el tipo de afiliacion: ");
            string TipoAfiliacion = Console.ReadLine();

            Console.Write("Ingrese el salario del paciente: ");
            double SalarioPaciente = double.Parse(Console.ReadLine());

            Console.Write("Ingrese el valor del servicio del paciente: ");
            double ValorServicio = double.Parse(Console.ReadLine());

            Console.Write("Ingrese la fecha de liquidacion del paciente: ");
            DateTime FechaLiquidacion = DateTime.Parse(Console.ReadLine());

            LiquidacionCuotaModeradora nuevaLiquidacion = new LiquidacionCuotaModeradora(NumeroLiquidacion,
                IdPaciente,NombrePaciente, TipoAfiliacion, SalarioPaciente, ValorServicio, FechaLiquidacion);

            Console.WriteLine(liquidacionService.Guardar(nuevaLiquidacion));
            Console.ReadKey();
        }
        private static void ConsultarTodas()
        {
            Console.Clear();
            Console.SetCursorPosition(15, 2); Console.Write("***Listado General***");
            Console.SetCursorPosition(10, 4); Console.Write("Numero de liquidacion");
            Console.SetCursorPosition(28, 4); Console.Write("Id_Paciente");
            Console.SetCursorPosition(40, 4); Console.Write("Nombre_Paciente");
            Console.SetCursorPosition(46, 4); Console.Write("Tipo de Afiliaxion");
            Console.SetCursorPosition(56, 4); Console.Write("Salario_Paciente");
            Console.SetCursorPosition(65, 4); Console.WriteLine("Valor_Servicio");
            Console.SetCursorPosition(74, 4); Console.WriteLine("Fecha_Liquidacion");
            int posicion = 2;
            foreach (var item in liquidacionService.ConsultarTodas())
            {

                Console.SetCursorPosition(15, 4 + posicion); Console.Write(item.NumeroLiquidacion);
                Console.SetCursorPosition(29, 4 + posicion); Console.Write(item.IdPaciente);
                Console.SetCursorPosition(42, 4 + posicion); Console.Write(item.NombrePaciente);
                Console.SetCursorPosition(48, 4 + posicion); Console.Write(item.TipoAfiliacion);
                Console.SetCursorPosition(59, 4 + posicion); Console.Write(item.SalarioPaciente);
                Console.SetCursorPosition(68, 4 + posicion); Console.Write(item.ValorServicio);
                Console.SetCursorPosition(75, 4 + posicion); Console.Write(item.FechaLiquidacion);
                posicion++;
            }
            Console.ReadKey();
        }
        private static void EliminarLiquidacion()
        {

        }
        private static void FiltroPorNombre()
        {

        }
        private static void FiltroPorAfiliacion()
        {

        }
    }
}
