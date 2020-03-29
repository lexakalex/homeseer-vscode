using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class Precompiler
{
    static string EMBED = @"
class HSPI : ScsService, IPlugInAPI
    {
        public HSPI()
        {
        }

        public string Name => throw new NotImplementedException();

        public bool HSCOMPort => throw new NotImplementedException();

        public bool ActionAdvancedMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool HasTriggers => throw new NotImplementedException();

        public int TriggerCount => throw new NotImplementedException();

        public int AccessLevel()
        {
            throw new NotImplementedException();
        }

        public string ActionBuildUI(string sUnique, IPlugInAPI.strTrigActInfo ActInfo)
        {
            throw new NotImplementedException();
        }

        public bool ActionConfigured(IPlugInAPI.strTrigActInfo ActInfo)
        {
            throw new NotImplementedException();
        }

        public int ActionCount()
        {
            throw new NotImplementedException();
        }

        public string ActionFormatUI(IPlugInAPI.strTrigActInfo ActInfo)
        {
            throw new NotImplementedException();
        }

        public IPlugInAPI.strMultiReturn ActionProcessPostUI(NameValueCollection PostData, IPlugInAPI.strTrigActInfo TrigInfoIN)
        {
            throw new NotImplementedException();
        }

        public bool ActionReferencesDevice(IPlugInAPI.strTrigActInfo ActInfo, int dvRef)
        {
            throw new NotImplementedException();
        }

        public int Capabilities()
        {
            throw new NotImplementedException();
        }

        public string ConfigDevice(int @ref, string user, int userRights, bool newDevice)
        {
            throw new NotImplementedException();
        }

        public Enums.ConfigDevicePostReturn ConfigDevicePost(int @ref, string data, string user, int userRights)
        {
            throw new NotImplementedException();
        }

        public string GenPage(string link)
        {
            throw new NotImplementedException();
        }

        public string GetPagePlugin(string page, string user, int userRights, string queryString)
        {
            throw new NotImplementedException();
        }

        public string get_ActionName(int ActionNumber)
        {
            throw new NotImplementedException();
        }

        public bool get_Condition(IPlugInAPI.strTrigActInfo TrigInfo)
        {
            throw new NotImplementedException();
        }

        public bool get_HasConditions(int TriggerNumber)
        {
            throw new NotImplementedException();
        }

        public int get_SubTriggerCount(int TriggerNumber)
        {
            throw new NotImplementedException();
        }

        public string get_SubTriggerName(int TriggerNumber, int SubTriggerNumber)
        {
            throw new NotImplementedException();
        }

        public bool get_TriggerConfigured(IPlugInAPI.strTrigActInfo TrigInfo)
        {
            throw new NotImplementedException();
        }

        public string get_TriggerName(int TriggerNumber)
        {
            throw new NotImplementedException();
        }

        public bool HandleAction(IPlugInAPI.strTrigActInfo ActInfo)
        {
            throw new NotImplementedException();
        }

        public void HSEvent(Enums.HSEvent EventType, object[] parms)
        {
            throw new NotImplementedException();
        }

        public string InitIO(string port)
        {
            throw new NotImplementedException();
        }

        public string InstanceFriendlyName()
        {
            throw new NotImplementedException();
        }

        public IPlugInAPI.strInterfaceStatus InterfaceStatus()
        {
            throw new NotImplementedException();
        }

        public string PagePut(string data)
        {
            throw new NotImplementedException();
        }

        public object PluginFunction(string procName, object[] parms)
        {
            throw new NotImplementedException();
        }

        public object PluginPropertyGet(string procName, object[] parms)
        {
            throw new NotImplementedException();
        }

        public void PluginPropertySet(string procName, object value)
        {
            throw new NotImplementedException();
        }

        public IPlugInAPI.PollResultInfo PollDevice(int dvref)
        {
            throw new NotImplementedException();
        }

        public string PostBackProc(string page, string data, string user, int userRights)
        {
            throw new NotImplementedException();
        }

        public bool RaisesGenericCallbacks()
        {
            throw new NotImplementedException();
        }

        public SearchReturn[] Search(string SearchString, bool RegEx)
        {
            throw new NotImplementedException();
        }

        public void SetIOMulti(List<CAPI.CAPIControl> colSend)
        {
            throw new NotImplementedException();
        }

        public void set_Condition(IPlugInAPI.strTrigActInfo TrigInfo, bool Value)
        {
            throw new NotImplementedException();
        }

        public void ShutdownIO()
        {
            throw new NotImplementedException();
        }

        public void SpeakIn(int device, string txt, bool w, string host)
        {
            throw new NotImplementedException();
        }

        public bool SupportsAddDevice()
        {
            throw new NotImplementedException();
        }

        public bool SupportsConfigDevice()
        {
            throw new NotImplementedException();
        }

        public bool SupportsConfigDeviceAll()
        {
            throw new NotImplementedException();
        }

        public bool SupportsMultipleInstances()
        {
            throw new NotImplementedException();
        }

        public bool SupportsMultipleInstancesSingleEXE()
        {
            throw new NotImplementedException();
        }

        public string TriggerBuildUI(string sUnique, IPlugInAPI.strTrigActInfo TrigInfo)
        {
            throw new NotImplementedException();
        }

        public string TriggerFormatUI(IPlugInAPI.strTrigActInfo TrigInfo)
        {
            throw new NotImplementedException();
        }

        public IPlugInAPI.strMultiReturn TriggerProcessPostUI(NameValueCollection PostData, IPlugInAPI.strTrigActInfo TrigInfoIN)
        {
            throw new NotImplementedException();
        }

        public bool TriggerReferencesDevice(IPlugInAPI.strTrigActInfo TrigInfo, int dvRef)
        {
            throw new NotImplementedException();
        }

        public bool TriggerTrue(IPlugInAPI.strTrigActInfo TrigInfo)
        {
            throw new NotImplementedException();
        }
    }
    
static IHSApplication CreateHs() {
    ConsoleTraceListener consoleTracer = new ConsoleTraceListener();
    consoleTracer.Name = ""ConsoleTracer"";

    Trace.Listeners.Add(consoleTracer);
    ScsTcpEndPoint endpoint = new ScsTcpEndPoint(""{hs_host}"", {hs_port});
    HSPI hspi = new HSPI();
    IScsServiceClient<IHSApplication> client = ScsServiceClientBuilder.CreateClient<IHSApplication>(endpoint, hspi);
    IScsServiceClient<IAppCallbackAPI> clientCallback = ScsServiceClientBuilder.CreateClient<IAppCallbackAPI>(endpoint, hspi);

    client.Connect();

    string instance = ""I"" + DateTime.Now.ToString(""hhmmss"");
    client.ServiceProxy.Connect(""MyScriptWrapper"", instance);

    return client.ServiceProxy;
}

private IHSApplication hs;

ScriptClass(IHSApplication hs){
    this.hs = hs;
}

public static void Main() {
    new ScriptClass(CreateHs()).Main(null);
}
";

    public static bool Compile(ref string code, string scriptFile, bool isPrimaryScript, Hashtable context)
    {
        int hsDirective = code.IndexOf("hs_connect");
        if (hsDirective != -1) {
            int hsDirectiveEnd = code.IndexOf('\n', hsDirective);
            Regex regex = new Regex(@"hs_connect (.*)\:(.*)");
            Match match = regex.Match(code, hsDirective, hsDirectiveEnd != -1 ? hsDirectiveEnd - hsDirective : 1000);

            if (match.Success) {
                string hsHost = match.Groups[1].Value;
                string hsPort = match.Groups[2].Value;

                StringBuilder newCode = new StringBuilder(code.Substring(0, hsDirective));
                newCode.Append(
                    EMBED
                        .Replace("{hs_host}", hsHost)
                        .Replace("{hs_port}", hsPort));

                if (hsDirectiveEnd != -1) {
                    newCode.Append(code.Substring(hsDirectiveEnd));
                }

                code = newCode.ToString();

                string generatedDir = Path.GetDirectoryName(scriptFile) + "/.generated/";
                Directory.CreateDirectory(generatedDir);

                File.WriteAllText(generatedDir + Path.GetFileNameWithoutExtension(scriptFile) + ".generated", code);
                
                return true; //true as the code has been modified
            }
        }
        return false;
    }
}