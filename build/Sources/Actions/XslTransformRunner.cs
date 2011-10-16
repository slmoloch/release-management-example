using System.Xml.Xsl;

namespace CommitStage
{
    public class XslTransformRunner
    {
        public void Run(string xmlPath, string xslPath, string outPath)
        {
            var transform = new XslCompiledTransform();

            transform.Load(xslPath);

            transform.Transform(xmlPath, outPath);
        }
    }
}