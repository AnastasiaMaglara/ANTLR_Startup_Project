using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ANTLR_Startup_Project
{
    public enum nodeType
    {
        NA = -1,
        NT_COMPILEUNIT = 0,
        NT_ADDITION = 1,
        NT_SUBSTRACTION = 3,
        NT_MULTIPLICATION = 5,
        NT_DIVISION = 7,
        NT_NUMBER = 9
    };

    public enum contextType
    {
        NA = -1,
        CT_COMPILEUNIT_EXPRESSIONS,
        CT_ADDITION_LEFT,
        CT_ADDITION_RIGHT,
        CT_SUBSTRACTION_LEFT,
        CT_SUBSTRACTION_RIGHT,
        CT_MULTIPLICATION_LEFT,
        CT_MULTIPLICATION_RIGHT,
        CT_DIVISION_LEFT,
        CT_DIVISION_RIGHT
    };

    public abstract class ASTElement
    {
        private int m_serial;
        private static int ms_serialCounter = 0;
        private nodeType m_nodeType;
        private ASTElement m_parent;
        private string m_nodeName;

        public nodeType MNodeType => m_nodeType;

        public virtual string GenerateNodeName()
        {
            return "_" + m_serial;
        }

        public ASTElement MParent
        {
            get { return m_parent; }
        }

        public string MNodeName => m_nodeName;

        protected ASTElement(nodeType type, ASTElement parent)
        {
            m_nodeType = type;
            m_parent = parent;
            m_serial = ms_serialCounter++;
            m_nodeName = GenerateNodeName();
        }
    }

    public abstract class ASTComposite : ASTElement
    {
        private List<ASTElement>[] m_children;
        protected ASTComposite(nodeType type, ASTElement parent, int numContexts) : base(type, parent)
        {
            m_children = new List<ASTElement>[numContexts];
        }

        protected int GetContextIndex(contextType ct)
        {
            return (int)ct - (int)MNodeType;
        }

        protected void AddChild(ASTElement child, contextType ct)
        {
            int index = GetContextIndex(ct);
            m_children[index].Add(child);
        }

        protected ASTElement GetChild(contextType ct, int index)
        {
            int i = GetContextIndex(ct);
            return m_children[i][index];
        }
    }

    public abstract class ASTTerminal : ASTElement
    {
        protected ASTTerminal(nodeType type, ASTElement parent) : base(type, parent)
        {

        }

    }
}

