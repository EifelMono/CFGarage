using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace CFGarage
{
    public class Module : NancyModule
    {
        public Module()
                : base("/garage")
        {

            Before += nancyContext =>
            {
                Console.WriteLine(DateTime.Now.ToString() + " " + Request.Url);
                return null;
            };


            Get["/home"] = parameters =>
            {
                return "CF Garage </br>"
                       +
                       "<input type=\"button\" onclick=\"location.href = 'http://localhost:47114/garage/open'; \" value=\"Open Garage\" />"
                       + "</br>"
                       +
                       "<input type=\"button\" onclick=\"location.href = 'http://localhost:47114/garage/close'; \" value=\"Close Garage\" />"
                       + "</br>"
                       +
                       "<input type=\"button\" onclick=\"location.href = 'http://localhost:47114/garage/open?exe=ping&ip=10.14.21.1'; \" value=\"open ping 10.14.21.1 (Router)\" />"
                       + "</br>"
                       +
                       "<input type=\"button\" onclick=\"location.href = 'http://localhost:47114/garage/open?exe=ping&ip=10.14.21.11'; \" value=\"open ping 10.14.21.11 (Mr2) \" />"
                       + "</br>"
                       +
                       "<input type=\"button\" onclick=\"location.href = 'http://localhost:47114/garage/open?exe=explorer&ip=10.14.21.11'; \" value=\"open explorer 10.14.21.11 (Mr2)\" />"
                       + "</br>"
                       +
                       "<input type=\"button\" onclick=\"location.href = 'http://localhost:47114/garage/open?exe=vncviewer&ip=10.14.21.11'; \" value=\"open vncviewer 10.14.21.11 (Mr2)\" />";
            };
            Get["/open"] = parameters =>
            {
                Console.WriteLine(DateTime.Now.ToString() + " Open");
                return "open</br><script language = \"javascript\"> window.location.href = \"http://localhost:47114/garage/home\"</script>";
            };

            Get["/close"] = parameters =>
            {
                Console.WriteLine(DateTime.Now.ToString() + " Close");
                return "close</br><script language = \"javascript\"> window.location.href = \"http://localhost:47114/garage/home\"</script>";
            };

            Get["/open"] = parameters =>
            {
                string exe = Request.Query["exe"];
                string ip = Request.Query["ip"];
                Console.WriteLine("exe={0} ip={1} ", exe, ip);

                if (exe != null && ip != null)
                {
                    if (exe.ToLower() == "explorer")
                        Process.Start(string.Format(@"\\{0}\c", ip));
                    if (exe.ToLower() == "vncviewer")
                        Process.Start(Path.Combine(Environment.Is64BitProcess ? "x64" : "x86", "vncviewer.exe"), string.Format("{0} -password WMK:ROWA", ip));
                    if (exe.ToLower() == "ping")
                        Process.Start(@"ping", string.Format("-t {0}", ip));

                }

                return "open</br><script language = \"javascript\"> window.location.href = \"http://localhost:47114/garage/home\"</script>";
            };

        }
    }
}