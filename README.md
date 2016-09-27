# Execute Exe-Files from the browser</p>(without changing the security of the browser)

## Home
http://localhost:47114/garage/home

## Ping router
http://localhost:47114/garage/open?exe=ping&ip=10.14.21.1
## Ping mr2
http://localhost:47114/garage/open?exe=ping&ip=10.14.21.11

## Open Explorer
http://localhost:47114/garage/open?exe=explorer&ip=10.14.21.11
## Open VncViewer
http://localhost:47114/garage/open?exe=vncviewer&ip=10.14.21.11


```c#
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
            Process.Start(Path.Combine(Environment.Is64BitProcess ? "x64" : "x86", "vncviewer.exe"), string.Format("{0} -password PASSWORD", ip));
        if (exe.ToLower() == "ping")
            Process.Start(@"ping", string.Format("-t {0}", ip));
    }

    return "open</br><script language = \"javascript\"> window.location.href = \"http://localhost:47114/garage/home\"</script>";
};
```