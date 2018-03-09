using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab1
{
    class XML
    {
        XmlDocument doc;
        string rutaXml;

        public void crearXml(string ruta, string nodoRaiz)
        {
            this.rutaXml = ruta;
            doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlNode root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            XmlNode element1 = doc.CreateElement(nodoRaiz);
            doc.AppendChild(element1);
            if (System.IO.File.Exists(ruta).Equals(false))
            {
                doc.Save(ruta);
            }

        }

        public void Añadir(int val, string id, string nombre, string pais, string lugar, string habitaciones, string foto, string ruta, string precio)
        {
            if (val.Equals(1))
            {
                doc.Load(rutaXml);
                XmlNode hotel = Crear_Hoteles(id, nombre, pais, lugar, habitaciones, foto);
                XmlNode nodoRaiz = doc.DocumentElement;
                nodoRaiz.InsertAfter(hotel, nodoRaiz.LastChild);
                doc.Save(rutaXml);
            }
            else
            {
                doc.Load(rutaXml);
                XmlNode tarifa = Crear_Tarifas(id, ruta, precio);
                XmlNode nodoRaiz = doc.DocumentElement;
                nodoRaiz.InsertAfter(tarifa, nodoRaiz.LastChild);
                doc.Save(rutaXml);
            }
        }
        private XmlNode Crear_Hoteles(string id, string nombre, string pais, string lugar, string habitaciones, string foto)
        {
            XmlNode hoteles = doc.CreateElement("hotel");
            XmlElement xid = doc.CreateElement("id");
            xid.InnerText = id;
            hoteles.AppendChild(xid);
            XmlElement xnombre = doc.CreateElement("nombre");
            xnombre.InnerText = nombre;
            hoteles.AppendChild(xnombre);
            XmlElement xpais = doc.CreateElement("pais");
            xpais.InnerText = pais;
            hoteles.AppendChild(xpais);
            XmlElement xlugar = doc.CreateElement("lugar");
            xlugar.InnerText = lugar;
            hoteles.AppendChild(xlugar);
            XmlElement xhabitaciones = doc.CreateElement("habitaciones");
            xhabitaciones.InnerText = habitaciones;
            hoteles.AppendChild(xhabitaciones);
            XmlElement xfoto = doc.CreateElement("foto");
            xfoto.InnerText = foto;
            hoteles.AppendChild(xfoto);
            return hoteles;
        }

        public XmlNode ReadXml(string id, string tipo)
        {
            doc.Load(rutaXml);
            XmlNodeList listahoteles = doc.SelectNodes("Hotel/" + tipo);
            XmlNode unhotel;
            for (int i = 0; i < listahoteles.Count; i++)
            {
                unhotel = listahoteles.Item(i);
                if (unhotel.SelectSingleNode("id").InnerText.Equals(id))
                {
                    return unhotel;
                }
            }
            return null;
        }

        public void UpdateXml(string tipo, string id, string nombre, string pais, string lugar, string habitaciones, string foto, string ruta, string precio)
        {
            if (tipo.Equals("hotel"))
            {
                doc.Load(rutaXml);
                XmlElement hoteles = doc.DocumentElement;
                XmlNodeList listahoteles = doc.SelectNodes("Hotel/" + tipo);
                XmlNode nuevo_hotel = Crear_Hoteles(id, nombre, pais, lugar, habitaciones, foto);
                foreach (XmlNode hotel in listahoteles)
                {
                    if (hotel.FirstChild.InnerText == id)
                    {
                        XmlNode nodoOld = hotel;
                        hoteles.ReplaceChild(nuevo_hotel, nodoOld);
                    }
                }
                doc.Save(rutaXml);
            }
            else
            {
                doc.Load(rutaXml);
                XmlElement hoteles = doc.DocumentElement;
                XmlNodeList listahoteles = doc.SelectNodes("Hotel/" + tipo);
                XmlNode nuevo_hotel = Crear_Tarifas(id, ruta, precio);
                foreach (XmlNode hotel in listahoteles)
                {
                    if (hotel.FirstChild.InnerText == id)
                    {
                        XmlNode nodoOld = hotel;
                        hoteles.ReplaceChild(nuevo_hotel, nodoOld);
                    }
                }
                doc.Save(rutaXml);
            }

        }

        public void DeleteNodo(string id_borrar, string tipo)
        {
            doc.Load(rutaXml);
            XmlNode hoteles = doc.DocumentElement;
            XmlNodeList listahoteles = doc.SelectNodes("Hotel/" + tipo);
            foreach (XmlNode item in listahoteles)
            {
                if (item.SelectSingleNode("id").InnerText == id_borrar)
                {
                    XmlNode nodoOld = item;
                    hoteles.RemoveChild(nodoOld);
                }
            }
            doc.Save(rutaXml);
        }

        private XmlNode Crear_Tarifas(string id, string ruta, string precio)
        {
            XmlNode tarifas = doc.CreateElement("tarifa");
            XmlElement xid = doc.CreateElement("id");
            xid.InnerText = id;
            tarifas.AppendChild(xid);
            XmlElement xruta = doc.CreateElement("ruta");
            xruta.InnerText = ruta;
            tarifas.AppendChild(xruta);
            XmlElement xprecio = doc.CreateElement("precio");
            xprecio.InnerText = precio;
            tarifas.AppendChild(xprecio);
            return tarifas;
        }

        public Boolean ValidarId(string id, string tipo)
        {
            doc.Load(rutaXml);
            XmlElement hoteles = doc.DocumentElement;
            XmlNodeList listahoteles = doc.SelectNodes("Hotel/" + tipo);
            foreach (XmlNode hotel in listahoteles)
            {
                if (hotel.FirstChild.InnerText == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
