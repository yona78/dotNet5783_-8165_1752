using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    internal class XMLTools
    {
        static string dir = @"..\xml\";
        //static XMLTools()
        //{
        //    if (!Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);
        //}
        #region SaveLoadWithXElement
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    return XElement.Load(dir + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dir + filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion


        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.OpenOrCreate);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
    }
}



[Serializable]
internal class XMLFileLoadCreateException : Exception
{
    private string filePath;
    private string v;
    private Exception ex;

    public XMLFileLoadCreateException()
    {
    }

    public XMLFileLoadCreateException(string? message) : base(message)
    {
    }

    public XMLFileLoadCreateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public XMLFileLoadCreateException(string filePath, string v, Exception ex)
    {
        this.filePath = filePath;
        this.v = v;
        this.ex = ex;
    }

    protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
