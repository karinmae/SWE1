using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using WebLibrary;

namespace WebServer
{
    public class Response
    {
        public StreamWriter sw;
        private NetworkStream stream;


        public Response(object clientStream, object theUrl)
        {
            stream = (NetworkStream)clientStream;
            StreamWriter sw = new StreamWriter(stream);
            Url newUrl = new Url();
            newUrl = (Url)theUrl;
            string pluginName = newUrl.getPluginName();
            string http_url = newUrl.getFullUrl();


            sw.WriteLine("HTTP/1.1 200 OK");
            sw.WriteLine("connection: close");
            sw.WriteLine("content-type: text/html");
            sw.WriteLine();
            sw.WriteLine("<html><head><title>Webserver</title>");
            sw.WriteLine("<style type='text/css'>*{margin:0px; padding: 0px;}");
            sw.WriteLine("body {font-family: Arial;background-color: #E6E6E6;}");
            sw.WriteLine("#content {padding-top:30px;}");
            sw.WriteLine("#Navi {padding-top:30px;padding-bottom:30px;background-color: #CC3333;}");
            sw.WriteLine("h1  {font-family: Hobo std;font-size: 40px;  font-weight: bold; color: #CC3333;padding-top: 20px;padding-left: 50px; padding-top:50px;}");
            sw.WriteLine("h4 {font-family: Verdana;font-size: 25px;  color: #DF0101;font-weight: bold;padding-bottom: 30px;padding-left: 50px;}");
            sw.WriteLine("p {font-family: Arial;color: #330000;padding-left: 50px;}");
            sw.WriteLine("ul {padding-left: 50px;}");
            sw.WriteLine("a {color:#330000;font-weight: bold;}");
            sw.WriteLine("ul li {list-style-type: none;}");
            sw.WriteLine("form {padding-left: 50px;}");
            sw.WriteLine("</style></head>");
            sw.WriteLine("<html><body><h1>WebServer</h1>");


            if (String.Compare(pluginName, "getTemperature") == 0)
            {
                sw.WriteLine("<h4>SensorCloud</h4>");
                sw.WriteLine("<div id='navi'>");
                sw.WriteLine("<form method=\"POST\" action=\"/getTemperature\">");
                sw.WriteLine("Day: <input maxlength=\"2\" name=\"day\" size=\"2\" type=\"text\" />");
                sw.WriteLine("Month: <input maxlength=\"2\" name=\"month\" size=\"2\" type=\"text\" />");
                sw.WriteLine("Year: <input maxlength=\"4\" name=\"year\" size=\"4\" type=\"text\" />");
                sw.WriteLine("<input type='submit' value='los'>");
                sw.WriteLine("</form>");
                sw.WriteLine("<br><p>Usage: '/day/month/year'</p>");
                sw.WriteLine("</div></body></html>");
                sw.Flush();
            }


            else if (String.Compare(pluginName, "StaticFile") == 0)
            {
                sw.WriteLine("<h4>StaticFile</h4>");
                sw.WriteLine("<div id='navi'>");
                sw.WriteLine("<br><p>Usage: '/Filename'</p>");
                sw.WriteLine("</div></body></html>");
                sw.Flush();
            }
            else if (String.Compare(pluginName, "Navi") == 0)
            {
                sw.WriteLine("<h4>Navi</h4>");
                sw.WriteLine("<div id='navi'>");
                sw.WriteLine("<br><p>Not implemented</p>");
                sw.WriteLine("</div></body></html>");
                sw.Flush();
            }
            else if (String.Compare(pluginName, "Esoterik") == 0)
            {
                sw.WriteLine("<h4>Esoterik Plugin</h4>");
                sw.WriteLine("<div id='navi'>");
                sw.WriteLine("<form method=post action=/form>");
                sw.WriteLine("First Name: <input maxlength=\"20\" name=\"name\" size=\"20\" type=\"text\" />");
                sw.WriteLine("Birthyear: <input maxlength=\"4\" name=\"year\" size=\"4\" type=\"text\" />");
                sw.WriteLine("<br>Gender:<p>");
			    sw.WriteLine("<input name=\"Gender\" type=\"radio\" value=\"w\" />female</p>");
		        sw.WriteLine("<p><input name=\"Gender\" type=\"radio\" value=\"m\" />male</p>");
                sw.WriteLine("<input type='submit' value='los'>");
                sw.WriteLine("</form>");
                sw.WriteLine("<br><p>Usage: '/Name/Birthyear/Gender [m/w]'</p>");
                sw.WriteLine("</div></body></html>");
                sw.Flush();
            }
        }
    }
}

