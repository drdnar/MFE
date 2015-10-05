using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Mfe.Exports
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class FontExporter : System.Attribute 
    {
        //public FontExporter(string
        protected string extension = "";
        /// <summary>
        /// e.g. "8xv" "8xp" "bin" &c.
        /// </summary>
        public string FileTypeExtension
        {
            get
            {
                return extension;
            }
            set
            {
                extension = value;
            }
        }

        protected string description = "";
        /// <summary>
        /// e.g. "zStart font file"
        /// </summary>
        public string FileTypeDescription
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        protected string helpText = "";
        /// <summary>
        /// Additional information that may help the user select the correct output format.
        /// </summary>
        public string HelpText
        {
            get
            {
                return helpText;
            }
            set
            {
                helpText = value;
            }
        }


        #region Reflection madness

        /// <summary>
        /// Basic type for an exporter
        /// </summary>
        /// <param name="font">The item to export</param>
        /// <param name="path">The path to export it to</param>
        public delegate void FontExportFunction(Mfe.Font font, string path);

        /// <summary>
        /// Contains information about an available export method and provides
        /// access to the export function.
        /// </summary>
        public struct Export
        {
            public readonly FontExportFunction DoExport;
            //MethodInfo MasterMethod;
            public readonly string FileTypeExtension;
            public readonly string FileTypeDescription;
            public readonly string HelpText;

            /*public Export(MethodInfo method)
            {
                //MasterMethod = method;
                IList<CustomAttributeNamedArgument> fontExporterAttributes = method.CustomAttributes
                    .Where(attrib => attrib.AttributeType == typeof(FontExporter))
                    .First()
                    .NamedArguments;
                FileTypeDescription = (string)(fontExporterAttributes
                    .Where(member => member.MemberName == "FileTypeDescription")
                    .First()
                    .TypedValue
                    .Value);
                FileTypeExtension = (string)(fontExporterAttributes
                    .Where(member => member.MemberName == "FileTypeExtension")
                    .First()
                    .TypedValue
                    .Value);
                HelpText = (string)(fontExporterAttributes
                    .Where(member => member.MemberName == "HelpText")
                    .First()
                    .TypedValue
                    .Value);
                DoExport = (FontExportFunction)Delegate.CreateDelegate(typeof(FontExportFunction), method);
            }*/
            public Export(MethodInfo method)
            {
                // Thanks to merthsoft from Cemetech for coming up with this
                // code, which is much more concise and removes the .NET 4.5
                // dependancy, and actually reduces the minimum to
                // .NET 3.5 Client Profile.
                FontExporter fontExporter = (FontExporter)method.GetCustomAttributes(typeof(FontExporter), false).First();

                HelpText = fontExporter.HelpText;
                FileTypeExtension = fontExporter.FileTypeExtension;
                FileTypeDescription = fontExporter.FileTypeDescription;

                DoExport = (FontExportFunction)Delegate.CreateDelegate(typeof(FontExportFunction), method);
            }
            /* Sadly, this approach did not play well with the debugger, due
             * to the Invoke call routing code through the reflection API.
             * Outside the debugger, it works fine.
            public void DoExport(Mfe.Font font, string path)
            {
                //MasterMethod.Invoke(null, new object[] { font, path });
            }*/
        }

        /// <summary>
        /// Master list of available exporters.
        /// </summary>
        public static readonly Export[] Exports = GetExportsList();

        /// <summary>
        /// This must be called once in order to initalize the list of available exports.
        /// </summary>
        private static Export[] GetExportsList()
        {
            List<Export> exports = new List<Export>();
            foreach (MethodInfo method in 
                Assembly.GetCallingAssembly().GetTypes()
                      .SelectMany(t => t.GetMethods())
                      .Where(m => m.GetCustomAttributes(typeof(FontExporter), false).Length > 0)
                )
            {
                exports.Add(new Export(method));
            }
            return exports.ToArray();
        }

        #endregion
    }
}
