/* TEMPLATE GENERATED TESTCASE FILE
Filename: CWE78_OS_Command_Injection__Listen_tcp_17.cs
Label Definition File: CWE78_OS_Command_Injection.label.xml
Template File: sources-sink-17.tmpl.cs
*/
/*
* @description
* CWE: 78 OS Command Injection
* BadSource: Listen_tcp Read data using a listening tcp connection
* GoodSource: A hardcoded string
* BadSink: exec dynamic command execution with Runtime.getRuntime().exec()
* Flow Variant: 17 Control flow: for loops
*
* */

using TestCaseSupport;
using System;

using System.Diagnostics;
using System.Runtime.InteropServices;

using System.Web;

using System.IO;
using System.Net.Sockets;
using System.Net;

namespace testcases.CWE78_OS_Command_Injection
{

class CWE78_OS_Command_Injection__Listen_tcp_17 : AbstractTestCase
{
#if (!OMITBAD)
    /* uses badsource and badsink */
    public override void Bad()
    {
        string data;
        data = ""; /* Initialize data */
        /* Read data using a listening tcp connection */
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Parse("10.10.1.10"), 39543);
                listener.Start();
                using (TcpClient tcpConn = listener.AcceptTcpClient())
                {
                    /* read input from socket */
                    using (StreamReader sr = new StreamReader(tcpConn.GetStream()))
                    {
                        /* POTENTIAL FLAW: Read data using a listening tcp connection */
                        data = sr.ReadLine();
                    }
                }
            }
            catch (IOException exceptIO)
            {
                IO.Logger.Log(NLog.LogLevel.Warn, exceptIO, "Error with stream reading");
            }
            finally
            {
                if (listener != null)
                {
                    try
                    {
                        listener.Stop();
                    }
                    catch(SocketException se)
                    {
                        IO.Logger.Log(NLog.LogLevel.Warn, se, "Error closing TcpListener");
                    }
                }
            }
        }
        for (int i = 0; i < 1; i++)
        {
            String osCommand;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                /* running on Windows */
                osCommand = "c:\\WINDOWS\\SYSTEM32\\cmd.exe /c dir ";
            }
            else
            {
                /* running on non-Windows */
                osCommand = "/bin/ls ";
            }
            /* POTENTIAL FLAW: command injection */
            Process process = Process.Start(osCommand + data);
            process.WaitForExit();
        }
    }
#endif //omitbad
#if (!OMITGOOD)
    /* goodG2B() - use goodsource and badsink by reversing the block outside the
     * for statement with the one in the for statement */
    private void GoodG2B()
    {
        string data;
        /* FIX: Use a hardcoded string */
        data = "foo";
        for (int i = 0; i < 1; i++)
        {
            String osCommand;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                /* running on Windows */
                osCommand = "c:\\WINDOWS\\SYSTEM32\\cmd.exe /c dir ";
            }
            else
            {
                /* running on non-Windows */
                osCommand = "/bin/ls ";
            }
            /* POTENTIAL FLAW: command injection */
            Process process = Process.Start(osCommand + data);
            process.WaitForExit();
        }
    }

    public override void Good()
    {
        GoodG2B();
    }
#endif //omitgood
}
}
