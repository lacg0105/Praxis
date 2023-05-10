using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Praxis.Business.Helpers
{
    public class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            try
            {
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(input)))
                {
                    return (T)ser.Deserialize(xmlReader);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
