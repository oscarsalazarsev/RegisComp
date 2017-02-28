using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RegisComp
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = "dir";
            Console.WriteLine("Registrar e Instalar de Componentes en Windows (32/64 bits)");
            Console.WriteLine("Oscar Salazar - oscarsalazarsevilla@outlook.com \r\n\r\n");

            string x86 = @"C:\WINDOWS\System32";
            string x64 = @"C:\WINDOWS\SysWOW64";

            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());

            foreach (var file in dir.GetFiles("*.dll",SearchOption.TopDirectoryOnly))
            {
                try
                {
                    if (Directory.Exists(x86))
                    {
                        File.Copy(Path.Combine(Directory.GetCurrentDirectory(),file.ToString()), Path.Combine(x86, file.ToString()), true);
                        ExecuteCommand(x86 + @"\REGSVR32 " + Path.Combine(x86, file.ToString()));
                    }
                    if (Directory.Exists(x64))
                    {
                        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), file.ToString()), Path.Combine(x64, file.ToString()), true);
                        ExecuteCommand(x64 + @"\REGSVR32 " + Path.Combine(x64, file.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //throw;
                }

            }
            foreach (var file in dir.GetFiles("*.ocx", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    if (Directory.Exists(x86))
                    {
                        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), file.ToString()), Path.Combine(x86, file.ToString()), true);
                        ExecuteCommand(x86 + @"\REGSVR32 " + Path.Combine(x86, file.ToString()));
                    }
                    if (Directory.Exists(x64))
                    {
                        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), file.ToString()), Path.Combine(x64, file.ToString()), true);
                        ExecuteCommand(x64 + @"\REGSVR32 " + Path.Combine(x64, file.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //throw;
                }
            }
        }
        static void ExecuteCommand(string _Command)
        {
            //Indicamos que deseamos inicializar el proceso cmd.exe junto a un comando de arranque. 
            //(/C, le indicamos al proceso cmd que deseamos que cuando termine la tarea asignada se cierre el proceso).
            //Para mas informacion consulte la ayuda de la consola con cmd.exe /? 
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + _Command);
            // Indicamos que la salida del proceso se redireccione en un Stream
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            //Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
            procStartInfo.CreateNoWindow = false;
            //Inicializa el proceso
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            //Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
            string result = proc.StandardOutput.ReadToEnd();
            //Muestra en pantalla la salida del Comando
            Console.WriteLine(result);
        }
    }
}
