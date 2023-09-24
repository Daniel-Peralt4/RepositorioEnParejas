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
        LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();
        public void Menu()
        {
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
        private void Guardar()
        {

            Console.Clear();
            Console.Write("Ingrese el numero de liquidacion: ");
            int NumeroLiquidacion = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la identicicacion del paciente: ");
            int IdPaciente = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el nombre del paciente: ");
            string NombrePaciente = Console.ReadLine();

            string TipoAfiliacion;
            do
            {
                Console.Write("Ingrese el tipo de afiliacion (1.Contributivo / 2.Subsidiado ): ");
                TipoAfiliacion = Console.ReadLine();

                if (TipoAfiliacion == "1")
                {
                    TipoAfiliacion = "Contributivo";
                }
                else
                {
                    TipoAfiliacion = "Subsidiado";
                }
            } while (TipoAfiliacion != "Subsidiado" && TipoAfiliacion != "Contributivo");

            Console.Write("Ingrese el salario del paciente: ");
            double SalarioPaciente = double.Parse(Console.ReadLine());

            Console.Write("Ingrese el valor del servicio del paciente: ");
            double ValorServicio = double.Parse(Console.ReadLine());

            DateTime FechaLiquidacion = DateTime.Now;

            LiquidacionCuotaModeradora nuevaLiquidacion = new LiquidacionCuotaModeradora(NumeroLiquidacion,
                IdPaciente,NombrePaciente, TipoAfiliacion, SalarioPaciente, ValorServicio, FechaLiquidacion);

            Console.WriteLine(liquidacionService.Guardar(nuevaLiquidacion));
            Console.ReadKey();
        }
        private void ConsultarTodas()
        {
            Console.Clear();
            Console.SetCursorPosition(15, 2); Console.Write("***Listado General***");
            Console.SetCursorPosition(2, 4); Console.Write("#liquidacion");
            Console.SetCursorPosition(16, 4); Console.Write("Id_Paciente");
            Console.SetCursorPosition(29, 4); Console.Write("Nombre");
            Console.SetCursorPosition(38, 4); Console.Write("Afiliacion");
            Console.SetCursorPosition(52, 4); Console.Write("Salario");
            Console.SetCursorPosition(62, 4); Console.WriteLine("Valor_Servicio");
            Console.SetCursorPosition(78, 4); Console.WriteLine("Fecha");
            Console.SetCursorPosition(88, 4); Console.WriteLine("Cuota");
            Console.SetCursorPosition(98, 4); Console.WriteLine("Tarifa");

            int posicion = 2;
            foreach (var item in liquidacionService.ConsultarTodas())
            {

                Console.SetCursorPosition(2 , 4 + posicion); Console.Write(item.NumeroLiquidacion);
                Console.SetCursorPosition(16, 4 + posicion); Console.Write(item.IDPaciente);
                Console.SetCursorPosition(29, 4 + posicion); Console.Write(item.NombrePaciente);
                Console.SetCursorPosition(38, 4 + posicion); Console.Write(item.TipoAfiliacion);
                Console.SetCursorPosition(52, 4 + posicion); Console.Write(item.SalarioPaciente);
                Console.SetCursorPosition(62, 4 + posicion); Console.Write(item.ValorServicio);
                Console.SetCursorPosition(78, 4 + posicion); Console.Write($"{item.FechaLiquidacion.Day}/{item.FechaLiquidacion.Month}/{item.FechaLiquidacion.Year}");
                Console.SetCursorPosition(88, 4+ posicion); Console.WriteLine(item.CalcularCuota());
                Console.SetCursorPosition(98, 4+ posicion); Console.WriteLine(item.CalcularTarifa());
                posicion++;
            }
            Console.ReadKey();
        }
        private void EliminarLiquidacion()
        {

        }
        private void FiltroPorNombre()
        {
            Console.Clear();
            string Nombre;

            Console.WriteLine("Ingrese el nombre a buscar");
            Nombre= Console.ReadLine();

            if (liquidacionService.FiltroPorNombre(Nombre) != null )            
            {


                Console.Clear();
                Console.SetCursorPosition(15, 2); Console.Write("***Listado General***");
                Console.SetCursorPosition(2, 4); Console.Write("#liquidacion");
                Console.SetCursorPosition(16, 4); Console.Write("Id_Paciente");
                Console.SetCursorPosition(29, 4); Console.Write("Nombre");
                Console.SetCursorPosition(38, 4); Console.Write("Afiliacion");
                Console.SetCursorPosition(52, 4); Console.Write("Salario");
                Console.SetCursorPosition(62, 4); Console.WriteLine("Valor_Servicio");
                Console.SetCursorPosition(78, 4); Console.WriteLine("Fecha");
                Console.SetCursorPosition(88, 4); Console.WriteLine("Cuota");
                Console.SetCursorPosition(98, 4); Console.WriteLine("Tarifa");

                int posicion = 2;
                foreach (var item in liquidacionService.FiltroPorNombre(Nombre))
                {

                    Console.SetCursorPosition(2, 4 + posicion); Console.Write(item.NumeroLiquidacion);
                    Console.SetCursorPosition(16, 4 + posicion); Console.Write(item.IDPaciente);
                    Console.SetCursorPosition(29, 4 + posicion); Console.Write(item.NombrePaciente);
                    Console.SetCursorPosition(38, 4 + posicion); Console.Write(item.TipoAfiliacion);
                    Console.SetCursorPosition(52, 4 + posicion); Console.Write(item.SalarioPaciente);
                    Console.SetCursorPosition(62, 4 + posicion); Console.Write(item.ValorServicio);
                    Console.SetCursorPosition(78, 4 + posicion); Console.Write($"{item.FechaLiquidacion.Day}/{item.FechaLiquidacion.Month}/{item.FechaLiquidacion.Year}");
                    Console.SetCursorPosition(88, 4 + posicion); Console.WriteLine(item.CalcularCuota());
                    Console.SetCursorPosition(98, 4 + posicion); Console.WriteLine(item.CalcularTarifa());
                    posicion++;
                }
            }
            else
            {
                Console.WriteLine($"NO SE ENCONTRARON PACIENTES CON EL NOMBRE {Nombre}");
            }

            Console.ReadKey();
        }
        private void FiltroPorAfiliacion()
        {
            Console.Clear();
            string TipoAfiliacion;

            do
            {
                Console.Write("Ingrese el tipo de afiliacion (1.Contributivo / 2.Subsidiado ): ");
                TipoAfiliacion = Console.ReadLine();

                if (TipoAfiliacion == "1")
                {
                    TipoAfiliacion = "Contributivo";
                }
                else
                {
                    TipoAfiliacion = "Subsidiado";
                }
            } while (TipoAfiliacion != "Subsidiado" && TipoAfiliacion != "Contributivo");

            if (liquidacionService.FiltroPorAfiliacion(TipoAfiliacion) != null)
            {

                Console.Clear();
                Console.SetCursorPosition(15, 2); Console.Write("***Listado General***");
                Console.SetCursorPosition(2, 4); Console.Write("#liquidacion");
                Console.SetCursorPosition(16, 4); Console.Write("Id_Paciente");
                Console.SetCursorPosition(29, 4); Console.Write("Nombre");
                Console.SetCursorPosition(38, 4); Console.Write("Afiliacion");
                Console.SetCursorPosition(52, 4); Console.Write("Salario");
                Console.SetCursorPosition(62, 4); Console.WriteLine("Valor_Servicio");
                Console.SetCursorPosition(78, 4); Console.WriteLine("Fecha");
                Console.SetCursorPosition(88, 4); Console.WriteLine("Cuota");
                Console.SetCursorPosition(98, 4); Console.WriteLine("Tarifa");

                int posicion = 2;
                foreach (var item in liquidacionService.FiltroPorAfiliacion(TipoAfiliacion))
                {

                    Console.SetCursorPosition(2, 4 + posicion); Console.Write(item.NumeroLiquidacion);
                    Console.SetCursorPosition(16, 4 + posicion); Console.Write(item.IDPaciente);
                    Console.SetCursorPosition(29, 4 + posicion); Console.Write(item.NombrePaciente);
                    Console.SetCursorPosition(38, 4 + posicion); Console.Write(item.TipoAfiliacion);
                    Console.SetCursorPosition(52, 4 + posicion); Console.Write(item.SalarioPaciente);
                    Console.SetCursorPosition(62, 4 + posicion); Console.Write(item.ValorServicio);
                    Console.SetCursorPosition(78, 4 + posicion); Console.Write($"{item.FechaLiquidacion.Day}/{item.FechaLiquidacion.Month}/{item.FechaLiquidacion.Year}");
                    Console.SetCursorPosition(88, 4 + posicion); Console.WriteLine(item.CalcularCuota());
                    Console.SetCursorPosition(98, 4 + posicion); Console.WriteLine(item.CalcularTarifa());
                    posicion++;
                }
            }
            else
            {
                Console.WriteLine($"No se encontraron liquidaciones de tipo {TipoAfiliacion}");
            }
            Console.ReadKey();
        }
    }
}
