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
                Console.WriteLine("3. CONSULTAR TODAS LAS LIQUIDACIONES   (MAXIMIZAR PANTALLA)");
                Console.WriteLine("4. CONSULTAR POR NOMBRE DEL PACIENTE   (MAXIMIZAR PANTALLA)");
                Console.WriteLine("5. CONSULTAR POR AFILIACION            (MAXIMIZAR PANTALLA)");
                Console.WriteLine("6. CONSULTAR POR FECHA                 (MAXIMIZAR PANTALLA)");
                Console.WriteLine("7. MODIFICAR LIQUIDACION");
                Console.WriteLine("8. SALIR DEL PROGRAMA");

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
                        FiltroPorFecha();
                        break;
                    case 7:
                        ModificarLiquidacion();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opcion no válida, vuelva a intentarlo");
                        break;
                }
            } while (opcion != 8);
        }
        private static void Guardar()
        {
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();

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
                IdPaciente, NombrePaciente, TipoAfiliacion, SalarioPaciente, ValorServicio, FechaLiquidacion);

            Console.WriteLine(liquidacionService.Guardar(nuevaLiquidacion));
            Console.ReadKey();
        }
        private static void ConsultarTodas()
        {
            Console.Clear();
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();
            double TotalLiquidado = 0;

            if (liquidacionService.ConsultarTodas() != null)
            {
                Console.SetCursorPosition(15, 2); Console.Write("***Listado General***");
                Console.SetCursorPosition(2, 4); Console.Write("#liquidacion");
                Console.SetCursorPosition(16, 4); Console.Write("Id_Paciente");
                Console.SetCursorPosition(29, 4); Console.Write("Nombre");
                Console.SetCursorPosition(38, 4); Console.Write("Afiliacion");
                Console.SetCursorPosition(52, 4); Console.Write("Salario");
                Console.SetCursorPosition(62, 4); Console.WriteLine("Valor_Servicio");
                Console.SetCursorPosition(78, 4); Console.WriteLine("Fecha");
                Console.SetCursorPosition(88, 4); Console.WriteLine("Cuota Real");
                Console.SetCursorPosition(100, 4); Console.WriteLine("Tarifa");
                Console.SetCursorPosition(108, 4); Console.WriteLine("Tope Maximo");
                Console.SetCursorPosition(121, 4); Console.WriteLine("Cuota Moderadora");

                int posicion = 2;
                foreach (var item in liquidacionService.ConsultarTodas())
                {

                    Console.SetCursorPosition(2, 4 + posicion); Console.Write(item.NumeroLiquidacion);
                    Console.SetCursorPosition(16, 4 + posicion); Console.Write(item.IDPaciente);
                    Console.SetCursorPosition(29, 4 + posicion); Console.Write(item.NombrePaciente);
                    Console.SetCursorPosition(38, 4 + posicion); Console.Write(item.TipoAfiliacion);
                    Console.SetCursorPosition(52, 4 + posicion); Console.Write(item.SalarioPaciente);
                    Console.SetCursorPosition(62, 4 + posicion); Console.Write(item.ValorServicio);
                    Console.SetCursorPosition(78, 4 + posicion); Console.Write($"{item.FechaLiquidacion.Day}/{item.FechaLiquidacion.Month}/{item.FechaLiquidacion.Year}");
                    Console.SetCursorPosition(88, 4 + posicion); Console.WriteLine(item.CalcularCuota());
                    Console.SetCursorPosition(100, 4 + posicion); Console.WriteLine(item.CalcularTarifa());
                    Console.SetCursorPosition(108, 4 + posicion); Console.WriteLine(item.topeMaximo());
                    Console.SetCursorPosition(121, 4 + posicion); Console.WriteLine(item.cuotaModeradora());

                    Console.SetCursorPosition(2, 6 + posicion); TotalLiquidado += item.cuotaModeradora();
                    posicion++;
                }

                Console.WriteLine($"TOTAL DEL VALOR LIQUIDADO : {TotalLiquidado}");

            }
            else
            {
                Console.WriteLine("LA LISTA ESTA VACIA");
            }
            Console.ReadKey();
        }
        private static void EliminarLiquidacion()
        {
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();
            int numeroLiquidacion;
            string op;

            Console.Clear();
            Console.WriteLine("Ingrese el numero de liquidacion a eliminar");
            numeroLiquidacion = int.Parse(Console.ReadLine());
            Console.WriteLine("");


            var liquidacionEliminada = liquidacionService.BuscarId(numeroLiquidacion);

            if (liquidacionEliminada == null)
            {
                Console.WriteLine("El numero de liquidacion generado no coincide con los registros");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Numero de liquidacion  : {liquidacionEliminada.NumeroLiquidacion}");
                Console.WriteLine($"ID del paciente        : {liquidacionEliminada.IDPaciente}");
                Console.WriteLine($"Nombre del paciente    : {liquidacionEliminada.NombrePaciente}");
                Console.WriteLine($"Tipo de afiliacion     : {liquidacionEliminada.TipoAfiliacion}");
                Console.WriteLine($"Salario del paciente   : {liquidacionEliminada.SalarioPaciente}");
                Console.WriteLine($"Valor del servicio     : {liquidacionEliminada.ValorServicio}");
                Console.WriteLine($"Valor Cuota real       : {liquidacionEliminada.CalcularCuota()}");
                Console.WriteLine($"Tope Maximo Aplicado   : {liquidacionEliminada.topeMaximo()}");
                Console.WriteLine($"Valor cuota Moderadora : {liquidacionEliminada.cuotaModeradora()}");
                Console.WriteLine("");

                do
                {
                    Console.WriteLine("DESEA ELIMINAR ESTE REGISTRO [S/N]");
                    op = Console.ReadLine();
                    op = op.ToUpper();

                } while (op != "S" && op != "N");

                if (op == "S")
                {
                    liquidacionService.EliminarLiquidacion(liquidacionEliminada);
                }
            }

            Console.ReadKey();
        }
        private static void FiltroPorNombre()
        {
            Console.Clear();
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();
            string Nombre;


            Console.WriteLine("Ingrese el nombre a buscar");
            Nombre = Console.ReadLine();

            if (liquidacionService.FiltroPorNombre(Nombre) != null)
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
                Console.SetCursorPosition(88, 4); Console.WriteLine("Cuota Real");
                Console.SetCursorPosition(100, 4); Console.WriteLine("Tarifa");
                Console.SetCursorPosition(108, 4); Console.WriteLine("Tope Maximo");
                Console.SetCursorPosition(121, 4); Console.WriteLine("Cuota Moderadora");

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
                    Console.SetCursorPosition(100, 4 + posicion); Console.WriteLine(item.CalcularTarifa());
                    Console.SetCursorPosition(108, 4 + posicion); Console.WriteLine(item.topeMaximo());
                    Console.SetCursorPosition(121, 4 + posicion); Console.WriteLine(item.cuotaModeradora());
                    posicion++;
                }
            }
            else
            {
                Console.WriteLine($"NO SE ENCONTRARON PACIENTES CON EL NOMBRE {Nombre}");
            }

            Console.ReadKey();
        }

        private static void FiltroPorFecha()
        {
            Console.Clear();
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();
            string Fecha;
            double TotalLiquidado = 0;


            Console.WriteLine($"Ingrese la fecha a buscar [Mes/Año] ejemplo:[{DateTime.Now.Month}/{DateTime.Now.Year}]");
            Fecha = Console.ReadLine();

            if (liquidacionService.FiltroPorFecha(Fecha) != null)
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
                Console.SetCursorPosition(88, 4); Console.WriteLine("Cuota Real");
                Console.SetCursorPosition(100, 4); Console.WriteLine("Tarifa");
                Console.SetCursorPosition(108, 4); Console.WriteLine("Tope Maximo");
                Console.SetCursorPosition(121, 4); Console.WriteLine("Cuota Moderadora");

                int posicion = 2;
                foreach (var item in liquidacionService.FiltroPorFecha(Fecha))
                {

                    Console.SetCursorPosition(2, 4 + posicion); Console.Write(item.NumeroLiquidacion);
                    Console.SetCursorPosition(16, 4 + posicion); Console.Write(item.IDPaciente);
                    Console.SetCursorPosition(29, 4 + posicion); Console.Write(item.NombrePaciente);
                    Console.SetCursorPosition(38, 4 + posicion); Console.Write(item.TipoAfiliacion);
                    Console.SetCursorPosition(52, 4 + posicion); Console.Write(item.SalarioPaciente);
                    Console.SetCursorPosition(62, 4 + posicion); Console.Write(item.ValorServicio);
                    Console.SetCursorPosition(78, 4 + posicion); Console.Write($"{item.FechaLiquidacion.Day}/{item.FechaLiquidacion.Month}/{item.FechaLiquidacion.Year}");
                    Console.SetCursorPosition(88, 4 + posicion); Console.WriteLine(item.CalcularCuota());
                    Console.SetCursorPosition(100, 4 + posicion); Console.WriteLine(item.CalcularTarifa());
                    Console.SetCursorPosition(108, 4 + posicion); Console.WriteLine(item.topeMaximo());
                    Console.SetCursorPosition(121, 4 + posicion); Console.WriteLine(item.cuotaModeradora());

                    Console.SetCursorPosition(2, 6 + posicion); TotalLiquidado += item.cuotaModeradora();
                    posicion++;
                }

                Console.WriteLine($"TOTAL DEL VALOR LIQUIDADO : {TotalLiquidado}");

            }
            else
            {
                Console.WriteLine($"NO SE ENCONTRARON LIQUIDACIONES CON LA FECHA [{Fecha}]");
            }

            Console.ReadKey();
        }
        private static void FiltroPorAfiliacion()
        {
            Console.Clear();
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();
            string TipoAfiliacion;
            double TotalLiquidado = 0;

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
                Console.SetCursorPosition(88, 4); Console.WriteLine("Cuota Real");
                Console.SetCursorPosition(100, 4); Console.WriteLine("Tarifa");
                Console.SetCursorPosition(108, 4); Console.WriteLine("Tope Maximo");
                Console.SetCursorPosition(121, 4); Console.WriteLine("Cuota Moderadora");

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
                    Console.SetCursorPosition(100, 4 + posicion); Console.WriteLine(item.CalcularTarifa());
                    Console.SetCursorPosition(108, 4 + posicion); Console.WriteLine(item.topeMaximo());
                    Console.SetCursorPosition(121, 4 + posicion); Console.WriteLine(item.cuotaModeradora());

                    Console.SetCursorPosition(2, 6 + posicion); TotalLiquidado += item.cuotaModeradora();
                    posicion++;
                }

                Console.WriteLine($"TOTAL DEL VALOR LIQUIDADO : {TotalLiquidado}");

            }
            else
            {
                Console.WriteLine($"No se encontraron liquidaciones de tipo {TipoAfiliacion}");
            }
            Console.ReadKey();
        }

        private static void ModificarLiquidacion()
        {
            LiquidacionCuotaModeradoraService liquidacionService = new LiquidacionCuotaModeradoraService();
            int numeroLiquidacion;
            string op;

            Console.Clear();
            Console.WriteLine("Ingrese el numero de liquidacion a modificar");
            numeroLiquidacion = int.Parse(Console.ReadLine());
            Console.WriteLine("");


            var liquidacionOriginal = liquidacionService.BuscarId(numeroLiquidacion);

            if (liquidacionOriginal == null)
            {
                Console.WriteLine("El numero de liquidacion generado no coincide con los registros");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Numero de liquidacion  : {liquidacionOriginal.NumeroLiquidacion}");
                Console.WriteLine($"ID del paciente        : {liquidacionOriginal.IDPaciente}");
                Console.WriteLine($"Nombre del paciente    : {liquidacionOriginal.NombrePaciente}");
                Console.WriteLine($"Tipo de afiliacion     : {liquidacionOriginal.TipoAfiliacion}");
                Console.WriteLine($"Salario del paciente   : {liquidacionOriginal.SalarioPaciente}");
                Console.WriteLine($"Valor del servicio     : {liquidacionOriginal.ValorServicio}");
                Console.WriteLine($"Valor Cuota real       : {liquidacionOriginal.CalcularCuota()}");
                Console.WriteLine($"Tope Maximo Aplicado   : {liquidacionOriginal.topeMaximo()}");
                Console.WriteLine($"Valor cuota Moderadora : {liquidacionOriginal.cuotaModeradora()}");
                Console.WriteLine("");

                do
                {
                    Console.WriteLine("DESEA MODIFICAR ESTE REGISTRO [S/N]");
                    op = Console.ReadLine();
                    op = op.ToUpper();

                } while (op != "S" && op != "N");

                if (op == "S")
                {
                    double ValorServicio;
                    Console.WriteLine("");
                    Console.WriteLine("Ingrese el nuevo costo del servicio");
                    ValorServicio = double.Parse(Console.ReadLine());

                    var LiquidacionModificada = liquidacionOriginal;
                    LiquidacionModificada.ValorServicio = ValorServicio;

                    Console.WriteLine(liquidacionService.ModificarLiquidacion(liquidacionOriginal, LiquidacionModificada));
                }
            }

            Console.ReadKey();
        }
    }
}
