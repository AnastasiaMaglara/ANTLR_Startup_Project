using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime.Misc;

namespace ANTLR_Startup_Project
{
    class GraphvizPrinterVisitor : firstBaseVisitor<int>
    {

        private Stack<string> m_labels = new Stack<string>();
        private StreamWriter outFile;
        private static int ms_serialCounter = 0;

        public GraphvizPrinterVisitor()
        {
            outFile = new StreamWriter("test.dot");
        }

        public override int VisitCompileUnit([NotNull] firstParser.CompileUnitContext context)
        {
            int serial = ms_serialCounter++;
            string s = "CompileUnit_" + serial;
            m_labels.Push(s);

            outFile.WriteLine("digraph G{");


            base.VisitChildren(context);

            outFile.WriteLine("}");
            m_labels.Pop();
            outFile.Close();
            return 0;
        }

        public override int VisitExpr([NotNull] firstParser.ExprContext context)
        {

            int serial = ms_serialCounter++;
            string s = "Expr_" + serial;
            // Preorder action
            outFile.WriteLine("\"{0}\"->\"{1}\";", m_labels.Peek(), s);

            m_labels.Push(s);

            base.VisitChildren(context);

            m_labels.Pop();
            return 0;
        }

    }
}
